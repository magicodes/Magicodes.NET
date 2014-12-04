using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Startup
//        description :AppBuilder总启动类
//
//        created by 雪雁 at  2014/10/25 15:21:38
//        http://www.magicodes.net
//
//======================================================================
[assembly: OwinStartup(typeof(Magicodes.Core.Web.Startup))]
namespace Magicodes.Core.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfigurationManager.AppBuilder(app);
        }
    }
}
