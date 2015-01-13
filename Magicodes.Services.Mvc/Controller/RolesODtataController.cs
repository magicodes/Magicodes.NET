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
using Magicodes.Services.Mvc.ViewModels;

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
        RoleManager<AppRole> roleManager;
        public RolesODtataController()
        {
            roleManager = new RoleManager<AppRole>(new AppRoleStore(db));
        }
        // GET odata/Roles
        //[ODataRoute("Roles")]
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<RoleViewModel> Get()
        {
            return db.Roles.Where(p => !p.Deleted).Select(p => new RoleViewModel() { Id = p.Id, Name = p.Name })
                .AsQueryable();
        }

        // GET odata/Roles(5)
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]string id)
        {
            var m = db.Roles.SingleOrDefault(p => p.Id == id);
            if (m == null) return NotFound();
            var role = new RoleViewModel() { Id = m.Id, Name = m.Name };
            return Ok(role);
        }

        // POST odata/Roles
        [HttpPost]
        //[ODataRoute("Roles")]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(RoleViewModel m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var role = new AppRole()
            {
                CreateBy = User.Identity.GetUserId(),
                CreateTime = DateTimeOffset.Now,
                Deleted = false,
                Id = Guid.NewGuid().ToString(),
                Name = m.Name
            };
            if (!roleManager.RoleExists(m.Name))
                roleManager.Create(role);
            else
            {
                ModelState.AddModelError("Failure", "该角色已存在。");
                return BadRequest(ModelState);
            }
            return Created<RoleViewModel>(new RoleViewModel() { Id = role.Id, Name = role.Name });
        }

        // PUT odata/Roles
        [HttpPut]
        //[ODataRoute("Roles")]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(RoleViewModel putModel)
        {

            if (roleManager.RoleExists(putModel.Name))
            {
                ModelState.AddModelError("Failure", "该角色已存在。");
                return BadRequest(ModelState);
            }
            var role = roleManager.FindById(putModel.Id);
            if (role == null)
                return NotFound();
            else
            {
                role.UpdateBy = User.Identity.GetUserId();
                role.UpdateTime = DateTimeOffset.Now;
                role.Name = putModel.Name;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await roleManager.UpdateAsync(role);
                return Updated<RoleViewModel>(new RoleViewModel() { Id = role.Id, Name = role.Name });
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
        protected override System.Web.Http.Results.NotFoundResult NotFound()
        {
            return base.NotFound();
        }
    }
}
