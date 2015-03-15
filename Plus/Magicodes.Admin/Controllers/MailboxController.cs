using Magicodes.Core.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Admin.Controllers
{
    public class MailboxController : AdminControllerBase
    {
        // GET: Mailbox
        public ActionResult Index()
        {
            return View();
        }
        // GET: ComposeEmail
        /// <summary>
        /// 新邮件
        /// </summary>
        /// <returns></returns>
        public ActionResult ComposeEmail()
        {
            return View();
        }
        // GET: EmailView
        /// <summary>
        /// 查看邮件
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailView()
        {
            return View();
        }
    }
}