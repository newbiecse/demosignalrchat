using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public class ProcessMessage
    {
        public static string ProcessMessageStatus(UserChatViewModel curUser, string message)
        {
            // Get first Url Preview
            //var firstLinkPreview = new LinkPreview().GetFirstLinkPreView(message);

            // Detect Url after replace it Achor tag
            message = Link.ReplaceAllLink(message);

            return ProcessMessage.StrMessage(curUser, message);
        }


        public static string StrMessage(UserChatViewModel curUser, string message)
        {
            return
            "<div class='status'>"
                    +"<div class='status-header'>"
                            + "<div class='status-user'>"
                                    + "<a href='#'>"
                                            + "<img src='/Uploads/Users/backham.jpg' alt='...' class='img-circle img60x60' />"
                                    + "</a>"
                            + "</div>"

                            + "<div class='status-content'>"
                                    + "<a class='peple-name' href='#'>Tan</a> " + message
                                    + "<div>"
                                            + "<a>Like</a> &nbsp;<span class='glyphicon glyphicon-thumbs-up'></span> <span class='numLike'>0</span>"
                                            + "&nbsp; &nbsp;"
                                            + "<a>Share</a> &nbsp;<span class='glyphicon glyphicon-share-alt'></span> <span class='numShare'>0</span>"
                                    + "</div>"

                            + "</div>"
                    + "</div>"

                    + "<hr />"

                    + "<div class='list-comment'>"

                            + "<div class='row block-textarea'>"
                                    + "<div class='col-md-1'>"
                                            + "<a href='#'>"
                                                    + "<img src='/Uploads/Users/backham.jpg' alt='...' class='img-rounded img33x33'>"
                                            + "</a>"
                                    + "</div>"
                                    + "<div class='col-md-10'>"
                                            + "<textarea name='txtComment' rows='1' cols='50' class='form-control txtComment' placeholder='Enter comment...'></textarea>"
                                    + "</div>"
                            + "</div>"

                    + "</div>"
            +"</div>";
        }


    }
}