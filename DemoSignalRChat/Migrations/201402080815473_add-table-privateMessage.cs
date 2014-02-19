namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableprivateMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeSent = c.DateTime(nullable: false),
                        Content = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        UserFrom_Id = c.String(nullable: false, maxLength: 128),
                        UserTo_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserFrom_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserTo_Id)
                .Index(t => t.UserFrom_Id)
                .Index(t => t.UserTo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateMessages", "UserTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "UserFrom_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PrivateMessages", new[] { "UserTo_Id" });
            DropIndex("dbo.PrivateMessages", new[] { "UserFrom_Id" });
            DropTable("dbo.PrivateMessages");
        }
    }
}
