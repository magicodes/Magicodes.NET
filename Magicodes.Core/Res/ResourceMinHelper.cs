using Magicodes.Web.Interfaces.WebHandler.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Res
{
    /// <summary>
    /// 资源压缩辅助类
    /// </summary>
    public class ResourceMinHelper : IResourceMin
    {
        public string MinCss(string cssContent)
        {
            return cssContent;
        }

        public string MinJs(string jsContent)
        {
            return jsContent;
        }
    }
}
