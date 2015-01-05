using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Magicodes.CMS.DAL.Models
{
    /// <summary>
    /// 内容
    /// </summary>
    public partial class CMS_Content : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbImageUrl { get; set; }
        /// <summary>
        /// 普通图片
        /// </summary>
        public string NormalImageUrl { get; set; }
        /// <summary>
        /// 外链链接地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 浏览数
        /// </summary>
        public int PvCount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public short State { get; set; }
        /// <summary>
        /// 频道类型
        /// </summary>
        public int ClassID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
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
        /// 附件
        /// </summary>
        public string Attachment { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
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
        public string BeFrom { get; set; }
        public string FileName { get; set; }
        public string Meta_Title { get; set; }
        public string Meta_Description { get; set; }
        public string Meta_Keywords { get; set; }
        public string SeoUrl { get; set; }
        public string SeoImageAlt { get; set; }
        public string SeoImageTitle { get; set; }
        public string StaticUrl { get; set; }
    }
}
