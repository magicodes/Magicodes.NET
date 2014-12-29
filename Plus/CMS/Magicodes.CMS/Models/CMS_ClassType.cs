using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Magicodes.CMS.Models
{
    /// <summary>
    /// 栏目类型
    /// </summary>
    public partial class CMS_ClassType : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(50)]
        public string ClassTypeName { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public Nullable<int> ParentId { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public Nullable<int> Sequence { get; set; }
        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool AllowComments { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [MaxLength(300)]
        public string ImageUrl { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        [MaxLength(300)]
        public string ThumbImageUrl { get; set; }
        /// <summary>
        /// 普通图片
        /// </summary>
        [MaxLength(300)]
        public string NormalImageUrl { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public CMS_ContentTypes ContentType { get; set; }
        /// <summary>
        /// 是否自动生成静态页
        /// </summary>
        public bool IsAutoGenerateStaticPages { get; set; }
    }
}
