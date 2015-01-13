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
using System.Data.Entity;
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
        UserManager<AppUser, string> userManager;
        public RolesODtataController()
        {
            roleManager = new RoleManager<AppRole>(new AppRoleStore(db));
            userManager = new UserManager<AppUser, string>(new AppUserStore(db));
        }
        #region 角色成员处理
        // GET odata/Roles(1)/Users
        [ODataRoute("({id})/Users")]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<UserViewModel> GetUsers([FromODataUri]string id)
        {
            var users = db.Users
                .Where(p => !p.Deleted && p.Roles.Any(p1 => p1.RoleId == id))
                .Select(p => new UserViewModel() { Id = p.Id, UserName = p.UserName, DisplayName = p.DisplayName, Email = p.Email, PhoneNumber = p.PhoneNumber })
                .AsQueryable();
            return users;
        }
        // POST odata/Roles(1)/Users
        [HttpPost]
        [ODataRoute("({id})/Users")]
        public IHttpActionResult AddUserToRole([FromODataUri]string id, [FromBody]UserViewModel user)
        {
            var role = db.Roles.SingleOrDefault(p => p.Id == id);
            if (role == null) return NotFound();
            var result = userManager.AddToRole(user.Id, role.Name);
            if (result.Succeeded)
                return Ok(user);
            else
            {
                var str = string.Join("；", result.Errors);
                ModelState.AddModelError("错误", str);
                return BadRequest(ModelState);
            }
        }
        // DELTE odata/Roles(1)/Users(1)
        [HttpDelete]
        [ODataRoute("({id})/Users({userId})")]
        public IHttpActionResult RemoveUserToRole([FromODataUri]string id, [FromODataUri]string userId)
        {
            var role = db.Roles.SingleOrDefault(p => p.Id == id);
            if (role == null) return NotFound();

            var result = userManager.RemoveFromRole(userId, role.Name);
            if (result.Succeeded)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {
                var str = string.Join("；", result.Errors);
                ModelState.AddModelError("错误", str);
                return BadRequest(ModelState);
            }
        }
        #endregion
        // GET odata/Roles
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
