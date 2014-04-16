namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablenewfeeds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewFeeds",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TypeActionId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        StatusId_Or_UserId = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.TypeActionId, t.Time })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewFeeds", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.NewFeeds", new[] { "UserId" });
            DropTable("dbo.NewFeeds");
        }
    }
}
