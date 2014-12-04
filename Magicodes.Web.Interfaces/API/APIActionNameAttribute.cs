using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.API
{
    /// <summary>
    /// API方法特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class APIActionNameAttribute : Attribute
    {
        /// <summary>
        /// API名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// HTTP方法
        /// </summary>
        public HttpActions HttpAction { get; set; }
        public APIActionNameAttribute(string name, HttpActions httpAction)
        {
            this.Name = name;
            this.HttpAction = httpAction;
        }
    }
}
