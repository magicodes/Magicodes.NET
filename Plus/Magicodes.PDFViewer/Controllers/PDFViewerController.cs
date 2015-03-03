using Magicodes.Core.Web.Controllers;
using Magicodes.Core.Web.Controllers.Viewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.PDFViewer.Controllers
{
    public class PDFViewerController : PlusControllerBase
    {
        public ActionResult Viewer(DocumentProtocolInfo documentProtocolInfo)
        {
            return View("Index", documentProtocolInfo);
        }
        public ActionResult Index()
        {
            var data = TempData["DocumentProtocolInfo"] as DocumentProtocolInfo;
            return Viewer(data);
        }
    }
}