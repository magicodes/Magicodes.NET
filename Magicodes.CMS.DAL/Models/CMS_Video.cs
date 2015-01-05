using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// สำฦต
    /// </summary>
    public partial class CMS_Video : CommonBusinessModelBase<int, string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> AlbumID { get; set; }
        public int Sequence { get; set; }
        public Nullable<int> VideoClassID { get; set; }
        public bool IsRecomend { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbImageUrl { get; set; }
        public string NormalImageUrl { get; set; }
        public Nullable<int> TotalTime { get; set; }
        public int TotalComment { get; set; }
        public int TotalFav { get; set; }
        public int TotalUp { get; set; }
        public int Reference { get; set; }
        public string Tags { get; set; }
        public string VideoUrl { get; set; }
        public short UrlType { get; set; }
        public string VideoFormat { get; set; }
        public string Domain { get; set; }
        public int Grade { get; set; }
        public string Attachment { get; set; }
        public short Privacy { get; set; }
        public short State { get; set; }
        public string Remark { get; set; }
        public int PVCount { get; set; }
    }
}
