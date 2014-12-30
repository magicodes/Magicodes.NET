using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
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
//        filename :PrincipalExtensions
//        description :
//
//        created by 雪雁 at  2014/12/30 12:53:44
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Utility
{
    /// <summary>
    /// 用户对象扩展
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <typeparam name="TKey">用户主键类型</typeparam>
        /// <param name="identity">用户对象</param>
        /// <returns>当前登录用户</returns>
        public static IUser<TKey> GetUser<TKey>(this IIdentity identity)
        {
            var strategy = GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy<TKey>>();
            if (strategy == null) return null;
            return strategy.GetUser();
        }
        /// <summary>
        /// 获取显示名
        /// </summary>
        /// <typeparam name="TKey">用户主键类型</typeparam>
        /// <param name="identity">用户对象</param>
        /// <returns>当前昵称或显示名</returns>
        public static string GetUserDisplayName<TKey>(this IIdentity identity)
        {
            var user = identity.GetUser<TKey>();
            if (user != null)
                return user.DisplayName;
            return null;
        }
    }
}
