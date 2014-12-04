using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Strategy.Logger
{
    /// <summary>
    /// 日志参数
    /// </summary>
    public class LogArgs : EventArgs
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public LoggerLevels loggerLevels { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public object Message { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }

    }
}
