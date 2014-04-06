$("#window-chat-list").on("click", ".close", function () {
    $(this).closest(".window-chat").remove();
});