

function wall(paramUserId) {
    $("#img-loader-wall").show("fast");

    var request = $.ajax({
        url: "/Wall/Index",
        type: "POST",       
        data: { userId: paramUserId },
        dataType: 'html'
        //contentType: 'application/json, charset=utf-8'
    })// end ajax
        .done(function (response, textStatus, jqXHR) {

            $("#status-list").hide().html(response).fadeIn('slow');
            console.log(response);

        })// end done
        .fail(function (response, textStatus, jqXHR) {

            alert("error");
        })// end fail
       .always(function () {
           $("#img-loader-wall").hide("fast");
       });

    event.preventDefault();
}

$("#btn-wall").click(function () {

    wall();
}); // end keyup

$("#search-result").on("click", ".user-search", function () {

    wall($(this).attr("data-userId"));
    console.log($(this).attr("data-userId"));
});