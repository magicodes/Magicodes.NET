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
//        filename :DebugRoute
//        description :
//
//        created by 雪雁 at  2014/11/17 15:30:44
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.RouteDebugger
{
    public class DebugRoute : Route
    {
        private static DebugRoute singleton = new DebugRoute();

        public static DebugRoute Singleton
        {
            get { return singleton; }
        }

        private DebugRoute()
            : base("{*catchall}", new DebugRouteHandler())
        { }
    }
}
