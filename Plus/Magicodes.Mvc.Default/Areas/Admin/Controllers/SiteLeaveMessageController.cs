using Magicodes.Core.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Mvc.Default.Areas.Admin.Controllers
{
    public class SiteLeaveMessageController : AdminControllerBase
    {
        // GET: Admin/SiteLeaveMessage
        public ActionResult Index()
        {
            return View();
        }
    }
}