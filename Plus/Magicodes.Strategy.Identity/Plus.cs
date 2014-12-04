using Magicodes.Core.Web;
using Magicodes.Web.Interfaces.Plus;
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
//        filename :Plus
//        description :
//
//        created by 雪雁 at  2014/10/25 20:54:54
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Strategy.Identity
{
    class Plus : IPlus
    {
        public void Initialize()
        {
            GlobalConfigurationManager.OnConfiguration_AppBuilder += GlobalConfigurationManager_OnConfiguration_AppBuilder;
        }

        void GlobalConfigurationManager_OnConfiguration_AppBuilder(object sender, EventArgs e)
        {
            var app = (IAppBuilder)sender;
            AppAuthConfig.ConfigureAuth(app);
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
