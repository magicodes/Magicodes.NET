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
//        created by 雪雁 at  2015/1/4 9:57:59
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class JSONActionResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}
