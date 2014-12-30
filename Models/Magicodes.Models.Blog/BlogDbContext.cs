using Magicodes.Models.Blog.Migrations;
using Magicodes.Models.Blog.Models.Account;
using Magicodes.Models.Blog.Models.Post;
using Magicodes.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :BlogDbContext
//        description :
//
//        created by 雪雁 at  2014/11/4 18:22:48
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Models.Blog
{
    public class BlogDbContext : DbContext
    {
        static BlogDbContext()
        {
            //初始化时自动更新迁移到最新版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogDbContext, Configuration>());
        }
        /// <summary>
        /// 初始化DbContext
        /// </summary>
        public BlogDbContext()
            : base(GlobalApplicationObject.Current.ConnectionStringName)
        {
            
        }
        public DbSet<BlogUser> User { get; set; }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<BlogPostReply> BlogPostReply { get; set; }
        public DbSet<BlogTag> BlogTag { get; set; }

        public DbSet<BlogPostTag> BlogPostTag { get; set; }
        public DbSet<SubjectCategory> SubjectCategory { get; set; }

        public DbSet<BlogPostSubject> BlogPostSubject { get; set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            //博客和用户的关系 
            modelBuilder.Entity<BlogPost>()
                .HasRequired(s => s.Sender)
                .WithMany(s => s.BlogPosts)
                .HasForeignKey(s => s.UserId);
            //博客和主题  
            modelBuilder.Entity<BlogPostSubject>()
                .HasRequired(s => s.Subject)
                .WithMany(s => s.BlogPosts)
                .HasForeignKey(s => s.SubjectId);
            modelBuilder.Entity<BlogPostSubject>()
            .HasRequired(s => s.Post)
            .WithMany(s => s.Subjects)
             .HasForeignKey(s => s.PostId);
            //博客和标签 
            modelBuilder.Entity<BlogPostTag>()
                .HasRequired(s => s.BlogTag)
                .WithMany(s => s.BlogPosts)
                .HasForeignKey(s => s.BlogTagId);

            modelBuilder.Entity<BlogPostTag>()
                .HasRequired(s => s.BlogPost)
                .WithMany(s => s.BlogTags)
                .HasForeignKey(s => s.PostId);

            //博客和点赞的用户  n:n
            modelBuilder.Entity<BlogPostPraise>()
                .HasRequired(s => s.BlogUser)
                .WithMany(s => s.PraisePosts)
                .HasForeignKey(s => s.UserId);
            modelBuilder.Entity<BlogPostPraise>()
                .HasRequired(s => s.BlogPost)
                .WithMany(s => s.PraiseUsers)
                .HasForeignKey(s => s.PostId);

            //配置回帖BlogPostReply关系
            //回帖和回帖人
            modelBuilder.Entity<BlogPostReply>()
                .HasRequired(s => s.Sender)
                .WithMany(s => s.BlogPostReplies)
                .HasForeignKey(s => s.UerId);
             //   .WillCascadeOnDelete(false); ;
            //回帖和博客 
            modelBuilder.Entity<BlogPostReply>()
               .HasRequired(s => s.Post)
               .WithMany(s => s.BlogPostReplies)
               .HasForeignKey(s => s.PostId);
              // .WillCascadeOnDelete(false); ;

            //配置主题SubjectCategory关系
            modelBuilder.Entity<BlogUserFocusSubject>()
               .HasRequired(s => s.BlogUser)
               .WithMany(s => s.UserFocusSubjects)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<BlogUserFocusSubject>()
               .HasRequired(s => s.Subject)
               .WithMany(s => s.UserFocusSubject)
                .HasForeignKey(s => s.SubjectId);

           

            //modelBuilder.Entity<BlogPraiseUser>().HasKey(d => d.PrarseUser);
            //关注人关系
            modelBuilder.Entity<BlogFocusUser>()
                .HasRequired(s => s.BlogUser)
                .WithMany(s => s.BlogUsers)
                 .HasForeignKey(s => s.BlogUerId);
                //.WillCascadeOnDelete(true); 

            modelBuilder.Entity<BlogFocusUser>()
                .HasRequired(s => s.FocusUser)
                .WithMany(s => s.FocusUser)
                .HasForeignKey(s => s.FocusUserId);
               //.WillCascadeOnDelete(true); 
        }
    }
}
