chat.client.unLike = function (userName, statusId) {

    var btnLike = $("#numLike-" + statusId);
    var textCurNumLike = btnLike.text();
    var numLike = parseInt(textCurNumLike);
    btnLike.text(numLike - 1);
};