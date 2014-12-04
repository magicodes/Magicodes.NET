namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Account_RoleMenuLinksUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Account_RoleMenuLinks", new[] { "RoleId" });
            DropIndex("dbo.Account_RoleMenuLinks", new[] { "MenuLinkId" });
            RenameColumn(table: "dbo.Account_RoleMenuLinks", name: "RoleId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Account_RoleMenuLinks", name: "MenuLinkId", newName: "RoleId");
            RenameColumn(table: "dbo.Account_RoleMenuLinks", name: "__mig_tmp__0", newName: "MenuLinkId");
            DropPrimaryKey("dbo.Account_RoleMenuLinks");
            AlterColumn("dbo.Account_RoleMenuLinks", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Account_RoleMenuLinks", "MenuLinkId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Account_RoleMenuLinks", new[] { "MenuLinkId", "RoleId" });
            CreateIndex("dbo.Account_RoleMenuLinks", "MenuLinkId");
            CreateIndex("dbo.Account_RoleMenuLinks", "RoleId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Account_RoleMenuLinks", new[] { "RoleId" });
            DropIndex("dbo.Account_RoleMenuLinks", new[] { "MenuLinkId" });
            DropPrimaryKey("dbo.Account_RoleMenuLinks");
            AlterColumn("dbo.Account_RoleMenuLinks", "MenuLinkId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Account_RoleMenuLinks", "RoleId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Account_RoleMenuLinks", new[] { "RoleId", "MenuLinkId" });
            RenameColumn(table: "dbo.Account_RoleMenuLinks", name: "MenuLinkId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Account_RoleMenuLinks", name: "RoleId", newName: "MenuLinkId");
            RenameColumn(table: "dbo.Account_RoleMenuLinks", name: "__mig_tmp__0", newName: "RoleId");
            CreateIndex("dbo.Account_RoleMenuLinks", "MenuLinkId");
            CreateIndex("dbo.Account_RoleMenuLinks", "RoleId");
        }
    }
}
