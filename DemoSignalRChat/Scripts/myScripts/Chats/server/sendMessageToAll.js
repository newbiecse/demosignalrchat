$('#sendmessage').click(function () {

    // Call the Send method on the hub.
    chat.server.sendMessageToAll(userName, $('#message').val());
    // Clear text box and reset focus for next comment.
    $('#message').val('').focus();
});