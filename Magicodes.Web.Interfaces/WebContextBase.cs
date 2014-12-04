using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Magicodes.Web.Interfaces
{
    /// <summary>
    /// Web上下文对象
    /// </summary>
    public class WebContextBase
    {
        public RequestContext ReqeustContext { get; set; }
        /// <summary>
        /// 获取当前请求附带的路由参数的集合
        /// </summary>
        public RouteValueDictionary RouteValues
        {
            get
            {
                if (ReqeustContext != null)
                    return ReqeustContext.RouteData.Values;
                return null;
            }
        }
        /// <summary>
        /// 获取指定名称的路由地址参数的值
        /// </summary>
        /// <param name="key">名称</param>
        /// <returns></returns>
        public object GetRouteValue(string key)
        {
            object resultValue = null;
            if (RouteValues != null && RouteValues.Count > 0)
                RouteValues.TryGetValue(key, out resultValue);
            return resultValue;
        }

        /// <summary>
        /// 当前Http上下文对象
        /// </summary>
        public HttpContext HttpContext { get { return HttpContext.Current; } }
        /// <summary>
        /// 当前应用程序上下文对象
        /// </summary>
        public ApplicationContextBase ApplicationContext { get { return GlobalApplicationObject.Current.ApplicationContext; } }
        /// <summary>
        /// 当前用户凭据
        /// </summary>
        public IUser CurrentUser
        {
            get
            {
                return GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>().GetCurrentLoginUser();
            }
        }
        /// <summary>
        /// 获取一个值，该值指示是否验证了用户。
        /// </summary>
        public virtual bool IsAuthenticated
        {
            get
            {
                return GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>().IsAuthenticated;
            }
        }
    }
}
