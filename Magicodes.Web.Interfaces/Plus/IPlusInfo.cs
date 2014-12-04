using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus
{
    public interface IPlusInfo
    {
        /// <summary>
        /// 程序集
        /// </summary>
        Assembly Assembly { get; set; }
        /// <summary>
        /// 插件程序集信息
        /// </summary>
        IPlusAssemblyInfo PlusAssemblys { get; set; }
    }
}
