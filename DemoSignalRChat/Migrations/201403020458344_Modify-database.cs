namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifydatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatusImages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StatusMessages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusImages", new[] { "UserId" });
            DropIndex("dbo.StatusMessages", new[] { "UserId" });
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.String(nullable: false, maxLength: 128),
                        TimePost = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.StatusId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StatusLocations",
                c => new
                    {
                        StatusId = c.String(nullable: false, maxLength: 128),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.StatusId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
            AddColumn("dbo.StatusImages", "ImageId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.StatusImages", "StatusId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.StatusMessages", "StatusId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.StatusImages");
            AddPrimaryKey("dbo.StatusImages", "ImageId");
            DropPrimaryKey("dbo.StatusMessages");
            AddPrimaryKey("dbo.StatusMessages", "StatusId");
            CreateIndex("dbo.StatusImages", "StatusId");
            CreateIndex("dbo.StatusMessages", "StatusId");
            AddForeignKey("dbo.StatusImages", "StatusId", "dbo.Status", "StatusId", cascadeDelete: true);
            AddForeignKey("dbo.StatusMessages", "StatusId", "dbo.Status", "StatusId", cascadeDelete: true);
            DropColumn("dbo.StatusImages", "Id");
            DropColumn("dbo.StatusImages", "Title");
            DropColumn("dbo.StatusImages", "TimePost");
            DropColumn("dbo.StatusImages", "Location");
            DropColumn("dbo.StatusImages", "UserId");
            DropColumn("dbo.StatusMessages", "Id");
            DropColumn("dbo.StatusMessages", "TimePost");
            DropColumn("dbo.StatusMessages", "Location");
            DropColumn("dbo.StatusMessages", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StatusMessages", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.StatusMessages", "Location", c => c.String());
            AddColumn("dbo.StatusMessages", "TimePost", c => c.DateTime(nullable: false));
            AddColumn("dbo.StatusMessages", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.StatusImages", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.StatusImages", "Location", c => c.String());
            AddColumn("dbo.StatusImages", "TimePost", c => c.DateTime(nullable: false));
            AddColumn("dbo.StatusImages", "Title", c => c.String());
            AddColumn("dbo.StatusImages", "Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Status", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StatusMessages", "StatusId", "dbo.Status");
            DropForeignKey("dbo.StatusLocations", "StatusId", "dbo.Status");
            DropForeignKey("dbo.StatusImages", "StatusId", "dbo.Status");
            DropIndex("dbo.Status", new[] { "UserId" });
            DropIndex("dbo.StatusMessages", new[] { "StatusId" });
            DropIndex("dbo.StatusLocations", new[] { "StatusId" });
            DropIndex("dbo.StatusImages", new[] { "StatusId" });
            DropPrimaryKey("dbo.StatusMessages");
            AddPrimaryKey("dbo.StatusMessages", "Id");
            DropPrimaryKey("dbo.StatusImages");
            AddPrimaryKey("dbo.StatusImages", "Id");
            DropColumn("dbo.StatusMessages", "StatusId");
            DropColumn("dbo.StatusImages", "StatusId");
            DropColumn("dbo.StatusImages", "ImageId");
            DropTable("dbo.StatusLocations");
            DropTable("dbo.Status");
            CreateIndex("dbo.StatusMessages", "UserId");
            CreateIndex("dbo.StatusImages", "UserId");
            AddForeignKey("dbo.StatusMessages", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StatusImages", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
