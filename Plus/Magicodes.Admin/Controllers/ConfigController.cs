
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magicodes.Core.Web.Controllers;
using System.Threading.Tasks;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes.NET团队    
//        All rights reserved
//
//        filename :ConfigController
//        description :配置视图 控制器
//
//        created by 雪雁 at  2015/02/09 15:14:36
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Admin.Controllers
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
            var model = ApplicationContext.ConfigManager.GetConfig<Magicodes.Web.Interfaces.Config.Info.AdminSiteConfigInfo>();
            return View(model);
        }
        /// <summary>
        /// 保存后台站点配置
        /// </summary>
        /// <returns>后台站点配置</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminSiteConfigInfo(Magicodes.Web.Interfaces.Config.Info.AdminSiteConfigInfo model)
        {
            ApplicationContext.ConfigManager.SaveConfig<Magicodes.Web.Interfaces.Config.Info.AdminSiteConfigInfo>(model);
            return View(model);
        }

        /// <summary>
        /// 邮箱信息配置
        /// </summary>
        /// <returns>邮箱信息配置配置视图</returns>
        [HttpGet]
        public ActionResult MailConfigInfo()
        {
            var model = ApplicationContext.ConfigManager.GetConfig<Magicodes.Web.Interfaces.Config.Info.MailConfigInfo>();
            return View(model);
        }
        /// <summary>
        /// 保存邮箱信息配置
        /// </summary>
        /// <returns>邮箱信息配置</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MailConfigInfo(Magicodes.Web.Interfaces.Config.Info.MailConfigInfo model)
        {
            ApplicationContext.ConfigManager.SaveConfig<Magicodes.Web.Interfaces.Config.Info.MailConfigInfo>(model);
            return View(model);
        }

        /// <summary>
        /// 站点信息配置
        /// </summary>
        /// <returns>站点信息配置配置视图</returns>
        [HttpGet]
        public ActionResult SiteConfigInfo()
        {
            var model = ApplicationContext.ConfigManager.GetConfig<Magicodes.Web.Interfaces.Config.Info.SiteConfigInfo>();
            return View(model);
        }
        /// <summary>
        /// 保存站点信息配置
        /// </summary>
        /// <returns>站点信息配置</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SiteConfigInfo(Magicodes.Web.Interfaces.Config.Info.SiteConfigInfo model)
        {
            ApplicationContext.ConfigManager.SaveConfig<Magicodes.Web.Interfaces.Config.Info.SiteConfigInfo>(model);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>配置视图</returns>
        [HttpGet]
        public ActionResult SystemConfigInfo()
        {
            var model = ApplicationContext.ConfigManager.GetConfig<Magicodes.Web.Interfaces.Config.Info.SystemConfigInfo>();
            return View(model);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SystemConfigInfo(Magicodes.Web.Interfaces.Config.Info.SystemConfigInfo model)
        {
            ApplicationContext.ConfigManager.SaveConfig<Magicodes.Web.Interfaces.Config.Info.SystemConfigInfo>(model);
            return View(model);
        }

    }
}

