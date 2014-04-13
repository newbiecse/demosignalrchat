// signout
$("body").on("click", "#btn-logOff", function () {
    chat.server.onDisconnected();
});// end on click