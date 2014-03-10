using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DemoSignalRChat.Preview
{
    public static class ProcessMessage
    {
        public static string ProcessMessageStatus(UserViewModel curUser, string message)
        {
            // Get first Url Preview
            var firstLinkPreview = new LinkPreview().GetFirstLinkPreView(message);

            // Detect Url after replace it Achor tag
            message = Link.ReplaceAllLink(message);

            if(firstLinkPreview == null)
            {
                return ProcessMessage.StrMessageNotLink(curUser, message);
            }

            return ProcessMessage.StrMessageHaveLink(curUser, message, firstLinkPreview);
        }


        public static LinkPreview GetFirstLinkPreview(string message)
        {
            // Get first Url Preview
            LinkPreview firstLinkPreview = new LinkPreview().GetFirstLinkPreView(message);
            if(firstLinkPreview != null)
            {
                return firstLinkPreview;
            }
            return null;
        }

        public static string ProcMessage(string message)
        {
            return Link.ReplaceAllLink(message);
        }


        public static string StrMessageHaveLink(UserViewModel curUser, string message, LinkPreview linkPreview)
        {
            return
            "<div class='status'>"
                    +"<div class='status-header'>"
                            + "<div class='status-user'>"
                                    + "<a href='#'>"
                                            + "<img src='" + curUser.Avatar + "' class='img-circle img60x60' />"
                                    + "</a>"
                            + "</div>"

                            + "<div class='status-content'>"
                                    + "<a class='peple-name' href='#'>" + curUser.UserName + "</a> " + message
                                    + "<a class='link-preview' href='" + linkPreview.url + "' target='_blank'>"
		                                    + "<div class='preview'>"
				                                    + "<div class='preview-image'>"
						                                    + "<img src='" + linkPreview.src + "' class='img90x90' />"
				                                    + "</div>"
				                                    + "<div class='preview-description'>"
						                                    + "<b>" + linkPreview.title + "</b>"
                                                            + "<p>"
                                                                + Regex.Match(linkPreview.url, @"://(.+?)/").Groups[1].Value + "<br />"
						                                        + linkPreview.description
                                                            + "</p>"
				                                    + "</div>"
		                                    + "</div>"
                                    + "</a>"
                                    + "<div class='box-like-share'>"
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
                                                    + "<img src='" + curUser.Avatar + "' alt='...' class='img-rounded img33x33'>"
                                            + "</a>"
                                    + "</div>"
                                    + "<div class='col-md-10'>"
                                            + "<textarea name='txtComment' rows='1' cols='50' class='form-control txtComment' placeholder='Enter comment...'></textarea>"
                                    + "</div>"
                            + "</div>"

                    + "</div>"
            +"</div>";
        }

        public static string StrMessageNotLink(UserViewModel curUser, string message)
        {
            return
            "<div class='status'>"
                    + "<div class='status-header'>"
                            + "<div class='status-user'>"
                                    + "<a href='#'>"
                                            + "<img src='" + curUser.Avatar + "' class='img-circle img60x60' />"
                                    + "</a>"
                            + "</div>"

                            + "<div class='status-content'>"
                                    + "<a class='peple-name' href='#'>Tan</a> " + message
                                    + "<div class='box-like-share'>"
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
                                                    + "<img src='" + curUser.Avatar + "' alt='...' class='img-rounded img33x33'>"
                                            + "</a>"
                                    + "</div>"
                                    + "<div class='col-md-10'>"
                                            + "<textarea name='txtComment' rows='1' cols='50' class='form-control txtComment' placeholder='Enter comment...'></textarea>"
                                    + "</div>"
                            + "</div>"

                    + "</div>"
            + "</div>";
        }

    }
}