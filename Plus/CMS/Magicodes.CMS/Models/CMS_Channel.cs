using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Magicodes.Web.Interfaces.Models;

namespace Magicodes.CMS.Models
{
    public class CMS_Channel: CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 栏目名
        /// </summary>
        [MaxLength(50)]
        public string ChannelName { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public Nullable<int> Sequence { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
    }
}