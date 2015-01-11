using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.OData;
using System.Web.OData.Extensions;
using System.Web.OData.Builder;
using System.Web.Http;
using Magicodes.Web.Interfaces.Events;
using Magicodes.Models.Mvc.Models.Account;
using Magicodes.Core.Web;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Plus
//        description :
//
//        created by 雪雁 at  2014/10/22 18:20:48
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Services.Mvc
{
    class Plus : IPlus
    {
        public void Initialize()
        {
            GlobalConfigurationManager.ODataBuilder.EntitySet<Magicodes.Models.Mvc.Models.Site.SiteLeaveMessage>("SiteLeaveMessage");
            GlobalConfigurationManager.ODataBuilder.EntitySet<Magicodes.Models.Mvc.Models.PublishVersion>("PublishVersion");
            GlobalConfigurationManager.ODataBuilder.EntitySet<AppRole>("Roles");
            var user = GlobalConfigurationManager.ODataBuilder.EntitySet<AppUser>("Users").EntityType;
            //忽略此属性（OData不支持DateTime类型）
            user.Ignore(t => t.LockoutEndDateUtc);
            //映射此属性
            user.Property(t => t.LockoutEndDateUtcOffset).Name = "LockoutEndDateUtc";
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
