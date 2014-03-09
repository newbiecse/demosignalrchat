namespace DemoSignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.String(nullable: false, maxLength: 128),
                        TimeComment = c.DateTime(nullable: false),
                        Content = c.String(),
                        UserId = c.String(nullable: false, maxLength: 128),
                        StatusId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LikeComments",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CommentId = c.String(nullable: false, maxLength: 128),
                        TimeLike = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CommentId })
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Sex = c.Int(),
                        Avatar = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        FriendStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FriendId, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .Index(t => t.UserId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        StatusId = c.String(nullable: false, maxLength: 128),
                        TimeLiked = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.StatusId })
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.String(nullable: false, maxLength: 128),
                        TimePost = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.StatusId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        StatusId = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.StatusId })
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.StatusImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        StatusId = c.String(nullable: false, maxLength: 128),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.StatusLocations",
                c => new
                    {
                        StatusId = c.String(nullable: false, maxLength: 128),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.StatusId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.StatusMessages",
                c => new
                    {
                        StatusId = c.String(nullable: false, maxLength: 128),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.StatusId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeSent = c.DateTime(nullable: false),
                        Content = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        UserSent_Id = c.String(nullable: false, maxLength: 128),
                        UserRetrieved_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserRetrieved_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserSent_Id)
                .Index(t => t.UserRetrieved_Id)
                .Index(t => t.UserSent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "StatusId", "dbo.Status");
            DropForeignKey("dbo.LikeComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "UserSent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessages", "UserRetrieved_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Status", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StatusMessages", "StatusId", "dbo.Status");
            DropForeignKey("dbo.StatusLocations", "StatusId", "dbo.Status");
            DropForeignKey("dbo.StatusImages", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Shares", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Shares", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Friends", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeComments", "CommentId", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "StatusId" });
            DropIndex("dbo.LikeComments", new[] { "UserId" });
            DropIndex("dbo.PrivateMessages", new[] { "UserSent_Id" });
            DropIndex("dbo.PrivateMessages", new[] { "UserRetrieved_Id" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "StatusId" });
            DropIndex("dbo.Status", new[] { "UserId" });
            DropIndex("dbo.StatusMessages", new[] { "StatusId" });
            DropIndex("dbo.StatusLocations", new[] { "StatusId" });
            DropIndex("dbo.StatusImages", new[] { "StatusId" });
            DropIndex("dbo.Shares", new[] { "UserId" });
            DropIndex("dbo.Shares", new[] { "StatusId" });
            DropIndex("dbo.Friends", new[] { "FriendId" });
            DropIndex("dbo.Friends", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.LikeComments", new[] { "CommentId" });
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.StatusMessages");
            DropTable("dbo.StatusLocations");
            DropTable("dbo.StatusImages");
            DropTable("dbo.Shares");
            DropTable("dbo.Status");
            DropTable("dbo.Likes");
            DropTable("dbo.Friends");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.LikeComments");
            DropTable("dbo.Comments");
        }
    }
}
