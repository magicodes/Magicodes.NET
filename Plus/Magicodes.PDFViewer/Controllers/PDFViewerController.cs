using Magicodes.Core.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.PDFViewer.Controllers
{
    public class PDFViewerController : PlusControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}