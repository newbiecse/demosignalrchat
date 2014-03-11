chat.client.comment = function (userName, statusId, commentDisplay) {

    $("#block-txtComment-" + statusId).before(commentDisplay);
};