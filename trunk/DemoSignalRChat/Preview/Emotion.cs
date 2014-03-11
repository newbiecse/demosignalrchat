using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public static class Emotions
    {
        private static string InsertImage(string emotionName)
        {
            var url = "/images/Emotions/";
            return "<img src='" + url + emotionName + "' />";
        }

        //public static string ReplaceEmotion(string specialKey, string image)
        //{
        //    return 
        //}

        public static string ReplaceEmotions(string text)
        {

            return text.Replace(":)", Emotions.InsertImage("happy.gif"))
                        .Replace(":(", Emotions.InsertImage("sad.gif"))
                        .Replace(";)", Emotions.InsertImage("winking.gif"))
                        .Replace(";D", Emotions.InsertImage("big_grin.gif"))

                        .Replace(":-c", Emotions.InsertImage("call_me.gif"))
                        .Replace(":-*", Emotions.InsertImage("kiss.gif"))
                        .Replace(":-SS", Emotions.InsertImage("applause.gif"))
                        .Replace(":v", Emotions.InsertImage("pacman.png"))
                        .Replace("^_^", Emotions.InsertImage("kiki.png"))
                        .Replace("B|", Emotions.InsertImage("sunglasses.png"));                      
        }
    }
}