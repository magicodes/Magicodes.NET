using Magicodes.Core.Web.Mvc;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData.Builder;
using System.Web.Routing;
using System.Web.OData.Extensions;
using Magicodes.Web.Interfaces;
using System.Web;
using System.Text.RegularExpressions;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :GlobalConfigurationManager
//        description :
//
//        created by 雪雁 at  2014/10/21 21:01:41
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web
{
    public class GlobalConfigurationManager
    {
        private static readonly Lazy<ODataConventionModelBuilder> oDataBuilder = new Lazy<ODataConventionModelBuilder>(() => new ODataConventionModelBuilder());
        /// <summary>
        /// ODataConventionModelBuilder
        /// </summary>
        public static ODataConventionModelBuilder ODataBuilder
        {
            get
            {
                return oDataBuilder.Value;

            }
        }
        /// <summary>
        /// App配置事件，您可以在此配置应用程序
        /// </summary>
        public static event EventHandler OnConfiguration_Config = (o, e) => { };
        /// <summary>
        /// App构建事件
        /// </summary>
        public static event EventHandler OnConfiguration_AppBuilder = (o, e) => { };

        /// <summary>
        /// 所有的配置均注册完成后再调用此函数
        /// </summary>
        public static void Config()
        {
            try
            {
                GlobalConfiguration.Configure(config =>
                    {
                        OnConfiguration_Config(config, null);
                    });
            }
            catch (Exception ex)
            {
                throw new Magicodes.Web.Interfaces.MagicodesException("初始化配置失败！", ex);
            }
        }
        public static void AppBuilder(IAppBuilder app)
        {
            //可以在此事件配置SignalR、Auth等
            OnConfiguration_AppBuilder(app, null);
        }
        public static void MapHttpAttributeRoutes()
        {
            GlobalConfigurationManager.OnConfiguration_Config += GlobalConfigurationManager_OnConfiguration_Config_MapHttpAttributeRoutes;
        }
        static void GlobalConfigurationManager_OnConfiguration_Config_MapHttpAttributeRoutes(object sender, EventArgs e)
        {
            HttpConfiguration config = (HttpConfiguration)sender;
            config.MapHttpAttributeRoutes();
        }
        /// <summary>
        /// 初始化MVC模板引擎
        /// </summary>
        public static void InitializeMVCEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new PluginRazorViewEngine());
            GlobalConfigurationManager.OnConfiguration_Config += GlobalConfigurationManager_OnConfiguration_Config_MVC;
        }
        /// <summary>
        /// 初始化OData
        /// </summary>
        public static void InitializeODATA()
        {
            GlobalConfigurationManager.OnConfiguration_Config += GlobalConfigurationManager_OnConfiguration_Config_OData;
        }
        static void GlobalConfigurationManager_OnConfiguration_Config_OData(object sender, EventArgs e)
        {
            HttpConfiguration config = (HttpConfiguration)sender;
            //config.EnableQuerySupport();
            config.MapODataServiceRoute(routeName: "OData", routePrefix: "odata", model: ODataBuilder.GetEdmModel());
        }
        /// <summary>
        /// 初始化WebAPI
        /// </summary>
        public static void InitializeWebAPI()
        {
            GlobalConfigurationManager.OnConfiguration_Config += GlobalConfigurationManager_OnConfiguration_Config_WEBAPI;
        }
        static void GlobalConfigurationManager_OnConfiguration_Config_MVC(object sender, EventArgs e)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            AreaRegistration.RegisterAllAreas();

            foreach (var mvcPlus in MvcConfigManager.MVCPlusList.OrderByDescending(p => p.MvcPlusType))
            {
                switch (mvcPlus.MvcPlusType)
                {
                    case MvcPlusTypes.MVCHome:
                        {
                            RouteTable.Routes.MapRoute(
                                name: "MCV_" + mvcPlus.PlusName,
                                url: "{controller}/{action}/{id}",
                                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, pluginName = mvcPlus.PlusName });
                        }
                        break;
                    case MvcPlusTypes.MVC:
                        {
                            RouteTable.Routes.MapRoute(name: "MCV_" + mvcPlus.PlusName, url: "_{pluginName}/{controller}/{action}/{id}", defaults: new { action = "Index", id = UrlParameter.Optional, pluginName = mvcPlus.PlusName });
                        }
                        break;
                    default:
                        break;
                }

            }
            //注册请求事件，处理插件资源加载问题
            GlobalApplicationObject.Current.EventsManager.BeginRequest += (requestSender,arg) =>
            {
                //应用程序对象
                var application = (HttpApplication)requestSender;
                //HTTP上下文对象
                var context = application.Context;
                #region 如果非站内请求，随它去吧
                if (!context.Request.IsLocal)
                    return; 
                #endregion
                #region 如果非插件类请求，随它去吧
                if (!context.Request.Url.AbsolutePath.StartsWith("/Magicodes.", StringComparison.CurrentCultureIgnoreCase))
                    return; 
                #endregion
                foreach (var plusItem in MvcConfigManager.MVCPlusList)
                {
                    //插件名称
                    var plusName = plusItem.PlusName;
                    //插件路径
                    var plusPath = "/plus/Plugins/" + plusName;
                    //匹配当前插件路径
                    if (context.Request.Url.AbsolutePath.StartsWith("/" + plusName + "/", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //非bundles路径，则跳转到相应插件目录加载资源
                        if (!context.Request.Url.AbsolutePath.StartsWith("/" + plusName + "/bundles", StringComparison.CurrentCultureIgnoreCase))
                        {
                            var url = Regex.Replace(context.Request.Url.AbsolutePath, "/" + plusName + "/", plusPath + "/", RegexOptions.IgnoreCase);
                            context.Response.Redirect(url);
                        }
                        else
                        {
                            //如果Accept不为*/*或者text/css（脚本和样式）,则去掉bundles进行跳转
                            if (!context.Request.AcceptTypes.Any(t => t.Equals("text/css", StringComparison.OrdinalIgnoreCase) || t.Equals("*/*", StringComparison.OrdinalIgnoreCase)))
                            {
                                var url = Regex.Replace(context.Request.Url.AbsolutePath, "/" + plusName + "/bundles/", plusPath + "/", RegexOptions.IgnoreCase);
                                context.Response.Redirect(url);
                            }
                        }
                        return;
                    }
                }
            };
        }
        static void GlobalConfigurationManager_OnConfiguration_Config_WEBAPI(object sender, EventArgs e)
        {
            HttpConfiguration config = (HttpConfiguration)sender;

            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // 对 JSON 数据使用混合大小写。
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API 路由

            config.Routes.MapHttpRoute(
              name: "WebAPI",
              routeTemplate: "api/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }

          );
        }
    }
}
