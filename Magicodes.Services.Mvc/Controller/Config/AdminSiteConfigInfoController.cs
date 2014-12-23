
using Magicodes.Core.API;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Config.Info;
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
//        filename :AdminSiteConfigInfoController
//        description :后台站点配置 WebAPI
//
//        created by 雪雁 at  2014/12/23 21:42:27
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller.Config
{
    /// <summary>
    /// 后台站点配置
    /// </summary>
    [Authorize]
    [RoutePrefix("api/config/AdminSiteConfigInfo")]

    public class AdminSiteConfigInfoController : WebAPIControllerBase
    {
        // /api/config/AdminSiteConfigInfo
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var m = ApplicationContext.ConfigManager.GetConfig<AdminSiteConfigInfo>();
            if (m == null)
                return NotFound();
            return Ok(m);
        }

        // POST /api/config/AdminSiteConfigInfo
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(AdminSiteConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;

            ApplicationContext.ConfigManager.SaveConfig<AdminSiteConfigInfo>(info);
            return Created<AdminSiteConfigInfo>("api/config/AdminSiteConfigInfo", info);
        }


        // PUT /api/config/AdminSiteConfigInfo
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put(AdminSiteConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;
            GlobalApplicationObject.Current.ApplicationContext.ConfigManager.SaveConfig<AdminSiteConfigInfo>(info);
            return Ok(info);
        }
    }
}

