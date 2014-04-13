var chat = $.connection.chatHub;

chat.client.onNewUserConnected = function (userIdConnected) {
    $("#img-online-" + userIdConnected).show("fast");
    $("#" + userIdConnected).attr('data-isonline', 1);
}