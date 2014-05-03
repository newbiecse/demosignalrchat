

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

    var userId = $(this).attr("data-userId");
    wall(userId);

    //var oldHref = $(location).attr('href');
    //var newHref = oldHref + "wall/index/" + userId;
    //console.log(newHref);
    //$(location).attr('href', newHref);
});