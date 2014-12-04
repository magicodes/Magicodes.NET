using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus
{
    public interface IPlus
    {
        /// <summary>
        /// 在系统启动后，插件加载时调用该方法
        /// </summary>
        void Initialize();
        /// <summary>
        /// 插件启用后将调用该方法
        /// </summary>
        void Install();
        /// <summary>
        /// 插件禁用后将调用该方法
        /// </summary>
        void Uninstall();
    }
}
