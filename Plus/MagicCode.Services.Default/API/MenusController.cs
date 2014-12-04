using Magicodes.Core.API;
using Magicodes.Models.Default;
using Magicodes.Models.Default.Entitys.Menu;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :MenusController
//        description :
//
//        created by 雪雁 at  2014/10/11 15:51:38
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Default.API
{
    public class MenusController : WebAPIControllerBase
    {
        private MagicodesDefaultDbContext db = new MagicodesDefaultDbContext();
        public IHttpActionResult Get()
        {
            var userId = CurrentUser.Id;
            var menus = from link in db.MenuLinks
                        where !link.IsDelete && link.Roles.Any(p => db.Members.FirstOrDefault(p1 => p1.Id == userId).Roles.Any(p2 => p2.Id == p.Id))
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
        }
    }
}
