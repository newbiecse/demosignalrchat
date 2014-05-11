using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class PlanStatus
    {
        public string PlanId { get; set; }
        public string StatusId { get; set; }

        public virtual Plan Plan { get; set; }
    }
}