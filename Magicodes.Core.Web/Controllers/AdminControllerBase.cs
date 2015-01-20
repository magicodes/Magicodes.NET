using Magicodes.Core.Web.Security;
using Magicodes.Core.Web.Utility;
using Magicodes.Web.Interfaces.Config.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :AdminControllerBase
//        description :
//
//        created by 雪雁 at  2014/10/28 16:43:03
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Controllers
{
    /// <summary>
    /// 后台控制器基础类
    /// </summary>

    public class AdminControllerBase : PlusControllerBase
    {
        /// <summary>
        /// 当前用户角色
        /// </summary>
        public string[] UserRoles
        {
            get
            {
                return AuthHelper.GetUserRoles();
                //if (User.Identity is ClaimsIdentity)
                //{
                //    return ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
                //}
                //else
                //{
                //    return Roles.GetRolesForUser();
                //}
            }
        }
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthHelper.IsAllowEnterAdminPlatform)
            {

                throw new HttpException(401, "您没有权限进行此操作！");
            }
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext">当前请求</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            string message;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (exception is HttpAntiForgeryException)
                {
                   
                }
                //filterContext.Result = Json(new AjaxResult(message, AjaxResultType.Error));
                //filterContext.ExceptionHandled = true;
            }
            else
            {

                var error = new HandleErrorInfo(
                    exception,
                    filterContext.RouteData.Values["controller"].ToString(),
                    filterContext.RouteData.Values["action"].ToString());

                filterContext.Result = View("Error", error);
                filterContext.ExceptionHandled = true;
            }
        }

    }
}
