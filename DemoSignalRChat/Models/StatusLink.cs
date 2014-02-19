using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class StatusLink : Status
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string ContentSumary { get; set; }
        public string Href { get; set; }
    }
}