using Magicodes.Web.Interfaces.Config;
using Magicodes.Web.Interfaces.T4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Plus.Info
{
    /// <summary>
    /// 插件配置
    /// </summary>
    [T4GenerationIgnoreAttribute]
    public class PlusConfigInfo : ConfigBase
    {
        public AssemblyTypes AssemblyType { get; set; }
    }
}
