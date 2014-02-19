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
        public int Id { get; set; }
        public DateTime TimePost { get; set; }
        public string Location { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}