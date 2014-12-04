namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable_Publish : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publish_Nuget",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CmdText = c.String(nullable: false, maxLength: 500),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateTime = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 128),
                        UpdateBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publish_Version",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false, maxLength: 4000),
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
            DropTable("dbo.Publish_Version");
            DropTable("dbo.Publish_Nuget");
        }
    }
}
