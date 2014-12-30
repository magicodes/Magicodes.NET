using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :UserStrategy
//        description :
//
//        created by 雪雁 at  2014/12/30 9:51:41
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Strategy.Identity
{
    public class IdentityStrategy : IUserAuthenticationStrategy<string>
    {
        /// <summary>
        /// Http上下文对象
        /// </summary>
        public HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }
        /// <summary>
        /// HTTP上下文对象基类
        /// </summary>
        public HttpContextBase ContextBase
        {
            get
            {
                return new HttpContextWrapper(Context);
            }
        }

        private AppUserManager _userManager;
        /// <summary>
        /// 用户管理器
        /// </summary>
        public AppUserManager UserManager
        {
            get
            {
                if (_userManager != null) return _userManager;
                return ContextBase.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private AppSignInManager _signInManager;
        /// <summary>
        /// 登录管理器
        /// </summary>
        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? ContextBase.GetOwinContext().Get<AppSignInManager>();
            }
            private set { _signInManager = value; }
        }
        /// <summary>
        /// 验证管理器
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return ContextBase.GetOwinContext().Authentication;
            }
        }
        /// <summary>
        /// 是否已经验证
        /// </summary>
        public bool IsAuthenticated
        {
            get { return Context.Request.IsAuthenticated; }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public void LoginOut()
        {
            AuthenticationManager.SignOut();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="isRememberPassword"></param>
        /// <returns></returns>
        public AuthResult Login(string loginName, string password, bool isRememberPassword)
        {
            var status = new AuthResult()
            {
                IsSuccess = false
            };
            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 在多次输入错误密码的情况下触发帐户锁定 shouldLockout: true
            var result = SignInManager.PasswordSignIn(loginName, password, isRememberPassword, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    status.IsSuccess = true;
                    status.Status = LoginStatus.Success;
                    break;
                case SignInStatus.LockedOut:
                    status.Message = "当前账户已被锁定！";
                    status.Status = LoginStatus.LockedOut;
                    break;
                case SignInStatus.RequiresVerification:
                    status.Message = "当前账户需要验证！";
                    status.Status = LoginStatus.RequiresVerification;
                    break;
                case SignInStatus.Failure:
                    status.Message = "您输入的用户名或密码不对，请重新输入！";
                    status.Status = LoginStatus.Failure;
                    break;
                default:
                    status.Message = "无效的登录尝试。";
                    status.Status = LoginStatus.Orthers;
                    break;
            }
            return status;
        }
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public Magicodes.Web.Interfaces.Strategy.User.IUser<string> GetUser()
        {
            var user = UserManager.FindById(Context.User.Identity.GetUserId());
            return user as Magicodes.Web.Interfaces.Strategy.User.IUser<string>;
        }
        public void Initialize()
        {

        }
    }
}
