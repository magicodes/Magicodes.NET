using System;
using System.Collections.Generic;

namespace Magicodes.Web.Interfaces.Strategy.User
{
    public interface IUser
    {
        int Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        string LoginName { get; set; }
        /// <summary>
        /// 用户名或昵称
        /// </summary>        
        string UserNickName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        bool Deleted { get; set; }
        /// <summary>
        /// 是否已经激活
        /// </summary>
        bool IsActive { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTimeOffset CreateTime { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 当前主题
        /// </summary>
        string Theme { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        DateTimeOffset? LastLoginTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        string HeadPortrait { get; set; }
    }
}
