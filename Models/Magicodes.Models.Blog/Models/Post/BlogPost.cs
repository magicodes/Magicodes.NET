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
//        description :博客发帖
//        created by Anton at 2014年11月6日 10:11:32
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.Models.Post
{
    /// <summary>
    /// 博客贴
    /// </summary>
    [Description("博客")]
    [Table("Blog_Post")]
    public class BlogPost : CommonBusinessModelBase<int, string>
    {
       
        /// <summary>
        /// 博客标题
        /// </summary>
        [Display(Name = "标题")]
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 博客内容
        /// </summary>
        [Display(Name = "内容")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 浏览阅读数
        /// </summary>
        public int BlowNum { get; set; }

        /// <summary>
        /// 回帖数
        /// </summary>
        public int ReplyNum { get; set; }

        /// <summary>
        /// 赞数
        /// </summary>
        public int PraiseNum { get; set; }

        /// <summary>
        /// 发博用户
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 发博用户 关联账户
        /// </summary>
     //  [ForeignKey("UserId2")]
        public virtual BlogUser Sender { get; set; }


        /// <summary>
        /// 关联主题
        /// </summary>
        public virtual ICollection<BlogPostSubject> Subjects { get; set; }

        /// <summary>
        /// 关联标签
        /// </summary>
        public virtual ICollection<BlogPostTag> BlogTags { get; set; }
        [NotMapped]
        public List<int> BlogTagIds { get; set; }

        /// <summary>
        /// 关联回帖
        /// </summary>
        public virtual ICollection<BlogPostReply> BlogPostReplies { get; set; }

        /// <summary>
        /// 关联赞点赞的账户
        /// </summary>
        public virtual ICollection<BlogPostPraise> PraiseUsers { get; set; }
    }
}
