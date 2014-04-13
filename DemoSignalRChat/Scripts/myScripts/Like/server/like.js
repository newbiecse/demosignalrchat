$("body").on("click", ".like", function () {
    var statusId = $(this).attr('data-statusid');
    var isLiked = $(this).attr('data-isliked');

    // Call like.
    if (isLiked == 0) {
        chat.server.like(statusId);
        $(this).attr('data-isliked', 1);
        $(this).text("unlike");
    }
    else {
        chat.server.unLike(statusId);
        $(this).attr('data-isliked', 0);
        $(this).text("like");
    }

});// end on click