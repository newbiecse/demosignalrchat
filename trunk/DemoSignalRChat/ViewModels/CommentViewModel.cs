using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class CommentViewModel
    {
        public string CommentId { get; set; }
        public DateTime TimeCommented { get; set; }
        public string Content { get; set; }
        public UserViewModel UserOwner { get; set; }
        public IEnumerable<UserViewModel> ListUserLiked { get; set; }
    }
}