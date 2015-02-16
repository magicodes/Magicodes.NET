using Magicodes.Web.Interfaces.Config;
using Magicodes.Web.Interfaces.Config.Info;
using Magicodes.Web.Interfaces.DocumentProtocols;
using Magicodes.Web.Interfaces.Paths;
using Magicodes.Web.Interfaces.Plus;
using Magicodes.Web.Interfaces.Plus.Resource;
using Magicodes.Web.Interfaces.Routing;
using Magicodes.Web.Interfaces.Strategy;
using Magicodes.Web.Interfaces.Strategy.Logger;
using Magicodes.Web.Interfaces.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;

namespace Magicodes.Web.Interfaces
{
    /// <summary>
    /// 当前应用程序上下文对象
    /// </summary>
    public abstract class ApplicationContextBase
    {
        /// <summary>
        /// 站点路径信息
        /// </summary>
        readonly Lazy<SitePaths> sitePaths = new Lazy<SitePaths>(() => new SitePaths());

        #region 属性
        /// <summary>
        /// 网站路径
        /// </summary>
        public SitePaths SitePaths { get { return sitePaths.Value; } }
        /// <summary>
        /// 文档打开协议管理器
        /// </summary>
        public IDocumentsOpenProtocolManager DocumentsOpenProtocolManager { get;set; }
        /// <summary>
        /// 应用程序日志对象
        /// </summary>
        public abstract LoggerStrategyBase ApplicationLog { get; }

        /// <summary>
        /// 程序集资源辅助对象
        /// </summary>
        public IResourceHelper CurrentResourceHelper { get; set; }
        /// <summary>
        /// 当前应用程序域
        /// </summary>
        public AppDomain CurrentAppDomain { get; set; }
        /// <summary>
        /// 应用程序模式
        /// </summary>
        public virtual ApplicationModes ApplicationMode { get; set; }
        /// <summary>
        /// 主题列表
        /// </summary>
        public virtual List<ITheme> ThemesList { get; set; }
        /// <summary>
        /// 策略管理器
        /// </summary>
        public abstract StrategyManagerBase StrategyManager { get; set; }
        /// <summary>
        /// 默认主题
        /// </summary>
        public virtual ITheme DefaultTheme { get; set; }

        /// <summary>
        /// 插件列表
        /// </summary>
        public virtual List<IPlusInfo> PlusAssemblysList { get; set; }
        /// <summary>
        /// 路由管理器
        /// </summary>
        public IRoutingManager RoutingManager { get; set; }
        /// <summary>
        /// 插件管理器
        /// </summary>
        public PlusManagerBase PlusManager { get; set; }
        /// <summary>
        /// 程序集资源管理器
        /// </summary>
        public IManifestResourceManager ManifestResourceManager { get; set; }
        /// <summary>
        /// 配置管理器
        /// </summary>
        public ConfigManagerBase ConfigManager { get; set; }
        /// <summary>
        /// 是否调试模式
        /// </summary>
        public virtual bool IsDebug
        {
            get
            {
                var sysConfig = ConfigManager.GetConfig<SystemConfigInfo>();
                if (sysConfig != null)
                    return sysConfig.ApplicationMode == ApplicationModes.Debug;
                return false;
            }
        }

        /// <summary>
        /// 是否为Sql跟踪模式
        /// </summary>
        public virtual bool IsSqlTrace
        {
            get
            {
                var sysConfig = ConfigManager.GetConfig<SystemConfigInfo>();
                if (sysConfig != null)
                    return sysConfig.IsSqlTrace;
                return false;
            }
        }

        /// <summary>
        /// 是否为跟踪模式
        /// </summary>
        public virtual bool IsTrace
        {
            get
            {
                var sysConfig = ConfigManager.GetConfig<SystemConfigInfo>();
                if (sysConfig != null)
                    return sysConfig.ApplicationMode == ApplicationModes.Trace;
                return false;
            }
        }
        #endregion
        /// <summary>
        /// 初始化插件
        /// </summary>
        public abstract void PreApplicationStartInitialize();
        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="folder"></param>
        [FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
        public abstract void LoadAssemblies();

    }
}
