$("#btn-req-Friends").click(function () {

    $.ajax({
        url: "/Home/GetListFriendWaitAccept",
        type: "GET"
    })
    .done(function (response, textStatus, jqXHR) {

        var htmlString = "";
        for (var i = 0; i < response.length; i++)
        {
            htmlString +=
              "<li>"
	            + "<div class='user-wait-image'>"
			            + "<img class='img40x40' src='" + response[i]["Avatar"] + "' />"
	            + "</div>"
	            + "<div class='user-wait-info'>"
			            + "<a>" + response[i]["UserName"] + "</a>"
			            + "<span>16 ban chung</span>"
	            + "</div>"
	            + "<div class='user-wait-accept'>"
			            + "<button data-userid='" + response[i]["UserId"] + "' class='btn btn-accept btn-sm btn-danger'>Accept</button>"
                        + "&nbsp;"
                        + "<button class='btn btn-sm btn-warning'>Later</button>"
	            + "</div>"
            + "</li>"
        }

        $("#listWaitAccept").html("");
        $("#listWaitAccept").html(htmlString);

        console.log(response);
    })// end done
    .fail(function (response, textStatus, jqXHR) {
            alert("error");
    });// end fail

        event.preventDefault();
}); // end click