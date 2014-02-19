namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryrelationuserstatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatusMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusMessages", new[] { "UserId" });
            DropIndex("dbo.StatusLinks", new[] { "UserId" });
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.StatusMessages");
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
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatusMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        TimePost = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Status", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Status", new[] { "UserId" });
            DropTable("dbo.Status");
            CreateIndex("dbo.StatusLinks", "UserId");
            CreateIndex("dbo.StatusMessages", "UserId");
            AddForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StatusMessages", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
