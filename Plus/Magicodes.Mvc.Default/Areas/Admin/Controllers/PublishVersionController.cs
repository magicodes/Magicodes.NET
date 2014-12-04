using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magicodes.Models.Mvc.Models;
using Magicodes.Core.Web.Controllers;
namespace Magicodes.Mvc.Default.Areas.Admin.Controllers
{
    public class PublishVersionController : AdminControllerBase
    {
        // GET: Admin/PublishVersion
        public ActionResult Index()
        {
            return View();
        }

    }
}
