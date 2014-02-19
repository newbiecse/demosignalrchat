namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlocationtostatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusLinks", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusLinks", "Location");
        }
    }
}
