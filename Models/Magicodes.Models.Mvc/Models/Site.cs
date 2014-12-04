using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Site
//        description :
//
//        created by 雪雁 at  2014/10/28 20:49:56
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc.Models.Site
{
    /// <summary>
    /// 网站留言信息
    /// </summary>
    [Description("网站留言信息")]
    [Table("Site_LeaveMessage")]
    public class SiteLeaveMessage : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 留言内容
        /// </summary>
        [Display(Name = "留言内容")]
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
    }
}
