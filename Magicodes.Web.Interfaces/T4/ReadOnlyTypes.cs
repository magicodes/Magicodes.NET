using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.T4
{
    /// <summary>
    /// 只读类型
    /// </summary>
    public enum ReadOnlyTypes
    {
        /// <summary>
        /// 所有字段均只读
        /// </summary>
        All = 0,
        /// <summary>
        /// 添加时只读
        /// </summary>
        Add = 1,
        /// <summary>
        /// 编辑时只读
        /// </summary>
        Edit = 2
    }
}
