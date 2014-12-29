using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Magicodes.Web.Interfaces.Config.Info
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigInfo : ConfigBase
    {
        /// <summary>
        /// 应用程序模式
        /// </summary>
        [Display(Name = "应用程序模式")]
        public ApplicationModes ApplicationMode { get; set; }
        /// <summary>
        /// 是否压缩样式
        /// </summary>
        [Display(Name = "是否压缩样式")]
        public bool IsMinCss { get; set; }
        /// <summary>
        /// 是否压脚本文件
        /// </summary>
        [Display(Name = "是否压脚本文件")]
        public bool IsMinJs { get; set; }
        /// <summary>
        /// 是否进行SQl跟踪
        /// </summary>
        [Display(Name = "是否进行SQl跟踪")]
        public bool IsSqlTrace { get; set; }
        /// <summary>
        /// SQL连接字符串名称
        /// </summary>
        [Display(Name = "SQL连接字符串名称")]
        public string SqlConnectName { get; set; }
    }
}
