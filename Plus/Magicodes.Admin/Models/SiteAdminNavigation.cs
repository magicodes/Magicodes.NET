using Magicodes.Web.Interfaces.Data.API.SiteNavs;
using Magicodes.Web.Interfaces.T4.DataTable;
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
namespace Magicodes.Admin.Models
{
    /// <summary>
    /// 后台导航菜单信息
    /// </summary>
    [Table("Nav_SiteAdminNavigation")]
    [DisplayName("导航菜单管理")]
    [T4DataTable(Title = "后台导航菜单管理", Description = "后台导航菜单来自插件配置文件，请确保已经配置了相关菜单。")]
    public class SiteAdminNavigation : SiteAdminNavigationBase<string>
    {
    }
}
