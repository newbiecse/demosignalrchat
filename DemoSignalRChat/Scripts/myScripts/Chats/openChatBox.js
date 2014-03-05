// open window chatbox
$(".friend-chat").click(function () {

    var friendId = $(this).attr("id");
    var friendName = $(this).attr("data-userName");
    var windowId = "w-" + friendId;
    var wBodyId = "w-b-" + friendId;

    if ($("#" + windowId).length == 0) {
        //it doesn't exist

        var strNewWindowChat =
              "<div class='window-chat'" + "id='" + windowId + "'>"
            + "    <div class='window-chat-header'>"
            + "        <p>" + friendName + "</p>"
            + "    </div>"
            + "    <div class='window-chat-body' id='" + wBodyId + "'>"
            + "    </div>"
            + "    <div class='window-chat-footer'>"
            + "        <input type='text' placeholder='Enter message...' class='txt-message' data-friendId='" + friendId + "' />"
            + "    </div>"
            + "</div>";

        $("#window-chat-list").append(strNewWindowChat);
    } // end doesn't exist
    else {
        alert("Already existing :)");
    } // end else
});//end click