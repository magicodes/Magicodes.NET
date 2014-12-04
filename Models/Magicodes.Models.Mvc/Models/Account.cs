using Magicodes.Models.Mvc.Models.Menu;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :Account
//        description :
//
//        created by 雪雁 at  2014/10/22 14:14:00
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc.Models.Account
{
    //如果需要使用Int类型主键，请参考以下链接：
    //http://stackoverflow.com/questions/19553424/how-to-change-type-of-id-in-microsoft-aspnet-identity-entityframework-identityus
    //http://www.codeproject.com/Articles/777733/ASP-NET-Identity-Change-Primary-Key
    /// <summary>
    /// 用户
    /// </summary>
    public class AppUser : IdentityUser<string, AppUserLogin, AppUserRole, AppUserClaim>,
    IUser<string>
    {
        /// <summary>
        /// 用户名或昵称
        /// </summary>        
        [Required]
        [StringLength(50)]
        [Display(Name = "用户名")]
        public string UserNickName { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserId { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 是否已经激活
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreateTime { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? LastLoginTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(300)]
        [DisplayFormat(NullDisplayText = "none.jpg")]
        public string HeadPortrait { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser, string> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class AppRole : IdentityRole<string, AppUserRole>
    {
        public AppRole()
            : base()
        {
        }
        /// <summary>
        ///     获取或设置 拥有此菜单的角色信息集合
        /// </summary>
        public virtual ICollection<MenuLink> MenuLinks { get; set; }
    }
    public class AppUserLogin : IdentityUserLogin { }

    public class AppUserRole : IdentityUserRole { }

    public class AppUserClaim : IdentityUserClaim { }


    //public class AppClaimsPrincipal : ClaimsPrincipal
    //{
    //    public AppClaimsPrincipal(ClaimsPrincipal principal)
    //        : base(principal)
    //    { }

    //    public int UserId
    //    {
    //        get { return int.Parse(this.FindFirst(ClaimTypes.Sid).Value); }
    //    }
    //}


    public interface IAppUserStore : IUserStore<AppUser, string>
    {

    }
    public interface IAppRoleStore : IRoleStore<AppRole, string>
    {

    }
    public class AppUserStore :
       UserStore<AppUser, AppRole, string, AppUserLogin, AppUserRole, AppUserClaim>,
       IAppUserStore
    {
        public AppUserStore()
            : base(new AppDbContext())
        {

        }

        public AppUserStore(AppDbContext context)
            : base(context)
        {

        }
    }

    public class AppRoleStore : RoleStore<AppRole, string, AppUserRole>,
   IAppRoleStore
    {
        public AppRoleStore()
            : base(new AppDbContext())
        {

        }

        public AppRoleStore(AppDbContext context)
            : base(context)
        {

        }
    }
}
