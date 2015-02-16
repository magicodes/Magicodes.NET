using Magicodes.Web.Interfaces.Plus.Info;
using Magicodes.Web.Interfaces.Plus.Mvc;
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
    public class MVCPlusInfo : IMVCPlusInfo
    {
        public string PlusName { get; set; }
        public string PlusFullName { get; set; }
        public MvcPlusTypes MvcPlusType { get; set; }
        public PlusConfigInfo PlusConfigInfo { get; set; }
    }
    public class MvcConfigManager
    {
        static MvcConfigManager()
        {
        }
        /// <summary>
        /// Mvc插件列表
        /// </summary>
        public static List<MVCPlusInfo> MVCPlusList = new List<MVCPlusInfo>();
        /// <summary>
        /// 配置MVC插件程序集路由
        /// </summary>
        /// <param name="mvcPlus"></param>
        public static void Config(Assembly mvcPlus, PlusConfigInfo plusInfo)
        {
            MVCPlusList.Add(
                new MVCPlusInfo()
                {
                    PlusName = mvcPlus.GetName().Name,
                    PlusFullName = mvcPlus.FullName,
                    PlusConfigInfo = plusInfo,
                    MvcPlusType = plusInfo.MvcPlusType == null ? MvcPlusTypes.MVC : plusInfo.MvcPlusType
                });
        }

    }
}
