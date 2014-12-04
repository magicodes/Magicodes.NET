namespace Magicodes.Models.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu_MenuLink",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Text = c.String(maxLength: 100),
                        IconCls = c.String(maxLength: 256),
                        TextCls = c.String(maxLength: 256),
                        isShowBadge = c.Boolean(nullable: false),
                        MenuBadgeType = c.Int(nullable: false),
                        BadgeRequestUrl = c.String(maxLength: 300),
                        Href = c.String(maxLength: 300),
                        IsDelete = c.Boolean(nullable: false),
                        UpdateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdateBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account_Users", t => t.UpdateBy_Id)
                .Index(t => t.UpdateBy_Id);
            
            CreateTable(
                "dbo.Account_Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Account_UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Account_Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Account_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Account_Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserNickName = c.String(nullable: false, maxLength: 50),
                        Deleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        LastLoginTime = c.DateTimeOffset(precision: 7),
                        HeadPortrait = c.String(maxLength: 300),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Account_UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Account_UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Account_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Account_RoleMenuLinks",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        MenuLinkId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.MenuLinkId })
                .ForeignKey("dbo.Menu_MenuLink", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Account_Roles", t => t.MenuLinkId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.MenuLinkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menu_MenuLink", "UpdateBy_Id", "dbo.Account_Users");
            DropForeignKey("dbo.Account_UserRoles", "UserId", "dbo.Account_Users");
            DropForeignKey("dbo.Account_UserLogin", "UserId", "dbo.Account_Users");
            DropForeignKey("dbo.Account_UserClaims", "UserId", "dbo.Account_Users");
            DropForeignKey("dbo.Account_RoleMenuLinks", "MenuLinkId", "dbo.Account_Roles");
            DropForeignKey("dbo.Account_RoleMenuLinks", "RoleId", "dbo.Menu_MenuLink");
            DropForeignKey("dbo.Account_UserRoles", "RoleId", "dbo.Account_Roles");
            DropIndex("dbo.Account_RoleMenuLinks", new[] { "MenuLinkId" });
            DropIndex("dbo.Account_RoleMenuLinks", new[] { "RoleId" });
            DropIndex("dbo.Account_UserLogin", new[] { "UserId" });
            DropIndex("dbo.Account_UserClaims", new[] { "UserId" });
            DropIndex("dbo.Account_Users", "UserNameIndex");
            DropIndex("dbo.Account_UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Account_UserRoles", new[] { "UserId" });
            DropIndex("dbo.Account_Roles", "RoleNameIndex");
            DropIndex("dbo.Menu_MenuLink", new[] { "UpdateBy_Id" });
            DropTable("dbo.Account_RoleMenuLinks");
            DropTable("dbo.Account_UserLogin");
            DropTable("dbo.Account_UserClaims");
            DropTable("dbo.Account_Users");
            DropTable("dbo.Account_UserRoles");
            DropTable("dbo.Account_Roles");
            DropTable("dbo.Menu_MenuLink");
        }
    }
}
