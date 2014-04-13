using DemoSignalRChat.DAL;
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
        public static string ProcessMessageStatus(string statusId, UserViewModel curUser, string message, string[] imageNames)
        {
            // Get first Url Preview
            var firstLinkPreview = new LinkPreview().GetFirstLinkPreView(message);

            // Detect Url after replace it Achor tag
            string messageProcessed = ProcessComment.ProcessMessage(message);

            string htmlStatus = "<div class='status'>";

            htmlStatus += ProcessMessage.GetHtml_statusOwner(curUser);
            htmlStatus += ProcessMessage.GetHtml_statusContent(messageProcessed);

            if(firstLinkPreview != null)
            {
                htmlStatus += ProcessMessage.GetHtml_statusPreview(firstLinkPreview);
            }

            htmlStatus += ProcessMessage.GetHtml_statusBoxLikeShare(statusId, curUser.UserId);
            htmlStatus += ProcessMessage.GetHtml_statusListComment(statusId, curUser);

            htmlStatus += "</div>";

            return htmlStatus;
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


        public static string GetHtml_statusOwner(UserViewModel curUser)
        {
            return
                  "<div class='status-owner'>"
                + "    <div class='status-owner-image'>"
                + "        <img src='" + curUser.Avatar + "' class='img-circle img40x40' />"
                + "    </div>"
                + "    <div class='status-owner-name'>"
                + "        <a class='peple-name' href='#'>" + curUser.UserName + "</a>"
                + "        <br />"
                + "        " + DateTime.Now
                + "    </div>"
                + "    <div class='clear-left'></div>"
                + "</div>";
        }

        public static string GetHtml_statusContent(string messageProcessed)
        {
            return
              "<div class='status-content'>"
                + messageProcessed
             +"</div>";
        }

        public static string GetHtml_statusPreview(LinkPreview linkPreview)
        {
            return
                    "<a class='link-preview' href='" + linkPreview.url + "' target='_blank'>"
                        +"<div class='preview'>"
                            +"<div class='preview-image'>"
                                +"<img src='" + linkPreview.src + "' class='img90x90' />"
                            +"</div>"
                            +"<div class='preview-description'>"
                                +"<b>" + linkPreview.title + "</b>"
                                +"<p>"
                                    + Regex.Match(linkPreview.url, @"://(.+?)/").Groups[1].Value + " <br />"
                                    + linkPreview.description
                                +"</p>"
                            +"</div>"
                            +"<div class='clear-left'></div>"
                        +"</div>"
                    +"</a>";
        }

        public static string GetHtml_statusBoxLikeShare(string statusId, string userId)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            ILikeRepository likeRepository = new LikeRepository(dbContext);

            int isLike = likeRepository.IsLiked(statusId, userId);
            string strLike = (isLike == 1) ? "unlike" : "like";

            return
            "<div class='box-like-share'>"
                + "<a class='like' data-isliked='" + isLike + "' data-statusid='" + statusId + "'>"
                    + strLike
                + "</a> &nbsp;<span data-statusid='" + statusId + "' class='glyphicon glyphicon-thumbs-up icon-liked' data-toggle='modal' data-target='#liked'></span> <span id='numLike-" + statusId + "'>" + likeRepository.Count(statusId) + "</span>"
                + "&nbsp;.&nbsp;"
                + "<a class='share' data-statusid='" + statusId + "'>Share</a> &nbsp;<span class='glyphicon glyphicon-share-alt'></span> <span id='numShare-" + statusId + "'>0</span>"
            + "</div>";
        }

        public static string GetHtml_statusListComment(string statusId, UserViewModel curUser)
        {
            return
            "<hr />"
            +"<div class='list-comment' id='list-comment-" + statusId + "'>"
                +"<div class='block-textarea' id='block-txtComment-" + statusId + "'>"
                    +"<div class='comment-user'>"
                        +"<a href='#'>"
                            +"<img src='" + curUser.Avatar  + "' class='img-rounded img33x33 cur-user-avatar'>"
                        +"</a>"
                    +"</div>"
                    +"<div class='comment-content'>"
                        +"<textarea data-statusid='" + statusId + "' name='txtComment' rows='1' cols='59' class='form-control txtComment' placeholder='Enter comment...'></textarea>"
                    +"</div>"
                    +"<div class='clear-left'></div>"
                +"</div>"
            +"</div>";
        }


        public static string StrImages(string[] imageNames)
        {
            string htmlImages = "";
            foreach(var image in imageNames)
            {
                htmlImages += "<img src='/images/" + image + "' />";
            }
            return htmlImages;
        }
    }
}