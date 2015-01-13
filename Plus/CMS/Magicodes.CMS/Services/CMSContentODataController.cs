using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
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
    [ODataRoutePrefix("CMSContent")]
    public class CMSContentODataController:ODataControllerBase
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
        public IQueryable<CMS_ContentInfoViewModel> Get()
        {
            return UnitOfWork.CMS_ContentRepository.GetContentDetailInfo().AsQueryable();
        }
        [System.Web.Http.HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri] Guid id)
        {
            var o = UnitOfWork.CMS_ContentRepository.GetByID(id);
            if (o == null)
            {
                return NotFound();
            }
            return Ok(o);
        }
        [System.Web.Http.HttpPost]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(CMS_Content cmsContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cmsContent.CreateTime = DateTimeOffset.Now;
            cmsContent.Id = Guid.NewGuid();
            cmsContent.CreateBy = User.Identity.GetUserName();

            UnitOfWork.CMS_ContentRepository.Add(cmsContent);
            UnitOfWork.SaveChanges();
            return Created(cmsContent);
        }

        [System.Web.Http.HttpPut]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(CMS_Content cmsContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tempContent = UnitOfWork.CMS_ContentRepository.GetByID(cmsContent.Id);
            if (tempContent == null)
            {
                return NotFound();
            }
            else
            {
                tempContent.Title = cmsContent.Title;
                tempContent.Content = cmsContent.Content;
                tempContent.Sequence = cmsContent.Sequence;
                tempContent.Keywords = cmsContent.Keywords;
                tempContent.UpdateTime = DateTimeOffset.Now;
                tempContent.UpdateBy = User.Identity.GetUserName();

                UnitOfWork.CMS_ContentRepository.Update(tempContent);
                UnitOfWork.SaveChanges();
                return Updated(tempContent);
            }
        }

        [System.Web.Http.HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]Guid id)
        {
            var tempContent = UnitOfWork.CMS_ContentRepository.GetByID(id);
            if (tempContent == null)
                return NotFound();
            else
            {
                tempContent.Deleted = true;
                UnitOfWork.CMS_ContentRepository.Remove(tempContent);
                UnitOfWork.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}