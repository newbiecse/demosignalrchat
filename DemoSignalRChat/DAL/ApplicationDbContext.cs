using DemoSignalRChat.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            //public DbSet<ApplicationUser> Users { get; set; }
        }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<StatusMessage> StatusMessages { get; set; }
        public DbSet<StatusLocation> StatusLocations { get; set; }
        public DbSet<StatusImage> StatusImages { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }

        public DbSet<Like> Likes { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikeComment> LikeComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region config-relation user-friend
            // one-to-many Friend
            modelBuilder.Entity<Friend>()
                .HasRequired<ApplicationUser>(f => f.User)
                .WithMany(u => u.UserList)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Friend>()
                .HasRequired<ApplicationUser>(f => f.UserFriend)
                .WithMany(u => u.FriendList)
                .HasForeignKey(f => f.FriendId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Friend>()
                .HasKey(f => new { f.FriendId, f.UserId });
            #endregion

            #region config-relation user-statusMessage
            // one-to-many Friend
            modelBuilder.Entity<Status>()
                .HasRequired<ApplicationUser>(s => s.User)
                .WithMany(u => u.StatusList)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(true);
            #endregion


            modelBuilder.Entity<StatusMessage>()
                .HasKey(msg => msg.StatusId)
                .HasRequired<Status>(msg => msg.Status)
                .WithRequiredDependent(s => s.StatusMessage)
                //.WithRequiredPrincipal(s => s.StatusMessage)
                //.HasForeignKey(msg => msg.StatusId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<StatusLocation>()
                .HasKey(loc => loc.StatusId)
                .HasRequired<Status>(loc => loc.Status)
                .WithRequiredDependent(s => s.StatusLocation)
                //.WithRequiredPrincipal(s => s.StatusMessage)
                //.HasForeignKey(msg => msg.StatusId)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<StatusImage>()
                .HasKey(img => img.ImageId)
                .HasRequired<Status>(img => img.Status)
                .WithMany(s => s.StatusImages)
                .HasForeignKey(img => img.StatusId)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<Like>()
                .HasKey(l => new{ l.UserId , l.StatusId})
                .HasRequired<Status>(l => l.Status)
                .WithMany(s => s.Likes)
                .HasForeignKey(l => l.StatusId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Like>()
                .HasRequired<ApplicationUser>(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Share>()
                .HasKey(l => new { l.UserId, l.StatusId })
                .HasRequired<Status>(l => l.Status)
                .WithMany(s => s.Shares)
                .HasForeignKey(l => l.StatusId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Share>()
                .HasRequired<ApplicationUser>(l => l.User)
                .WithMany(u => u.Shares)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasRequired<ApplicationUser>(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasRequired<Status>(c => c.Status)
                .WithMany(s => s.Comments)
                .HasForeignKey(s => s.StatusId)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<LikeComment>()
                .HasKey(lc => new { lc.UserId, lc.CommentId })
                .HasRequired<Comment>(l => l.Comment)
                .WithMany(c => c.LikeComments)
                .HasForeignKey(lc => lc.CommentId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<LikeComment>()
                .HasRequired<ApplicationUser>(lc => lc.User)
                .WithMany(u => u.LikeComments)
                .HasForeignKey(lc => lc.UserId)
                .WillCascadeOnDelete(false);


            #region config-relation user-privateMessage
            // one-to-many
            modelBuilder.Entity<PrivateMessage>()
                .HasRequired<ApplicationUser>(u => u.UserSent)
                .WithMany(PMsg => PMsg.PrivateMessageList_Sent)
                .HasForeignKey(PMsg => PMsg.UserSent_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrivateMessage>()
                .HasRequired<ApplicationUser>(u => u.UserRetrieved)
                .WithMany(PMsg => PMsg.PrivateMessageList_Retrieved)
                .HasForeignKey(PMsg => PMsg.UserRetrieved_Id)
                .WillCascadeOnDelete(false);

            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}