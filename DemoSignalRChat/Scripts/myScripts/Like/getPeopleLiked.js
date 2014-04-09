


$(".icon-liked").click(function () {

    var statusId = $(this).attr("data-statusid");

    //alert(statusId);

    $.ajax({
        url: "/Home/GetPeopleLiked",
        type: "POST",
        data: {
            statusid: $(this).attr("data-statusid")
        }
        })// end ajax
            .done(function (response, textStatus, jqXHR) {

                console.log(response);

                var bodyHtml = "";

                for (var i = 0; i < response.length; i++)
                {
                    bodyHtml +=
                                "<div class='people-like'>"
                                + "    <div class='user-image'>"
                                + "        <img class='img50x50' src='" + response[i]["Avatar"] + "' />"
                                + "    </div>"
                                + "    <div class='user-name'>"
                                + "        <a href='#'>" + response[i]["UserName"] + "</a>"
                                + "    </div>"
                                + "    <div class='button-relation'>"
                                + "        <button class='btn btn-success'>Add friend</button>"
                                + "    </div>"
                                + "</div>"
                                + "<hr />";
                }

                $("#liked .modal-body").html(bodyHtml);

            })// end done
            .fail(function (response, textStatus, jqXHR) {
                alert("error");
            });// end fail

        event.preventDefault();
}); // end click