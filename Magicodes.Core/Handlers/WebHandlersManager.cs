using Magicodes.Core.Performance.Watch;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.WebHandler;
using Magicodes.Web.Interfaces.WebHandler.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Magicodes.Utility;
using Magicodes.Web.Interfaces.Strategy.Logger;
using Magicodes.Web.Interfaces.API;
using System.ComponentModel;
namespace Magicodes.Core.Handlers
{
    /// <summary>
    /// Web处理程序管理
    /// </summary>
    public class WebHandlersManager
    {
        static readonly LoggerStrategyBase Log = GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
        static List<IWebHandlerInfo> WebHandlerList;
        static WebHandlersManager()
        {
            if (GlobalApplicationObject.Current.ApplicationContext.WebHandlerList == null)
                GlobalApplicationObject.Current.ApplicationContext.WebHandlerList = new List<IWebHandlerInfo>();
            WebHandlerList = GlobalApplicationObject.Current.ApplicationContext.WebHandlerList;
        }
        /// <summary>
        /// 初始化JSONHandler
        /// </summary>
        public static void Initialize()
        {

            foreach (var plus in GlobalApplicationObject.Current.ApplicationContext.PlusAssemblysList)
            {
                var currentAssembly = plus;
                Initialize(currentAssembly.Assembly);
            }
        }
        /// <summary>
        /// 初始化Handler
        /// </summary>
        /// <param name="assembly"></param>
        public static void Initialize(Assembly assembly)
        {
            using (new CodeWatch(string.Format("HandlersManager.Initialize  AssemblyFullName:{0}", assembly.FullName), 1000))
            {
                assembly.GetTypes().Where(p => p.IsClass && p.GetInterface((typeof(IJSONHandler)).FullName) != null).Each(
                    t =>
                    {
                        try
                        {
                            var type = (IJSONHandler)Activator.CreateInstance(t);
                            var jsonHandler = new WebHandlerInfo()
                            {
                                FullName = t.FullName,
                                AssemblyFullName = assembly.FullName,
                                HandlerInstance = t,
                                AssemblyName = assembly.GetName().Name,
                                Name = type.GetType().Name,
                                WebHandlerType = WebHandlerTypes.JSONHandler
                            };
                            //获取描述
                            var descriptionAttr = t.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                            if (descriptionAttr != null) jsonHandler.Description = (descriptionAttr as DescriptionAttribute).Description;
                            WebHandlerList.Add(jsonHandler);
                        }
                        catch (Exception ex)
                        {
                            Log.Log(LoggerLevels.Error, string.Format("设置JSONHandler失败。AssemblyFullName：{0}", t.FullName), ex);
                        }
                    });

                //assembly.GetTypes().Where(p => p.IsClass && p.GetInterface((typeof(IWebAPI)).FullName) != null).Each(
                //    t =>
                //    {
                //        try
                //        {
                //            var type = (IWebAPI)Activator.CreateInstance(t);
                //            var webAPIHandler = new WebHandlerInfo()
                //            {
                //                FullName = t.FullName,
                //                AssemblyFullName = assembly.FullName,
                //                HandlerInstance = t,
                //                AssemblyName = assembly.GetName().Name,
                //                Name = type.GetType().Name,
                //                WebHandlerType = WebHandlerTypes.WebAPIHandler
                //            };
                //            //获取描述
                //            var descriptionAttr = t.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                //            if (descriptionAttr != null) webAPIHandler.Description = (descriptionAttr as DescriptionAttribute).Description;
                //            //获取WebAPI名称
                //            var urlAttr = t.GetCustomAttributes(typeof(APINameAttribute), false).FirstOrDefault();
                //            if (urlAttr != null) webAPIHandler.WebAPIName = (urlAttr as APINameAttribute).Url;
                //            //确保WebAPI名称为小写，并且不能为空
                //            if (webAPIHandler.WebAPIName != null) webAPIHandler.WebAPIName = webAPIHandler.WebAPIName.ToLower();
                //            else webAPIHandler.WebAPIName = t.Name.ToLower();
                //            WebHandlerList.Add(webAPIHandler);
                //        }
                //        catch (Exception ex)
                //        {
                //            Log.Log(LoggerLevels.Error, string.Format("设置WebAPIHandler失败。AssemblyFullName：{0}", t.FullName), ex);
                //        }
                //    });
            }
        }
    }
}
