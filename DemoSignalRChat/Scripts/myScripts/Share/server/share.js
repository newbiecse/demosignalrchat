$("body").on("click", ".share", function () {

    var statusId = $(this).attr('data-statusid');

    // Call share
    chat.server.share(statusId);
});// end on click