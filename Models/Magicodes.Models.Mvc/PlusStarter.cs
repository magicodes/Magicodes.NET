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
                var r = APIContext<string>.Current.SiteAdminNavigationRepository;
                var nav = new SiteAdminNavigationBase<string>()
                    {
                        BadgeRequestUrl = null,
                        Href = "/",
                        IconCls = "fa fa-tachometer",
                        Id = Guid.Parse("{BFD1DBFE-5501-4202-B0E1-A7DA0E5167D5}"),
                        isShowBadge = false,
                        MenuBadgeType = MenuBadgeTypes.FromChildrenCount,
                        ParentId = null,
                        Text = "CMS",
                        TextCls = string.Empty,
                        Deleted = false,
                        CreateBy = "{B0FBB2AC-3174-4E5A-B772-98CF776BD4B9}",
                        CreateTime = DateTime.Now
                    };
                if (r.GetByID(nav.Id) == null)
                {
                    r.Add(nav);
                    r.SaveChanges();
                }
                
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
