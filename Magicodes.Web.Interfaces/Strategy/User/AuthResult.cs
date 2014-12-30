using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.User
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// 是否登录成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 登录状态
        /// </summary>
        public LoginStatus Status { get; set; }
    }
}
