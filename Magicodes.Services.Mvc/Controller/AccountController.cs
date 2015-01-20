using Magicodes.Core.API;
using Magicodes.Core.Web.Controllers;
using Magicodes.Models.Mvc.Models.Account;
using Magicodes.Services.Mvc.ViewModels;
using Magicodes.Strategy.Identity;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :AccountController
//        description :
//
//        created by 雪雁 at  2014/10/22 18:27:57
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    [RoutePrefix("api/Account")]
    public class AccountController : WebAPIControllerBase
    {
        private AppSignInManager _signInManager;

        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<AppSignInManager>();
            }
            private set { _signInManager = value; }
        }
        private AppUserManager _userManager;
        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // POST /api/Account/Login
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            #region 设置默认返回JSON
            JSONActionResult actionResult = new JSONActionResult() { IsSuccess = true, Message = "登录成功！" };
            #endregion

            //if (user != null && !await UserManager.IsEmailConfirmedAsync(user.Id))
            //{
            //    ModelState.AddModelError("RequiresVerification", "您的账号需要验证，请验证后再登陆。");
            //    return BadRequest(ModelState);
            //}
            // 若要在多次输入错误密码的情况下触发帐户锁定，需要设置 shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(loginViewModel.LoginName, loginViewModel.Password, loginViewModel.RememberMe, shouldLockout: true);

            //var result = await IdentityStrategy.Login(SignInManager, loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return Ok(actionResult);
                case SignInStatus.LockedOut:
                    {
                        ModelState.AddModelError("LockedOut", "由于登录错误次数较多，为了您的账号安全，您的账号已被锁定半小时。");
                        return BadRequest(ModelState);
                    }
                case SignInStatus.RequiresVerification:
                    {
                        var userId = await SignInManager.GetVerifiedUserIdAsync();
                        if (userId == null)
                        {
                            ModelState.AddModelError("RequiresVerification", "意外错误，找不到当前登录用户。");
                            return BadRequest(ModelState);
                        }
                        ModelState.AddModelError("RequiresVerification", "您的账号需要验证，请验证后再登陆。");
                        return BadRequest(ModelState);
                    }
                case SignInStatus.Failure:
                    ModelState.AddModelError("Failure", "登录失败，用户名或密码不正确。");
                    return BadRequest(ModelState);
            }
            ModelState.AddModelError("", "无效的登录尝试。");
            return BadRequest(ModelState);
        }

        // /api/Account/LoginOut
        [HttpGet]
        [Route("LoginOut")]
        public void LoginOut()
        {
            var userStrategy = ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy<string>>();
            if (userStrategy == null)
            {
                userStrategy.LoginOut();
            }
            HttpContext.Current.Response.Redirect("/");
            //return Redirect("/");
        }
    }
}
