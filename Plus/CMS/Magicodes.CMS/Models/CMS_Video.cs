using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.Models
{
    /// <summary>
    /// สำฦต
    /// </summary>
    public partial class CMS_Video : CMS_ContentTypeBase
    {
        public Nullable<int> TotalTime { get; set; }
        public string VideoUrl { get; set; }
        public short UrlType { get; set; }
        public string VideoFormat { get; set; }
        public string Domain { get; set; }
        public int Grade { get; set; }
    }
}
