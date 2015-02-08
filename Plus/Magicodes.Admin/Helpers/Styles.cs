using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magicodes.Admin
{
    public static class Styles
    {
        /// <summary>
        /// 呈现路径集的插件链接标记。
        /// </summary>
        /// <param name="html"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IHtmlString Render(params string[] paths)
        {
            var strs = paths.Select(p => p.Replace("~/", "~/" + Starter.PlusName + "/")).ToArray();
            return System.Web.Optimization.Styles.Render(strs);
        }
    }
}