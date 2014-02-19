using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class MessageDetail
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Message { get; set; }
    }
}