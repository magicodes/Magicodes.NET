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
    /// 博客标签API
    /// </summary>
    [ODataRoutePrefix("BlogTag")]
    public class BlogTagODataController : ODataControllerBase
    {
        private BlogDbContext db = new BlogDbContext();
        // GET odata/BlogTag
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BlogTag> Get()
        {
            return db.BlogTag.Where(p => !p.Deleted).AsQueryable();
        }
        // GET odata/BlogTag(5)
        [HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]int id)
        {
            var m = db.BlogTag.SingleOrDefault(p => p.Id == id);
            if (m == null) return NotFound();
            return Ok(m);
        }
        /// <summary>
        /// POST odata/BlogTag
        /// 添加标签
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(BlogTag m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            m.CreateTime = System.DateTimeOffset.Now;
            m = db.BlogTag.Add(m);
            await db.SaveChangesAsync();
            return Created<BlogTag>(m);
        }

        // PUT odata/BlogTag
        [HttpPut]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(BlogTag putModel)
        {
            var m = await db.BlogTag.FindAsync(putModel.Id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = putModel.Deleted;
                //TODO:其他属性待补充
                m.Name = putModel.Name;
                m.UpdateBy = CurrentUserId;
                m.UpdateTime = DateTimeOffset.Now;
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await db.SaveChangesAsync();
                return Updated(m);
            }
        }

        // DELETE odata/BlogTag
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]int id)
        {
            var m = await db.BlogTag.FindAsync(id);
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
