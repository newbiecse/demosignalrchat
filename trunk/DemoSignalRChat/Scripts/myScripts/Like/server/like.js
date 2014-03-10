$('.like').click(function () {

    var statusId = $(this).attr('data-statusid');

    // Call the Send method on the hub.
    chat.server.like(statusId);
    $(this).text("unlike");
});