using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :AdminSiteConfigInfo
//        description :
//
//        created by 雪雁 at  2014/10/28 17:49:15
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Config.Info
{
    [Description("后台站点配置")]
    public class AdminSiteConfigInfo : ConfigBase
    {
        private string adminAccessIps;
        /// <summary>
        /// 后台访问IP
        /// </summary>
        [Display(Name = "后台访问IP，以逗号分隔")]
        public string AdminAccessIps
        {
            get { return adminAccessIps; }
            set { adminAccessIps = value; }
        }
        private string adminRoles = "Admin";
        /// <summary>
        /// 后台登陆角色
        /// </summary>
        [Display(Name = "后台登陆角色，多个请以“,”号分隔【慎重更改】")]
        public string AdminRoles
        {
            get { return adminRoles; }
            set { adminRoles = value; }
        }
    }
}
