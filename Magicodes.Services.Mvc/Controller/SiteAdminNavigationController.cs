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
using Magicodes.Web.Interfaces.Data.API;
using Magicodes.Core.Web.Utility;
using Magicodes.Models.Mvc.DAL;
using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using Magicodes.Core.Web.Controllers;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :SiteAdminNavigationController
//        description :
//
//        created by 雪雁 at  2015/01/15 18:28:16
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    [Authorize]
    public class SiteAdminNavigationController : WebAPIControllerBase
    {
        SiteAdminNavigationRepositoryBase<string> navigationRepository = APIContext<string>.Current.SiteAdminNavigationRepository;

        //Get /api/SiteAdminNavigation
        /// <summary>
        /// 获取用户当前的菜单项
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            if (User.Identity.IsAdmin())
            {
                var navs = navigationRepository.GetQueryable().Where(p => p.Deleted == false).OrderBy(p => p.SortNo);
                return Ok(navs);
            }
            return NotFound();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
