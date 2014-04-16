namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeprimatykeytablenewfeeds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewFeeds", "NewFeedId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.NewFeeds");
            AddPrimaryKey("dbo.NewFeeds", "NewFeedId");
            DropColumn("dbo.NewFeeds", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewFeeds", "Time", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.NewFeeds");
            AddPrimaryKey("dbo.NewFeeds", new[] { "UserId", "TypeActionId", "Time" });
            DropColumn("dbo.NewFeeds", "NewFeedId");
        }
    }
}
