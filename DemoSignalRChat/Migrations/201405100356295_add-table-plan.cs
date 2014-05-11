namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableplan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        PlanId = c.String(nullable: false, maxLength: 128),
                        AdminId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PlanId)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId, cascadeDelete: true)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.PlanStatus",
                c => new
                    {
                        PlanId = c.String(nullable: false, maxLength: 128),
                        StatusId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PlanId, t.StatusId })
                .ForeignKey("dbo.Plans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plans", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PlanStatus", "PlanId", "dbo.Plans");
            DropIndex("dbo.Plans", new[] { "AdminId" });
            DropIndex("dbo.PlanStatus", new[] { "PlanId" });
            DropTable("dbo.PlanStatus");
            DropTable("dbo.Plans");
        }
    }
}
