using Magicodes.Web.Interfaces.Strategy.Logger;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :LogHub
//        description :
//
//        created by 雪雁 at  2014/11/19 22:37:44
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.SignalR.Hubs.LogHub
{
    public class LoggingHub : Hub<LoggerStrategyBase>
    {
        public void Log(LoggerLevels loggerLevels, object message)
        {
            Clients.Others.Log(loggerLevels, message);
        }
        public void Log(LoggerLevels loggerLevels, object message, Exception exception)
        {
            Clients.Others.Log(loggerLevels, message, exception);
        }
        //public  void LogFormat(LoggerLevels loggerLevels, string format, params object[] args);
        //public  void LogFormat(LoggerLevels loggerLevels, string format, Exception exception, params object[] args);
        //public  void LogFormat(LoggerLevels loggerLevels, IFormatProvider formatProvider, string format, params object[] args);
        //public  void LogFormat(LoggerLevels loggerLevels, IFormatProvider formatProvider, string format, Exception exception
        //                    , params object[] args);
    }
}
