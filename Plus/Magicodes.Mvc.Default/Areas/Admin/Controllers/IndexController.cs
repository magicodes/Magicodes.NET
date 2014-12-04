using Magicodes.Core.Web.Controllers;
using Magicodes.Web.Interfaces.Config.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Mvc.Default.Areas.Admin.Controllers
{
    public class IndexController : AdminControllerBase
    {
        // GET: Admin/Index
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