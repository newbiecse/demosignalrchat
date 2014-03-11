using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class UserChatViewModel : UserViewModel
    {
        public string ConnectionId { get; set; }
    }
}