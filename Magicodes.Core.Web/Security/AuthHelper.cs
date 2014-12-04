using Magicodes.Core.Web.Utility;
using Magicodes.Web.Interfaces;
using Magicodes.Web.Interfaces.Config.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :AuthHelper
//        description :
//
//        created by 雪雁 at  2014/11/20 23:13:43
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Security
{
    public class AuthHelper
    {
        /// <summary>
        /// 当前应用程序上下文对象
        /// </summary>
        public static ApplicationContextBase ApplicationContext { get { return GlobalApplicationObject.Current.ApplicationContext; } }
        /// <summary>
        /// 判断当前用户是否为管理后台用户
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                var adminConfig = GetAdminConfig();
                var userRoles = GetUserRoles();
                var adminRoles = adminConfig.AdminRoles.Split(',');
                var isAdmin = false;
                foreach (var role in adminRoles)
                {
                    if (userRoles.Contains(role))
                    {
                        isAdmin = true;
                        break;
                    }
                }
                return isAdmin;
            }
        }

        private static AdminSiteConfigInfo GetAdminConfig()
        {
            var adminConfig = ApplicationContext.ConfigManager.GetConfig<AdminSiteConfigInfo>();
            if (adminConfig == null)
            {
                //如果为NULL，则使用系统默认的设置
                adminConfig = new AdminSiteConfigInfo();
                ApplicationContext.ConfigManager.SaveConfig(adminConfig);
            }
            return adminConfig;
        }
        /// <summary>
        /// 是否允许进入管理后台
        /// </summary>
        public static bool IsAllowEnterAdminPlatform
        {
            get
            {
                if (!IsAdmin) return false;
                return IsInAdminIp();
            }
        }
        /// <summary>
        /// 是否为允许的管理后台IP
        /// </summary>
        /// <returns></returns>
        public static bool IsInAdminIp()
        {
            var adminConfig = GetAdminConfig();
            if (adminConfig != null && !string.IsNullOrWhiteSpace(adminConfig.AdminAccessIps))
            {
                var ip = WebHelper.GetIP();
                if (!adminConfig.AdminAccessIps.Split(',').Contains(ip))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 获取当前用户角色
        /// </summary>
        /// <returns></returns>
        public static string[] GetUserRoles()
        {
            if (HttpContext.Current.User.Identity is ClaimsIdentity)
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
            else
                return Roles.GetRolesForUser();
        }


    }
}
