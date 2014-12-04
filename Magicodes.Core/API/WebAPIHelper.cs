using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Magicodes.Utility;
namespace Magicodes.Core.API
{
    public class WebAPIHelper
    {
        /// <summary>
        /// 获取路径参数列表
        /// </summary>
        /// <param name="apiName">API名称</param>
        /// <returns></returns>
        public static string[] GetUrlParams(string apiName)
        {
            var absolutePath = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            var paramsUrl = absolutePath.RightOf("/api/" + apiName).Trim('/');
            if (paramsUrl.Length > 0)
            {
                if (paramsUrl.Contains("?"))
                    paramsUrl = paramsUrl.LeftOf('?');
                return paramsUrl.Split('/');
            }
            return null;
        }
        /// <summary>
        /// 获取Url参数中的Id（默认为第一个）
        /// </summary>
        /// <param name="apiName">API名称</param>
        /// <returns>Id</returns>
        public static string GetUrlId(string apiName)
        {
            var paramsStr = GetUrlParams(apiName);
            if (paramsStr != null && paramsStr.Length > 0)
                return paramsStr[0];
            return null;
        }
        /// <summary>
        /// 获取提交的JSON
        /// </summary>
        /// <returns></returns>
        public static string GetPostJSON()
        {
            return HttpContext.Current.Request.Form["JSON"];
        }
    }
}
