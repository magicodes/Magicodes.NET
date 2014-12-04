namespace Magicodes.Models.Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogUser_DeleteTest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Blog_User", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blog_User", "Test", c => c.String());
        }
    }
}
