namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShareTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        StatusId = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.StatusId })
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Likes", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shares", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Shares", "StatusId", "dbo.Status");
            DropIndex("dbo.Shares", new[] { "UserId" });
            DropIndex("dbo.Shares", new[] { "StatusId" });
            DropColumn("dbo.Likes", "Time");
            DropTable("dbo.Shares");
        }
    }
}
