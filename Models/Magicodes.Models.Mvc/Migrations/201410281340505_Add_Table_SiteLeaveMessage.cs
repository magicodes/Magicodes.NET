namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_SiteLeaveMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Site_LeaveMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
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
            DropTable("dbo.Site_LeaveMessage");
        }
    }
}
