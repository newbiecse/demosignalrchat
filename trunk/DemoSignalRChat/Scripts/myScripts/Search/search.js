
$("body").click(function () {
    //alert("clicked");
    $("#search-result").html("");
});// end click

$("#txtSearch").keyup(function () {

    if ($(this).val().length > 0)
    {

        $form = $("#searchform");

        var request = $.ajax({
            url: "/Home/Search",
            type: "POST",
            data: $form.serialize()
        })// end ajax
            .done(function (response, textStatus, jqXHR) {

                console.log(response);
                if(response.length > 0)
                {
                    console.log(response[0]["Displayname"]);
                }// end if

                var searchResult = $("#search-result");

                searchResult.html("");

                var htmlSearch = "";

                for(var i = 0; i < response.length; i++)
                {
                    htmlSearch +=
                    "<a class='user-search'>"
                        + "<div class='search-image'>"
                            + "<img src='" + response[i]["Avatar"] + "' />"
                        + "</div>"
                        + "<div class='search-info'>"
                            + "<span>" + response[i]["Displayname"] + "</span>"
                            + "<span>University of technology</span>"
                        + "</div>"
                    + "</a>";
                }// end for

                searchResult.html(htmlSearch);

            })// end done
            .fail(function (response, textStatus, jqXHR) {
                alert("error");
            });// end fail

        event.preventDefault();
    }// end if
}); // end keyup