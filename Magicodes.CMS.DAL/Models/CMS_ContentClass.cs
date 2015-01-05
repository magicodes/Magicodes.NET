using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// 内容类型
    /// </summary>
    public partial class CMS_ContentClass : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sequence { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public Nullable<int> ParentId { get; set; }
        /// <summary>
        /// 是否允许子级
        /// </summary>
        public bool AllowSubclass { get; set; }
        /// <summary>
        /// 是否允许添加内容
        /// </summary>
        public bool AllowAddContent { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 栏目类别
        /// </summary>
        public int ClassTypeID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string Meta_Title { get; set; }
        public string Meta_Description { get; set; }
        public string Meta_Keywords { get; set; }
        public string SeoUrl { get; set; }
        public string SeoImageAlt { get; set; }
        public string SeoImageTitle { get; set; }
    }
}
