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
//        filename :SystemConfigInfoController
//        description : WebAPI
//
//        created by 雪雁 at  2015/01/05 13:26:45
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller.Config
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/config/SystemConfigInfo")]

    public class SystemConfigInfoController : WebAPIControllerBase
    {
        // /api/config/SystemConfigInfo
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var m = ApplicationContext.ConfigManager.GetConfig<SystemConfigInfo>();
            if (m == null)
                return NotFound();
            return Ok(m);
        }

        // POST /api/config/SystemConfigInfo
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(SystemConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;

            ApplicationContext.ConfigManager.SaveConfig<SystemConfigInfo>(info);
            return Created<SystemConfigInfo>("api/config/SystemConfigInfo", info);
        }


        // PUT /api/config/SystemConfigInfo
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put(SystemConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;
            GlobalApplicationObject.Current.ApplicationContext.ConfigManager.SaveConfig<SystemConfigInfo>(info);
            return Ok(info);
        }
    }
}

