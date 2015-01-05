using Magicodes.Models.Mvc.DAL;
using Magicodes.Web.Interfaces.Data.API;
using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using Magicodes.Web.Interfaces.Plus;
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
//        filename :PlusStarter
//        description :
//
//        created by 雪雁 at  2015/1/5 14:12:49
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc
{
    public class PlusStarter : IPlus
    {
        public void Initialize()
        {
            try
            {
                //注册后台管理菜单仓储
                APIContext<string>.Current.SiteAdminNavigationRepository = new SiteAdminNavigationRepository();               
            }
            catch (Exception)
            {

            }
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
