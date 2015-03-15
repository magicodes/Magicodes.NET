using Magicodes.Core.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Admin.Controllers
{
    public class ContactsController : AdminControllerBase
    {
        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }
    }
}