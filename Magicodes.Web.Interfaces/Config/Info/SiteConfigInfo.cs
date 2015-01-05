using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Config.Info
{
    [Description("站点信息配置")]
    public class SiteConfigInfo : ConfigBase
    {
        #region 站点信息

        private string _siteName = "Magicodes.NET平台";//站点名称
        private string _siteurl = "";//网站网址
        private string _sitetitle = "Magicodes.NET通用平台";//网站标题
        private string _seokeyword = "Magicodes.NET通用平台";//seo关键字
        private string _seodescription = "Magicodes.NET框架是一套插件框架，以方便中间件的选择、组装和集成";//seo描述
        private string _icp = "";//备案编号
        private bool _isShowlicensed = true;//是否显示版权

        /// <summary>
        /// 站点名称
        /// </summary>
        [Display(Name = "站点名称")]
        [Required]
        [StringLength(50)]
        public string SiteName
        {
            get { return _siteName; }
            set { _siteName = value; }
        }
        /// <summary>
        /// 网站网址
        /// </summary>
        [Display(Name = "网站网址")]
        [StringLength(300)]
        public string SiteUrl
        {
            get { return _siteurl; }
            set { _siteurl = value; }
        }
        /// <summary>
        /// 网站标题
        /// </summary>
        [Display(Name = "网站标题")]
        [StringLength(50)]
        public string SiteTitle
        {
            get { return _sitetitle; }
            set { _sitetitle = value; }
        }
        /// <summary>
        /// 网站默认页
        /// </summary>
        [Display(Name = "网站默认页")]
        [Description("为空则为/home。此路径只能为站内路径。")]
        [StringLength(200)]
        public string SiteDefaultUrl { get; set; }
        /// <summary>
        /// SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        public string SEOKeyword
        {
            get { return _seokeyword; }
            set { _seokeyword = value; }
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        [Display(Name = "SEO关键字")]
        public string SEODescription
        {
            get { return _seodescription; }
            set { _seodescription = value; }
        }
        [Display(Name = "备案编号")]
        [StringLength(50)]
        /// <summary>
        /// 备案编号
        /// </summary>
        public string ICP
        {
            get { return _icp; }
            set { _icp = value; }
        }
        /// <summary>
        /// 顶部代码
        /// </summary>
        [Display(Name = "顶部代码")]
        public string HeadHtmlOrScripts { get; set; }
        /// <summary>
        /// 底部代码
        /// </summary>
        [Display(Name = "底部代码")]
        public string FootHtmlOrScripts { get; set; }
        /// <summary>
        /// 是否显示版权
        /// </summary>
        [Display(Name = "是否显示版权")]
        public bool IsShowLicensed
        {
            get { return _isShowlicensed; }
            set { _isShowlicensed = value; }
        }

        #endregion

        #region 主题设置
        private bool isSupportMobile = true;
        /// <summary>
        /// 是否支持手机访问
        /// </summary>
        [Display(Name = "是否支持手机访问")]
        public bool IsSupportMobile
        {
            get { return isSupportMobile; }
            set { isSupportMobile = value; }
        }

        private string defaultTheme;
        /// <summary>
        /// 默认主题
        /// </summary>
        [Display(Name = "默认主题")]
        public string DefaultTheme
        {
            get { return defaultTheme; }
            set { defaultTheme = value; }
        }
        #endregion

        #region 访问控制
        private bool isCloseSite = false;
        /// <summary>
        /// 是否关闭站点
        /// </summary>
        [Display(Name = "是否关闭站点")]
        public bool IsCloseSite
        {
            get { return isCloseSite; }
            set { isCloseSite = value; }
        }

        private string closeSiteMessage;
        /// <summary>
        /// 站点关闭信息
        /// </summary>
        [Display(Name = "站点关闭信息")]
        public string CloseSiteMessage
        {
            get { return closeSiteMessage; }
            set { closeSiteMessage = value; }
        }
        #endregion
    }
}
