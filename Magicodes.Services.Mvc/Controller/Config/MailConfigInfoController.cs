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
//        filename :MailConfigInfoController
//        description :邮箱信息配置 WebAPI
//
//        created by 雪雁 at  2014/10/29 00:21:37
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller.Config
{
    /// <summary>
    /// 邮箱信息配置
    /// </summary>
    [Authorize]
    [RoutePrefix("api/config/MailConfigInfo")]

    public class MailConfigInfoController : WebAPIControllerBase
    {
        // /api/config/MailConfigInfo
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var m = ApplicationContext.ConfigManager.GetConfig<MailConfigInfo>();
            if (m == null)
                return NotFound();
            return Ok(m);
        }

        // POST /api/config/MailConfigInfo
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(MailConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;

            ApplicationContext.ConfigManager.SaveConfig<MailConfigInfo>(info);
            return Created<MailConfigInfo>("api/config/MailConfigInfo", info);
        }


        // PUT /api/config/MailConfigInfo
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put(MailConfigInfo info)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            info.UpdateTime = DateTime.Now;
            GlobalApplicationObject.Current.ApplicationContext.ConfigManager.SaveConfig<MailConfigInfo>(info);
            return Ok(info);
        }
    }
}

