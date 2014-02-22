// private message
$('body').on('keyup', '.txt-message', function (e) {
    // enter textbox
    if ($(this).val().length > 0 && e.keyCode == 13) {

        // sent to the friend
        var _friendId = $(this).attr("data-friendId");
        var _msg = $(this).val();
        chat.server.sendPrivateMessage(_friendId, _msg);

        // process the message
        var msg_link = replaceURLWithHTMLLinks(_msg);
        var msg_emotion = replaceEmotion(msg_link);

        // process the current page
        // update window body of window chat
        $("#w-b-" + _friendId).append("<p class='msg msg-curent-user'>" + msg_emotion + "</p>");
        // scroll window body
        $("#w-b-" + _friendId).scrollTop($("#w-b-" + _friendId).height());
        // clear input
        $(this).val('').focus();
    }// end if
});// end on txt-message keyup