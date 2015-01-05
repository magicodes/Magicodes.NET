using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// Í¼Æ¬×¨¼­
    /// </summary>
    public partial class CMS_PhotoAlbum : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// ×¨¼­Ãû³Æ
        /// </summary>
        public string AlbumName { get; set; }
        /// <summary>
        /// ÃèÊö
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ·âÃæÕÕÆ¬Id
        /// </summary>
        public Nullable<int> CoverPhoto { get; set; }
        /// <summary>
        /// ä¯ÀÀÊı
        /// </summary>
        public int PVCount { get; set; }
        /// <summary>
        /// ÅÅĞòºÅ
        /// </summary>
        public int Sequence { get; set; }
    }
}
