using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.Logger
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LoggerLevels
    {
        /// <summary>
        /// 跟踪
        /// </summary>
        Trace, 
        /// <summary>
        /// 调试
        /// </summary>
        Debug, 
        /// <summary>
        /// 信息
        /// </summary>
        Info, 
        /// <summary>
        /// 警告
        /// </summary>
        Warn, 
        /// <summary>
        /// 错误
        /// </summary>
        Error, 
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal,
        /// <summary>
        /// 性能警告
        /// </summary>
        PerformanceWarn,
        /// <summary>
        /// SQL跟踪日志
        /// </summary>
        SqlTrace
    }
}
