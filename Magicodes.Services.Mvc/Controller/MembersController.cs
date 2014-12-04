using Magicodes.Core.OData;
using Magicodes.Models.Mvc;
using Magicodes.Models.Mvc.Models.Account;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Strategy.User;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Routing;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :MembersController
//        description :
//
//        created by 雪雁 at  2014/10/22 18:28:30
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc.Controller
{
    public class MembersController : ODataControllerBase
    {
        private AppDbContext db = new AppDbContext();
        // GET odata/Members
        //[EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        //public IQueryable<AppUser> Get()
        //{
        //    //return db..Where(p => !p.Deleted).AsQueryable();
        //}

        //// GET odata/Members/$count
        //[HttpGet]
        //[ODataRoute("Members/$count")]
        //public IHttpActionResult GetCount(ODataQueryOptions<Member> options)
        //{
        //    IQueryable<Member> members = db.Members.AsQueryable();
        //    if (options.Filter != null)
        //    {
        //        members = options.Filter.ApplyTo(members, new ODataQuerySettings()).Cast<Member>();
        //    }
        //    return Ok(members.Count());
        //}

        // GET odata/Members(5)
        //[HttpGet]
        //[EnableQuery]
        //[ODataRoute("Members({id})")]
        //public IHttpActionResult Get([FromODataUri]int id)
        //{
        //    var m = db.Members.SingleOrDefault(p => p.Id == id);
        //    if (m == null) return NotFound();
        //    return Ok(m);
        //}

        // POST odata/Members
        //[HttpPost]
        //[ODataRoute("Members")]
        //public async Task<IHttpActionResult> Post(Member member)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    if (db.Members.Any(p => p.LoginName == member.LoginName))
        //    {
        //        ModelState.AddModelError("LoginName", "此登录名已经存在！");
        //    }
        //    member.CreateTime = System.DateTimeOffset.Now;
        //    member.Password = ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>().GetPasword(member.Password);
        //    var m = db.Members.Add(member);
        //    await db.SaveChangesAsync();
        //    return Created<Member>(m);
        //}

        // PUT odata/Members
        //[HttpPut]
        //[ODataRoute("Members")]
        //public async Task<IHttpActionResult> Put(Member member)
        //{
        //    var m = await db.Members.FindAsync(member.Id);
        //    if (m == null)
        //        return NotFound();
        //    else
        //    {
        //        if (db.Members.Any(p => p.LoginName == member.LoginName && p.Id != member.Id))
        //        {
        //            ModelState.AddModelError("LoginName", "此登录名已经存在！");
        //        }

        //        m.Deleted = member.Deleted;
        //        m.Email = member.Email;
        //        m.HeadPortrait = member.HeadPortrait;
        //        m.IsActive = member.IsActive;
        //        m.LoginName = member.LoginName;
        //        m.MemberExtend = member.MemberExtend;
        //        m.Password = ApplicationContext.StrategyManager.GetDefaultStrategy<IUserAuthenticationStrategy>().GetPasword(member.Password);
        //        m.UserNickName = member.UserNickName;
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);
        //        await db.SaveChangesAsync();
        //        return Updated(m);
        //    }
        //}

        //// DELETE odata/Members
        //[HttpDelete]
        ////[ODataRoute("Members")]
        //public async Task<IHttpActionResult> Delete([FromODataUri]int key)
        //{
        //    var m = await db.Members.FindAsync(key);
        //    if (m == null)
        //        return NotFound();
        //    else
        //    {
        //        db.Members.Remove(m);
        //        await db.SaveChangesAsync();
        //        return StatusCode(HttpStatusCode.NoContent);
        //    }
        //}

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
        }
    }
}
