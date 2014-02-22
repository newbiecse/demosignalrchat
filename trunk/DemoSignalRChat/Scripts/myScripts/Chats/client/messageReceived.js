chat.client.messageReceived = function (userName, src, title, decription) {
    // Add the message to the page.
    $('#discussion').append('<li><strong>' + userName
        + '</strong>: ' + "<img src='" + src + "' />" + "title: " + title + "<br />Decription: " + decription + '</li>');
};