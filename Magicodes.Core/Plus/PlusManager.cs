using Magicodes.Core.Performance.Watch;
using Magicodes.Core.Web.Mvc;
using Magicodes.Utility;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus;
using Magicodes.Web.Interfaces.Strategy;
using Magicodes.Web.Interfaces.Strategy.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :PlusManager
//        description :
//
//        created by 雪雁 at  2014/10/19 16:29:54
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Plus
{
    public class PlusManager : PlusManagerBase
    {
        public PlusManager()
        {
            if (File.Exists(PlusXmlPath))
                PluginsList = SerializeHelper.Load<List<IPlusAssemblyInfo>>(PlusXmlPath);
            else
                pluginsList = new List<IPlusAssemblyInfo>();
        }
        private List<IPlusAssemblyInfo> pluginsList;
        /// <summary>
        /// 插件列表
        /// </summary>
        public override List<IPlusAssemblyInfo> PluginsList
        {
            get
            {
                return pluginsList;
            }
            set
            {
                pluginsList = value;
            }
        }

        public override IPlusAssemblyInfo GetPluginInfo(FileInfo dllFile)
        {
            Assembly assembly = Assembly.LoadFrom(dllFile.FullName);
            return this.GetPluginInfo(assembly, dllFile);
        }

        public override IPlusAssemblyInfo GetPluginInfo(System.Reflection.Assembly assembly, FileInfo dllFile)
        {
            var plusInfo = GetPluginInfo(assembly.FullName);
            if (plusInfo == null)
            {
                plusInfo = AssemblyManager.GetPlusAssemblysInfo(assembly, dllFile);
            }
            if (!PluginsList.Any(p => p.FullName == assembly.FullName))
            {
                plusInfo.IsInstalled = false;
                PluginsList.Add(plusInfo);
            }
            return plusInfo;
        }

        public override void LoadPlusStrategys(Assembly assembly)
        {
            var strategyBaseFullName = typeof(IStrategyBase).FullName;
            //获取所有实现策略接口的类
            assembly.GetTypes()
                .Where(
                p => p.IsClass
                    &&
                    (
                        (p.GetInterfaces().Length > 0 && p.GetInterfaces().Any(pI => pI.FullName == strategyBaseFullName))
                    )
                    ).Each(
                            t =>
                            {
                                try
                                {
                                    var type = Activator.CreateInstance(t);
                                    if (t.BaseType != null && (t.BaseType.GetInterfaces().Any(p => p.FullName == strategyBaseFullName)))
                                    {
                                        GlobalApplicationObject.Current.ApplicationContext
                                        .StrategyManager
                                        .AddStrategy(t.BaseType.FullName, ((IStrategyBase)type))
                                        .Initialize();
                                    }
                                    else
                                        GlobalApplicationObject.Current.ApplicationContext
                                            .StrategyManager
                                            .AddStrategy(t.GetInterfaces()[0].FullName, ((IStrategyBase)type))
                                            .Initialize();
                                }
                                catch (Exception ex)
                                {
                                    if (GlobalApplicationObject.Current.ApplicationContext.ApplicationLog != null)
                                        GlobalApplicationObject.Current.ApplicationContext.ApplicationLog.Log(LoggerLevels.Error, string.Format("加载策略失败，Assembly:{0}，Type:{1}{2}", assembly.FullName, t.FullName, Environment.NewLine), ex);
                                }
                            });

        }

        public override Assembly Deploy(FileInfo dllFile)
        {
            var newDllFile = CopyToDynamicDirectory(dllFile);
            Assembly assembly = Assembly.LoadFrom(newDllFile.FullName);
            var plusInfo = this.GetPluginInfo(assembly, dllFile);
            switch (plusInfo.PlusConfigInfo.AssemblyType)
            {
                case Magicodes.Web.Interfaces.Plus.Info.AssemblyTypes.WF:
                    break;
                case Magicodes.Web.Interfaces.Plus.Info.AssemblyTypes.Resource:
                    break;
                case Magicodes.Web.Interfaces.Plus.Info.AssemblyTypes.Code:
                    break;
                case Magicodes.Web.Interfaces.Plus.Info.AssemblyTypes.Theme:
                    break;
                case Magicodes.Web.Interfaces.Plus.Info.AssemblyTypes.Strategy:
                    this.LoadPlusStrategys(assembly);
                    break;
                case Magicodes.Web.Interfaces.Plus.Info.AssemblyTypes.Models:
                    break;
                case Magicodes.Web.Interfaces.Plus.Info.AssemblyTypes.MVC:
                    {
                        //配置插件路由
                        MvcConfigManager.Config(assembly, plusInfo.PlusConfigInfo);
                        break;
                    }
                    break;
                default:
                    break;
            }
            try
            {
                //将程序集添加到当前应用程序域
                BuildManager.AddReferencedAssembly(assembly);
                //执行插件初始化函数
                assembly.GetTypes().Where(p => p.IsClass && p.GetInterface(typeof(IPlus).FullName) != null).Each(
                    t =>
                    {
                        using (new CodeWatch("Plu Initialize", 3000))
                        {
                            try
                            {
                                var type = (IPlus)Activator.CreateInstance(t);
                                type.Initialize();
                            }
                            catch (Exception ex)
                            {
                                throw new MagicodesException(string.Format("插件初始化失败！Assembly:{0}，Type:{1}{2}", assembly.FullName, t.FullName, Environment.NewLine), ex);
                            }
                        }
                    });
            }
            catch (FileLoadException ex)
            {
                throw new MagicodesException(string.Format("加载此程序失败！Assembly：{0}，FileName：{1}", assembly.FullName, ex.FileName), ex);
            }
            catch (System.Reflection.ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                throw new MagicodesException(errorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return assembly;
        }

        public override FileInfo CopyToDynamicDirectory(FileInfo dllFile)
        {
            var copyFolder = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
            var newDllFile = new FileInfo(Path.Combine(copyFolder.FullName, dllFile.Name));
            try
            {
                File.Copy(dllFile.FullName, newDllFile.FullName, true);
            }
            catch (Exception ex)//如果出现"正由另一进程使用，因此该进程无法访问该文件"错误，则先重命名再复制
            {
                try
                {
                    File.Move(newDllFile.FullName, newDllFile.FullName + Guid.NewGuid().ToString("N") + ".locked");
                }
                catch (Exception ioex)
                {
                    throw new MagicodesException("部署插件程序集失败。PlusName：" + dllFile.Name, ioex);
                }
                File.Copy(dllFile.FullName, newDllFile.FullName, true);
            }
            return newDllFile;
        }

        /// <summary>
        /// 安装或更新
        /// </summary>
        /// <param name="assembly"></param>
        public override void Install(Assembly assembly)
        {
            //在未安装的插件列表中获得对应插件
            var pluginInfo = PluginsList.FirstOrDefault(p => p.FullName.Equals(assembly.FullName, StringComparison.InvariantCultureIgnoreCase));
            //当插件为空时直接返回
            if (pluginInfo == null)
                throw new Exception(string.Format("插件[{0}]不存在", assembly.FullName));
            if (pluginInfo.IsInstalled)
            {

            }
            else
            {
                pluginInfo.IsInstalled = true;
            }
            SerializeHelper.Save(PluginsList, PlusXmlPath);
        }

        public override void Uninstall(Assembly assembly)
        {
            //在未安装的插件列表中获得对应插件
            var pluginInfo = PluginsList.FirstOrDefault(p => p.FullName.Equals(assembly.FullName, StringComparison.InvariantCultureIgnoreCase));
            //当插件为空时直接返回
            if (pluginInfo == null)
                throw new Exception(string.Format("插件[{0}]不存在", assembly.FullName));
            if (pluginInfo.IsInstalled)
            {
                pluginInfo.IsInstalled = false;
            }
            else
            {
                throw new Exception(string.Format("插件[{0}]尚未安装", assembly.FullName));
            }
            SerializeHelper.Save(PluginsList, PlusXmlPath);
        }
    }
}
