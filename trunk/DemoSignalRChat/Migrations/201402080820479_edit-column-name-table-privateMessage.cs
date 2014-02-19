namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcolumnnametableprivateMessage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateMessages", "UserTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "UserFrom_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PrivateMessages", new[] { "UserTo_Id" });
            DropIndex("dbo.PrivateMessages", new[] { "UserFrom_Id" });
            AddColumn("dbo.PrivateMessages", "UserSent_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PrivateMessages", "UserRetrieved_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.PrivateMessages", "UserRetrieved_Id");
            CreateIndex("dbo.PrivateMessages", "UserSent_Id");
            AddForeignKey("dbo.PrivateMessages", "UserRetrieved_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrivateMessages", "UserSent_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.PrivateMessages", "UserFrom_Id");
            DropColumn("dbo.PrivateMessages", "UserTo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateMessages", "UserTo_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PrivateMessages", "UserFrom_Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.PrivateMessages", "UserSent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "UserRetrieved_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PrivateMessages", new[] { "UserSent_Id" });
            DropIndex("dbo.PrivateMessages", new[] { "UserRetrieved_Id" });
            DropColumn("dbo.PrivateMessages", "UserRetrieved_Id");
            DropColumn("dbo.PrivateMessages", "UserSent_Id");
            CreateIndex("dbo.PrivateMessages", "UserFrom_Id");
            CreateIndex("dbo.PrivateMessages", "UserTo_Id");
            AddForeignKey("dbo.PrivateMessages", "UserFrom_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrivateMessages", "UserTo_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
