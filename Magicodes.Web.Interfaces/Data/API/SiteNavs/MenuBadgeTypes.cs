using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Data.API.SiteNavs
{
    /// <summary>
    /// 菜单右侧的数字类型
    /// </summary>
    public enum MenuBadgeTypes
    {
        /// <summary>
        /// 计算子级菜单数
        /// </summary>
        FromChildrenCount = 0,
        /// <summary>
        /// 从请求中获取数值
        /// </summary>
        FromBadgeRequestUrl = 1,
    }
}
