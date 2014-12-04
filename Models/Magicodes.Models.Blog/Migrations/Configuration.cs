
using Magicodes.Models.Blog.Models.Account;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Magicodes.Models.Blog.Models.Post;

namespace Magicodes.Models.Blog.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlogDbContext>
    {
        public Configuration()
        {
            //关闭自动生成迁移（让程序只打我们自己生成的迁移）
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BlogDbContext context)
        {
            var blogUser = new BlogUser {Id = "{B0FBB2AC-3174-4E5A-B772-98CF776BD4B9}"};
            var user = context.User.FirstOrDefault(m => m.Id == blogUser.Id);
            if (user!=null)
                blogUser = user;
            else
            context.User.Add(blogUser);
                context.SaveChanges();   
            var subjectList = new List<SubjectCategory> 
            {
                new SubjectCategory 
                { 
                   Title="专题名称_Test",Description="专题描述_Test"
                }
            };
           context.SubjectCategory.AddOrUpdate(l => l.Title, subjectList.ToArray());
           var blogTag = new BlogTag
            {
                Name = "标签_Test"
            };
           var tag = context.BlogTag.FirstOrDefault(m => m.Name == "标签_Test");
            if (tag != null)
                // ReSharper disable once RedundantAssignment
                blogTag = tag;
            else
                context.BlogTag.Add(blogTag);
            context.SaveChanges();
            var blogPost = new BlogPost
            {
                Sender = blogUser,
                Title = "博客标题_Test",
                Content = "<h1>博客内容_Test<h2>",
            };
            var post = context.BlogPost.FirstOrDefault(m => m.Title == blogPost.Title);
            if (post!=null)
                blogPost = post;
            else
                context.BlogPost.Add(blogPost);
            context.SaveChanges();

            var blogPostSubject =  new BlogPostSubject {Post = blogPost, Subject = subjectList[0]};
            var postSubject = context.BlogPostSubject.FirstOrDefault(m => m.PostId == blogPost.Id);
            if (postSubject!=null)
                // ReSharper disable once RedundantAssignment
                blogPostSubject = postSubject;
            else
              context.BlogPostSubject.Add(blogPostSubject);
            context.SaveChanges();
        }
    }
}
