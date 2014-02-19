namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableStatusLink : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ContentSumary = c.String(),
                        Href = c.String(),
                        TimePost = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusLinks", new[] { "UserId" });
            DropTable("dbo.StatusLinks");
        }
    }
}
