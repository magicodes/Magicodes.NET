using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// Í¼Æ¬
    /// </summary>
    public partial class CMS_Photo : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// Í¼Æ¬Ãû³Æ
        /// </summary>
        public string PhotoName { get; set; }
        /// <summary>
        /// Í¼Æ¬
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// ÃèÊö
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Í¼Æ¬×¨¼­Id
        /// </summary>
        public int AlbumID { get; set; }
        /// <summary>
        /// ä¯ÀÀÊı
        /// </summary>
        public int PVCount { get; set; }
        /// <summary>
        /// ÀàĞÍId
        /// </summary>
        public int ClassID { get; set; }
        /// <summary>
        /// ËõÂÔÍ¼
        /// </summary>
        public string ThumbImageUrl { get; set; }
        /// <summary>
        /// Õı³£Í¼
        /// </summary>
        public string NormalImageUrl { get; set; }
        /// <summary>
        /// ÅÅĞòºÅ
        /// </summary>
        public Nullable<int> Sequence { get; set; }
        /// <summary>
        /// ÊÇ·ñÍÆ¼ö
        /// </summary>
        public bool IsRecomend { get; set; }
        /// <summary>
        /// ÆÀÂÛÊı
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// ±êÇ©
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Ï²»¶Êı
        /// </summary>
        public int FavouriteCount { get; set; }
    }
}
