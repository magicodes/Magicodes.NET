using Magicodes.Core.Performance.Watch;
using Magicodes.Core.Plus;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Magicodes.Utility;
using Magicodes.Core.Res;
using Magicodes.Web.Interfaces.Strategy.Logger;
using System.Web.Compilation;
using Magicodes.Core.Strategy;
using Magicodes.Web.Interfaces.Plus.Info;
using Magicodes.Web.Interfaces.Strategy;
using Magicodes.Web.Interfaces.Strategy.Cache;
using Magicodes.Web.Interfaces.Strategy.Email;
using Magicodes.Web.Interfaces.Strategy.Sesstion;
using Magicodes.Web.Interfaces.Strategy.SMS;
using Magicodes.Core.Config;
using Magicodes.Web.Interfaces.Strategy.User;
using Magicodes.Core.Routing;
using System.Web;
using Magicodes.Core.Web.Mvc;
using Magicodes.Core.Web;

namespace Magicodes.Core
{
    /// <summary>
    /// 应用程序上下文对象
    /// </summary>
    public class ApplicationContext : ApplicationContextBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            #region 初始化路由
            this.RoutingManager = new RoutingManager();
            this.RoutingManager.Initialize();
            #endregion
            //初始化全局配置
            GlobalConfigurationManager.Config();
            using (var watch = new CodeWatch("Initialize", 20000))
            {
                #region 处理插件
                using (new CodeWatch("LoadPlusAndPlusResource", 5000))
                {
                    var taskDoingList = new List<Task>();
                    #region 【废弃】加载程序集资源
                    //foreach (var plus in PlusManager.InstalledPluginsList)
                    //{
                    //    var loadPlusTask = new Task((plusAss) => CurrentResourceHelper.LoadPlusAndPlusResource((IPlusInfo)plusAss), plus);
                    //    taskDoingList.Add(loadPlusTask);
                    //    loadPlusTask.Start();
                    //}
                    //Task.WaitAll(taskDoingList.ToArray());
                    //taskDoingList.Clear();
                    #endregion

                    #region 【废弃】初始化程序集
                    //foreach (var plus in PlusAssemblysList)
                    //{
                    //    //程序集初始化
                    //    var initTask = new Task((plusInfo) =>
                    //    {
                    //        var plu = (IPlusInfo)plusInfo;
                    //        CurrentResourceHelper.AssemblyInitialize(plu.Assembly, plu.PlusAssemblys);
                    //    }, plus);
                    //    taskDoingList.Add(initTask);
                    //    initTask.Start();
                    //}
                    //Task.WaitAll(taskDoingList.ToArray());
                    //taskDoingList.Clear();
                    #endregion

                    #region 【废弃】执行程序集初始化函数
                    //foreach (var plus in PlusManager.PluginsList)
                    //{
                    //    var loadingTask = new Task((plusInfo) =>
                    //    {
                    //        var plu = (IPlusInfo)plusInfo;
                    //        //执行插件Loading函数
                    //        plu.Assembly.GetTypes().Where(p => p.IsClass && p.GetInterface(typeof(IPlus).FullName) != null).Each(
                    //            t =>
                    //            {
                    //                using (new CodeWatch("Plu Initialize", ApplicationLog, 3000))
                    //                {
                    //                    try
                    //                    {
                    //                        var type = (IPlus)Activator.CreateInstance(t);
                    //                        type.Initialize();
                    //                    }
                    //                    catch (Exception ex)
                    //                    {
                    //                        ApplicationLog.Log(LoggerLevels.Error, string.Format("Assembly:{0}，Type:{1}{2}", plu.Assembly.FullName, t.FullName, Environment.NewLine), ex);
                    //                    }
                    //                }
                    //            });
                    //    }, plus);
                    //    taskDoingList.Add(loadingTask);
                    //    loadingTask.Start();
                    //}
                    //Task.WaitAll(taskDoingList.ToArray());
                    //taskDoingList.Clear();
                    #endregion
                }
                #endregion
            }
        }
        /// <summary>
        /// 加载应用程序集
        /// </summary>
        /// <param name="folder"></param>
        public override void LoadAssemblies()
        {
            if (PlusAssemblysList == null)
                PlusAssemblysList = new List<IPlusInfo>();
            else
                PlusAssemblysList.Clear();

            #region 插件文件目录
            var plusFilesDirectoryInfo = new DirectoryInfo(SitePaths.PlusFilesDirPath);
            #endregion
            //插件前缀必须为Magicodes
            var plusDlls = plusFilesDirectoryInfo.GetFiles("*.dll", SearchOption.AllDirectories).ToList();
            if (plusDlls.Count == 0) return;
            //插件程序集
            //必须是Magicodes插件才会执行插件部署
            //dll名称必须与插件目录名称一致才能部署
            //{PlusDir}/{dllName}
            //{PlusDir}/bin/{dllName}
            var magicodesPlusDlls = plusDlls.Where(p => p.Name.StartsWith("Magicodes.") && (p.Directory.Name + ".dll" == p.Name || p.Directory.Parent.Name + ".dll" == p.Name));
            //依赖的程序集
            var orthersDlls = plusDlls.Where(p => !p.Name.StartsWith("Magicodes.")).Distinct();
            #region 设置程序域
            //var setup = new AppDomainSetup
            //{
            //    ApplicationName = "Magicodes.Core",
            //    //DynamicBase = SitePaths.PlusShadowCopyDirPath,
            //    PrivateBinPath = SitePaths.PlusShadowCopyDirPath,
            //    DisallowCodeDownload = true,
            //    ShadowCopyFiles = "true",
            //    CachePath = SitePaths.PlusCacheDirPath,
            //    ShadowCopyDirectories = SitePaths.PlusShadowCopyDirPath,
            //    //PrivateBinPath = SitePaths.PlusShadowCopyDirPath
            //};
            //var appDomain = AppDomain.CreateDomain("Magicodes.Core.Domain", null, setup);
            CurrentAppDomain = AppDomain.CurrentDomain;
            if (!CurrentAppDomain.IsFullyTrusted)
                throw new MagicodesException("请将当前应用程序信任级别设置为完全信任。");

            #endregion
            var binDir = new DirectoryInfo(SitePaths.SiteRootBinDirPath);
            foreach (var plus in orthersDlls)
            {
                //如果网站bin目录不存在此dll，则将该dll复制到动态程序集目录
                if (binDir.GetFiles(plus.Name).Length == 0 && PlusManager.DynamicDirectory.GetFiles(plus.Name).Length == 0)
                {
                    PlusManager.CopyToDynamicDirectory(plus);
                    Assembly assembly = Assembly.LoadFrom(plus.FullName);

                    //将程序集添加到当前应用程序域
                    BuildManager.AddReferencedAssembly(assembly);
                }
            }
            foreach (var plus in magicodesPlusDlls)
            {
                PlusManager.Deploy(plus);
            }

            //foreach (var plus in plusDlls)
            //{
            //    //必须是Magicodes插件才会执行插件部署
            //    //dll名称必须与插件目录名称一致才能部署
            //    //{PlusDir}/{dllName}
            //    //{PlusDir}/bin/{dllName}
            //    if (plus.Name.StartsWith("Magicodes.") && (plus.Directory.Name + ".dll" == plus.Name || plus.Directory.Parent.Name + ".dll" == plus.Name))
            //        PlusManager.Deploy(plus);
            //    //如果网站bin目录不存在此dll，则将该dll复制到动态程序集目录
            //    else if (binDir.GetFiles(plus.Name).Length == 0 && PlusManager.DynamicDirectory.GetFiles(plus.Name).Length == 0)
            //    {
            //        PlusManager.CopyToDynamicDirectory(plus);
            //        Assembly assembly = Assembly.LoadFrom(plus.FullName);

            //        //将程序集添加到当前应用程序域
            //        BuildManager.AddReferencedAssembly(assembly);
            //    }
            //}
        }
        /// <summary>
        /// 应用程序日志类
        /// </summary>
        public override LoggerStrategyBase ApplicationLog
        {
            get
            {
                return this.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
            }
        }

        public override void PreApplicationStartInitialize()
        {
            //初始化策略管理器
            StrategyManager = new StrategyManager();
            //初始化插件管理器
            PlusManager = new PlusManager();
            //【优先加载框架策略】加载框架策略作为默认策略，如果插件实现了该策略，则会被覆盖
            //默认集成了日志策略
            PlusManager.LoadPlusStrategys(this.GetType().Assembly);
            //初始化资源压缩类
            ResourceMinHelper = new ResourceMinHelper();
            //初始化嵌入资源管理器
            ManifestResourceManager = new ManifestResourceManager();
            //初始化资源管理辅助类
            CurrentResourceHelper = new ResourceHelper();
            //初始化配置管理器
            ConfigManager = new ConfigManager();
            //加载程序集
            LoadAssemblies();

            GlobalConfigurationManager.MapHttpAttributeRoutes();
            //初始化OData
            GlobalConfigurationManager.InitializeODATA();
            //初始化WebApi
            GlobalConfigurationManager.InitializeWebAPI();
            //初始化MVC插件引擎
            GlobalConfigurationManager.InitializeMVCEngines();

        }
        /// <summary>
        /// 策略管理器
        /// </summary>
        public override StrategyManagerBase StrategyManager { get; set; }
    }
}
