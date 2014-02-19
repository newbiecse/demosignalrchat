namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablestatusMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        TimePost = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Friends", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "Status", c => c.Int(nullable: false));
            DropForeignKey("dbo.StatusMessages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusMessages", new[] { "UserId" });
            DropTable("dbo.StatusMessages");
        }
    }
}
