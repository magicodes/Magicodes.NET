using Magicodes.Web.Interfaces.WebHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Handlers
{
    public class WebHandlerInfo : IWebHandlerInfo
    {
        public string FullName { get; set; }
        public string AssemblyFullName { get; set; }

        public string AssemblyName { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public dynamic HandlerInstance { get; set; }

        public WebHandlerTypes WebHandlerType { get; set; }
        public string WebAPIName { get; set; }
    }
}
