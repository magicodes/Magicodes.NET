using Magicodes.Web.Interfaces.Strategy;
using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :IdentityStrategy
//        description :
//
//        created by 雪雁 at  2014/10/22 17:14:27
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Strategy.Identity
{
    public class IdentityStrategy : IIdentityStrategy
    {
        // Authorize 操作是当你访问任何
        // 受保护的 Web API 时调用的终结点。如果用户未登录，则将被重定向到
        // Login 页。在成功登录后，你可以调用 Web API。
        public void Authorize(IPrincipal User, IAuthenticationManager AuthenticationManager)
        {
            var claims = new ClaimsPrincipal(User).Claims.ToArray();
            var identity = new ClaimsIdentity(claims, "Bearer");
            AuthenticationManager.SignIn(identity);
        }
        public async Task<SignInStatus> Login(AppSignInManager SignInManager, string loginName, string password, bool rememberMe, bool shouldLockout)
        {
            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(loginName, password, rememberMe, shouldLockout);
            return result;
        }
        public void Initialize()
        {
            
        }
    }
}
