using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :SiteAdminNavigation
//        description :
//
//        created by 雪雁 at  2014/12/31 15:55:57
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc.Models
{
    /// <summary>
    /// 后台导航菜单信息
    /// </summary>
    [Description("后台导航菜单信息")]
    [Table("Nav_SiteAdminNavigation")]
    public class SiteAdminNavigation : SiteAdminNavigationBase<string>
    {

    }
}
