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
//        filename :DocumentsOpenProtocolManager
//        description :文档打开协议管理器
//
//        created by 雪雁 at  2014/12/18 14:54:24
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Web.Interfaces.DocumentProtocols
{
    /// <summary>
    /// 文档打开协议管理器
    /// </summary>
    public class DocumentsOpenProtocolManager
    {
        /// <summary>
        /// 协议列表
        /// </summary>
        Lazy<List<DocumentOpenProtocol>> documentOpenProtocols = new Lazy<List<DocumentOpenProtocol>>(() => new List<DocumentOpenProtocol>());
        /// <summary>
        /// 协议列表
        /// </summary>
        public List<DocumentOpenProtocol> DocumentOpenProtocols
        {
            get
            {
                return documentOpenProtocols.Value;
            }
        }
    }
}
