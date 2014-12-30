using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.User
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public enum LoginStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 账户被锁定
        /// </summary>
        LockedOut = 1,
        /// <summary>
        /// 需要验证
        /// </summary>
        RequiresVerification = 2,
        /// <summary>
        /// 登录失败
        /// </summary>
        Failure = 3,
        /// <summary>
        /// 无效的登录尝试
        /// </summary>
        Orthers = 4
    }
}
