using Magicodes.Models.Blog.Models.Account;
using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicodes.Models.Blog.Models.Post
{
    [Description("博客点赞")]
    [Table("Blog_Post_PraiseUser")]
    public class BlogPostPraise : CommonBusinessModelBase<int, string>
    {
        public string UserId { get; set; }
        public virtual BlogUser BlogUser { get; set; }

        public int PostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}
