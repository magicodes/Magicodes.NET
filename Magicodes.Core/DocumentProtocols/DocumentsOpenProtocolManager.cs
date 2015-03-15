using Magicodes.Web.Interfaces.DocumentProtocols;
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
namespace Magicodes.Core.DocumentProtocols
{
    /// <summary>
    /// 文档打开协议管理器
    /// </summary>
    public class DocumentsOpenProtocolManager : IDocumentsOpenProtocolManager
    {
        public DocumentsOpenProtocolManager()
        {
            DocumentOpenProtocols = new List<IDocumentOpenProtocol>();
        }
        /// <summary>
        /// 协议列表
        /// </summary>
        public List<IDocumentOpenProtocol> DocumentOpenProtocols { get; set; }
        /// <summary>
        /// 注册文档协议
        /// </summary>
        /// <param name="mvcPlus"></param>
        public void RegisterDocumentsOpenProtocols(IMVCPlusInfo mvcPlus)
        {
            if (mvcPlus.PlusConfigInfo != null 
                && mvcPlus.PlusConfigInfo.DocumentOpenProtocols != null 
                && mvcPlus.PlusConfigInfo.DocumentOpenProtocols.Length > 0)
            {
                foreach (var protocol in mvcPlus.PlusConfigInfo.DocumentOpenProtocols)
                {
                    if (!DocumentOpenProtocols.Any(p => p.ContentType.Equals(protocol.ContentType, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        protocol.PluginName = mvcPlus.PlusName;
                        DocumentOpenProtocols.Add(protocol);
                    }
                }
            }


        }
    }
}
