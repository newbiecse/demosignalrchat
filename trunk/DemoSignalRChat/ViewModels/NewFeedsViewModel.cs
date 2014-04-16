using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class NewFeedsViewModel
    {
        public string NewFeedId { get; set; }
        public int TypeAtionId { get; set; }
        public UserViewModel Friend { get; set; }
        public UserViewModel AnotherUser { get; set; }
        public StatusViewModel Status { get; set; }
        public CommentViewModel Comment { get; set; }
    }
}