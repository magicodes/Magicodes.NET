using Magicodes.Web.Interfaces.Plus.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Core.Res
{
    /// <summary>
    /// 资源路径信息
    /// </summary>
    public class ResourceUrl : IResourceUrl
    {
        public string ManifestResourceName { get; set; }


        public int RequestCount { get; set; }
        public DateTime? LastRequestTime { get; set; }

        public string AssemblyFullName { get; set; }

        public bool HasWrittenToSiteDir { get; set; }

        public string SiteRelativeUrl { get; set; }

        public bool IsAlias { get; set; }

        public long FileWriteTicks { get; set; }
    }
}
