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
//        filename :DebugRouteHandler
//        description :
//
//        created by 雪雁 at  2014/11/17 15:31:33
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.RouteDebugger
{
    public class DebugRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new DebugHttpHandler();
        }
    }
}
