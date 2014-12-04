using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.User
{
    /// <summary>
    /// 用户验证接口
    /// </summary>
    public interface IUserAuthenticationStrategy : IStrategyBase
    {
        /// <summary>
        /// 是否已验证
        /// </summary>
        bool IsAuthenticated { get; }
        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        AuthStatus IsCorrectLoginName(string userName);

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        AuthStatus IsCorrectPassword(string password);

        /// <summary>
        /// 获取密码（这里编写加密逻辑）
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string GetPasword(string password);
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
        AuthStatus Login(string loginName, string password, bool isRememberPassword);
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        IUser GetCurrentLoginUser();
        /// <summary>
        /// 跳转到登陆页
        /// </summary>
        void RedirectToLoginPage();
    }
}
