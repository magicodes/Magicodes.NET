using Magicodes.Web.Interfaces.WebHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Routing
{
    public class RouteWebHandlerInfo
    {
        public string routeName { get; set; }
        public string routeUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IWebHandlerInfo WebHandlerInfo { get; set; }
    }
}
