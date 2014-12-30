using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.User
{
    /// <summary>
    /// 用户验证接口
    /// </summary>
    public interface IUserAuthenticationStrategy<out TKey> : IStrategyBase
    {
        /// <summary>
        /// 是否已验证
        /// </summary>
        bool IsAuthenticated { get; }
        /// <summary>
        /// 退出
        /// </summary>
        void LoginOut();
        /// <summary>
        /// 这里编写验证逻辑
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="isRememberPassword">记住密码</param>
        /// <returns></returns>
        AuthResult Login(string loginName, string password, bool isRememberPassword);
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        IUser<TKey> GetUser();
    }
}
