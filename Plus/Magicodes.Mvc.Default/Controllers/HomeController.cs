using Magicodes.Core.Web.Controllers;
using Magicodes.Models.Mvc;
using Magicodes.Web.Interfaces.Config.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Mvc.Default.Controllers
{
    
    public class HomeController : PlusControllerBase
    {
        private AppDbContext db = new AppDbContext(); 
        public ActionResult Index()
        {
            var siteInfo = ApplicationContext.ConfigManager.GetConfig<SiteConfigInfo>();
            ViewBag.SiteInfo = siteInfo;
            ViewBag.Title = siteInfo.SiteName;
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [Authorize]
        public ActionResult VersionHistory()
        {
            Magicodes.Models.Mvc.AppDbContext db = new AppDbContext();
            return View(db.PublishVersions.Where(moer=>moer.Deleted==false).ToList().OrderByDescending(item=>item.CreateTime));
        }
        public ActionResult License()
        {
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}