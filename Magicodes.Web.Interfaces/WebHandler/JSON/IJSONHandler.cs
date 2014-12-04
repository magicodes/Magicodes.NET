using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.WebHandler.JSON
{
    public interface IJSONHandler : IWebHandler
    {
        /// <summary>
        /// 处理JSON请求
        /// </summary>
        /// <returns></returns>
        JSONDataReturnValueBase ProcessJSONRequest(WebContextBase WebContext);
    }
}
