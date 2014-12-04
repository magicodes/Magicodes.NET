using Magicodes.Models.Blog.Models.Post;
using Magicodes.Web.Interfaces.Models;
using Magicodes.Web.Interfaces.T4;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//        description :用户
//        created by Anton at 2014年11月6日 10:37:55
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.Models.Account 
{
    [Table("Blog_User")]
    public class BlogUser : CommonBusinessModelBase<string, string>
    {
        public int FansNum { get; set; }

        public int FocusNum { get; set; }

        //[Display(Name = "当前用户Id")]
        //[T4GenerationIgnoreAttribute]
        //[StringLength(128)]
        //public string UserId { get; set; }

        [Display(Name = "当前用户Id")]
        [T4GenerationIgnoreAttribute]
        [StringLength(128)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override string Id { get; set; }

        /// <summary>
        /// 关联博客
        /// </summary>
        public ICollection<BlogPost> BlogPosts { get; set; }
        /// <summary>
        /// 关联回帖
        /// </summary>
        public ICollection<BlogPostReply> BlogPostReplies { get; set; }
        /// <summary>
        /// 关联关注的专题
        /// </summary>
        public ICollection<SubjectCategory> FocusSubject { get; set; }

        /// <summary>
        /// 关联点赞的博客
        /// </summary>
        public ICollection<BlogPostPraise> PraisePosts { get; set; }

        /// <summary>
        /// 关注的当前用户
        /// </summary>
        public ICollection<BlogFocusUser> BlogUsers { get; set; }

        /// <summary>
        /// 被关注的用户
        /// </summary>
        public ICollection<BlogFocusUser> FocusUser { get; set; }

        /// <summary>
        /// 用户关注的主题
        /// </summary>
        public ICollection<BlogUserFocusSubject> UserFocusSubjects { get; set; }
    }
}
