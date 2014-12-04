using Magicodes.Core.API;
using Magicodes.Services.Default.API.Models;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
using Magicodes.Web.Interfaces.WebHandler.JSON;
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
//        created by 雪雁 at  2014/10/11 11:15:13
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Default.API
{
    [RoutePrefix("api/Account")]
    public class AccountController : WebAPIControllerBase
    {
        // POST /api/Account/Login
        [HttpPost]
        [Route("Login")]
        //[AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #region 设置默认返回JSON
            AuthStatus actionResult = new AuthStatus() { IsSuccess = true, Message = "登录成功！" };
            #endregion
            #region 获取验证策略
            var userStrategy = ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>();
            if (userStrategy == null)
            {
                actionResult = new AuthStatus() { IsSuccess = true, Message = "无法登录，没有找到登录策略！" };
                return Ok(actionResult);
            }
            #endregion
            actionResult = userStrategy.Login(loginViewModel.LoginName, loginViewModel.Password, loginViewModel.RememberMe);
            return Ok(actionResult);
        }

        // /api/Account/LoginOut
        [HttpGet]
        [Route("LoginOut")]
        public void LoginOut()
        {
            var userStrategy = ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>();
            if (userStrategy == null)
            {
                userStrategy.LoginOut();
            }
            HttpContext.Current.Response.Redirect("/");
            //return Redirect("/");
        }
    }
}
