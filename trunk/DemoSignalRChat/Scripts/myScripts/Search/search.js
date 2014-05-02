
$("body").click(function () {
    //alert("clicked");
    $("#search-result").html("");
});// end click

$("#txtSearch").keyup(function () {

    if ($(this).val().length > 0)
    {

        $form = $("#searchform");

        $("#search-result").hide('fast').html("<div><img src='images/ajax-loader-wall.gif' /></div>");

        var request = $.ajax({
            url: "/Home/Search",
            type: "POST",
            data: $form.serialize()
        })// end ajax
            .done(function (response, textStatus, jqXHR) {

                //console.log(response);
                $("#search-result").hide().html(response).fadeIn('slow');

            })// end done
            .fail(function (response, textStatus, jqXHR) {
                alert("error");
            });// end fail

        event.preventDefault();
    }// end if

    else {
        $("#search-result").html('');
    }

}); // end keyup