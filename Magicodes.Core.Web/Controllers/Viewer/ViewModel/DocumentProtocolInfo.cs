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
//        filename :DocumentProtocolInfo
//        description :
//
//        created by 雪雁 at  2014/12/19 11:31:13
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Controllers.Viewer.ViewModel
{
    public class DocumentProtocolInfo
    {
        public string FilePath { get; set; }
        /// <summary>
        /// contentType:内容类型（MIME 类型）。
        /// </summary>
        public string ContentType { get; set; }
    }
}
