$(".txtComment").keypress(function (e) {

        if (e.keyCode == 13 && !e.shiftKey) {
            e.preventDefault();

            if ($(this).val().length > 0) {

                var statusId = $(this).attr('data-statusid');
                var cmtMessage = $(this).val();

                // Call the Send method on the hub.
                chat.server.comment(statusId, cmtMessage);

                $(this).val('').focus();
            } // end if length
            return;
        } // end if enter
    }); // end keypress