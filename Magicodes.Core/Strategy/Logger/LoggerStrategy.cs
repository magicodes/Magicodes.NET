using Common.Logging;
using Common.Logging.Configuration;
using Magicodes.Web.Interfaces.Strategy.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Strategy.Logger
{
    /// <summary>
    /// 日志策略
    /// </summary>
    public class LoggerStrategy : LoggerStrategyBase
    {
        ILog log;
        private bool hasInitialize = false;
        public LoggerStrategy()
        {
            InitLog();
        }
        
        public override void Log(LoggerLevels loggerLevels, object message)
        {
            try
            {
                switch (loggerLevels)
                {
                    case LoggerLevels.Trace:
                        log.Trace(message);
                        break;
                    case LoggerLevels.Debug:
                        log.Debug(message);
                        break;
                    case LoggerLevels.Info:
                        log.Info(message);
                        break;
                    case LoggerLevels.Warn:
                        log.Warn(message);
                        break;
                    case LoggerLevels.Error:
                        log.Error(message);
                        break;
                    case LoggerLevels.Fatal:
                        log.Fatal(message);
                        break;
                    //性能警告
                    //TODO:需要记录性能警报信息
                    case LoggerLevels.PerformanceWarn:
                        log.Warn(message);
                        break;
                    default:
                        break;
                }
                ExcuteOnLog(loggerLevels, message);
            }
            catch (Exception)
            {
                
            }
        }

        public override void Log(LoggerLevels loggerLevels, object message, Exception exception)
        {
            try
            {
                switch (loggerLevels)
                {
                    case LoggerLevels.Trace:
                        log.Trace(message, exception);
                        break;
                    case LoggerLevels.Debug:
                        log.Debug(message, exception);
                        break;
                    case LoggerLevels.Info:
                        log.Info(message, exception);
                        break;
                    case LoggerLevels.Warn:
                        log.Warn(message, exception);
                        break;
                    case LoggerLevels.Error:
                        log.Error(message, exception);
                        break;
                    case LoggerLevels.Fatal:
                        log.Fatal(message, exception);
                        break;
                    default:
                        break;
                }
                ExcuteOnLog(loggerLevels, message, exception);
            }
            catch (Exception)
            {
            }
        }


        public override void LogFormat(LoggerLevels loggerLevels, string format, params object[] args)
        {
            try
            {
                switch (loggerLevels)
                {
                    case LoggerLevels.Trace:
                        log.TraceFormat(format, args);
                        break;
                    case LoggerLevels.Debug:
                        log.DebugFormat(format, args);
                        break;
                    case LoggerLevels.Info:
                        log.InfoFormat(format, args);
                        break;
                    case LoggerLevels.Warn:
                        log.WarnFormat(format, args);
                        break;
                    case LoggerLevels.Error:
                        log.ErrorFormat(format, args);
                        break;
                    case LoggerLevels.Fatal:
                        log.FatalFormat(format, args);
                        break;
                    default:
                        break;
                }
                ExcuteOnLog(loggerLevels, string.Format(format, args));
            }
            catch (Exception)
            {
            }
        }

        public override void LogFormat(LoggerLevels loggerLevels, string format, Exception exception, params object[] args)
        {
            try
            {
                switch (loggerLevels)
                {
                    case LoggerLevels.Trace:
                        log.TraceFormat(format, exception, args);
                        break;
                    case LoggerLevels.Debug:
                        log.DebugFormat(format, exception, args);
                        break;
                    case LoggerLevels.Info:
                        log.InfoFormat(format, exception, args);
                        break;
                    case LoggerLevels.Warn:
                        log.WarnFormat(format, exception, args);
                        break;
                    case LoggerLevels.Error:
                        log.ErrorFormat(format, exception, args);
                        break;
                    case LoggerLevels.Fatal:
                        log.FatalFormat(format, exception, args);
                        break;
                    default:
                        break;
                }
                ExcuteOnLog(loggerLevels, string.Format(format, args), exception);
            }
            catch (Exception)
            {
            }
        }

        public override void LogFormat(LoggerLevels loggerLevels, IFormatProvider formatProvider, string format, params object[] args)
        {
            try
            {
                switch (loggerLevels)
                {
                    case LoggerLevels.Trace:
                        log.TraceFormat(formatProvider, format, args);
                        break;
                    case LoggerLevels.Debug:
                        log.DebugFormat(formatProvider, format, args);
                        break;
                    case LoggerLevels.Info:
                        log.InfoFormat(formatProvider, format, args);
                        break;
                    case LoggerLevels.Warn:
                        log.WarnFormat(formatProvider, format, args);
                        break;
                    case LoggerLevels.Error:
                        log.ErrorFormat(formatProvider, format, args);
                        break;
                    case LoggerLevels.Fatal:
                        log.FatalFormat(formatProvider, format, args);
                        break;
                    default:
                        break;
                }
                ExcuteOnLog(loggerLevels, string.Format(formatProvider, format, args));
            }
            catch (Exception)
            {
            }
        }

        public override void LogFormat(LoggerLevels loggerLevels, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            try
            {
                switch (loggerLevels)
                {
                    case LoggerLevels.Trace:
                        log.TraceFormat(formatProvider, format, exception, args);
                        break;
                    case LoggerLevels.Debug:
                        log.DebugFormat(formatProvider, format, exception, args);
                        break;
                    case LoggerLevels.Info:
                        log.InfoFormat(formatProvider, format, exception, args);
                        break;
                    case LoggerLevels.Warn:
                        log.WarnFormat(formatProvider, format, exception, args);
                        break;
                    case LoggerLevels.Error:
                        log.ErrorFormat(formatProvider, format, exception, args);
                        break;
                    case LoggerLevels.Fatal:
                        log.FatalFormat(formatProvider, format, exception, args);
                        break;
                    default:
                        break;
                }
                ExcuteOnLog(loggerLevels, string.Format(formatProvider, format, args), exception);
            }
            catch (Exception)
            {
            }
        }
        
        /// <summary>
        /// 初始化设置配置文件地址
        /// </summary>
        public override void Initialize()
        {
            InitLog();
        }

        private void InitLog()
        {
            if (hasInitialize) return;
            var properties = new NameValueCollection();
            properties["configType"] = "FILE";
            properties["configFile"] = "~/App_Data/config/NLog.config";
            LogManager.Adapter = new Common.Logging.NLog.NLogLoggerFactoryAdapter(properties);
            log = LogManager.GetLogger("Magicodes.Core.Strategy.Logger");
            hasInitialize = true;
        }
    }
}
