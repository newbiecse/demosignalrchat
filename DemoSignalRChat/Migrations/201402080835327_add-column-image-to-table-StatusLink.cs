namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnimagetotableStatusLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusLinks", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusLinks", "Image");
        }
    }
}
