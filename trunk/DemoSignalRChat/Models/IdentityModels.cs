using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace DemoSignalRChat.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int Sex { get; set; }

        public virtual ICollection<Friend> FriendList { get; set; }
        public virtual ICollection<Friend> UserList { get; set; }
        public virtual ICollection<StatusMessage> StatusMessageList { get; set; }
        public virtual ICollection<StatusLink> StatusLinkList { get; set; }
        public virtual ICollection<StatusImage> StatusImageList { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessageList_Sent { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessageList_Retrieved { get; set; }
    }
}