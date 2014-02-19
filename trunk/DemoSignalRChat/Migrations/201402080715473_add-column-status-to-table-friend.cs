namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnstatustotablefriend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "Status");
        }
    }
}
