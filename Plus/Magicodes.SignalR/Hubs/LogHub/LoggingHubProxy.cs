using Magicodes.Web.Interfaces.Strategy.Logger;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :LogHubProxy
//        description :
//
//        created by 雪雁 at  2014/11/19 22:45:44
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.SignalR.Hubs.LogHub
{
    public class LoggingHubProxy
    {
        //private readonly SignalRTarget _target;
        public HubConnection Connection;
        private IHubProxy _proxy;
        public string Uri { get; set; }

        [DefaultValue("LoggingHub")]
        public string HubName { get; set; }

        [DefaultValue("Log")]
        public string MethodName { get; set; }

        public LoggingHubProxy(string uri)
        {
            Uri = uri;
            HubName = "LoggingHub";
            MethodName = "Log";
        }

        public void Log(LoggerLevels loggerLevels, object message, Exception ex)
        {
            EnsureProxyExists();

            if (_proxy != null)
            {
                if (ex != null)
                    _proxy.Invoke(MethodName, loggerLevels, message, ex);
                else
                    _proxy.Invoke(MethodName, loggerLevels, message);
            }
        }

        public void EnsureProxyExists()
        {
            if (_proxy == null || Connection == null)
            {
                BeginNewConnection();
            }
            else if (Connection.State == ConnectionState.Disconnected)
            {
                StartExistingConnection();
            }
        }

        private void BeginNewConnection()
        {
            try
            {
                Connection = new HubConnection(Uri);
                _proxy = Connection.CreateHubProxy(HubName);
                Connection.Start().Wait();

                _proxy.Invoke("Notify", Connection.ConnectionId);
            }
            catch (Exception)
            {
                _proxy = null;
            }
        }

        private void StartExistingConnection()
        {
            try
            {
                Connection.Start().Wait();
            }
            catch (Exception)
            {
                _proxy = null;
            }
        }

    }
}
