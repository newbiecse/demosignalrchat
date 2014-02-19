namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class completerelationuserstatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Status", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Status", new[] { "UserId" });
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
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            DropTable("dbo.Status");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimePost = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Image = c.String(),
                        ContentSumary = c.String(),
                        Href = c.String(),
                        Message = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.StatusMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusMessages", new[] { "UserId" });
            DropIndex("dbo.StatusLinks", new[] { "UserId" });
            DropTable("dbo.StatusMessages");
            DropTable("dbo.StatusLinks");
            CreateIndex("dbo.Status", "UserId");
            AddForeignKey("dbo.Status", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
