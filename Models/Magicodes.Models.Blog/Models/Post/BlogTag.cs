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
//        description :标签分类
//        created by Anton at 2014年11月6日 10:12:05
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.Models.Post
{
    /// <summary>
    /// 标签分类
    /// </summary>
    [Description("标签分类")]
    [Table("Blog_Tag")]
    public class BlogTag : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [Display(Name = "名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 关联博客帖
        /// </summary>
        public virtual ICollection<BlogPostTag> BlogPosts { get; set; }

    }
}
