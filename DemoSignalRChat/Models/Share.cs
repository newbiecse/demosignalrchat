using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class Share
    {
        public string StatusId { get; set; }
        public string UserId { get; set; }

        public DateTime TimeShared { get; set; }

        public virtual Status Status { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}