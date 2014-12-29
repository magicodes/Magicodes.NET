using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magicodes.CMS.Models
{
    /// <summary>
    /// CMS内容类型基类
    /// </summary>
    protected class CMS_ContentTypeBase : CommonBusinessModelBase<Guid, string>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        [MaxLength(50)]
        public string SubTitle { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(500)]
        public string Summary { get; set; }
       
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
        /// 外链链接地址
        /// </summary>
        [MaxLength(300)]
        public string LinkUrl { get; set; }
        /// <summary>
        /// 浏览数
        /// </summary>
        public int PvCount { get; set; }

        /// <summary>
        /// 频道类型
        /// </summary>
        public int ClassTypeId { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        [MaxLength(200)]
        public string Keywords { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sequence { get; set; }
        /// <summary>
        /// 推荐
        /// </summary>
        public bool IsRecomend { get; set; }
        /// <summary>
        /// 热点
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 醒目
        /// </summary>
        public bool IsColor { get; set; }
        /// <summary>
        /// 置顶
        /// </summary>
        public bool IsTop { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remary { get; set; }
        /// <summary>
        /// 总评数
        /// </summary>
        public int TotalComment { get; set; }
        /// <summary>
        /// 支持数
        /// </summary>
        public int TotalSupport { get; set; }
        /// <summary>
        /// 喜欢数
        /// </summary>
        public int TotalFav { get; set; }
        /// <summary>
        /// 分享数
        /// </summary>
        public int TotalShare { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [MaxLength(50)]
        public string BeFrom { get; set; }
        /// <summary>
        /// Meta标题
        /// </summary>
        [MaxLength(50)]
        public string Meta_Title { get; set; }
        /// <summary>
        /// Meta描述
        /// </summary>
        [MaxLength(500)]
        public string Meta_Description { get; set; }
        /// <summary>
        /// Meta关键字
        /// </summary>
        [MaxLength(300)]
        public string Meta_Keywords { get; set; }

    }
}