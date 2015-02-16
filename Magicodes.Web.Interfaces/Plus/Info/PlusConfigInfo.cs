using Magicodes.Web.Interfaces.Config;
using Magicodes.Web.Interfaces.DocumentProtocols;
using Magicodes.Web.Interfaces.T4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Magicodes.Web.Interfaces.Plus.Info
{
    /// <summary>
    /// 插件配置
    /// </summary>
    [T4GenerationIgnoreAttribute]
    public class PlusConfigInfo : ConfigBase
    {
        public AssemblyTypes AssemblyType { get; set; }
        /// <summary>
        /// MVC插件类型
        /// </summary>
        public MvcPlusTypes MvcPlusType { get; set; }
        [XmlArray("PlusAdminMenus")]
        [XmlArrayItem("Menu")]
        public PlusMenu[] PlusMenus { get; set; }
        /// <summary>
        /// 文档协议
        /// </summary>
        [XmlArray("DocumentOpenProtocols")]
        [XmlArrayItem("DocumentOpenProtocol")]
        public IDocumentOpenProtocol[] DocumentOpenProtocols { get; set; }
    }
}
