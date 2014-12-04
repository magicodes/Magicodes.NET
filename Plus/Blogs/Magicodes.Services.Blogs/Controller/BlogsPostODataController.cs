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


//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :BlogsPostODataController
//        description :
//
//        created by 雪雁 at  2014/11/12 22:19:51
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Blogs.Controller
{
    [ODataRoutePrefix("BlogsPost")]
    public class BlogsPostODataController : ODataControllerBase
    {
        private BlogDbContext db = new BlogDbContext();
        // GET odata/BlogsPost
        [ODataRoute]
        [EnableQuery(PageSize = 1000, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<BlogPost> Get()
        {
            return db.BlogPost.Where(p => !p.Deleted).AsQueryable();
        }
        // GET odata/BlogsPost(5)
        [HttpGet]
        [ODataRoute("({id})")]
        public IHttpActionResult Get([FromODataUri]int id)
        {
            var m = db.BlogPost.SingleOrDefault(p => p.Id == id);
            if (m == null) return NotFound();
            return Ok(m);
        }
        /// <summary>
        /// POST odata/BlogsPost
        /// 发表博客
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        [ODataRoute]
        public async Task<IHttpActionResult> Post(BlogPost m)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            m.CreateTime = System.DateTimeOffset.Now;
            //TODO:获取当前用户
            m.UserId = "{B0FBB2AC-3174-4E5A-B772-98CF776BD4B9}";
            //添加博客
            m = db.BlogPost.Add(m);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("发布博客失败：" + ex.Message);
            }
             int postId =m.Id;
            //添加多个标签
            foreach (int tagId in m.BlogTagIds)
            {
                db.BlogPostTag.Add(
                new BlogPostTag
                {
                    PostId = postId,
                    BlogTagId = tagId,
                    CreateTime = DateTime.Now,
                    Deleted = false
                });
            }
            await db.SaveChangesAsync();
            return Created<BlogPost>(m);
        }

        // PUT odata/BlogsPost
        [HttpPut]
        [ODataRoute]
        public async Task<IHttpActionResult> Put(BlogPost putModel)
        {
            var m = await db.BlogPost.FindAsync(putModel.Id);
            if (m == null)
                return NotFound();
            else
            {
                m.Deleted = putModel.Deleted;
                //TODO:其他属性待补充
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

        // DELETE odata/BlogsPost
        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IHttpActionResult> Delete([FromODataUri]int id)
        {
            var m = await db.BlogPost.FindAsync(id);
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
