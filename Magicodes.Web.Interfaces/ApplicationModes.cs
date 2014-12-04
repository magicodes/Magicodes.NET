using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces
{
    /// <summary>
    /// 应用程序模式
    /// </summary>
    public enum ApplicationModes
    {
        /// <summary>
        /// 跟踪模式
        /// </summary>
        Trace,
        /// <summary>
        /// 调试模式
        /// </summary>
        Debug,
        /// <summary>
        /// 发布模式
        /// </summary>
        Release
    }
}
