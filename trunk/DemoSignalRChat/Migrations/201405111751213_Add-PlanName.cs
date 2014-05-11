namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlanName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plans", "PlanName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plans", "PlanName");
        }
    }
}
