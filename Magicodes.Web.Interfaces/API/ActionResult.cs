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
//        filename :ActionResult
//        description :
//
//        created by 雪雁 at  2014/10/22 21:41:01
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.API
{
    public class ActionResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
