using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityImage { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}