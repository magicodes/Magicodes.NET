using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.WebHandler.Resource
{
    /// <summary>
    /// Web资源处理程序
    /// </summary>
    public interface IResourceHandler : IWebHandler
    {
        /// <summary>
        /// 处理Web资源请求
        /// </summary>
        /// <returns></returns>
        WebResourceReturnValueBase ProcessResourceRequest();
    }
}
