namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Col_SortNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nav_SiteAdminNavigation", "SortNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nav_SiteAdminNavigation", "SortNo");
        }
    }
}
