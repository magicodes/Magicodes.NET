using Magicodes.Web.Interfaces.Plus.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :GlobalConfigManager
//        description :
//
//        created by 雪雁 at  2014/10/21 16:10:09
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Mvc
{
    /// <summary>
    /// MVC插件类型
    /// </summary>
    public enum MvcPlusTypes
    {
        MVCHome = 1,
        MVC = 2
    }
    public class MVCPlusInfo
    {
        public string PlusName { get; set; }
        public MvcPlusTypes MvcPlusType { get; set; }
    }
    public class MvcConfigManager
    {
        static MvcConfigManager()
        {
            MVCPlusList = new List<MVCPlusInfo>();
        }
        /// <summary>
        /// Mvc插件列表
        /// </summary>
        public static List<MVCPlusInfo> MVCPlusList { get; set; }
        /// <summary>
        /// 配置MVC插件程序集路由
        /// </summary>
        /// <param name="mvcPlus"></param>
        public static void Config(Assembly mvcPlus, AssemblyTypes assType)
        {
            MVCPlusList.Add(
                new MVCPlusInfo()
                {
                    PlusName = mvcPlus.GetName().Name,
                    MvcPlusType = assType == AssemblyTypes.MVCHome ? MvcPlusTypes.MVCHome : MvcPlusTypes.MVC
                });
        }

    }
}
