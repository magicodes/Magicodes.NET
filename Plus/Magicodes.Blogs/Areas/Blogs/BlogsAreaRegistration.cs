using System.Web.Mvc;

namespace Magicodes.Blogs.Areas.Blogs
{
    public class BlogsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Blogs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "Blogs_default",
            //    "Blogs/{controller}/{action}/{id}",
            //    new { controller = "BlogsHome", action = "Index", id = UrlParameter.Optional }
            //);
            context.MapRoute(
                "Blogs_default",
                "Blogs/{controller}/{action}/{id}",
                new { controller = "BlogsHome", action = "Index", id = UrlParameter.Optional, pluginName = this.GetType().Assembly.GetName().Name }
            );
        }
    }
}