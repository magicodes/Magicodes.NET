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
//        filename :Config
//        description :
//
//        created by 雪雁 at  2014/10/10 22:18:41
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Default.API.Config
{
    [RoutePrefix("api/config/siteInfo")]
    public class SiteInfoController : WebAPIControllerBase
    {
        // /api/config/siteInfo
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var m = ApplicationContext.ConfigManager.GetConfig<SiteConfigInfo>();
            if (m == null)
                return NotFound();
            return Ok(m);
        }

        // POST /api/config/siteInfo
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(SiteConfigInfo siteConfigInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            siteConfigInfo.UpdateTime = DateTime.Now;

            ApplicationContext.ConfigManager.SaveConfig<SiteConfigInfo>(siteConfigInfo);
            return Created<SiteConfigInfo>("api/config/siteInfo", siteConfigInfo);
        }


        // PUT /api/config/siteInfo
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put(SiteConfigInfo siteConfigInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            siteConfigInfo.UpdateTime = DateTime.Now;
            GlobalApplicationObject.Current.ApplicationContext.ConfigManager.SaveConfig<SiteConfigInfo>(siteConfigInfo);
            return Ok(siteConfigInfo);
        }
    }
}
