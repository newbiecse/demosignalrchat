using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class UserChatViewModel
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
    }
}