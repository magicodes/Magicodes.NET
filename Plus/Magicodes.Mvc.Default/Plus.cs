using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Magicodes.Web.Interfaces.Plus;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Events;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Plus
//        description :
//
//        created by 雪雁 at  2014/10/19 19:09:29
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Mvc.Default
{
    public class Plus : IPlus
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

        }

        public void Uninstall()
        {

        }
    }
}