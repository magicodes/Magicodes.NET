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
//        description :主题分类
//        created by Anton at 2014年11月5日 16:56:37
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.Models.Post
{
    /// <summary>
    /// 博客主题
    /// </summary>
    [Description("博客专题")]
    [Table("Blog_SubjectCategory")]
    /// <summary>
    /// 专题分类
    /// </summary>
    public class SubjectCategory : CommonBusinessModelBase<int, string>
    {
        /// <summary>
        /// 专题主题
        /// </summary>
        [Display(Name = "专题名称")]
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 专题描述
        /// </summary>
        [Display(Name = "专题描述")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 分类ID、父节点Id
        /// </summary>
        [Display(Name = "父节点Id")]
        [Required]
        public int ParentId { get; set; }

        /// <summary>
        /// 关联博客帖
        /// </summary>
        public virtual ICollection<BlogPostSubject> BlogPosts { get; set; }

        /// <summary>
        /// 关联被关注的账户
        /// </summary>
        public virtual ICollection<BlogUser> FocusUsers { get; set; }

        /// <summary>
        /// 关注的主题
        /// </summary>
        public ICollection<BlogUserFocusSubject> UserFocusSubject { get; set; }
    }
}
