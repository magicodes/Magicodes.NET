namespace Magicodes.Models.Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog_Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                        BlowNum = c.Int(nullable: false),
                        ReplyNum = c.Int(nullable: false),
                        PraiseNum = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog_User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Blog_PostReply",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, unicode: false),
                        PostId = c.Int(nullable: false),
                        UerId = c.String(nullable: false, maxLength: 128),
                        ParentId = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog_Post", t => t.PostId)
                .ForeignKey("dbo.Blog_User", t => t.UerId)
                .Index(t => t.PostId)
                .Index(t => t.UerId);
            
            CreateTable(
                "dbo.Blog_User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FansNum = c.Int(nullable: false),
                        FocusNum = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blog_BlogUser_FocusUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogUerId = c.String(nullable: false, maxLength: 128),
                        FocusUserId = c.String(nullable: false, maxLength: 128),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog_User", t => t.BlogUerId)
                .ForeignKey("dbo.Blog_User", t => t.FocusUserId)
                .Index(t => t.BlogUerId)
                .Index(t => t.FocusUserId);
            
            CreateTable(
                "dbo.Blog_SubjectCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        ParentId = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blog_Post_Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog_Post", t => t.PostId)
                .ForeignKey("dbo.Blog_SubjectCategory", t => t.SubjectId)
                .Index(t => t.PostId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Blog_User_FocusSubject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog_User", t => t.UserId)
                .ForeignKey("dbo.Blog_SubjectCategory", t => t.SubjectId)
                .Index(t => t.UserId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Blog_Post_PraiseUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog_Post", t => t.PostId)
                .ForeignKey("dbo.Blog_User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Blog_Post_Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        BlogTagId = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blog_Post", t => t.PostId)
                .ForeignKey("dbo.Blog_Tag", t => t.BlogTagId)
                .Index(t => t.PostId)
                .Index(t => t.BlogTagId);
            
            CreateTable(
                "dbo.Blog_Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectCategoryBlogUsers",
                c => new
                    {
                        SubjectCategory_Id = c.Int(nullable: false),
                        BlogUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SubjectCategory_Id, t.BlogUser_Id })
                .ForeignKey("dbo.Blog_SubjectCategory", t => t.SubjectCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.Blog_User", t => t.BlogUser_Id, cascadeDelete: true)
                .Index(t => t.SubjectCategory_Id)
                .Index(t => t.BlogUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blog_Post", "UserId", "dbo.Blog_User");
            DropForeignKey("dbo.Blog_Post_Tag", "BlogTagId", "dbo.Blog_Tag");
            DropForeignKey("dbo.Blog_Post_Tag", "PostId", "dbo.Blog_Post");
            DropForeignKey("dbo.Blog_PostReply", "UerId", "dbo.Blog_User");
            DropForeignKey("dbo.Blog_Post_PraiseUser", "UserId", "dbo.Blog_User");
            DropForeignKey("dbo.Blog_Post_PraiseUser", "PostId", "dbo.Blog_Post");
            DropForeignKey("dbo.Blog_User_FocusSubject", "SubjectId", "dbo.Blog_SubjectCategory");
            DropForeignKey("dbo.Blog_User_FocusSubject", "UserId", "dbo.Blog_User");
            DropForeignKey("dbo.SubjectCategoryBlogUsers", "BlogUser_Id", "dbo.Blog_User");
            DropForeignKey("dbo.SubjectCategoryBlogUsers", "SubjectCategory_Id", "dbo.Blog_SubjectCategory");
            DropForeignKey("dbo.Blog_Post_Subject", "SubjectId", "dbo.Blog_SubjectCategory");
            DropForeignKey("dbo.Blog_Post_Subject", "PostId", "dbo.Blog_Post");
            DropForeignKey("dbo.Blog_BlogUser_FocusUser", "FocusUserId", "dbo.Blog_User");
            DropForeignKey("dbo.Blog_BlogUser_FocusUser", "BlogUerId", "dbo.Blog_User");
            DropForeignKey("dbo.Blog_PostReply", "PostId", "dbo.Blog_Post");
            DropIndex("dbo.SubjectCategoryBlogUsers", new[] { "BlogUser_Id" });
            DropIndex("dbo.SubjectCategoryBlogUsers", new[] { "SubjectCategory_Id" });
            DropIndex("dbo.Blog_Post_Tag", new[] { "BlogTagId" });
            DropIndex("dbo.Blog_Post_Tag", new[] { "PostId" });
            DropIndex("dbo.Blog_Post_PraiseUser", new[] { "PostId" });
            DropIndex("dbo.Blog_Post_PraiseUser", new[] { "UserId" });
            DropIndex("dbo.Blog_User_FocusSubject", new[] { "SubjectId" });
            DropIndex("dbo.Blog_User_FocusSubject", new[] { "UserId" });
            DropIndex("dbo.Blog_Post_Subject", new[] { "SubjectId" });
            DropIndex("dbo.Blog_Post_Subject", new[] { "PostId" });
            DropIndex("dbo.Blog_BlogUser_FocusUser", new[] { "FocusUserId" });
            DropIndex("dbo.Blog_BlogUser_FocusUser", new[] { "BlogUerId" });
            DropIndex("dbo.Blog_PostReply", new[] { "UerId" });
            DropIndex("dbo.Blog_PostReply", new[] { "PostId" });
            DropIndex("dbo.Blog_Post", new[] { "UserId" });
            DropTable("dbo.SubjectCategoryBlogUsers");
            DropTable("dbo.Blog_Tag");
            DropTable("dbo.Blog_Post_Tag");
            DropTable("dbo.Blog_Post_PraiseUser");
            DropTable("dbo.Blog_User_FocusSubject");
            DropTable("dbo.Blog_Post_Subject");
            DropTable("dbo.Blog_SubjectCategory");
            DropTable("dbo.Blog_BlogUser_FocusUser");
            DropTable("dbo.Blog_User");
            DropTable("dbo.Blog_PostReply");
            DropTable("dbo.Blog_Post");
        }
    }
}
