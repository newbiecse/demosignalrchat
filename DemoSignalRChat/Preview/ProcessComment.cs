using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public static class ProcessComment
    {
        public static string ProcessMessage(UserViewModel userComment, string message)
        {
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
                                + message
                            + "</p>"
                        + "</div>"
                    + "</div>";
        }
    }
}