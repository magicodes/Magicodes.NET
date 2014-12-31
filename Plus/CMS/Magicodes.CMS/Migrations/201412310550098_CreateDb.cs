namespace Magicodes.CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_Channel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChannelName = c.String(maxLength: 50),
                        Sequence = c.Int(),
                        Url = c.String(),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_ClassType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassTypeName = c.String(maxLength: 50),
                        ParentId = c.Int(),
                        Sequence = c.Int(),
                        AllowComments = c.Boolean(nullable: false),
                        ImageUrl = c.String(maxLength: 300),
                        ThumbImageUrl = c.String(maxLength: 300),
                        NormalImageUrl = c.String(maxLength: 300),
                        ContentType = c.Int(nullable: false),
                        IsAutoGenerateStaticPages = c.Boolean(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentId = c.Int(nullable: false),
                        Comment = c.String(maxLength: 1000),
                        ReplyCount = c.Int(nullable: false),
                        ParentID = c.Int(),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_Content",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        StaticUrl = c.String(maxLength: 300),
                        Title = c.String(maxLength: 50),
                        SubTitle = c.String(maxLength: 50),
                        Summary = c.String(maxLength: 500),
                        ImageUrl = c.String(maxLength: 300),
                        ThumbImageUrl = c.String(maxLength: 300),
                        NormalImageUrl = c.String(maxLength: 300),
                        LinkUrl = c.String(maxLength: 300),
                        PvCount = c.Int(nullable: false),
                        ClassTypeId = c.Int(nullable: false),
                        Keywords = c.String(maxLength: 200),
                        Sequence = c.Int(nullable: false),
                        IsRecomend = c.Boolean(nullable: false),
                        IsHot = c.Boolean(nullable: false),
                        IsColor = c.Boolean(nullable: false),
                        IsTop = c.Boolean(nullable: false),
                        Remary = c.String(maxLength: 500),
                        TotalComment = c.Int(nullable: false),
                        TotalSupport = c.Int(nullable: false),
                        TotalFav = c.Int(nullable: false),
                        TotalShare = c.Int(nullable: false),
                        BeFrom = c.String(maxLength: 50),
                        Meta_Title = c.String(maxLength: 50),
                        Meta_Description = c.String(maxLength: 500),
                        Meta_Keywords = c.String(maxLength: 300),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_ContentTag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagId = c.Guid(nullable: false),
                        ContentId = c.Guid(nullable: false),
                        TotalRecommended = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_Photo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 50),
                        SubTitle = c.String(maxLength: 50),
                        Summary = c.String(maxLength: 500),
                        ImageUrl = c.String(maxLength: 300),
                        ThumbImageUrl = c.String(maxLength: 300),
                        NormalImageUrl = c.String(maxLength: 300),
                        LinkUrl = c.String(maxLength: 300),
                        PvCount = c.Int(nullable: false),
                        ClassTypeId = c.Int(nullable: false),
                        Keywords = c.String(maxLength: 200),
                        Sequence = c.Int(nullable: false),
                        IsRecomend = c.Boolean(nullable: false),
                        IsHot = c.Boolean(nullable: false),
                        IsColor = c.Boolean(nullable: false),
                        IsTop = c.Boolean(nullable: false),
                        Remary = c.String(maxLength: 500),
                        TotalComment = c.Int(nullable: false),
                        TotalSupport = c.Int(nullable: false),
                        TotalFav = c.Int(nullable: false),
                        TotalShare = c.Int(nullable: false),
                        BeFrom = c.String(maxLength: 50),
                        Meta_Title = c.String(maxLength: 50),
                        Meta_Description = c.String(maxLength: 500),
                        Meta_Keywords = c.String(maxLength: 300),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_Tag",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 50),
                        Color = c.String(maxLength: 20),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CMS_Video",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TotalTime = c.Int(),
                        VideoUrl = c.String(),
                        UrlType = c.Short(nullable: false),
                        VideoFormat = c.String(),
                        Domain = c.String(),
                        Grade = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        SubTitle = c.String(maxLength: 50),
                        Summary = c.String(maxLength: 500),
                        ImageUrl = c.String(maxLength: 300),
                        ThumbImageUrl = c.String(maxLength: 300),
                        NormalImageUrl = c.String(maxLength: 300),
                        LinkUrl = c.String(maxLength: 300),
                        PvCount = c.Int(nullable: false),
                        ClassTypeId = c.Int(nullable: false),
                        Keywords = c.String(maxLength: 200),
                        Sequence = c.Int(nullable: false),
                        IsRecomend = c.Boolean(nullable: false),
                        IsHot = c.Boolean(nullable: false),
                        IsColor = c.Boolean(nullable: false),
                        IsTop = c.Boolean(nullable: false),
                        Remary = c.String(maxLength: 500),
                        TotalComment = c.Int(nullable: false),
                        TotalSupport = c.Int(nullable: false),
                        TotalFav = c.Int(nullable: false),
                        TotalShare = c.Int(nullable: false),
                        BeFrom = c.String(maxLength: 50),
                        Meta_Title = c.String(maxLength: 50),
                        Meta_Description = c.String(maxLength: 500),
                        Meta_Keywords = c.String(maxLength: 300),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CMS_Video");
            DropTable("dbo.CMS_Tag");
            DropTable("dbo.CMS_Photo");
            DropTable("dbo.CMS_ContentTag");
            DropTable("dbo.CMS_Content");
            DropTable("dbo.CMS_Comment");
            DropTable("dbo.CMS_ClassType");
            DropTable("dbo.CMS_Channel");
        }
    }
}
