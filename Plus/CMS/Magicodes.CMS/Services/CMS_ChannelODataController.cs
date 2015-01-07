using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using Magicodes.CMS.Models;
using Magicodes.CMS.UnitOfWork;
using Magicodes.Core.Web.Controllers;
using Microsoft.AspNet.Identity;

namespace Magicodes.CMS.Services
{

    [ODataRoutePrefix("CMSChannel")]
    public class CMS_ChannelODataController:ODataControllerBase
    {
        private CMS_DbSession dbSession;
        private CMS_DbSession DbSession
        {
            get
            {
                if (dbSession == null)
                {
                    dbSession = new CMS_DbSession();
                }
                return dbSession;
            }
        }

        [ODataRoute]
        [EnableQuery(PageSize = 1000,AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<CMS_Channel> Get()
        {
            return DbSession.CMS_ChannelRepository.Get(w => !w.Deleted).AsQueryable();
        }

        [System.Web.Http.HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri] int id)
        {
            var o = DbSession.CMS_ChannelRepository.GetByID(id);
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
            using (var unitOfWork=DbSession.StartupUnitOfWork())
            {
                try
                {
                    DbSession.CMS_ChannelRepository.Add(cmsChannel);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw new Exception(ex.ToString());
                }
            }
            return Created(cmsChannel);
        }
	}
}