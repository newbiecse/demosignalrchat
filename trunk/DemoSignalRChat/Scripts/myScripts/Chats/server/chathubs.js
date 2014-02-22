$.connection.hub.start().done(function () {

        // connect to server
        chat.server.connect(curUserId);
});

// GLOBAL VARIABLE
var chat = $.connection.chatHub;
