using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Magicodes.Web.Interfaces.Strategy.Logger
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public abstract class LoggerStrategyBase : IStrategyBase
    {
        public delegate void LogEventHandler(object sender, LogArgs e);
        /// <summary>
        /// 日志记录事件
        /// </summary>
        public event LogEventHandler OnLog;
        protected void ExcuteOnLog(LoggerLevels loggerLevels, object message, Exception ex)
        {
            if (OnLog == null) return;
            var e = new LogArgs()
            {
                Exception = ex,
                loggerLevels = loggerLevels,
                Message = message
            };
            OnLog(null, e);
        }
        protected void ExcuteOnLog(LoggerLevels loggerLevels, object message)
        {
            if (OnLog == null) return;
            var e = new LogArgs()
            {
                loggerLevels = loggerLevels,
                Message = message
            };
            OnLog(null, e);
        }
        public abstract void Log(LoggerLevels loggerLevels, object message);
        public abstract void Log(LoggerLevels loggerLevels, object message, Exception exception);
        public abstract void LogFormat(LoggerLevels loggerLevels, string format, params object[] args);
        public abstract void LogFormat(LoggerLevels loggerLevels, string format, Exception exception, params object[] args);
        public abstract void LogFormat(LoggerLevels loggerLevels, IFormatProvider formatProvider, string format, params object[] args);
        public abstract void LogFormat(LoggerLevels loggerLevels, IFormatProvider formatProvider, string format, Exception exception
                            , params object[] args);

        public abstract void Initialize();
    }
}
