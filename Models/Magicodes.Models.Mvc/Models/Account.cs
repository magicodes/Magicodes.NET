using Magicodes.Models.Mvc.Models.Menu;
using Magicodes.Web.Interfaces.Models;
using Magicodes.Web.Interfaces.T4;
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
    IUser<string>,
    Magicodes.Web.Interfaces.Strategy.User.IUser<string>,
    ICommonBusinessModelBase<string, string>
    {
        /// <summary>
        /// 用户名或昵称
        /// </summary>        
        [Required]
        [StringLength(50)]
        [Display(Name = "用户名")]
        public string DisplayName { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserId { get; set; }
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

        [NotMapped]
        public DateTimeOffset LockoutEndDateUtcOffset { get; set; }
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

        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        [T4GenerationIgnoreAttribute]
        public virtual bool Deleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        [T4GenerationIgnoreAttribute]
        [StringLength(128)]
        public virtual string CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Display(Name = "更新人")]
        [T4GenerationIgnoreAttribute]
        [StringLength(128)]
        public virtual string UpdateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [T4GenerationIgnoreAttribute]
        public virtual DateTimeOffset? UpdateTime { get; set; }
    }

    public class AppRole : IdentityRole<string, AppUserRole>, ICommonBusinessModelBase<string, string>
    {
        public AppRole()
            : base()
        {
        }
        /// <summary>
        ///     获取或设置 拥有此菜单的角色信息集合
        /// </summary>
        [T4GenerationIgnoreAttribute]
        public virtual ICollection<MenuLink> MenuLinks { get; set; }
        [T4GenerationIgnoreAttribute]
        public override ICollection<AppUserRole> Users
        {
            get
            {
                return base.Users;
            }
        }
        ///// <summary>
        ///// 角色名
        ///// </summary>
        //[Display(Name = "角色名")]
        //public new string Name { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        [T4GenerationIgnoreAttribute]
        public virtual bool Deleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        [T4GenerationIgnoreAttribute]
        [StringLength(128)]
        public virtual string CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Display(Name = "更新人")]
        [T4GenerationIgnoreAttribute]
        [StringLength(128)]
        public virtual string UpdateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [T4GenerationIgnoreAttribute]
        public virtual DateTimeOffset? UpdateTime { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "创建时间")]
        [T4GenerationIgnoreAttribute]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreateTime { get; set; }
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
