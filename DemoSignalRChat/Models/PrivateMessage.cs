using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class PrivateMessage
    {
        public PrivateMessage()
        {
            this.IsRead = false;
            this.TimeSent = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime TimeSent { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public string UserSent_Id { get; set; }
        public virtual ApplicationUser UserSent { get; set; }

        public string UserRetrieved_Id { get; set; }
        public virtual ApplicationUser UserRetrieved { get; set; }
    }
}