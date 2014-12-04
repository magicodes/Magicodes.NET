using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Magicodes.Core
{
    /// <summary>
    /// 框架总初始化类
    /// </summary>
    public class Starter
    {
        /// <summary>
        /// 初始化插件
        /// </summary>
        public static void Start()
        {
            //设置全局Context对象
            GlobalApplicationObject.Current.ApplicationContext = new ApplicationContext();
            
            GlobalApplicationObject.Current.ApplicationContext.PreApplicationStartInitialize();
            //执行预初始化事件
            GlobalApplicationObject.Current.EventsManager.InitApplicationEvents(null, ApplicationEvents.OnApplication_PreInitialize);
        }
    }
}
