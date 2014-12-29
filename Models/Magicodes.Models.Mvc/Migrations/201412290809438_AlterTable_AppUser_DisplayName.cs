namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTable_AppUser_DisplayName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account_Users", "DisplayName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Account_Users", "UserNickName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account_Users", "UserNickName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Account_Users", "DisplayName");
        }
    }
}
