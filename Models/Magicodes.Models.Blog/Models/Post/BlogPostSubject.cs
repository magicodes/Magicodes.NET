using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//        description :博客和主题关系实体
//        created by Anton at 2014年11月7日 13:49:52
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog.Models.Post
{
    /// <summary>
    /// 博客和主题关系实体
    /// </summary>
    [Description("博客和主题关系实体")]
    [Table("Blog_Post_Subject")]
    public class BlogPostSubject : CommonBusinessModelBase<int, string>
    {
        public int PostId { get; set; }
        public virtual BlogPost Post { get; set; }

        public int SubjectId { get; set; }
        public virtual SubjectCategory Subject { get; set; }
    }
}
