namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattributeRequiredtofielduserIdofclassStatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusLinks", new[] { "UserId" });
            AlterColumn("dbo.StatusLinks", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.StatusLinks", "UserId");
            AddForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StatusLinks", new[] { "UserId" });
            AlterColumn("dbo.StatusLinks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.StatusLinks", "UserId");
            AddForeignKey("dbo.StatusLinks", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
