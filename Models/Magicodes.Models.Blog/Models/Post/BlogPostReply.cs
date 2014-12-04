using Magicodes.Models.Blog.Models.Account;
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
//        description :博客回帖
//        created by Anton at 2014年11月6日 10:11:51
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.Models.Post
{
    /// <summary>
    /// 博客回帖
    /// </summary>
    [Description("博客回帖")]
    [Table("Blog_PostReply")]
    public class BlogPostReply : CommonBusinessModelBase<int, string>
    {
        [Display(Name = "回帖内容")]
        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Content { get; set; }

        public int PostId { get; set; }

        /// <summary>
        /// 关联博客帖
        /// </summary>
        public virtual BlogPost Post { get; set; }

        public string UerId { get; set; }
        /// <summary>
        /// 关联回复人
        /// </summary>
        public virtual BlogUser Sender { get; set; }

        /// <summary>
        /// 回帖Id、用于盖楼回复
        /// </summary>
        public int ParentId { get; set; }




    }
}
