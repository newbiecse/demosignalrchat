var chat = $.connection.chatHub;

chat.client.signOut = function (userIdSignOut) {
    $("#" + userIdSignOut).css("background-color", "#ffffff");
}
