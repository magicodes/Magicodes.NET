using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Magicodes.Utility;
using System.Web;
using Magicodes.Web.Interfaces;
namespace Magicodes.Core.Routing
{
    public class MagicodesUrlProvider : RouteBase
    {
        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            ////如果HttpHandler为NULL，则不处理
            //if (httpContext.Handler != null) return null;
            ////请求Url的绝对路径
            //string filePath = httpContext.Request.Url.AbsolutePath;
            //if (filePath.IsEmpty()) return null;
            //filePath = filePath.ToLower();
            //var endPage = filePath.RightOfRightmostOf('/');
            //if (endPage.IsNotEmpty() && endPage.Contains("?"))
            //    endPage = endPage.Split('?')[0];
            //var routeWebHander = GlobalApplicationObject.Current.ApplicationContext.RoutingManager.RouteWebHandlerInfoList.FirstOrDefault(p => p.routeUrl == filePath);
            //if (routeWebHander.IsNotNull())
            //{
            //    var routeHandler = new RouteHandler()
            //    {
            //        RouteWebHandlerInfo = routeWebHander
            //    };
            //    var data = new RouteData(this, routeHandler);
            //    return data;
            //}
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
