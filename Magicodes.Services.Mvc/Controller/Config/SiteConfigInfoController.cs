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
//        filename :SiteConfigInfoController
//        description :站点信息配置 WebAPI
//
//        created by 雪雁 at  2015/01/05 13:26:45
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller.Config
{
    /// <summary>
    /// 站点信息配置
    /// </summary>
    [Authorize]
    [RoutePrefix("api/config/SiteConfigInfo")]

    public class SiteConfigInfoController : WebAPIControllerBase
    {
        // /api/config/SiteConfigInfo
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var m = ApplicationContext.ConfigManager.GetConfig<SiteConfigInfo>();
            if (m == null)
                return NotFound();
            return Ok(m);
        }

        // POST /api/config/SiteConfigInfo
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(SiteConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;

            ApplicationContext.ConfigManager.SaveConfig<SiteConfigInfo>(info);
            return Created<SiteConfigInfo>("api/config/SiteConfigInfo", info);
        }


        // PUT /api/config/SiteConfigInfo
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put(SiteConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;
            GlobalApplicationObject.Current.ApplicationContext.ConfigManager.SaveConfig<SiteConfigInfo>(info);
            return Ok(info);
        }
    }
}

