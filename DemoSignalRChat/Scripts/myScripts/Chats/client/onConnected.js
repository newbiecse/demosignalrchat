var chat = $.connection.chatHub;

chat.client.onConnected = function (listFriendId) {
    if (listFriendId.length > 0) {
        var isOnline = ($("#" + listFriendId[0]).attr('data-isonline') == 0) ? 0 : 1;

        var color = (isOnline == 1) ? "red" : "white";

        for (i = 0; i < listFriendId.length; i++) {
            $("#" + listFriendId[i]).css("background-color", color).attr('data-isonline', isOnline);
        }
    }
}
