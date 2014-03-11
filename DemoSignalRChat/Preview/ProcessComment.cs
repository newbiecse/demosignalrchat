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
                     "<div class='row comment'>"
                        + "<div class='col-md-1'>"
                            + "<a href='#'>"
                                + "<img src='" + userComment.Avatar + "' class='img-rounded img33x33'>"
                            + "</a>"
                        + "</div>"
                        + "<div class='col-md-10'>"
                            + "<p class='comment-content'>"
                                + "<a href='#'>" + userComment.UserName + "</a>&nbsp;"
                                + messageProcessed
                            + "</p>"
                        + "</div>"
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