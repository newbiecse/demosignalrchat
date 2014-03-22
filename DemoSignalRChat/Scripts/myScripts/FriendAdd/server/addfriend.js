$("#suggest-friends").on('click', '.add-friend', function (event) {

    var friendId = $(this).attr("data-userid");
    // Call the Send method on the hub.
    chat.server.addFriend(friendId);

    $(this).closest(".suggest-friend").hide("slow");
});// end click