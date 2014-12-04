using Magicodes.Core.Performance.Watch;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.WebHandler.JSON;
using System;
using System.Web;
using Magicodes.Utility;
using Magicodes.Web.Interfaces.WebHandler;
using System.Linq;
using Magicodes.Core.Res;
using Magicodes.Web.Interfaces.Strategy.Logger;
using Magicodes.Web.Interfaces.Routing;
using Magicodes.Web.Interfaces.API;
using Magicodes.Core.API;
using Magicodes.Web.Interfaces.Operation;
namespace Magicodes.Core.Handlers
{
    public class WebHandlersProcess : IHttpHandler
    {
        readonly LoggerStrategyBase _log = GlobalApplicationObject.Current.ApplicationContext.StrategyManager.GetDefaultStrategy<LoggerStrategyBase>();
        /// <summary>
        /// 处理程序类型
        /// </summary>
        public WebHandlerTypes HandlerType { get; set; }
        public WebContextBase WebContext { get; set; }
        public RouteWebHandlerInfo RouteWebHandlerInfo { get; set; }
        #region IHttpHandler Members
        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (RouteWebHandlerInfo.IsNotNull())
            {
                switch (HandlerType)
                {
                    case WebHandlerTypes.JSONHandler:
                        {
                            ProcessHander(context, RouteWebHandlerInfo.WebHandlerInfo);
                        }
                        break;
                    case WebHandlerTypes.ResourceHandler:
                        {
                            var res = new ResourceManager(context);
                            res.SendResourceContent();
                            break;
                        }
                    case WebHandlerTypes.WebAPIHandler:
                        {
                            ProcessHander(context, RouteWebHandlerInfo.WebHandlerInfo);
                            break;
                        }
                    default:
                        break;
                }
                return;
            }


