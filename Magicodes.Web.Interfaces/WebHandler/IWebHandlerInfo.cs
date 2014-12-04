using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.WebHandler
{
    public interface IWebHandlerInfo
    {
        string FullName { get; set; }
        string AssemblyFullName { get; set; }
        string AssemblyName { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        dynamic HandlerInstance { get; set; }
        WebHandlerTypes WebHandlerType { get; set; }
        string WebAPIName { get; set; }
    }
}
