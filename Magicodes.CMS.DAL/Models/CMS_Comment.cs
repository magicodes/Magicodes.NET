using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// 评论
    /// </summary>
    public partial class CMS_Comment : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 内容Id
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 回复数
        /// </summary>
        public int ReplyCount { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public int? ParentID { get; set; }
    }
}
