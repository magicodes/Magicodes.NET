using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magicodes.Core.Web.Controllers;
using Magicodes.Web.Interfaces.Config.Info;

namespace Magicodes.CMS.Areas.CMSAdmin.Controllers
{
    public class AdminController : AdminControllerBase
    {
        //
        // GET: /CMSAdmin/Admin/
        public ActionResult Index()
        {
            var siteInfo = ApplicationContext.ConfigManager.GetConfig<SiteConfigInfo>();
            ViewBag.SiteInfo = siteInfo;
            ViewBag.Title = siteInfo.SiteName;
            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.Drives = System.IO.DriveInfo.GetDrives().Where(p => p.IsReady);
            return View();
        }
	}
}