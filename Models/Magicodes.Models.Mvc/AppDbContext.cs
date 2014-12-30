using Magicodes.Models.Mvc.Migrations;
using Magicodes.Models.Mvc.Models;
using Magicodes.Models.Mvc.Models.Account;
using Magicodes.Models.Mvc.Models.Menu;
using Magicodes.Models.Mvc.Models.Site;
using Magicodes.Web.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :MagicodesDefaultDbContext
//        description :
//
//        created by 雪雁 at  2014/10/22 14:09:44
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Mvc
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, AppUserLogin, AppUserRole, AppUserClaim>
    {
        static AppDbContext()
        {
            //初始化时自动更新迁移到最新版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }
        /// <summary>
        /// 初始化DbContext
        /// </summary>
        public AppDbContext()
            : base(GlobalApplicationObject.Current.ConnectionStringName)
        {

        }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        /// <summary>
        /// 初始化DbContext
        /// </summary>
        /// <param name="strConnection"></param>
        public AppDbContext(string strConnection)
            : base(strConnection)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //将默认的表面通通改了,用户角色表统一加上Account前缀
            modelBuilder.Entity<AppUser>().ToTable("Account_Users");
            modelBuilder.Entity<AppRole>().ToTable("Account_Roles");
            modelBuilder.Entity<AppUserClaim>().ToTable("Account_UserClaims");
            modelBuilder.Entity<AppUserLogin>().ToTable("Account_UserLogin");
            modelBuilder.Entity<AppUserRole>().ToTable("Account_UserRoles");

            modelBuilder.Entity<MenuLink>()
                .HasMany(p => p.Roles)
                .WithMany(p => p.MenuLinks)
                .Map(mp =>
                {
                    mp.ToTable("Account_RoleMenuLinks");
                    mp.MapLeftKey("MenuLinkId");
                    mp.MapRightKey("RoleId");
                });
        }

        /// <summary>
        /// 菜单信息
        /// </summary>
        public DbSet<MenuLink> MenuLinks { get; set; }

        /// <summary>
        /// 网站留言信息
        /// </summary>
        public DbSet<SiteLeaveMessage> SiteLeaveMessages { get; set; }

        /// <summary>
        /// Nuget包获取命令
        /// </summary>
        public DbSet<PublishNuget> PublishNugets { get; set; }

        /// <summary>
        /// 发布版的版本信息
        /// </summary>
        public DbSet<PublishVersion> PublishVersions { get; set; }
    }
}
