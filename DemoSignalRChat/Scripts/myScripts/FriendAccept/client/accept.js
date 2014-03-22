chat.client.notifyAcceptedFriend = function (userName) {

    var htmlFriend = "<li class='notify-require-friend'>"
                        + "<a>"
                          + "<button type='button' class='close' aria-hidden='true'>&times;</button>"
                          + "<p>" + userName + " accepted friend!</p>"
                        + "</a>"
                     + "</li>";

    $("#notify-bottom-left").prepend(htmlFriend);

    $("#notify-bottom-left li").delay(5000).fadeOut('slow', "swing");
};// end client