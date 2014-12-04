using Magicodes.Models.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using Microsoft.AspNet.Identity;
using System.Net;
using Magicodes.Models.Mvc.Models;
using Magicodes.Core.Web.Controllers;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :PublishVersionODataController
//        description :
//
//        created by 黄炎强 at  2014/11/05 21:44:54
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    [ODataRoutePrefix("PublishVersion")]
    public class PublishVersionODataController : ODataControllerBase
    {
        private AppDbContext db = new AppDbContext();

        // GET odata/PublishVersion
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<PublishVersion> Get()
        {
            return db.PublishVersions.Where(p => !p.Deleted).AsQueryable();
        }

        // GET odata/PublishVersion(5)
        [HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]int id)
        {
            var m = db.PublishVersions.SingleOrDefault(p => p.Id == id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        // POST odata/PublishVersion
        [HttpPost]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(PublishVersion m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            m.CreateTime = System.DateTimeOffset.Now;
            m = db.PublishVersions.Add(m);
            await db.SaveChangesAsync();
            return Created<PublishVersion>(m);
        }

        // PUT odata/PublishVersion
        [HttpPut]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(PublishVersion putModel)
        {
            var m = await db.PublishVersions.FindAsync(putModel.Id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = putModel.Deleted;
                m.Title = putModel.Title;
                m.Content = putModel.Content;
                m.UpdateBy = CurrentUserId;
                m.UpdateTime = DateTimeOffset.Now;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await db.SaveChangesAsync();
                return Updated(m);
            }
        }

        // DELETE odata/PublishVersion
        [HttpDelete]
        //[ODataRoute("PublishVersion")]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]int id)
        {
            var m = await db.PublishVersions.FindAsync(id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = true;
                //db.PublishVersions.Remove(m);
                await db.SaveChangesAsync();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
