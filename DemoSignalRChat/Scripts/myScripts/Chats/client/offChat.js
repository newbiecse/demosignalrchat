/*
*   turn off chat
*/
var chat = $.connection.chatHub;

chat.client.offChat = function (userIdOffLine) {
    $("#img-online-" + userIdOffLine).show("fast");
}
