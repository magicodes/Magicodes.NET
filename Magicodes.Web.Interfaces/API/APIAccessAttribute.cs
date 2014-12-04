using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.API
{
    /// <summary>
    /// API访问特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class APIAccessAttribute : Attribute
    {
        /// <summary>
        /// 标识（可以设置角色名或者其他权限标识）
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 使用默认的构造函数表示至少需要登录才能访问
        /// </summary>
        public APIAccessAttribute()
        {

        }
        public APIAccessAttribute(string identity)
        {
            this.Identity = identity;
        }
    }
}
