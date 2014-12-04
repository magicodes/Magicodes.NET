using Magicodes.Core.Web.Controllers;
using Magicodes.Models.Blog;
using Magicodes.Models.Blog.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;

namespace Magicodes.Services.Blogs.Controller
{
    /// <summary>
    /// 博客专题API
    /// </summary>
    [ODataRoutePrefix("Subject")]
    public class SubjectODataController : ODataControllerBase
    {
        private BlogDbContext db = new BlogDbContext();
        // GET odata/Subject
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<SubjectCategory> Get()
        {
            return db.SubjectCategory.Where(p => !p.Deleted).AsQueryable();
        }
        // GET odata/Subject(5)
        [HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]int id)
        {
            var m = db.SubjectCategory.SingleOrDefault(p => p.Id == id);
            if (m == null) return NotFound();
            return Ok(m);
        }
        /// <summary>
        /// POST odata/Subject
        /// 添加专题
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(SubjectCategory m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            m.CreateTime = System.DateTimeOffset.Now;
            //添加专题
            m = db.SubjectCategory.Add(m);
            await db.SaveChangesAsync();
            return Created<SubjectCategory>(m);
        }

        // PUT odata/Subject
        [HttpPut]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(SubjectCategory putModel)
        {
            var m = await db.SubjectCategory.FindAsync(putModel.Id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = putModel.Deleted;
                //TODO:其他属性待补充
                m.Title = putModel.Title;
                m.Description = putModel.Description;
                m.UpdateBy = CurrentUserId;
                m.UpdateTime = DateTimeOffset.Now;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await db.SaveChangesAsync();
                return Updated(m);
            }
        }

        // DELETE odata/Subject
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]int id)
        {
            var m = await db.SubjectCategory.FindAsync(id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = true;

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
        }
    }
}
