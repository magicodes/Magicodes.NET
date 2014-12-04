using Magicodes.Models.Mvc.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicodes.Models.Mvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.EntityFramework;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Menu
//        description :
//
//        created by 雪雁 at  2014/10/22 14:45:14
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc.Models.Menu
{
    /// <summary>
    /// 菜单右侧的数字类型
    /// </summary>
    public enum MenuBadgeTypes
    {
        /// <summary>
        /// 计算子级菜单数
        /// </summary>
        FromChildrenCount = 0,
        /// <summary>
        /// 从请求中获取数值
        /// </summary>
        FromBadgeRequestUrl = 1,
    }
    /// <summary>
    /// 菜单链接信息
    /// </summary>
    [Description("菜单链接信息")]
    [Table("Menu_MenuLink")]
    public class MenuLink
    {
        public MenuLink()
        {

        }
        public Guid Id { get; set; }
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
        /// <summary>
        /// 菜单链接
        /// </summary>
        [MaxLength(300)]
        public string Href { get; set; }
        /// <summary>
        ///     获取或设置 拥有此菜单的角色信息集合
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<AppRole> Roles { get; set; }
        /// <summary>
        /// 是否已经删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTimeOffset UpdateTime { get; set; }
        /// <summary>
        /// 最后更新人
        /// </summary>
        public AppUser UpdateBy { get; set; }
    }
}
