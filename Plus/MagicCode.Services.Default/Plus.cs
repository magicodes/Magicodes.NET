using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Magicodes.Models.Default.Entitys.Account;
using System.Web.Routing;
using System.Web.OData.Extensions;
using System.Web.OData.Builder;
using System.Web.Http;
using Magicodes.Web.Interfaces.Events;
namespace Magicodes.Services.Default
{
    class Plus : IPlus
    {
        public void Initialize()
        {
            GlobalApplicationObject.Current.EventsManager.OnApplication_InitializeComplete += EventsManager_OnApplication_InitializeComplete;
        }

        void EventsManager_OnApplication_InitializeComplete(object sender, ApplicationArgs e)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Member>("Members");
            builder.EntitySet<MemberExtend>("MemberExtends");
            System.Web.Http.GlobalConfiguration.Configure(config =>
            {
                //config.EnableQuerySupport();
                config.MapODataServiceRoute(routeName: "OData", routePrefix: "odata", model: builder.GetEdmModel());
            });
        }

        public void Install()
        {
            throw new NotImplementedException();
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}
