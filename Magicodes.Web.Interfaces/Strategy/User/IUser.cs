using System;
using System.Collections.Generic;

namespace Magicodes.Web.Interfaces.Strategy.User
{
    /// <summary>
    /// 用户
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IUser<out TKey>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        TKey Id { get; }
        /// <summary>
        /// 显示名或昵称
        /// </summary>
        string DisplayName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }
    }
}
