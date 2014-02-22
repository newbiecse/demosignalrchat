namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletestatuslink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusLinks", new[] { "UserId" });
            DropTable("dbo.StatusLinks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StatusLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        ContentSumary = c.String(),
                        Href = c.String(),
                        TimePost = c.DateTime(nullable: false),
                        Location = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.StatusLinks", "UserId");
            AddForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
