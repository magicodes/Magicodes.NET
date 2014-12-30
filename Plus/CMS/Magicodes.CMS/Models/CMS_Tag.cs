using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magicodes.CMS.Models
{
    /// <summary>
    /// 标签
    /// </summary>
    public class CMS_Tag : CommonBusinessModelBase<Guid, string>
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// 标签颜色
        /// </summary>
        [MaxLength(20)]
        public string Color { get; set; }

    }
}