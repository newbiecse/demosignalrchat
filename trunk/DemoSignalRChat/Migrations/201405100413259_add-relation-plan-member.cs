namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelationplanmember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserPlans",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Plan_PlanId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Plan_PlanId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: false)
                .ForeignKey("dbo.Plans", t => t.Plan_PlanId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Plan_PlanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserPlans", "Plan_PlanId", "dbo.Plans");
            DropForeignKey("dbo.ApplicationUserPlans", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserPlans", new[] { "Plan_PlanId" });
            DropIndex("dbo.ApplicationUserPlans", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserPlans");
        }
    }
}
