using Magicodes.Web.Interfaces.Strategy;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :IIdentityStrategy
//        description :
//
//        created by 雪雁 at  2014/10/22 18:36:17
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Strategy.Identity
{
    public interface IIdentityStrategy : IStrategyBase
    {
        // Authorize 操作是当你访问任何
        // 受保护的 Web API 时调用的终结点。如果用户未登录，则将被重定向到
        // Login 页。在成功登录后，你可以调用 Web API。
        void Authorize(IPrincipal User, IAuthenticationManager AuthenticationManager);
        Task<SignInStatus> Login(AppSignInManager SignInManager, string loginName, string password, bool rememberMe, bool shouldLockout);
    }
}
