using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.API
{
    /// <summary>
    /// API名称特性，例如[API("v1/MyApi")]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class APINameAttribute : Attribute
    {
        /// <summary>
        /// API名称
        /// </summary>
        public string Url { get; set; }
        public APINameAttribute(string url)
        {
            this.Url = url;
        }
    }
}
