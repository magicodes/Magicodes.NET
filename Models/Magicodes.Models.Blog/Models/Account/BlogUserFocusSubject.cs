using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicodes.Models.Blog.Models.Post;

namespace Magicodes.Models.Blog.Models.Account
{
    /// <summary>
    /// 用户关注的主题
    /// </summary>
    [Table(" Blog_User_FocusSubject")]
    [Description("用户关注的主题")]
    public class BlogUserFocusSubject
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual BlogUser BlogUser { get; set; }

        public int SubjectId { get; set; }
        public virtual SubjectCategory Subject { get; set; }


    }
}
