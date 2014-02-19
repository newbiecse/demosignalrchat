using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class Friend
    {
        public string FriendId { get; set; }
        public virtual ApplicationUser UserFriend { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}