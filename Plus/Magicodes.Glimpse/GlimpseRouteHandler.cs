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
//        filename :GlimpseRoutHandler
//        description :
//
//        created by 雪雁 at  2014/11/21 10:23:11
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.GlimpsePanel
{
    public class GlimpseRouteHandler : IRouteHandler
    {
        IHttpHandler handler;
        public GlimpseRouteHandler()
        {
            handler = new Glimpse.AspNet.HttpHandler();
        }
        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return handler;
        }
    }
}
