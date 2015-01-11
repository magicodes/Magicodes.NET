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
using Magicodes.CMS.Models;
using Magicodes.CMS.UnitOfWork;
using Magicodes.CMS.ViewModels;
using Magicodes.Core.Web.Controllers;
using Microsoft.AspNet.Identity;

namespace Magicodes.CMS.Services
{
    [ODataRoutePrefix("CMSClassType")]
    public class CMSClassTypeODataController : ODataControllerBase
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
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<CMS_ClassTypeInfoViewModel> Get()
        {
            return UnitOfWork.CMS_ClassTypeRepository.GetClassTypeDetailInfo().AsQueryable();
            //return UnitOfWork.CMS_ClassTypeRepository.Get(w => !w.Deleted).AsQueryable();
        }

        [System.Web.Http.HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri] int id)
        {
            var o = UnitOfWork.CMS_ClassTypeRepository.GetByID(id);
            if (o == null)
            {
                return NotFound();
            }
            return Ok(o);
        }

        [System.Web.Http.HttpPost]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(CMS_ClassType cmsClassType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cmsClassType.CreateTime = DateTimeOffset.Now;
            cmsClassType.CreateBy = User.Identity.GetUserName();

            UnitOfWork.CMS_ClassTypeRepository.Add(cmsClassType);
            UnitOfWork.SaveChanges();
            return Created(cmsClassType);
        }

        [System.Web.Http.HttpPut]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(CMS_ClassType cmsClassType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tempClassType= UnitOfWork.CMS_ClassTypeRepository.GetByID(cmsClassType.Id);
            if (tempClassType == null)
            {
                return NotFound();
            }
            else
            {
                //TODO:
                tempClassType.ClassTypeName = cmsClassType.ClassTypeName;
                tempClassType.ChannelId = cmsClassType.ChannelId;
                tempClassType.UpdateTime = DateTimeOffset.Now;
                tempClassType.UpdateBy = User.Identity.GetUserName();

                UnitOfWork.CMS_ClassTypeRepository.Update(tempClassType);
                UnitOfWork.SaveChanges();
                return Updated(tempClassType);
            }
        }

        [System.Web.Http.HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]int id)
        {
            var cmsClassType = UnitOfWork.CMS_ClassTypeRepository.GetByID(id);
            if (cmsClassType == null)
                return NotFound();
            else
            {
                cmsClassType.Deleted = true;
                UnitOfWork.CMS_ClassTypeRepository.Remove(cmsClassType);
                UnitOfWork.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
	}
}