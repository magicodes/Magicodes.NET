using Magicodes.Web.Interfaces.Plus.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes.NET团队    
//        All rights reserved
//
//        filename :IMVCPlusInfo
//        description :
//
//        created by 雪雁 at  2015/2/9 17:59:55
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.Plus.Mvc
{
    public interface IMVCPlusInfo
    {
        /// <summary>
        /// 插件名
        /// </summary>
        string PlusName { get; set; }
        /// <summary>
        /// 插件全称
        /// </summary>
        string PlusFullName { get; set; }
        /// <summary>
        /// 插件类型
        /// </summary>
        MvcPlusTypes MvcPlusType { get; set; }
        /// <summary>
        /// 插件配置信息
        /// </summary>
        PlusConfigInfo PlusConfigInfo { get; set; }
    }
}
