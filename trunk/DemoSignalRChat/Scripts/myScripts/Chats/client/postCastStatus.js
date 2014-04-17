chat.client.postCastStatus = function (userName, message) {

    $("#status-list").prepend(message);
};// end messageReceived

chat.client.statusNewFeeds = function (friend) {

    var htmlStatusNewFeeds =
      "<div class='new'>"
            + "<div class='new-user'>"
                    + "<img class='img28x28' src='" + friend['Avatar'] + "' />"
            + " </div>"
            + "<div class='new-action'>"
                    + "<a href='#'>" + friend['Displayname'] + "</a>"
                    + " posted a status on his wall"
            + "</div>"
            + "<div class='clear-left'></div>"
       + "</div>"

    $("#news").prepend(htmlStatusNewFeeds);
};// end messageReceived