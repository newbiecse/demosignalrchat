chat.client.notifyAcceptedFriend = function (userId, userName, avatar) {

    var htmlFriend = "<li class='notify-require-friend'>"
                        + "<a>"
                          + "<button type='button' class='close' aria-hidden='true'>&times;</button>"
                          + "<p>" + userName + " accepted friend!</p>"
                        + "</a>"
                     + "</li>";

    $("#notify-bottom-left").prepend(htmlFriend);

    $("#notify-bottom-left li").delay(5000).fadeOut('slow', "swing");
    new Audio('/sound/notify_friend.FLV').play();

    var htmlFriendChat =
        "<div id='" + userId + "' class='user-chat friend-chat' data-username='" + userName + "'>"
	        + "<img class='img28x28' src='" + avatar + "' />"
	        + "<span>" + userName + "</span>"
      + "</div>";

    $("#chat-list").prepend(htmlFriendChat);

    //window.location.reload(true);

};// end client