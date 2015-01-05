namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PlusId_to_Nav_SiteAdminNavigation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nav_SiteAdminNavigation", "PlusId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nav_SiteAdminNavigation", "PlusId");
        }
    }
}
