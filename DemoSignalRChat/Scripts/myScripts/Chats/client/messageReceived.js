chat.client.messageReceived = function (userName, message) {

    $("#status-list").prepend(message);
};// end messageReceived