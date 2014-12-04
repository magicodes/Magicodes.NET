using Magicodes.Web.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magicodes.Models.Blog.Models.Account
{
    /// <summary>
    /// 用户关注人
    /// </summary>
    [Table("Blog_BlogUser_FocusUser")]
    [Description("用户关注人")]
    public class BlogFocusUser : CommonBusinessModelBase<int, string>
    {
        public string BlogUerId { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public virtual BlogUser BlogUser { get; set; }

        public string FocusUserId { get; set; }

        /// <summary>
        /// 被关注的用户
        /// </summary>
        public virtual BlogUser FocusUser { get; set; }
    }
}
