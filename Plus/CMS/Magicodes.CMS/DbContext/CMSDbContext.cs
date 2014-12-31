using Magicodes.CMS.Migrations;
using Magicodes.CMS.Models;
using Magicodes.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Magicodes.CMS
{
    public class CMSDbContext:System.Data.Entity.DbContext
    {
        static CMSDbContext()
        {
            //初始化时自动更新迁移到最新版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CMSDbContext, Configuration>());
        }
        /// <summary>
        /// 初始化DbContext
        /// </summary>
        public CMSDbContext()
            : base(GlobalApplicationObject.Current.ConnectionStringName)
        {

        }
        public virtual DbSet<CMS_Channel> CMS_Channels { get; set; }
        public virtual DbSet<CMS_ClassType> CMS_ClassTypes{ get; set; }
        public virtual DbSet<CMS_Comment> CMS_Comments { get; set; }
        public virtual DbSet<CMS_Content> CMS_Contents { get; set; }
        public virtual DbSet<CMS_ContentTag> CMS_ContentTags { get; set; }
        public virtual DbSet<CMS_Photo> CMS_Photos { get; set; }
        public virtual DbSet<CMS_Tag> CMS_Tags { get; set; }
        public virtual DbSet<CMS_Video> CMS_Videos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder); 
        }

        public static CMSDbContext Create()
        {
            return new CMSDbContext();
        }

    }
}