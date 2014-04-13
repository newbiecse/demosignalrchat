using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public static class ProcessComment
    {
        public static string ProcessNewComment(UserViewModel userComment, string message)
        {
            var messageProcessed = Emotions.ReplaceEmotions(message);
            messageProcessed = Link.ReplaceAllLink(messageProcessed);

            return
            "<div class='comment'>"
                + "<div class='comment-user'>"
                    + "<a href='#'>"
                        + "<img src='" + userComment.Avatar + "' class='img-rounded img33x33'>"
                    + "</a>"
                + "</div>"
                + "<div class='comment-content'>"
                    + "<p>"
                        + "<a href='#'>" + userComment.UserName + "</a>"
                        + "&nbsp;&nbsp;"
                        + messageProcessed
                    + "</p>"
                    + "<p>" + DateTime.Now + " . <a href='#'>like</a></p>"
                + "</div>"
                + "<div class='clear-left'></div>"
            + "</div>";
        }

        public static string ProcessMessage(string message)
        {
            var messageProcessed = Emotions.ReplaceEmotions(message);
            messageProcessed = Link.ReplaceAllLink(messageProcessed);
            return messageProcessed;
        }
    }
}