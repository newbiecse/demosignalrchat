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
        public DbSet<StatusMessage> StatusMessages { get; set; }
        public DbSet<StatusLink> StatusLinks { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }

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
            modelBuilder.Entity<StatusMessage>()
                .HasRequired<ApplicationUser>(s => s.User)
                .WithMany(u => u.StatusMessageList)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(true);
            #endregion

            #region config-relation user-statusLink
            // one-to-many
            modelBuilder.Entity<StatusLink>()
                .HasRequired<ApplicationUser>(s => s.User)
                .WithMany(u => u.StatusLinkList)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(true);
            #endregion

            #region config-relation user-statusImage
            // one-to-many
            modelBuilder.Entity<StatusImage>()
                .HasRequired<ApplicationUser>(s => s.User)
                .WithMany(u => u.StatusImageList)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(true);
            #endregion


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