using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using System.Web.UI.WebControls;
using Magicodes.CMS.Models;
using Magicodes.CMS.Repositories;
using Magicodes.CMS.UnitOfWork;
using Magicodes.Core.Web.Controllers;
using Microsoft.AspNet.Identity;

namespace Magicodes.CMS.Services
{

    [ODataRoutePrefix("CMSChannel")]
    public class CMSChannelODataController:ODataControllerBase
    {
        private CMS_UnitOfWork unitOfWork;
        private CMS_UnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new CMS_UnitOfWork();
                }
                return unitOfWork;
            }
        }
        [ODataRoute]
        [EnableQuery(PageSize = 1000,AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<CMS_Channel> Get()
        {
            return UnitOfWork.CMS_ChannelRepository.Get(w => !w.Deleted).AsQueryable();
        }

        [System.Web.Http.HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri] int id)
        {
            var o = UnitOfWork.CMS_ChannelRepository.GetByID(id);
            if (o==null)
            {
                return NotFound();
            }
            return Ok(o);
        }
        [System.Web.Http.HttpPost]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(CMS_Channel cmsChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cmsChannel.CreateTime = DateTimeOffset.Now;
            cmsChannel.CreateBy = User.Identity.GetUserName();
          
            UnitOfWork.CMS_ChannelRepository.Add(cmsChannel);
            UnitOfWork.SaveChanges();
            return Created(cmsChannel);
        }

        [System.Web.Http.HttpPut]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(CMS_Channel cmsChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tempChannel = UnitOfWork.CMS_ChannelRepository.GetByID(cmsChannel.Id);
            if (tempChannel==null)
            {
                return NotFound();
            }
            else
            {
                tempChannel.ChannelName = cmsChannel.ChannelName;
                tempChannel.Sequence = cmsChannel.Sequence;
                tempChannel.UpdateTime = DateTimeOffset.Now;
                tempChannel.UpdateBy = User.Identity.GetUserName();
               
                UnitOfWork.CMS_ChannelRepository.Update(tempChannel);
                UnitOfWork.SaveChanges();
                return Updated(cmsChannel);
            }
        }

        [System.Web.Http.HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]int id)
        {
            var cmsChannel = UnitOfWork.CMS_ChannelRepository.GetByID(id);
            if (cmsChannel == null)
                return NotFound();
            else
            {
                cmsChannel.Deleted = true;
                UnitOfWork.CMS_ChannelRepository.Remove(cmsChannel);
                UnitOfWork.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
	}
}