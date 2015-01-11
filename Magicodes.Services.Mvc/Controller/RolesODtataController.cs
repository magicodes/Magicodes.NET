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
using Magicodes.Models.Mvc.Models.Account;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :RolesODtataController
//        description :
//
//        created by 雪雁 at  2014/10/28 21:44:54
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    [ODataRoutePrefix("Roles")]
    public class RolesODtataController : ODataControllerBase
    {
        private AppDbContext db = new AppDbContext();

        // GET odata/Roles
        //[ODataRoute("Roles")]
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<AppRole> Get()
        {
            return db.Roles.Where(p => !p.Deleted).AsQueryable();
        }

        // GET odata/Roles(5)
        [HttpGet]
        [EnableQuery]
        //[ODataRoute("Roles({id})")]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]string id)
        {
            var m = db.Roles.SingleOrDefault(p => p.Id == id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        // POST odata/Roles
        [HttpPost]
        //[ODataRoute("Roles")]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(AppRole m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            m.CreateTime = System.DateTimeOffset.Now;
            m.CreateBy = User.Identity.GetUserId();
            m = db.Roles.Add(m);
            await db.SaveChangesAsync();
            return Created<AppRole>(m);
        }

        // PUT odata/Roles
        [HttpPut]
        //[ODataRoute("Roles")]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(AppRole putModel)
        {
            var m = db.Roles.Find(putModel.Id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = putModel.Deleted;
                m.UpdateBy = User.Identity.GetUserId();
                m.UpdateTime = DateTimeOffset.Now;
                m.Name = putModel.Name;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await db.SaveChangesAsync();
                return Updated(m);
            }
        }

        // DELETE odata/Roles
        [HttpDelete]
        //[ODataRoute("Roles")]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]string id)
        {
            var m = db.Roles.Find(id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = true;
                //db.Roles.Remove(m);
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
