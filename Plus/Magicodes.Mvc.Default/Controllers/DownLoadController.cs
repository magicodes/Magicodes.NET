using Magicodes.Core.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Mvc.Default.Controllers
{
    [Authorize]
    public class DownLoadController : PlusControllerBase
    {
        // GET: DownLoad
        public ActionResult Index()
        {
            string downloadDirPath = Path.Combine(Server.MapPath("App_Data"), "Download");
            var file = new DirectoryInfo(downloadDirPath).GetFiles().OrderByDescending(p => p.LastWriteTime).FirstOrDefault();
            if (file != null)
                return File(file.FullName, "application/zip", file.Name);
            else
            {
                ViewBag.Message = "尚未发布，请等待群主发布。";
                return View("Download");
            }
        }
    }
}