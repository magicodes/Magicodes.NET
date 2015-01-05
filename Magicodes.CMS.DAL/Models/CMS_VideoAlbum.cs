using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// ÊÓÆµ×¨¼­
    /// </summary>
    public partial class CMS_VideoAlbum : CommonBusinessModelBase<int, string>
    {
        public string AlbumName { get; set; }
        public string CoverVideo { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public int PvCount { get; set; }
    }
}
