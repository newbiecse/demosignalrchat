chat.client.share = function (userName, messageProcessed) {
    //console.log(userName)
    $("#status-list").prepend(messageProcessed);
};// end share

chat.client.likeNewFeeds = function (friend, statusId, statusOwnerUsername) {

    var htmlLikeNewFeeds =
      "<div class='new'>"
            + "<div class='new-user'>"
                    + "<img class='img28x28' src='" + friend['Avatar'] + "' />"
            + " </div>"
            + "<div class='new-action'>"
                    + "<a href='#'>" + friend['Displayname'] + "</a>"
                    + " likes <a href='#'>" + statusOwnerUsername + "</a>'s status"
            + "</div>"
            + "<div class='clear-left'></div>"
       + "</div>"

    $("#news").prepend(htmlLikeNewFeeds);
};