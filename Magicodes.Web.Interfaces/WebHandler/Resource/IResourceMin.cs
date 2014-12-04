using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.WebHandler.Resource
{
    /// <summary>
    /// 资源压缩
    /// </summary>
    public interface IResourceMin
    {
        /// <summary>
        /// CSS压缩
        /// </summary>
        /// <param name="cssContent"></param>
        /// <returns></returns>
        string MinCss(string cssContent);
        /// <summary>
        /// JS压缩
        /// </summary>
        /// <param name="jsContent"></param>
        /// <returns></returns>
        string MinJs(string jsContent);
    }
}
