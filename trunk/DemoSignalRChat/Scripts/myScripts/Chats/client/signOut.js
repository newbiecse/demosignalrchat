var chat = $.connection.chatHub;

chat.client.signOut = function (userIdSignOut) {
    $("#img-online-" + userIdSignOut).hide("fast");
}// end signOut