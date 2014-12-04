using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;

namespace Magicodes.Web.Interfaces.Paths
{
    /// <summary>
    /// 站点路径
    /// </summary>
    public class SitePaths
    {
        /// <summary>
        /// 插件目录名称
        /// </summary>
        public const string PLUSDIR = "PLUS";
        /// <summary>
        /// 缓存目录名称
        /// </summary>
        public const string CACHEDIR = "Cache";
        /// <summary>
        /// 缓存资源目录
        /// </summary>
        public const string CACHERESOURCEDIR = "Resource";
        /// <summary>
        /// 插件目录
        /// </summary>
        public const string PLUSFILESDIR = "Plugins";
        /// <summary>
        /// 插件影射目录
        /// </summary>
        public const string PLUSSHADOWCOPYDIR = "bin";
        /// <summary>
        /// 站点根目录路径
        /// </summary>
        public string SiteRootDirPath { get; private set; }
        /// <summary>
        /// 缓存目录
        /// </summary>
        public string CacheDirPath { get; private set; }
        /// <summary>
        /// 插件目录路径
        /// </summary>
        public string PlusDirPath { get; private set; }
        /// <summary>
        /// 缓存资源目录
        /// </summary>
        public string CacheResourceDirPath { get; private set; }
        /// <summary>
        /// 插件缓存目录路径
        /// </summary>
        public string PlusFilesDirPath { get; private set; }
        /// <summary>
        /// 插件影像复制目录路径
        /// </summary>
        public string PlusShadowCopyDirPath { get; private set; }
        /// <summary>
        /// 站点配置文件目录路径
        /// </summary>
        public string SiteConfigDirPath { get; private set; }
        /// <summary>
        /// 获取网站Bin目录
        /// </summary>
        public string SiteRootBinDirPath
        {
            get
            {
                return Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);
            }
        }
        public SitePaths()
        {
            SiteRootDirPath = HttpRuntime.AppDomainAppPath.TrimEnd('\\');

            CacheDirPath = Path.Combine(SiteRootDirPath, CACHEDIR);
            if (!Directory.Exists(CacheDirPath)) Directory.CreateDirectory(CacheDirPath);

            PlusDirPath = Path.Combine(SiteRootDirPath, PLUSDIR);
            if (!Directory.Exists(PlusDirPath)) Directory.CreateDirectory(PlusDirPath);

            CacheResourceDirPath = Path.Combine(CacheDirPath, CACHERESOURCEDIR);
            if (!Directory.Exists(CacheResourceDirPath)) Directory.CreateDirectory(CacheResourceDirPath);

            PlusFilesDirPath = Path.Combine(PlusDirPath, PLUSFILESDIR);
            if (!Directory.Exists(PlusFilesDirPath)) Directory.CreateDirectory(PlusFilesDirPath);

            PlusShadowCopyDirPath = Path.Combine(PlusDirPath, PLUSSHADOWCOPYDIR);
            if (!Directory.Exists(PlusShadowCopyDirPath)) Directory.CreateDirectory(PlusShadowCopyDirPath);

            SiteConfigDirPath = Path.Combine(SiteRootDirPath, "App_Data", "Config");
            if (!Directory.Exists(SiteConfigDirPath)) Directory.CreateDirectory(SiteConfigDirPath);
        }
    }
}
