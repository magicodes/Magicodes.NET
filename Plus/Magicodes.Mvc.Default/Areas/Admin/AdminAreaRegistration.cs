using System.Web.Mvc;

namespace Magicodes.Mvc.Default.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Index", action = "Index", id = UrlParameter.Optional, pluginName = this.GetType().Assembly.GetName().Name },
                new string[] { "Magicodes.Mvc.Default.Areas.Admin.Controllers" }
            );
        }
    }
}