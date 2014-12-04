using Magicodes.Core.API;
using Magicodes.Models.Mvc;
using Magicodes.Strategy.Identity;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :MenusController
//        description :
//
//        created by 雪雁 at  2014/10/22 18:28:16
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    [Authorize]
    public class MenusController : WebAPIControllerBase
    {
        private AppDbContext db = new AppDbContext();
        private AppUserManager _userManager;
        public MenusController()
        {

        }
        public MenusController(AppUserManager userManager)
        {
            UserManager = userManager;
        }
        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //Get /api/Menus
        /// <summary>
        /// 获取用户当前的菜单项
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var userId = User.Identity.GetUserId();
            var menus = from link in db.MenuLinks
                        where
                        !link.IsDelete &&
                        link.Roles.Any(p => db.Users.FirstOrDefault(p1 => p1.Id == userId).Roles.Any(p2 => p2.RoleId == p.Id))
                        select link;
            return Ok(menus);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
