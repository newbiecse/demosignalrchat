using DemoSignalRChat.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Displayname { get; set; }

        public string Avatar { get; set; }
    }
}