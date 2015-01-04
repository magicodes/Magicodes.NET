namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminMenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteAdminNavigations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Text = c.String(maxLength: 100),
                        Href = c.String(maxLength: 300),
                        IconCls = c.String(maxLength: 256),
                        TextCls = c.String(maxLength: 256),
                        isShowBadge = c.Boolean(nullable: false),
                        MenuBadgeType = c.Int(nullable: false),
                        BadgeRequestUrl = c.String(maxLength: 300),
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
            DropTable("dbo.SiteAdminNavigations");
        }
    }
}
