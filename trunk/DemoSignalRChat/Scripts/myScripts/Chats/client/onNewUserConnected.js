var chat = $.connection.chatHub;

chat.client.onNewUserConnected = function (userIdConnected) {
    $("#" + userIdConnected).css("background-color", "red");
    $("#" + userIdConnected).attr('data-isonline', 1);
}