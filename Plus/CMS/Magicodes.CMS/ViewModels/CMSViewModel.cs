using Magicodes.Web.Interfaces.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magicodes.CMS.ViewModels
{
    [T4ODataGrid("CMSContent")]
    [Serializable]
    [T4FormGroup("基础信息")]
    public class ContentViewModel
    {
        [T4GenerationIgnore]
        public Guid Id { get; set; }
        [T4GenerationIgnore(IgnoreParts.Form)]
        [Display(Name = "创建时间")]
        public DateTimeOffset CreateTime { get; set; }
        #region 基础信息
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(50)]
        [T4FormGroup("基础信息")]
        [Display(Name = "标题")]
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        [MaxLength(50)]
        [T4FormGroup("基础信息")]
        [Display(Name = "子标题")]
        [Required]
        public string SubTitle { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(500)]
        [T4FormGroup("基础信息")]
        [Display(Name = "简介")]
        public string Summary { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [T4GenerationIgnore()]
        [T4FormGroup("基础信息")]
        [Display(Name = "所属类型")]
        //[DataType(DataType.Custom)]
        [T4SelectAttribute(DataUrl = "/", DisplayField = "Name", ValueField = "Id", Root = "")]
        public int ClassTypeId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(50)]
        [T4FormGroup("基础信息")]
        [Display(Name = "所属类型")]
        public string ClassTypeName { get; set; }
        #endregion

        #region SEO
        /// <summary>
        /// 关键字
        /// </summary>
        [MaxLength(200)]
        [T4FormGroup("SEO")]
        [Display(Name = "关键字")]
        public string Keywords { get; set; }
        #endregion

        #region 内容
        /// <summary>
        /// 文本内容
        /// </summary>
        [DataType(DataType.Html)]
        [Display(Name = "内容")]
        [T4FormGroup("内容")]
        [Required]
        public string Content { get; set; }
        #endregion

    }
}