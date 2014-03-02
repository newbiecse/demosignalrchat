using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class Status
    {
        public Status()
        {
            this.TimePost = DateTime.Now;
        }
        public string StatusId { get; set; }
        public DateTime TimePost { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual StatusLocation StatusLocation { get; set; }
        public virtual StatusMessage StatusMessage { get; set; }
        public virtual ICollection<StatusImage> StatusImages { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
    }
}