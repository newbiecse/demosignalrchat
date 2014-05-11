using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class Plan
    {
        public string PlanId { get; set; }

        public string PlanName { get; set; }

        public string AdminId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<PlanStatus> PlanStatus { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }
    }
}