chat.client.share = function (userName, messageProcessed) {
    //console.log(userName)
    $("#status-list").prepend(messageProcessed);
};// end share