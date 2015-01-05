namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Table_to_Nav_SiteAdminNavigation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SiteAdminNavigations", newName: "Nav_SiteAdminNavigation");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Nav_SiteAdminNavigation", newName: "SiteAdminNavigations");
        }
    }
}
