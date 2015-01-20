using System;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.IO;
using System.Web.Security;
using Magicodes.Web.Interfaces;
using Magicodes.Utility;
using Magicodes.Web.Interfaces.Strategy.Logger;
using Magicodes.Web.Interfaces.Paths;
using Magicodes.Web.Interfaces.Events;
namespace Magicodes.Core.Routing
{
    public class CoreModule : IHttpModule
    {
        static LoggerStrategyBase Log
        {
            get
            {
                return GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
            }
        }

        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此模块
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        #region IHttpModule Members
        static CoreModule()
        {
            GlobalApplicationObject.Current.ApplicationContext.Initialize();
            //执行初始化完成事件
            GlobalApplicationObject.Current.EventsManager.InitApplicationEvents(null, ApplicationEvents.OnApplication_InitializeComplete);
        }

        public void Dispose()
        {
            //此处放置清除代码。
        }

        public void Init(HttpApplication context)
        {
            context.Error += context_Error;
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
            context.PostResolveRequestCache += context_PostResolveRequestCache;
        }

        void context_PostResolveRequestCache(object sender, EventArgs e)
        {
            GlobalApplicationObject.Current.EventsManager.InitHttpApplicationEvents(sender, ApplicationEvents.PostResolveRequestCache);

            //应用程序对象
            var application = (HttpApplication)sender;
            //Http上下文对象
            var context = application.Context;
            //如果HttpHandler为NULL，则不处理
            if (context.Handler != null) return;
            //请求Url的绝对路径
            string filePath = context.Request.Url.AbsolutePath;
            if (filePath.IsEmpty())
            {

            }
            else
            {
                filePath = filePath.ToLower();
                var endPage = filePath.RightOfRightmostOf('/');
                if (endPage.IsNotEmpty() && endPage.Contains("?"))
                    endPage = endPage.Split('?')[0];

                var ext = VirtualPathUtility.GetExtension(filePath);
                //判断为默认页
                if (string.IsNullOrEmpty(ext) && string.IsNullOrEmpty(endPage))
                {
                    var siteDefaultUrl = GlobalApplicationObject.Current.ApplicationContext.ConfigManager.GetConfig<Magicodes.Web.Interfaces.Config.Info.SiteConfigInfo>().SiteDefaultUrl;
                    if (!siteDefaultUrl.IsEmpty())
                    {
                        context.Response.Redirect(siteDefaultUrl);
                        return;
                    }
                }
                Log.Log(LoggerLevels.Trace, context.Request.Url);
            }
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            GlobalApplicationObject.Current.EventsManager.InitHttpApplicationEvents(sender, ApplicationEvents.EndRequest);
        }

        void context_Error(object sender, EventArgs e)
        {
            GlobalApplicationObject.Current.EventsManager.InitHttpApplicationEvents(sender, ApplicationEvents.Error);
            var application = (HttpApplication)sender;
            //ExceptionHelper.CommonHandlerTheException(application);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            GlobalApplicationObject.Current.EventsManager.InitHttpApplicationEvents(sender, ApplicationEvents.BeginRequest);
        }

        #endregion
    }
}
