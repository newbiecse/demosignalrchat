using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class StatusMessage
    {
        //[Key]
        public string StatusId { get; set; }
        public virtual Status Status { get; set; }

        public string Message { get; set; }
    }
}