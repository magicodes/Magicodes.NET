using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Magicodes.Web.Interfaces.Plus.Resource;
using Magicodes.Web.Interfaces.Plus;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.WebHandler.Resource;
using Magicodes.Core.Plus;
using Magicodes.Utility;
using Magicodes.Web.Interfaces.Plus.Info;
using Magicodes.Web.Interfaces.Config.Info;
namespace Magicodes.Core.Res
{
    public class ResourceHelper : IResourceHelper
    {
        public static IResourceMin MinHelper { get { return GlobalApplicationObject.Current.ApplicationContext.ResourceMinHelper; } }
        public static IManifestResourceManager ManifestResourceManager { get { return GlobalApplicationObject.Current.ApplicationContext.ManifestResourceManager; } }

        static ResourceHelper()
        {
        }


        /// <summary>
        /// 加载程序集并且处理程序集资源
        /// </summary>
        /// <param name="plusInfo"></param>
        public IPlusAssemblyInfo LoadPlusAndPlusResource(IPlusInfo plusInfo)
        {
            if (ManifestResourceManager.ResourcesDic == null)
                ManifestResourceManager.ResourcesDic = new ConcurrentDictionary<string, IResourceUrl>();
            var isWriteResource = true;
            LoadAssemblyResources(plusInfo.Assembly, isWriteResource, plusInfo.PlusAssemblys);
            return plusInfo.PlusAssemblys;
        }
        /// <summary>
        /// 程序集初始化（处理Workflow、资源、代码、主题）
        /// </summary>
        /// <param name="pluAssembly"></param>
        /// <param name="fwAss"></param>
        public void AssemblyInitialize(Assembly pluAssembly, IPlusAssemblyInfo fwAss)
        {
            switch (fwAss.PlusConfigInfo.AssemblyType)
            {
                case AssemblyTypes.WF:
                    #region 解析流程配置文件并生成表单
                    //TODO:流程程序集处理
                    #endregion
                    break;
                case AssemblyTypes.Resource:

                    break;
                case AssemblyTypes.Code:
                    //初始化Handler
                    Handlers.WebHandlersManager.Initialize(pluAssembly);
                    break;
                case AssemblyTypes.Theme:

                    #region 获取所有主题。
                    //ThemeManager.GetThemes(pluAssembly);
                    #endregion
                    //初始化Handler
                    Handlers.WebHandlersManager.Initialize(pluAssembly);
                    break;
                case AssemblyTypes.Strategy:
                    //初始化Handler
                    Handlers.WebHandlersManager.Initialize(pluAssembly);
                    break;
                case AssemblyTypes.Models:
                    {
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 加载程序集资源
        /// </summary>
        /// <param name="pluAssembly"></param>
        /// <param name="isWriteResource"></param>
        /// <param name="fwAss"></param>
        public void LoadAssemblyResources(Assembly pluAssembly, bool isWriteResource, IPlusAssemblyInfo fwAss)
        {
            var resources = pluAssembly.GetManifestResourceNames();
            foreach (var resourceName in resources)
            {
                IResourceUrl resourceUrl = new ResourceUrl()
                {
                    LastRequestTime = null,
                    ManifestResourceName = resourceName,
                    RequestCount = 0,
                    AssemblyFullName = pluAssembly.FullName,
                    HasWrittenToSiteDir = isWriteResource
                };

                var assDirPath = Path.Combine(GlobalApplicationObject.Current.ApplicationContext.SitePaths.CacheResourceDirPath, fwAss.Name);
                //判断是否将资源写入站点目录
                if (isWriteResource)
                    WriteResourceToSiteDir(assDirPath, pluAssembly, ref resourceUrl, fwAss);
                else
                    CheckHasWrittenToSiteDir(assDirPath, fwAss, ref resourceUrl);
                //key为小写并且将“-”替换为“_”
                ManifestResourceManager.AddResourcesDic(resourceName.ToLower().Replace("-", "_"), resourceUrl);
            }
        }


        /// <summary>
        /// 检查是否已将资源写入站点目录
        /// </summary>
        /// <param name="assDirPath"></param>
        /// <param name="fwAss"></param>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        public bool CheckHasWrittenToSiteDir(string assDirPath, IPlusAssemblyInfo fwAss, ref IResourceUrl resourceUrl)
        {
            var childPath = resourceUrl.ManifestResourceName.Replace(fwAss.Name, string.Empty).Trim('.');
            var childDirs = childPath.Split('.');
            var fileName = string.Empty;
            if (childDirs.Length >= 2)
            {
                childPath = childDirs.Where((str, index) => index != childDirs.Length - 1)
                    .Aggregate(assDirPath, Path.Combine);
                childPath.CreateDirectoryIfNotExist();
                fileName = string.Format("{0}.{1}", childDirs[childDirs.Length - 2], childDirs[childDirs.Length - 1]);
            }
            //如果没有父级命名空间
            if (childDirs.Length == 1)
                fileName = childPath;
            if (!fileName.IsNotEmpty()) return true;
            var filePath = Path.Combine(childPath, fileName);
            if (!File.Exists(filePath)) return false;
            resourceUrl.FileWriteTicks = AssemblyManager.GetFileWriteTicks(filePath);
            resourceUrl.HasWrittenToSiteDir = true;
            resourceUrl.SiteRelativeUrl = filePath.Replace(GlobalApplicationObject.Current.ApplicationContext.SitePaths.SiteRootDirPath, string.Empty)
                    .Replace("\\", "/");
            return false;
        }

        /// <summary>
        /// 将文件输出到站点目录
        /// </summary>
        /// <param name="assDirPath"></param>
        /// <param name="pluAssembly"></param>
        /// <param name="resourceUrl"></param>
        /// <param name="fwAss"></param>
        public void WriteResourceToSiteDir(string assDirPath, Assembly pluAssembly, ref IResourceUrl resourceUrl, IPlusAssemblyInfo fwAss)
        {
            assDirPath.CreateDirectoryIfNotExist();
            if (resourceUrl.ManifestResourceName.EndsWith(".config"))
            {
                resourceUrl.HasWrittenToSiteDir = false;
                return;
            }
            #region 根据命名空间创建目录
            var childPath = resourceUrl.ManifestResourceName.Replace(fwAss.Name, string.Empty).Trim('.');
            var childDirs = childPath.Split('.');
            var fileName = string.Empty;
            if (childDirs.Length >= 2)
            {
                childPath = childDirs
                    .Where((str, index) =>
                        index < childDirs.Length - 2)
                    .Aggregate(assDirPath, Path.Combine);
                childPath.CreateDirectoryIfNotExist();
                fileName = string.Format("{0}.{1}", childDirs[childDirs.Length - 2], childDirs[childDirs.Length - 1]);
            }
            #endregion

            //如果没有父级命名空间
            if (childDirs.Length == 1)
                fileName = childPath;
            if (!fileName.IsNotEmpty()) return;
            var filePath = Path.Combine(childPath, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            //获取相对路径
            resourceUrl.SiteRelativeUrl = filePath.Replace(GlobalApplicationObject.Current.ApplicationContext.SitePaths.SiteRootDirPath, string.Empty)
                    .Replace("\\", "/");

            #region 压缩Js和Css
            var ext = Path.GetExtension(fileName);
            if ((ext == ".js" || ext == ".css"))
            {
                var content = ManifestResourceManager.GetWebResourceAsString(pluAssembly, resourceUrl.ManifestResourceName);
                if (!resourceUrl.ManifestResourceName.ToLower().Contains(".min."))
                {
                    #region 判断是否压缩脚本或者样式文件
                    var configManager = GlobalApplicationObject.Current.ApplicationContext.ConfigManager;
                    var sysConfig = configManager.GetConfig<SystemConfigInfo>();
                    if (ext == ".js" && sysConfig.IsMinJs)
                        content = MinHelper.MinJs(content);
                    else if (ext == ".css" && sysConfig.IsMinCss)
                        content = MinHelper.MinCss(content);
                    #endregion
                  
                }
                //写文件
                File.WriteAllText(filePath, content, Encoding.UTF8);
            }
            else
            {
                using (var stream = ManifestResourceManager.GetManifestResourceStream(pluAssembly, resourceUrl.ManifestResourceName))
                {
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);  //将流的内容读到缓冲区
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(buffer, 0, buffer.Length);
                        fs.Flush();
                    }
                }
            }
            #endregion
            resourceUrl.FileWriteTicks = AssemblyManager.GetFileWriteTicks(filePath);
        }

    }
}
