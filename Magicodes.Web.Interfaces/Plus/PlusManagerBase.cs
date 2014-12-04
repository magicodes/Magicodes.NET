using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :PlusManagerBase
//        description :
//
//        created by 雪雁 at  2014/10/19 16:03:21
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Plus
{
    public abstract class PlusManagerBase
    {
        public DirectoryInfo DynamicDirectory = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
        /// <summary>
        /// 插件目录记录文件路径
        /// </summary>
        protected string PlusXmlPath = Path.Combine(GlobalApplicationObject.Current.ApplicationContext.SitePaths.PlusDirPath, "plus.xml");
        /// <summary>
        /// 已安装的插件列表
        /// </summary>
        public abstract List<IPlusAssemblyInfo> PluginsList { get; set; }
        /// <summary>
        /// 部署程序集
        /// </summary>
        /// <param name="dllFile">插件程序集文件</param>
        public abstract Assembly Deploy(FileInfo dllFile);

        /// <summary>
        /// 获取插件信息与配置
        /// </summary>
        /// <param name="dllFile"></param>
        /// <returns></returns>
        public abstract IPlusAssemblyInfo GetPluginInfo(FileInfo dllFile);
        /// <summary>
        /// 获取插件信息与配置
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="dllFile"></param>
        /// <returns></returns>
        public abstract IPlusAssemblyInfo GetPluginInfo(Assembly assembly, FileInfo dllFile);
        /// <summary>
        /// 获取插件信息与配置
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="dllFile"></param>
        /// <returns></returns>
        public virtual IPlusAssemblyInfo GetPluginInfo(string fullName)
        {
            return PluginsList.FirstOrDefault(p => p.FullName.Equals(fullName, StringComparison.InvariantCultureIgnoreCase));
        }
        /// <summary>
        /// 判断插件是否已经安装
        /// </summary>
        /// <param name="systemName">插件名称</param>
        /// <returns> </returns>
        public virtual bool IsInstalled(string fullName)
        {
            return PluginsList.FirstOrDefault(p => p.IsInstalled && p.FullName.Equals(fullName, StringComparison.InvariantCultureIgnoreCase)) != null;
        }

        /// <summary>
        /// 加载策略
        /// </summary>
        /// <param name="plusAss">程序集</param>
        public abstract void LoadPlusStrategys(Assembly assembly);
        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="assembly"></param>
        public abstract void Install(Assembly assembly);

         /// <summary>
        /// 卸载插件
        /// </summary>
        public abstract void Uninstall(Assembly assembly);
        /// <summary>
        /// 将dll复制到动态程序集目录
        /// </summary>
        /// <param name="dllFile"></param>
        /// <returns></returns>
        public abstract FileInfo CopyToDynamicDirectory(FileInfo dllFile);

    }
}
