using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public class Link
    {
        public static string GetFirstLink(string message)
        {
            List<string> list = new List<string>();
            Regex urlRx = new 
                Regex(@"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?",
                RegexOptions.IgnoreCase);

            return urlRx.Match(message).Value;
        }

        public static string ReplaceAllLink(string message)
        {
            List<string> list = new List<string>();
            Regex urlRx = new
                Regex(@"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?",
                RegexOptions.IgnoreCase);

            MatchCollection mactches = urlRx.Matches(message);
            foreach (Match match in mactches)
            {
                message = message.Replace(match.Value, String.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", match.Value));
            }

            return message;
        }
    }
}