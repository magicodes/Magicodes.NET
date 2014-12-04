using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.WebHandler
{
    /// <summary>
    /// 处理程序类型
    /// </summary>
    public enum WebHandlerTypes
    {
        /// <summary>
        /// JSON处理程序
        /// </summary>
        JSONHandler,
        /// <summary>
        /// 资源处理程序
        /// </summary>
        ResourceHandler,
        /// <summary>
        /// WebAPI处理程序
        /// </summary>
        WebAPIHandler
    }
}
