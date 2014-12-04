using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Routing
{
    /// <summary>
    /// 路由信息
    /// </summary>
    public class RoutingInfo
    {
        /// <summary>
        /// 路由名称
        /// </summary>
        public string routeName { get; set; }
        /// <summary>
        /// 路由的 URL 模式
        /// </summary>
        public string routeUrl { get; set; }
        /// <summary>
        /// 路由的物理 URL
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 一个值，该值指示 ASP.NET 是否应验证用户是否有权访问物理 URL（始终会检查路由 URL）
        /// </summary>
        public bool checkPhysicalUrlAccess { get; set; }
        /// <summary>
        /// 路由参数的默认值
        /// </summary>
        public List<RoutingDictonaryInfo> defaults { get; set; }
        /// <summary>
        /// 一些约束，URL 请求必须满足这些约束才能作为此路由处理
        /// </summary>
        public List<RoutingDictonaryInfo> constraints { get; set; }
        /// <summary>
        /// 与路由关联的值，但这些值不用于确定路由是否匹配 URL 模式
        /// </summary>
        public List<RoutingDictonaryInfo> dataTokens { get; set; }
    }
}
