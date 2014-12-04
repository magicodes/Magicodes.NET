using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :RouteDebugger
//        description :
//
//        created by 雪雁 at  2014/11/17 15:32:12
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.RouteDebugger
{
    public static class RouteDebugger
    {
        public static void RewriteRoutesForTesting(RouteCollection routes)
        {
            using (routes.GetReadLock())
            {
                bool foundDebugRoute = false;
                foreach (RouteBase routeBase in routes)
                {
                    Route route = routeBase as Route;
                    if (route != null)
                    {
                        route.RouteHandler = new DebugRouteHandler();
                    }

                    if (route == DebugRoute.Singleton)
                        foundDebugRoute = true;

                }
                if (!foundDebugRoute)
                {
                    routes.Add(DebugRoute.Singleton);
                }
            }
        }
    }
}
