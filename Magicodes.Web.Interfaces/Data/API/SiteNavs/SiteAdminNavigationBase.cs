using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :ISiteNavigation
//        description :
//
//        created by 雪雁 at  2014/12/30 16:47:03
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Data.API.SiteNavs
{
    /// <summary>
    /// 后台站点导航
    /// </summary>
    [Serializable]
    public class SiteAdminNavigationBase<TUserKeyType> : CommonBusinessModelBase<Guid, TUserKeyType>
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [MaxLength(100)]
        public string Text { get; set; }
        /// <summary>
        /// 菜单链接
        /// </summary>
        [MaxLength(300)]
        public string Href { get; set; }
        /// <summary>
        /// 图标样式
        /// </summary>
        [MaxLength(256)]
        public string IconCls { get; set; }
        /// <summary>
        /// 文本样式
        /// </summary>
        [MaxLength(256)]
        public string TextCls { get; set; }
        /// <summary>
        /// 是否显示子级数或者通知数
        /// </summary>
        public bool isShowBadge { get; set; }
        /// <summary>
        /// 菜单右侧数字类型
        /// </summary>
        public MenuBadgeTypes MenuBadgeType { get; set; }
        /// <summary>
        /// 子级数或者通知数请求地址
        /// </summary>
        [MaxLength(300)]
        public string BadgeRequestUrl { get; set; }
    }
}
