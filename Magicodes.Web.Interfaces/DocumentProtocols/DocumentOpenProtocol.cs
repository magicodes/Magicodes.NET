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
//        filename :IDocumentOpenProtocol
//        description :文档打开协议信息
//
//        created by 雪雁 at  2014/12/18 14:56:48
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.DocumentProtocols
{
    /// <summary>
    /// 文档打开协议
    /// </summary>
    public class DocumentOpenProtocol
    {
        /// <summary>
        /// contentType:内容类型（MIME 类型）。
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 查看器路径
        /// </summary>
        public string ViewerUrl { get; set; }
    }
}
