using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Events
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventsManager
    {
        //定义delegate
        public delegate void ApplicationEventHandler(object sender, ApplicationArgs e);
        /// <summary>
        /// 预初始化
        /// </summary>
        public event ApplicationEventHandler OnApplication_PreInitialize = (o, e) => { };
        /// <summary>
        /// 应用程序类初始化完成事件
        /// </summary>
        public event ApplicationEventHandler OnApplication_InitializeComplete = (o, e) => { };

        /// <summary>
        /// 开始请求事件
        /// </summary>
        public event EventHandler BeginRequest = (o, e) => { };

        /// <summary>
        /// 请求结束事件
        /// </summary>
        public event EventHandler EndRequest = (o, e) => { };
        /// <summary>
        /// 错误事件
        /// </summary>
        public event EventHandler Error = (o, e) => { };
        /// <summary>
        /// 在 ASP.NET 跳过当前事件处理程序的执行并允许缓存模块满足来自缓存的请求时发生
        /// </summary>
        public event EventHandler PostResolveRequestCache = (o, e) => { };

        /// <summary>
        /// 资源压缩预加载
        /// </summary>
        public event EventHandler OnResourceMin_PreLoad = (o, e) => { };

        public void InitApplicationEvents(object sender, ApplicationEvents events)
        {
            var applicationArgs = new ApplicationArgs()
            {
                ApplicationContext = GlobalApplicationObject.Current.ApplicationContext
            };
            switch (events)
            {
                case ApplicationEvents.OnApplication_PreInitialize:
                    OnApplication_PreInitialize(sender, applicationArgs);
                    break;
                case ApplicationEvents.OnApplication_InitializeComplete:
                    OnApplication_InitializeComplete(sender, applicationArgs);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 初始化Http全局事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="events"></param>
        public void InitHttpApplicationEvents(object sender, ApplicationEvents events)
        {
            var applicationArgs = new EventArgs()
            {
                
            };
            switch (events)
            {
                case ApplicationEvents.BeginRequest:
                    BeginRequest(sender, applicationArgs);
                    break;
                case ApplicationEvents.EndRequest:
                    EndRequest(sender, applicationArgs);
                    break;
                case ApplicationEvents.Error:
                    Error(sender, applicationArgs);
                    break;
                case ApplicationEvents.PostResolveRequestCache:
                    PostResolveRequestCache(sender, applicationArgs);
                    break;
                default:
                    break;
            }
        }
        
    }
}
