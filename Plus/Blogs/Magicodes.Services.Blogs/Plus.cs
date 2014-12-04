using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.OData;
using System.Web.OData.Extensions;
using System.Web.OData.Builder;
using System.Web.Http;
using Magicodes.Web.Interfaces.Events;
using Magicodes.Core.Web;
using Magicodes.Models.Blog.Models.Post;

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
namespace Magicodes.Services.Blogs
{
    class Plus : IPlus
    {
        public void Initialize()
        {
            GlobalConfigurationManager.ODataBuilder.EntitySet<BlogPost>("BlogPost");
            GlobalConfigurationManager.ODataBuilder.EntitySet<SubjectCategory>("Subject");
            GlobalConfigurationManager.ODataBuilder.EntitySet<SubjectCategory>("BlogTag");
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
