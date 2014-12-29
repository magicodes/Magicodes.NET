using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Magicodes.CMS.Models
{
    /// <summary>
    /// ÄÚÈÝ
    /// </summary>
    public partial class CMS_Content : CMS_ContentTypeBase
    {
        /// <summary>
        /// ÄÚÈÝ
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// ¾²Ì¬Á´½Ó
        /// </summary>
        [MaxLength(300)]
        public string StaticUrl { get; set; }
    }
}
