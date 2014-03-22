$("#listWaitAccept").on('click', '.btn-accept', function (event) {

    var friendId = $(this).attr("data-userid");
    // Call the Send method on the hub.
    chat.server.acceptFriend(friendId);

});// end click