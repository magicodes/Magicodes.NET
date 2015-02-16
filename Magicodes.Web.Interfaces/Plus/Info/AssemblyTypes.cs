using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus.Info
{
    public enum AssemblyTypes
    {
        /// <summary>
        /// WF:流程程序集。用于承载流程表单资源以及相关代码
        /// </summary>
        WF = 0,
        /// <summary>
        /// Resource：资源程序集。用于承载资源，可以承载代码
        /// </summary>
        Resource = 1,
        /// <summary>
        /// Code：普通程序集，用于承载代码
        /// </summary>
        Code = 2,
        /// <summary>
        /// Theme：主题程序集。用于承载系统主题。
        /// </summary>
        Theme = 3,
        /// <summary>
        /// Strategy：策略程序集，用于加载策略
        /// </summary>
        Strategy = 4,
        /// <summary>
        /// Models：模型程序集
        /// </summary>
        Models = 5,
        /// <summary>
        /// MVC：MVC程序集
        /// </summary>
        MVC = 6
    }
}
