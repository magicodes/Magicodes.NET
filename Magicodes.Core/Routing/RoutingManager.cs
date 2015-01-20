using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Routing;
using Magicodes.Web.Interfaces.Strategy.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Magicodes.Utility;
using Magicodes.Web.Interfaces.Config.Info;
using System.Web;
using Magicodes.Core.Strategy.Logger;
using System.Web.Http;

namespace Magicodes.Core.Routing
{
    public class RoutingManager : IRoutingManager
    {
        public RoutingManager()
        {
            RoutingInfoList = new List<RoutingInfo>();
        }
        LoggerStrategyBase Log
        {
            get
            {
                return LoggerHelper.GetDefaultLogStrategy();
            }
        }
         /// <summary>
        /// 路由信息集合
        /// </summary>
        public List<RoutingInfo> RoutingInfoList { get; set; }
        
        public void Initialize()
        {
            RouteTable.Routes.RouteExistingFiles = false;

            //RouteTable.Routes.Add("MagicodesUrlProvider", new MagicodesUrlProvider());
        }

        public IRoutingManager Add(string routeName, string routeUrl, string url)
        {
            return Add(routeName, routeUrl, url, true, null, null, null);
        }

        public IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess)
        {
            return Add(routeName, routeUrl, url, checkPhysicalUrlAccess, null, null, null);
        }

        public IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess, List<RoutingDictonaryInfo> defaults)
        {
            return Add(routeName, routeUrl, url, checkPhysicalUrlAccess, defaults, null, null);
        }

        public IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess, List<RoutingDictonaryInfo> defaults, List<RoutingDictonaryInfo> constraints)
        {
            return Add(routeName, routeUrl, url, checkPhysicalUrlAccess, defaults, constraints, null);
        }

        public IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess, List<RoutingDictonaryInfo> defaults, List<RoutingDictonaryInfo> constraints, List<RoutingDictonaryInfo> dataTokens)
        {
            if (RoutingInfoList.Any(p => p.routeName == routeName))
            {
                if (Log != null)
                {
                    Log.LogFormat(LoggerLevels.Warn, "Route已存在。 routeName:{0} routeUrl:{1} url:{2} by:{3}", routeName, routeUrl, url, StackHelper.GetCallingType().FullName);
                }
                return this;
            }
            if (Log != null)
            {
                Log.LogFormat(LoggerLevels.Info, "AddRoute routeName:{0} routeUrl:{1} url:{2} by:{3}", routeName, routeUrl, url, StackHelper.GetCallingType().FullName);
            }
            #region 填充路由键值
            RouteValueDictionary defaultsRouteValueDictionary = null;
            if (defaults != null)
            {
                defaultsRouteValueDictionary = new RouteValueDictionary();
                defaults.Each(p => { defaultsRouteValueDictionary.Add(p.Key, p.Value); });
            }
            RouteValueDictionary constraintsRouteValueDictionary = null;
            if (constraints != null)
            {
                constraintsRouteValueDictionary = new RouteValueDictionary();
                constraints.Each(p => { constraintsRouteValueDictionary.Add(p.Key, p.Value); });
            }
            RouteValueDictionary dataTokensRouteValueDictionary = null;
            if (dataTokens != null)
            {
                dataTokensRouteValueDictionary = new RouteValueDictionary();
                dataTokens.Each(p => { dataTokensRouteValueDictionary.Add(p.Key, p.Value); });
            }
            #endregion
            RouteTable.Routes.MapPageRoute(routeName, routeUrl, url, checkPhysicalUrlAccess, defaultsRouteValueDictionary, constraintsRouteValueDictionary, dataTokensRouteValueDictionary);
            return this;
        }

        public IRoutingManager Ignore(string url)
        {
            RouteTable.Routes.Ignore(url);
            return this;
        }

        public IRoutingManager Ignore(string url, object constraints)
        {
            RouteTable.Routes.Ignore(url, constraints);
            return this;
        }


        public VirtualPathData GetVirtualPathData(string routingName, RouteValueDictionary parameters)
        {
            return RouteTable.Routes.GetVirtualPath(null, routingName, parameters);
        }
        public VirtualPathData GetVirtualPathData(RouteValueDictionary parameters)
        {
            return RouteTable.Routes.GetVirtualPath(null, parameters);
        }

        public string GetVirtualPath(string routingName, RouteValueDictionary parameters)
        {
            return GetVirtualPathData(routingName, parameters).VirtualPath;
        }
        /// <summary>
        /// 返回虚拟路径，例如：http://[server]/[application]/Goods/CA/2014
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetVirtualPath(RouteValueDictionary parameters)
        {
            return GetVirtualPathData(parameters).VirtualPath;
        }
    }
}
