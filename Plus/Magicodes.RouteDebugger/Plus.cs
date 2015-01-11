using Magicodes.Core.Web;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
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
namespace Magicodes.RouteDebugger
{
    public class Plus : IPlus
    {
        internal const string WatchPanelName = "路由规则";
        /// <summary>
        /// 是否启用路由跟踪
        /// </summary>
        private bool IsEnableRouteDebugger
        {
            get
            {
                return Magicodes.Web.Interfaces.GlobalApplicationObject.Current.ApplicationContext.IsDebug;
            }
        }
        public void Initialize()
        {
            //更改即用
            //if (IsEnableRouteDebugger)
            {
                Magicodes.Web.Interfaces.GlobalApplicationObject.Current.EventsManager.BeginRequest += EventsManager_BeginRequest;
                Magicodes.Web.Interfaces.GlobalApplicationObject.Current.EventsManager.EndRequest += EventsManager_EndRequest;
            }
        }

        void EventsManager_EndRequest(object sender, EventArgs e)
        {
            if (!IsEnableRouteDebugger) return;

            var handler = new DebugHttpHandler();
            handler.ProcessRequest(HttpContext.Current);
        }

        private void EventsManager_BeginRequest(object sender, EventArgs e)
        {
            if (!IsEnableRouteDebugger) return;
            GlobalApplicationObject.Current.WatchPanel.AddTabAsync(WatchPanelName);
            if (!RouteTable.Routes.Contains(DebugRoute.Singleton))
            {
                RouteTable.Routes.Add(DebugRoute.Singleton);
            }
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
