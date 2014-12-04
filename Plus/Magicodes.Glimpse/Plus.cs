using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.Web.Interfaces.Plus;
using Magicodes.Web.Interfaces;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Routing;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Plus
//        description :
//
//        created by 雪雁 at  2014/11/17 19:09:29
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.GlimpsePanel
{
    public class Plus : IPlus
    {
        public void Initialize()
        {
            //TODO:增加配置文件来决定是否启用此面板
            GlobalApplicationObject.Current.EventsManager.OnApplication_PreInitialize += EventsManager_OnApplication_PreInitialize;
            GlobalApplicationObject.Current.EventsManager.OnApplication_InitializeComplete += EventsManager_OnApplication_InitializeComplete;
            GlobalApplicationObject.Current.EventsManager.PostResolveRequestCache += EventsManager_PostResolveRequestCache;
        }

        void EventsManager_OnApplication_InitializeComplete(object sender, Web.Interfaces.Events.ApplicationArgs e)
        {
            //RouteTable.Routes.Add(new Route("glimpse.axd", new GlimpseRouteHandler()));
        }

        void EventsManager_PostResolveRequestCache(object sender, EventArgs e)
        {
            //应用程序对象
            var application = (HttpApplication)sender;
            //Http上下文对象
            var context = application.Context;
            //如果HttpHandler为NULL，则不处理
            if (context.Handler != null) return;
            //请求Url的绝对路径
            string filePath = context.Request.Url.AbsolutePath;
            if (filePath != null)
            {
                filePath = filePath.ToLower();
                if (filePath.StartsWith("/glimpse.axd"))
                {
                    context.RemapHandler(new Glimpse.AspNet.HttpHandler());
                }
            }
        }

        void EventsManager_OnApplication_PreInitialize(object sender, Web.Interfaces.Events.ApplicationArgs e)
        {
            DynamicModuleUtility.RegisterModule(typeof(Glimpse.AspNet.HttpModule));
        }

        public void Install()
        {

        }

        public void Uninstall()
        {

        }
    }
}