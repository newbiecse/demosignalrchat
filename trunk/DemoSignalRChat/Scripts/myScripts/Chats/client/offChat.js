/*
*   turn off chat
*/
var chat = $.connection.chatHub;

chat.client.offChat = function (userIdOffLine) {
    $("#" + userIdOffLine).css("background-color", "#ffffff");
}
