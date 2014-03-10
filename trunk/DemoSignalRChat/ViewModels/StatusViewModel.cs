using DemoSignalRChat.Preview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class StatusViewModel
    {
        public string StatusId { get; set; }
        public DateTime TimePost { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }

        public LinkPreview LinkPreview { get; set; }

        public IEnumerable<string> Images { get; set; }
        public UserViewModel UserOwner { get; set; }
        public IEnumerable<UserViewModel> ListUserLiked { get; set; }
        public int NumShared { get; set; }
        public IEnumerable<CommentViewModel> ListCommented { get; set; }
        public bool IsLiked { get; set; }
    }
}