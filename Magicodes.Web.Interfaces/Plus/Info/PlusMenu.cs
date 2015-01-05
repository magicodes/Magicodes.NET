using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Magicodes.Web.Interfaces.Plus.Info
{
    /// <summary>
    /// 插件菜单
    /// </summary>
    [Serializable]
    public class PlusMenu
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 菜单链接
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 图标样式
        /// </summary>
        public string IconCls { get; set; }
        /// <summary>
        /// 文本样式
        /// </summary>
        public string TextCls { get; set; }
        /// <summary>
        /// 是否显示子级数或者通知数
        /// </summary>
        public bool IsShowBadge { get; set; }
        /// <summary>
        /// 菜单右侧数字类型
        /// </summary>
        public MenuBadgeTypes MenuBadgeType { get; set; }
        /// <summary>
        /// 子级数或者通知数请求地址
        /// </summary>
        public string BadgeRequestUrl { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        [XmlArray("SubMenus")]
        [XmlArrayItem("Menu")]
        public PlusMenu[] SubMenus { get; set; }
    }
}
