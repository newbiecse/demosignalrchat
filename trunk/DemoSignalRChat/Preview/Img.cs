using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public class Img
    {
        static int GetInt(string text)
        {
            int number = 0;
            Int32.TryParse(text, out number);
            return number;
        }
        public Img(string height, string width, string src)
        {
            this.Height = Img.GetInt(height);
            this.Width = Img.GetInt(width);
            this.Src = src;
        }
        public string Src { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public static string GetSrcLargetestImage(List<Img> images)
        {
            List<Img> imagesSorted = (from i in images
                                      select i).OrderByDescending(i => i.Height * i.Width).ToList();
            if (imagesSorted != null && imagesSorted.Count > 0)
            { 
                return imagesSorted.First().Src;
            }
            return "";
        }
    }
}