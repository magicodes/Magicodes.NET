namespace Magicodes.Models.Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Blog_Tag", "Name1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blog_Tag", "Name1", c => c.String());
        }
    }
}
