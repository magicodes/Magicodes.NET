using Magicodes.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.OData;
using Microsoft.AspNet.Identity;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :ODataControllerBase
//        description :
//
//        created by 雪雁 at  2014/11/12 22:20:56
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Controllers
{
    public class ODataControllerBase : ODataController
    {
        /// <summary>
        /// 当前应用程序上下文对象
        /// </summary>
        public ApplicationContextBase ApplicationContext { get { return GlobalApplicationObject.Current.ApplicationContext; } }
        /// <summary>
        /// 当前用户Id
        /// </summary>
        public string CurrentUserId { get { return User.Identity.GetUserId(); } }
    }
}
