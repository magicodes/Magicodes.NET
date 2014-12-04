using Magicodes.Core.Web;
using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Magicodes.Web.Interfaces;
using Magicodes.SignalR.Hubs.LogHub;
using System.Web;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Plus
//        description :
//
//        created by 雪雁 at  2014/10/25 20:58:40
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.SignalR
{
    class Plus : IPlus
    {
        LoggingHubProxy hubProxy;
        public void Initialize()
        {
            GlobalConfigurationManager.OnConfiguration_AppBuilder += GlobalConfigurationManager_OnConfiguration_AppBuilder;
            //TODO:判断是否启用Signalr日志组件
            //GlobalApplicationObject.Current.EventsManager.BeginRequest += EventsManager_BeginRequest;

        }

        void EventsManager_BeginRequest(object sender, EventArgs e)
        {
            if (hubProxy == null)
            {
                hubProxy = new LoggingHubProxy(HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.Port == 80 ? string.Empty : HttpContext.Current.Request.Url.Port.ToString()));
                GlobalApplicationObject.Current.ApplicationContext.ApplicationLog.OnLog += ApplicationLog_OnLog;
            }
        }

        void ApplicationLog_OnLog(object sender, Web.Interfaces.Strategy.Logger.LogArgs e)
        {
            hubProxy.Log(e.loggerLevels, e.Message, e.Exception);
        }



        void GlobalConfigurationManager_OnConfiguration_AppBuilder(object sender, EventArgs e)
        {
            var app = (IAppBuilder)sender;
            //配置SignalR
            app.MapSignalR();
        }

        public void Install()
        {
            throw new NotImplementedException();
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}
