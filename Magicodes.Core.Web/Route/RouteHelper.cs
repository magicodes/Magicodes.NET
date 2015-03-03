using Magicodes.Core.Web.Mvc;
using Magicodes.Web.Interfaces.Plus.Info;
using Magicodes.Web.Interfaces.Plus.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :RouteHelper
//        description :
//
//        created by 雪雁 at  2015/2/9 17:12:01
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Route
{
    public class RouteHelper
    {
        /// <summary>
        /// 路由结束方法
        /// </summary>
        public static void RouteEnd()
        {
            //最后注册门户MVC插件
            var mvcHomePlus = MvcConfigManager.MVCPlusList.First(p => p.MvcPlusType == MvcPlusTypes.MVCHome);
            RouteTable.Routes.MapRoute(
                           name: "MCV_" + mvcHomePlus.PlusName,
                           url: "{controller}/{action}/{id}",
                           defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, pluginName = mvcHomePlus.PlusName });
        }
        /// <summary>
        /// 映射MVC插件路由
        /// </summary>
        /// <param name="mvcPlus"></param>
        public static void MapRouteMVCPlus(IMVCPlusInfo mvcPlus)
        {
            switch (mvcPlus.MvcPlusType)
            {
                ////此类型插件只支持一个
                //case MvcPlusTypes.MVCHome:
                //    break;
                case MvcPlusTypes.MVC:
                    {
                        RouteTable.Routes.MapRoute(
                            name: "MCV_" + mvcPlus.PlusName,
                            url: "_{pluginName}/{controller}/{action}/{id}",
                            defaults: new { action = "Index", id = UrlParameter.Optional, pluginName = mvcPlus.PlusName });
                    }
                    break;
                //此类型插件只支持一个
                case MvcPlusTypes.MVCAdmin:
                    {
                        RouteTable.Routes.MapRoute(
                           name: "MCV_" + mvcPlus.PlusName,
                           url: "admin/{controller}/{action}/{id}",
                           defaults: new { action = "Index", controller = "Admin", id = UrlParameter.Optional, pluginName = mvcPlus.PlusName });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
