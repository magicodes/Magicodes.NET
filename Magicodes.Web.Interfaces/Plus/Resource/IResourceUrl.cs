using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus.Resource
{
    /// <summary>
    /// 资源路径信息
    /// </summary>
    public interface IResourceUrl
    {
        /// <summary>
        /// 资源嵌入路径
        /// </summary>
        string ManifestResourceName { get; set; }
        /// <summary>
        /// 请求数
        /// </summary>
        int RequestCount { get; set; }
        /// <summary>
        /// 最后请求时间
        /// </summary>
        DateTime? LastRequestTime { get; set; }
        /// <summary>
        /// 程序集全称
        /// </summary>
        string AssemblyFullName { get; set; }
        /// <summary>
        /// 是否已经写入站点目录
        /// </summary>
        bool HasWrittenToSiteDir { get; set; }
        /// <summary>
        /// 站点相对Url
        /// </summary>
        string SiteRelativeUrl { get; set; }
        /// <summary>
        /// 是否别名
        /// </summary>
        bool IsAlias { get; set; }
        /// <summary>
        /// 文件写入时间
        /// </summary>
        long FileWriteTicks { get; set; }
    }
}
