namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelationplancity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanCities",
                c => new
                    {
                        Plan_PlanId = c.String(nullable: false, maxLength: 128),
                        City_CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Plan_PlanId, t.City_CityId })
                .ForeignKey("dbo.Plans", t => t.Plan_PlanId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.City_CityId, cascadeDelete: true)
                .Index(t => t.Plan_PlanId)
                .Index(t => t.City_CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanCities", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.PlanCities", "Plan_PlanId", "dbo.Plans");
            DropIndex("dbo.PlanCities", new[] { "City_CityId" });
            DropIndex("dbo.PlanCities", new[] { "Plan_PlanId" });
            DropTable("dbo.PlanCities");
        }
    }
}
