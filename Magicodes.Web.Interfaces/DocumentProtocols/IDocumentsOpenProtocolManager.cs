using Magicodes.Web.Interfaces.Plus.Mvc;
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
    public interface IDocumentsOpenProtocolManager
    {
        /// <summary>
        /// 协议列表
        /// </summary>
        List<IDocumentOpenProtocol> DocumentOpenProtocols { get; set; }
        /// <summary>
        /// 注册插件文件协议
        /// </summary>
        /// <param name="mvcPlus"></param>
        void RegisterDocumentsOpenProtocols(IMVCPlusInfo mvcPlus);
    }
}
