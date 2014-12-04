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
//        filename :Publish
//        description :
//
//        created by 雪雁 at  2014/11/4 10:47:43
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc.Models
{
    [Description("Nuget包获取命令")]
    [Table("Publish_Nuget")]
    public class PublishNuget : CommonBusinessModelBase<int, string>
    {
        [Display(Name = "Nuget命令")]
        [Required]
        [StringLength(500)]
        public string CmdText { get; set; }
    }

    [Description("发布版的版本信息")]
    [Table("Publish_Version")]
    public class PublishVersion:CommonBusinessModelBase<int, string>
    {
        [Display(Name = "标题")]
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Display(Name = "详细介绍")]
        [Required]
        [StringLength(4000)]
        public string Content { get; set; }
    }
}
