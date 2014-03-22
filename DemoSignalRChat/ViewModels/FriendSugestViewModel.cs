using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class FriendSugestViewModel : UserViewModel
    {
        public IEnumerable<UserViewModel> ListFriendMutual { get; set; }
    }
}