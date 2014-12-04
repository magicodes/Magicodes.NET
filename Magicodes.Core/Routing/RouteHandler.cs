using Magicodes.Core.Handlers;
using Magicodes.Web.Interfaces.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

namespace Magicodes.Core.Routing
{
    public class RouteHandler : IRouteHandler
    {
        public RouteWebHandlerInfo RouteWebHandlerInfo { get; set; }
        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var handler = new WebHandlersProcess()
            {
                HandlerType = RouteWebHandlerInfo.WebHandlerInfo.WebHandlerType,
                WebContext = new WebContext()
                {
                    ReqeustContext = requestContext
                },
                RouteWebHandlerInfo = RouteWebHandlerInfo
            };
            return handler;
        }
    }
}
