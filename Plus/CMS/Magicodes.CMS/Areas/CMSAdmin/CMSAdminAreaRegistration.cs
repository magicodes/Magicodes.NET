using System.Web.Mvc;

namespace Magicodes.CMS.Areas.CMSAdmin
{
    public class CMSAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CMSAdmin_default",
                "CMSAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, pluginName = this.GetType().Assembly.GetName().Name },
                new string[] { "Magicodes.CMS.Areas.CMSAdmin.Controllers" }
            );
        }
    }
}