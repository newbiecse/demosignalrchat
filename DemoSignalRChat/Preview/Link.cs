using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public class Link
    {
        public string Url { get; set; }
        public Link(string message)
        {
            List<string> list = new List<string>();
            Regex urlRx = new 
                Regex(@"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?",
                RegexOptions.IgnoreCase);

            this.Url = urlRx.Match(message).Value;
        }
    }
}