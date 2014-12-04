using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Strategy.Logger
{
    public class LoggerHelper
    {
        /// <summary>
        /// 获取默认的日志策略
        /// </summary>
        /// <returns></returns>
        public static LoggerStrategyBase GetDefaultLogStrategy()
        {
            return GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
        }
    }
}
