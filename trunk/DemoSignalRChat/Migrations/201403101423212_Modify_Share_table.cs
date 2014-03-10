namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Share_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shares", "TimeShared", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.Shares");
            AddPrimaryKey("dbo.Shares", new[] { "UserId", "StatusId", "TimeShared" });
            DropColumn("dbo.Shares", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shares", "Time", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.Shares");
            AddPrimaryKey("dbo.Shares", new[] { "UserId", "StatusId" });
            DropColumn("dbo.Shares", "TimeShared");
        }
    }
}
