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
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string FriendId { get; set; }
        public virtual ApplicationUser UserFriend { get; set; }

        // 0: UserId Sent require add friend to FriendId
        // 1: UserId and FriendId was friend
        public int FriendStatus { get; set; }
    }
}