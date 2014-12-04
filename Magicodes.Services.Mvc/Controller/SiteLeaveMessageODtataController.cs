using Magicodes.Models.Mvc;
using Magicodes.Models.Mvc.Models.Site;
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
using Magicodes.Core.Web.Controllers;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :SiteLeaveMessageController
//        description :
//
//        created by 雪雁 at  2014/10/28 21:44:54
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    [ODataRoutePrefix("SiteLeaveMessage")]
    public class SiteLeaveMessageODtataController : ODataControllerBase
    {
        private AppDbContext db = new AppDbContext();

        // GET odata/SiteLeaveMessage
        //[ODataRoute("SiteLeaveMessage")]
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<SiteLeaveMessage> Get()
        {
            return db.SiteLeaveMessages.Where(p => !p.Deleted).AsQueryable();
        }

        // GET odata/SiteLeaveMessage(5)
        [HttpGet]
        [EnableQuery]
        //[ODataRoute("SiteLeaveMessage({id})")]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]int id)
        {
            var m = db.SiteLeaveMessages.SingleOrDefault(p => p.Id == id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        // POST odata/SiteLeaveMessage
        [HttpPost]
        //[ODataRoute("SiteLeaveMessage")]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(SiteLeaveMessage m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            m.CreateTime = System.DateTimeOffset.Now;
            m = db.SiteLeaveMessages.Add(m);
            await db.SaveChangesAsync();
            return Created<SiteLeaveMessage>(m);
        }

        // PUT odata/SiteLeaveMessage
        [HttpPut]
        //[ODataRoute("SiteLeaveMessage")]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(SiteLeaveMessage putModel)
        {
            var m = await db.SiteLeaveMessages.FindAsync(putModel.Id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = putModel.Deleted;
                m.Content = putModel.Content;
                m.UpdateBy = User.Identity.GetUserId();
                m.UpdateTime = DateTimeOffset.Now;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await db.SaveChangesAsync();
                return Updated(m);
            }
        }

        // DELETE odata/SiteLeaveMessage
        [HttpDelete]
        //[ODataRoute("SiteLeaveMessage")]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]int id)
        {
            var m = await db.SiteLeaveMessages.FindAsync(id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = true;
                //db.SiteLeaveMessages.Remove(m);
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
