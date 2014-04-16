chat.client.like = function (userName, statusId) {

    var btnLike = $("#numLike-" + statusId);
    var textCurNumLike = btnLike.text();
    var numLike = parseInt(textCurNumLike);
    btnLike.text(numLike + 1);
};

chat.client.likeNewFeeds = function (friend, statusId, statusOwnerUsername) {

    var htmlLikeNewFeeds =
      "<div class='new'>"
            +"<div class='new-user'>"
                    +"<img class='img28x28' src='" + friend['Avatar'] + "' />"
            +" </div>"
            +"<div class='new-action'>"
                    +"<a href='#'>" + friend['UserName'] + "</a>"
                    +" likes <a href='#'>" + statusOwnerUsername + "</a>'s status"
            +"</div>"
            +"<div class='clear-left'></div>"
       +"</div>"

    $("#news").prepend(htmlLikeNewFeeds);
};