            ////程序集名称
            //var assemblyName = WebContext.RouteValues["assemblyName"].ToString();
            ////对应Name
            //var handlerName = WebContext.RouteValues["handlerName"].ToString();
            switch (HandlerType)
            {
                case WebHandlerTypes.JSONHandler:
                    #region 处理JSON
                    {
                        var mainUrls = context.Request.Url.AbsolutePath.ToLower().Trim('/').Split('/');
                        //程序集名称
                        var assemblyName = mainUrls[1];
                        //对应Name
                        var handlerName = mainUrls[2];
                        //根据程序集名称以及Name匹配Handler
                        var jsonHandler = GlobalApplicationObject.Current.ApplicationContext.WebHandlerList
                            .FirstOrDefault(p =>
                                p.WebHandlerType == WebHandlerTypes.JSONHandler &&
                                p.AssemblyName.Equals(assemblyName, StringComparison.CurrentCultureIgnoreCase) &&
                                p.Name.Equals(handlerName, StringComparison.CurrentCultureIgnoreCase));
                        if (jsonHandler != null)
                        {
                            ProcessHander(context, jsonHandler);
                        }
                    }
                    break;
                    #endregion
                case WebHandlerTypes.ResourceHandler:
                    {
                        var res = new ResourceManager(context);
                        res.SendResourceContent();
                    }
                    break;
                case WebHandlerTypes.WebAPIHandler:
                    {
                        var mainUrls = context.Request.Url.AbsolutePath.ToLower().Trim('/');
                        //根据程序集名称以及Name匹配Handler
                        var handlers = GlobalApplicationObject.Current.ApplicationContext.WebHandlerList
                            .Where(p =>
                                p.WebHandlerType == WebHandlerTypes.WebAPIHandler &&
                                mainUrls.StartsWith("api/" + p.WebAPIName));
                        var count = handlers.Count();
                        if (count == 1)
                            ProcessHander(context, handlers.First());
                        else if (count > 1)
                        {
                            mainUrls = mainUrls.RightOf("api/");
                            var urlParams = mainUrls.Split('/');
                            //TODO:应该改为按照位置0开始的相邻的字符串的相似度比较
                            //比较相似度，取相似度最高的
                            var webAPIHandler = handlers.Select(p => new { webApi = p, SimilarityCount = p.WebAPIName.Split('/').Intersect(urlParams).Count() })
                                .OrderByDescending(p => p.SimilarityCount)
                                .First().webApi;
                            ProcessHander(context, webAPIHandler);
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void ProcessHander(HttpContext context, IWebHandlerInfo webHandlerInfo)
        {
            using (var codeWatch = new CodeWatch("IJsonHandler ProcessRequest", 2000,
                    new Action<string, LoggerStrategyBase, int?, long>(
                        (tag, currentLog, wcount, execms) =>
                            currentLog.LogFormat(LoggerLevels.Warn, "\t{0}:JSONHandler({3})执行时间为({1})ms.已超过阀值（{2}）ms.",
                            tag,
                            execms,
                            wcount,
                            context.Request.Url.AbsolutePath))))
            {
                if (WebContext == null) WebContext = new WebContext();
                switch (webHandlerInfo.WebHandlerType)
                {
                    case WebHandlerTypes.JSONHandler:
                        {
                            var handler = (IJSONHandler)Activator.CreateInstance(webHandlerInfo.HandlerInstance);
                            var jsonReturn = handler.ProcessJSONRequest(WebContext);
                            if (jsonReturn != null && jsonReturn.IsResponseJsonObject)
                            {
                                context.Response.ContentType = "application/json";
                                //输出JSON对象
                                context.Response.Write(jsonReturn.HasSetJsonObject
                                    ? jsonReturn.JsonObject.ToJson(jsonReturn.JSONCommonFormatHanding)
                                    : new { IsSuccess = true }.ToJson(jsonReturn.JSONCommonFormatHanding));
                            }
                        }
                        break;
                    case WebHandlerTypes.ResourceHandler:
                        break;
                    case WebHandlerTypes.WebAPIHandler:
                        {
                            context.Response.ContentType = "application/json";
                            var result = new OperationResult()
                                {
                                    ResultType = OperationResultType.Success,
                                    Message = "操作成功！"
                                };
                            var handler = (IWebAPI)Activator.CreateInstance(webHandlerInfo.HandlerInstance);
                            handler.OperationResult = result;
                            try
                            {
                                switch (WebContext.HttpContext.Request.HttpMethod)
                                {
                                    case "GET":
                                        {
                                            var paramStrs = WebAPIHelper.GetUrlParams(webHandlerInfo.WebAPIName);
                                            if (paramStrs != null && paramStrs.Length > 0)
                                            {
                                                if (paramStrs.Length > 1)
                                                    result = handler.Get(paramStrs);
                                                else
                                                    result = handler.Get(paramStrs[0]);
                                            }
                                            else
                                            {
                                                result = handler.Get(paramStrs);
                                            }
                                            //输出JSON对象
                                            context.Response.Write(result.Data.ToJsonWithDateFormatyyyyMMddHHmmss());
                                            return;
                                        }
                                    case "POST":
                                        {
                                            result = handler.Post(WebAPIHelper.GetPostJSON());
                                            break;
                                        }
                                    case "PUT":
                                        {
                                            var id = WebAPIHelper.GetUrlId(webHandlerInfo.WebAPIName);
                                            if (id.IsEmpty())
                                            {
                                                result.ResultType = OperationResultType.ParamError;
                                                result.Message = "参数错误！";
                                                result.LogMessage = "Id不能为空！";
                                            }
                                            result = handler.Put(id, WebAPIHelper.GetPostJSON());
                                            break;
                                        }
                                    case "DELETE":
                                        {
                                            var id = WebAPIHelper.GetUrlId(webHandlerInfo.WebAPIName);
                                            if (id.IsEmpty())
                                            {
                                                result.ResultType = OperationResultType.ParamError;
                                                result.Message = "参数错误！";
                                                result.LogMessage = "Id不能为空！";
                                            }
                                            result = handler.Delete(id);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                            catch (NotImplementedException ex)
                            {
                                result.ResultType = OperationResultType.NoImplemented;
                                result.Message = "该接口尚未实现！";
                                result.LogMessage = "该接口尚未实现！";
                            }
                            catch (Exception ex)
                            {
                                result.ResultType = OperationResultType.Error;
                                result.Message = "意外错误，请联系管理员！";
                            }

                            //输出JSON对象
                            context.Response.Write(result.ToJsonWithDateFormatyyyyMMddHHmmss());
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        #endregion
    }
}
