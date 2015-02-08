namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Nav : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Nav_SiteAdminNavigation", "Text", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nav_SiteAdminNavigation", "Text", c => c.String(maxLength: 100));
        }
    }
}
