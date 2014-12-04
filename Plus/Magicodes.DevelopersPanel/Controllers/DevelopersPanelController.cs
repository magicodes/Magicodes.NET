using Magicodes.Core.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.DevelopersPanel.Controllers
{
    public class DevelopersPanelController : AdminControllerBase
    {
        // GET: DevelopersPanel
        public ActionResult Index()
        {
            var model = Magicodes.Web.Interfaces.GlobalApplicationObject.Current.WatchPanel.WatchTabs;
            return View(model);
        }
    }
}