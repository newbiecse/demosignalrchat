using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class NewFeeds
    {
        public string NewFeedId { get; set; }
        public string UserId { get; set; }
        public string StatusId_Or_UserId { get; set; }
        //0 -> poststatus
        //1 -> comment
        //2 -> Like
        //3 -> Share
        //4 -> addfriend
        public int TypeActionId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}