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
using Magicodes.Core.Web.OData;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :UsersODtataController
//        description :
//
//        created by 雪雁 at  2014/10/28 21:44:54
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    [ODataRoutePrefix("Users")]
    public class UsersODtataController : ODataAdminControllerbase
    {
        private AppDbContext db = new AppDbContext();
        UserManager<AppUser, string> userManager;
        public UsersODtataController()
        {
            userManager = new UserManager<AppUser, string>(new AppUserStore(db));
        }
        // GET odata/Users
        //[ODataRoute("Users")]
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<UserViewModel> Get()
        {
            return db.Users.Where(p => !p.Deleted).Select(p => new UserViewModel() { Id = p.Id, UserName = p.UserName, DisplayName = p.DisplayName, Email = p.Email, PhoneNumber = p.PhoneNumber })
                .AsQueryable();
        }

        // GET odata/Users(5)
        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]string id)
        {
            var user = db.Users.SingleOrDefault(p => p.Id == id);
            if (user == null) return NotFound();
            var role = new UserViewModel() { Id = user.Id, UserName = user.UserName, DisplayName = user.DisplayName, Email = user.Email, PhoneNumber = user.PhoneNumber };
            return Ok(role);
        }

        // POST odata/Users
        [HttpPost]
        //[ODataRoute("Users")]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(UserViewModel m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new AppUser()
            {
                CreateBy = User.Identity.GetUserId(),
                CreateTime = DateTimeOffset.Now,
                Deleted = false,
                Id = Guid.NewGuid().ToString(),
                UserName = m.UserName,
                DisplayName = m.DisplayName,
                PhoneNumber = m.PhoneNumber,
                Email = m.Email
            };
            if (!userManager.Users.Any(p => p.Email == m.Email || p.UserName == m.UserName))
                userManager.Create(user);
            else
            {
                ModelState.AddModelError("Failure", "该用户已存在（请确定用户名或邮箱唯一）。");
                return BadRequest(ModelState);
            }
            return Created<UserViewModel>(new UserViewModel() { Id = user.Id, UserName = user.UserName, DisplayName = user.DisplayName, Email = user.Email, PhoneNumber = user.PhoneNumber });
        }

        // PUT odata/Users
        [HttpPut]
        //[ODataRoute("Users")]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(UserViewModel m)
        {
            //不允许通过此接口更新用户名
            var user = userManager.FindById(m.Id);
            if (user == null)
                return NotFound();
            else
            {
                user.UpdateBy = User.Identity.GetUserId();
                user.UpdateTime = DateTimeOffset.Now;
                user.PhoneNumber = m.PhoneNumber;
                user.Email = m.Email;
                user.DisplayName = m.DisplayName;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await userManager.UpdateAsync(user);
                return Updated<UserViewModel>(new UserViewModel() { Id = user.Id, UserName = user.UserName, DisplayName = user.DisplayName, Email = user.Email, PhoneNumber = user.PhoneNumber });
            }
        }

        // DELETE odata/Users
        [HttpDelete]
        //[ODataRoute("Users")]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]string id)
        {
            var m = db.Users.Find(id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = true;
                //db.Users.Remove(m);
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
