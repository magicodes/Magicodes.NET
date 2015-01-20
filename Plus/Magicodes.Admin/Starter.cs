using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Data.API;
using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using Magicodes.Web.Interfaces.Events;
using Magicodes.Web.Interfaces.Plus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :PlusStarter
//        description :
//
//        created by 雪雁 at  2015/1/19 14:12:49
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Admin
{
    public class Starter : IPlus
    {
        public void Initialize()
        {
            GlobalApplicationObject.Current.EventsManager.OnApplication_InitializeComplete += EventsManager_OnApplication_InitializeComplete;
        }
        void EventsManager_OnApplication_InitializeComplete(object sender, ApplicationArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
