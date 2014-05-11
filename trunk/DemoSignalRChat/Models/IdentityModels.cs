using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace DemoSignalRChat.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public int Sex { get; set; }

        public string Avatar { get; set; }

        public virtual ICollection<Friend> FriendList { get; set; }
        public virtual ICollection<Friend> UserList { get; set; }
        public virtual ICollection<Status> StatusList { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessageList_Sent { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessageList_Retrieved { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikeComment> LikeComments { get; set; }
        public virtual ICollection<NewFeeds> NewFeeds { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }

        public virtual ICollection<Plan> PlanMember { get; set; }
    }
}