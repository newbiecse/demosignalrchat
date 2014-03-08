$('#btn-text').click(function () {

    //alert("clicked");

    // Call the Send method on the hub.
    var msg = $('#txt-post').val();
    chat.server.sendMessageToAll(msg, "Ho Chi Minh");
    // Clear text box and reset focus for next comment.
    $('#txt-post').val('').focus();
});