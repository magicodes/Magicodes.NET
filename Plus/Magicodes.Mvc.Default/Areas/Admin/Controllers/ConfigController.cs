using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magicodes.Core.Web.Controllers;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :ConfigController
//        description :配置视图 控制器
//
//        created by 雪雁 at  2015/01/05 13:26:42
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Mvc.Default.Areas.Admin.Controllers
{
    /// <summary>
    /// 配置页面控制器
    /// </summary>
    public class ConfigController : AdminControllerBase
    {
        /// <summary>
        /// 后台站点配置
        /// </summary>
        /// <returns>后台站点配置配置视图</returns>
        [HttpGet]
        public ActionResult AdminSiteConfigInfo()
        {
            return View();
        }


        /// <summary>
        /// 邮箱信息配置
        /// </summary>
        /// <returns>邮箱信息配置配置视图</returns>
        [HttpGet]
        public ActionResult MailConfigInfo()
        {
            return View();
        }


        /// <summary>
        /// 站点信息配置
        /// </summary>
        /// <returns>站点信息配置配置视图</returns>
        [HttpGet]
        public ActionResult SiteConfigInfo()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>配置视图</returns>
        [HttpGet]
        public ActionResult SystemConfigInfo()
        {
            return View();
        }


    }
}