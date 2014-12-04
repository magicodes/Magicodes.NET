using Magicodes.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :DebugHttpHandler
//        description :
//
//        created by 雪雁 at  2014/11/17 15:28:55
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.RouteDebugger
{
    public class DebugHttpHandler : IHttpHandler
    {
        readonly VirtualPathProvider _virtualPathProvider;

        public DebugHttpHandler() : this(null) { }

        public DebugHttpHandler(VirtualPathProvider virtualPathProvider)
        {
            _virtualPathProvider = virtualPathProvider ?? HostingEnvironment.VirtualPathProvider;
        }

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;

            if (!IsRoutedRequest(request) || context.Response.ContentType == null || !context.Response.ContentType.Equals("text/html", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            string generatedUrlInfo = string.Empty;
            var requestContext = request.RequestContext;

            if (request.QueryString.Count > 0)
            {
                var rvalues = new RouteValueDictionary();
                foreach (string key in request.QueryString.Keys)
                {
                    if (key != null)
                    {
                        rvalues.Add(key, request.QueryString[key]);
                    }
                }

                var vpd = RouteTable.Routes.GetVirtualPath(requestContext, rvalues);
                if (vpd != null)
                {
                    generatedUrlInfo = "<p><label style=\"font-weight: bold; font-size: 1.1em;\">生成的URL</label>： ";
                    generatedUrlInfo += "<strong style=\"color: #00a;\">" + vpd.VirtualPath + "</strong>";
                    var vpdRoute = vpd.Route as Route;
                    if (vpdRoute != null)
                    {
                        generatedUrlInfo += " 使用此路由： \"" + vpdRoute.Url + "\"</p>";
                    }
                }
            }

            const string htmlFormat = @"<html>
<div id=""haackroutedebugger"" style=""background-color: #fff; padding-bottom: 10px;"">
    <style>
        #haackroutedebugger, #haackroutedebugger td, #haackroutedebugger th {{background-color: #fff; font-family: verdana, helvetica, san-serif; font-size: small;}}
        #haackroutedebugger tr.header td, #haackroutedebugger tr.header th {{background-color: #ffc;}}
    </style>
    <hr style=""width: 100%; border: solid 1px #000; margin:0; padding:0;"" />
    <h1 style=""margin: 0; padding: 4px; border-bottom: solid 1px #bbb; padding-left: 10px; font-size: 1.2em; background-color: #ffc;"">路由调试工具</h1>
    <div id=""main"" style=""margin-top:0; padding: 0 10px;"">
        <p style=""font-size: .9em; padding-top:0"">
            下面列表会列出当前Url匹配的路由规则。 
            一个名为 {{*catchall}} 的路由会自动添加到路由列表即使不匹配任何路由。
        </p>
        <p style=""font-size: .9em;"">
            可以使用Get参数来生成路由链接，例如： <code>http://localhost/?id=111</code>
        </p>
        <p><label style=""font-weight: bold; font-size: 1.1em;"">匹配的路由</label>： {1}</p>
        {5}
        <div style=""float: left;"">
            <table border=""1"" cellpadding=""3"" cellspacing=""0"" width=""300"">
                <caption style=""font-weight: bold;"">路由数据</caption>
                <tr class=""header""><th>Key</th><th>Value</th></tr>
                {0}
            </table>
        </div>
        <div style=""float: left; margin-left: 10px;"">
            <table border=""1"" cellpadding=""3"" cellspacing=""0"" width=""300"">
                <caption style=""font-weight: bold;"">自定义值：</caption>
                <tr class=""header""><th>Key</th><th>Value</th></tr>
                {4}
            </table>
        </div>
        <hr style=""clear: both;"" />
        <table border=""1"" cellpadding=""3"" cellspacing=""0"">
            <caption style=""font-weight: bold;"">当前所有的路由</caption>
            <tr class=""header"">
                <th>是否匹配当前请求</th>
                <th>Url</th>
                <th>默认值</th>
                <th>约束</th>
                <th>自定义值</th>
            </tr>
            {2}
        </table>
        <hr />
        <h3>当前请求信息</h3>
        <p>
            当前路由作用于应用程序根的虚拟路径。
        </p>
        <p><strong>获取应用程序根的虚拟路径（AppRelativeCurrentExecutionFilePath）</strong>： {3}</p>
    </div>
</div>";
            string routeDataRows = string.Empty;

            var routeData = requestContext.RouteData;
            var routeValues = routeData.Values;
            var matchedRouteBase = routeData.Route;

            string routes = string.Empty;
            using (RouteTable.Routes.GetReadLock())
            {
                foreach (var routeBase in RouteTable.Routes)
                {
                    bool matchesCurrentRequest = (routeBase.GetRouteData(requestContext.HttpContext) != null);
                    string matchText = string.Format(@"<span{0}>{1}</span>", BoolStyle(matchesCurrentRequest), matchesCurrentRequest);
                    string url = "n/a";
                    string defaults = "n/a";
                    string constraints = "n/a";
                    string dataTokens = "n/a";

                    Route route = CastRoute(routeBase);

                    if (route != null)
                    {
                        url = route.Url;
                        defaults = FormatDictionary(route.Defaults);
                        constraints = FormatDictionary(route.Constraints);
                        dataTokens = FormatDictionary(route.DataTokens);
                    }

                    routes += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>"
                            , matchText
                            , url
                            , defaults
                            , constraints
                            , dataTokens);
                }
            }

            string matchedRouteUrl = "n/a";

            string dataTokensRows = "";

            if (!(matchedRouteBase is DebugRoute))
            {
                foreach (string key in routeValues.Keys)
                {
                    routeDataRows += string.Format("\t<tr><td>{0}</td><td>{1}&nbsp;</td></tr>", key, routeValues[key]);
                }

                foreach (string key in routeData.DataTokens.Keys)
                {
                    dataTokensRows += string.Format("\t<tr><td>{0}</td><td>{1}&nbsp;</td></tr>", key, routeData.DataTokens[key]);
                }

                var matchedRoute = matchedRouteBase as Route;

                if (matchedRoute != null)
                    matchedRouteUrl = matchedRoute.Url;
            }
            else
            {
                matchedRouteUrl = string.Format("<strong{0}>NO MATCH!</strong>", BoolStyle(false));
            }
            var resonseHtml = string.Format(htmlFormat
                , routeDataRows
                , matchedRouteUrl
                , routes
                , request.AppRelativeCurrentExecutionFilePath
                , dataTokensRows
                , generatedUrlInfo);

            var tab = GlobalApplicationObject.Current.WatchPanel.Find(Plus.WatchPanelName);
            if (tab != null)
            {
                tab.AddMessage(resonseHtml);
            }
            //context.Response.Write(string.Format(htmlFormat
            //    , routeDataRows
            //    , matchedRouteUrl
            //    , routes
            //    , request.AppRelativeCurrentExecutionFilePath
            //    , dataTokensRows
            //    , generatedUrlInfo));
        }

        private Route CastRoute(RouteBase routeBase)
        {
            var route = routeBase as Route;
            if (route == null)
            {
                // cheat!
                // TODO: Create an interface for self reporting routes.
                var type = routeBase.GetType();
                var property = type.GetProperty("__DebugRoute", BindingFlags.NonPublic | BindingFlags.Instance);
                if (property != null)
                {
                    route = property.GetValue(routeBase, null) as Route;
                }
            }
            return route;
        }

        private static string FormatDictionary(IDictionary<string, object> values)
        {
            if (values == null)
                return "(null)";

            if (values.Count == 0)
            {
                return "(empty)";
            }

            string display = string.Empty;
            foreach (string key in values.Keys)
            {
                display += string.Format("{0} = {1}, ", key, FormatObject(values[key]));
            }
            if (display.EndsWith(", "))
                display = display.Substring(0, display.Length - 2);
            return display;
        }

        private static string FormatObject(object value)
        {
            if (value == null)
            {
                return "(null)";
            }

            var values = value as object[];
            if (values != null)
            {
                return string.Join(", ", values);
            }

            var dictionaryValues = value as IDictionary<string, object>;
            if (dictionaryValues != null)
            {
                return FormatDictionary(dictionaryValues);
            }

            if (value.GetType().Name == "UrlParameter")
            {
                return "UrlParameter.Optional";
            }

            return value.ToString();
        }

        private static string BoolStyle(bool boolean)
        {
            if (boolean) return " style=\"color: #0c0\"";
            return " style=\"color: #c00\"";
        }

        private bool IsRoutedRequest(HttpRequest request)
        {
            string path = request.AppRelativeCurrentExecutionFilePath;
            if (path != "~/" && (_virtualPathProvider.FileExists(path) || _virtualPathProvider.DirectoryExists(path)))
            {
                return false;
            }
            return true;
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
