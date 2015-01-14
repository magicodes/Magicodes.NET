
using Magicodes.Core.Web.Controllers;
using Magicodes.CMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.CMS.Areas.CMSAdmin.Controllers
{
    public class ODataGridController : AdminControllerBase
    {
        // GET: ODataGrid/CMSContent
        public ActionResult CMSContent()
        {
            return View();
        }
    }
}
