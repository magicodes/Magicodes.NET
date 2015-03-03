using Magicodes.Core.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Admin.Controllers
{
    public class AdminController : AdminControllerBase
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}