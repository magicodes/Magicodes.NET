namespace Magicodes.CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChannelId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_ClassType", "ChannelId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_ClassType", "ChannelId");
        }
    }
}
