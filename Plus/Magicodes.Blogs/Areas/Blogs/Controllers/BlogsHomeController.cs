using Magicodes.Models.Blog;
using Magicodes.Models.Blog.Models.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magicodes.Blogs.Areas.Blogs.Controllers
{
    public class BlogsHomeController : Controller
    {
        BlogDbContext Context = new BlogDbContext();
        // GET: Blogs/BlogsHome
        public ActionResult Index()
        {
           
            return View();
        }

       
    }
}