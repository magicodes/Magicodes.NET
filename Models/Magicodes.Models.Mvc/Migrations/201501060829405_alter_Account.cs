namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_Account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account_Roles", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Account_Roles", "CreateBy", c => c.String(maxLength: 128));
            AddColumn("dbo.Account_Roles", "UpdateBy", c => c.String(maxLength: 128));
            AddColumn("dbo.Account_Roles", "UpdateTime", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Account_Roles", "CreateTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Account_Users", "CreateBy", c => c.String(maxLength: 128));
            AddColumn("dbo.Account_Users", "UpdateBy", c => c.String(maxLength: 128));
            AddColumn("dbo.Account_Users", "UpdateTime", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account_Users", "UpdateTime");
            DropColumn("dbo.Account_Users", "UpdateBy");
            DropColumn("dbo.Account_Users", "CreateBy");
            DropColumn("dbo.Account_Roles", "CreateTime");
            DropColumn("dbo.Account_Roles", "UpdateTime");
            DropColumn("dbo.Account_Roles", "UpdateBy");
            DropColumn("dbo.Account_Roles", "CreateBy");
            DropColumn("dbo.Account_Roles", "Deleted");
        }
    }
}
