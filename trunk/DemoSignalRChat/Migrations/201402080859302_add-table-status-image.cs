namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablestatusimage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        Location = c.String(),
                        TimePost = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.StatusMessages", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusImages", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusImages", new[] { "UserId" });
            DropColumn("dbo.StatusMessages", "Location");
            DropTable("dbo.StatusImages");
        }
    }
}
