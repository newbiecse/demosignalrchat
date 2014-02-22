/*
*   A sent message to B
*   display message B side
*   => need: A_userId(user sent)
*           b1: check exist window chat B -> A
*           b2: update window chat have id = 'w-A_userId'
*/
chat.client.privateMessageReceived = function (userIdSent, message) {

    var _B_windowId = "#w-" + userIdSent;
    var _B_windowBodyId = "#w-b-" + userIdSent;

    if ($(_B_windowBodyId).length == 0) {
        //it doesn't exist
        $("#" + userIdSent).click();
    }

    // process the message
    var msg_link = replaceURLWithHTMLLinks(message);
    var msg_emotion = replaceEmotion(msg_link);

    // process the window chat
        // update window body of window chat
        $(_B_windowBodyId).append("<p class='msg msg-friend'>" + msg_emotion + "</p>");
        // scroll window body
        $(_B_windowBodyId).scrollTop($(_B_wBodyId).height());
};
