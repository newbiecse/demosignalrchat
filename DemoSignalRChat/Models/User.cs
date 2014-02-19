using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class User : ApplicationUser
    {
        public string ConnectionID { get; set; }
    }
}