using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.Logger;
using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :ControllerBase
//        description :
//
//        created by 雪雁 at  2014/10/19 18:18:18
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Controllers
{
    public class PlusControllerBase : Controller
    {
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
                return ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>().GetCurrentLoginUser();
            }
        }
        /// <summary>
        /// 获取一个值，该值指示是否验证了用户。
        /// </summary>
        public virtual bool IsAuthenticated
        {
            get
            {
                return ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>().IsAuthenticated;
            }
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
        protected override void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //可以在此更新 更新在线用户，更新PV统计
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //记录异常信息
            ApplicationContext.ApplicationLog.Log(LoggerLevels.Error, filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}
