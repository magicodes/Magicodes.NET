using Magicodes.Core.Web.Security;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Config.Info;
using Magicodes.Web.Interfaces.Plus;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :PlusStarter
//        description :插件启动类
//
//        created by 雪雁 at  2014/11/23 22:19:46
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.MiniProfilter
{
    /// <summary>
    /// 插件启动类
    /// </summary>
    class PlusStarter : IPlus
    {
        public void Initialize()
        {
            GlobalApplicationObject.Current.EventsManager.OnApplication_PreInitialize += EventsManager_OnApplication_PreInitialize;
            GlobalApplicationObject.Current.EventsManager.BeginRequest += EventsManager_BeginRequest;
            GlobalApplicationObject.Current.EventsManager.EndRequest += EventsManager_EndRequest;
        }

        void EventsManager_EndRequest(object sender, EventArgs e)
        {
            MiniProfiler.Stop();
        }

        void EventsManager_BeginRequest(object sender, EventArgs e)
        {
            //应用程序对象
            var application = (HttpApplication)sender;
            //Http上下文对象
            var context = application.Context;
            if (context.Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        void EventsManager_OnApplication_PreInitialize(object sender, Web.Interfaces.Events.ApplicationArgs e)
        {
            //StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();
            MiniProfiler.Settings.Results_Authorize = IsUserAllowedToSeeMiniProfilerUI;
            MiniProfiler.Settings.Results_List_Authorize = IsUserAllowedToSeeMiniProfilerUI;
            //动态注册httpModule
            //DynamicModuleUtility.RegisterModule(typeof(MiniProfilterStartupModule));
        }

        private bool IsUserAllowedToSeeMiniProfilerUI(HttpRequest arg)
        {
            var sys = GlobalApplicationObject.Current.ApplicationContext.ConfigManager.GetConfig<SystemConfigInfo>();
            return sys != null && sys.EnableDevelopersPanel && AuthHelper.IsAdmin;
        }

        public void Install()
        {

        }

        public void Uninstall()
        {

        }
    }
}
