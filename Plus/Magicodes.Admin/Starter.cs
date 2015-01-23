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
using System.Web;
using System.Web.Optimization;
using System.Text.RegularExpressions;

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
        //插件名
        public const string PlusName = "Magicodes.Admin";
        public const string PlusPath = "/plus/Plugins/Magicodes.Admin";

        public void Initialize()
        {
            GlobalApplicationObject.Current.EventsManager.OnApplication_InitializeComplete += EventsManager_OnApplication_InitializeComplete;
            GlobalApplicationObject.Current.EventsManager.BeginRequest += EventsManager_BeginRequest;
        }

        void EventsManager_BeginRequest(object sender, EventArgs e)
        {
            //应用程序对象
            var application = (HttpApplication)sender;
            //HTTP上下文对象
            var context = application.Context;
            if (context.Request.IsLocal && context.Request.Url.AbsolutePath.StartsWith("/" + PlusName + "/", StringComparison.CurrentCultureIgnoreCase))
            {
                var ext = VirtualPathUtility.GetExtension(context.Request.Url.AbsolutePath);
                if (ext == ".woff" || ext == ".ttf" || ext == ".eot" || ext == ".jpg" || ext == ".png")
                {
                    var str = Regex.Replace(context.Request.Url.AbsolutePath, "/" + PlusName + "/", PlusPath + "/", RegexOptions.IgnoreCase);
                    //context.RewritePath(str);
                    context.Response.Redirect(str);
                }
            }
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
