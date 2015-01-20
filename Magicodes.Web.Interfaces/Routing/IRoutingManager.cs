using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Magicodes.Web.Interfaces.Routing
{
    public interface IRoutingManager
    {
        /// <summary>
        /// 添加路由
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeUrl">路由的 URL 模式。</param>
        /// <param name="url">路由的物理 URL。</param>
        IRoutingManager Add(string routeName, string routeUrl, string url);
        /// <summary>
        /// 添加路由
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeUrl">路由的 URL 模式。</param>
        /// <param name="url">路由的物理 URL。</param>
        /// <param name="checkPhysicalUrlAccess">一个值，该值指示 ASP.NET 是否应验证用户是否有权访问物理 URL（始终会检查路由 URL）。</param>
        IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess);
        /// <summary>
        /// 添加路由
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeUrl">路由的 URL 模式。</param>
        /// <param name="url">路由的物理 URL。</param>
        /// <param name="checkPhysicalUrlAccess">一个值，该值指示 ASP.NET 是否应验证用户是否有权访问物理 URL（始终会检查路由 URL）。</param>
        /// <param name="defaults">路由参数的默认值</param>
        IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess, List<RoutingDictonaryInfo> defaults);
        /// <summary>
        /// 添加路由
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeUrl">路由的 URL 模式。</param>
        /// <param name="url">路由的物理 URL。</param>
        /// <param name="checkPhysicalUrlAccess">一个值，该值指示 ASP.NET 是否应验证用户是否有权访问物理 URL（始终会检查路由 URL）。</param>
        /// <param name="defaults">路由参数的默认值</paparam>
        /// <param name="constraints">一些约束，URL 请求必须满足这些约束才能作为此路由处理。</param>
        IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess, List<RoutingDictonaryInfo> defaults, List<RoutingDictonaryInfo> constraints);
        /// <summary>
        /// 添加路由
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeUrl">路由的 URL 模式。</param>
        /// <param name="url">路由的物理 URL。</param>
        /// <param name="checkPhysicalUrlAccess">一个值，该值指示 ASP.NET 是否应验证用户是否有权访问物理 URL（始终会检查路由 URL）。</param>
        /// <param name="defaults">路由参数的默认值</paparam>
        /// <param name="constraints">一些约束，URL 请求必须满足这些约束才能作为此路由处理。</param>
        /// <paparam name="dataTokens">与路由关联的值，但这些值不用于确定路由是否匹配 URL 模式。</paparam>
        IRoutingManager Add(string routeName, string routeUrl, string url, bool checkPhysicalUrlAccess, List<RoutingDictonaryInfo> defaults, List<RoutingDictonaryInfo> constraints, List<RoutingDictonaryInfo> dataTokens);
        /// <summary>
        /// 定义一个 URL 模式，此模式在请求 URL 满足指定约束的情况下不需要检查 URL 是否与路由匹配。
        /// </summary>
        /// <param name="url">要忽略的 URL 模式。</param>
        IRoutingManager Ignore(string url);
        /// <summary>
        /// 定义一个 URL 模式，此模式在请求 URL 满足指定约束的情况下不需要检查 URL 是否与路由匹配。
        /// </summary>
        /// <param name="url">要忽略的 URL 模式。</param>
        /// <param name="constraints"> 附加条件，用于确定是否忽略匹配 URL 模式的请求。</param>
        IRoutingManager Ignore(string url, object constraints);
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();
    }
}
