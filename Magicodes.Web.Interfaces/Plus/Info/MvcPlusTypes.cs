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
//        filename :MvcPlusTypes
//        description :
//
//        created by 雪雁 at  2015/2/9 15:47:59
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Plus.Info
{
    /// <summary>
    /// MVC插件类型
    /// </summary>
    public enum MvcPlusTypes
    {
        /// <summary>
        /// 普通的MVC程序集
        /// </summary>
        MVC = 0,
        /// <summary>
        /// 主页程序集（插件环境中只允许存在一个）
        /// </summary>
        MVCHome = 1,
        /// <summary>
        /// 后台程序集（插件环境中只允许存在一个）
        /// </summary>
        MVCAdmin = 2,
        /// <summary>
        /// MVC程序集（不注册路由）
        /// </summary>
        MVCAndNotRoute = 3
    }
}
