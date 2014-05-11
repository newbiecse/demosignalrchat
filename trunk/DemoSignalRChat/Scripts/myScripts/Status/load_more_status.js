

function load_more_status() {
    //$("#img-loader-wall").show("fast");

    var request = $.ajax({
        url: "/Home/StatusNewest",
        type: "POST",
        dataType: 'html'
    })// end ajax
        .done(function (response, textStatus, jqXHR) {

            $("#div-load-more").replaceWith(response).fadeIn('slow');

        })// end done
        .fail(function (response, textStatus, jqXHR) {

            alert("error");
        })// end fail
       .always(function () {
           //$("#img-loader-wall").hide("fast");
       });

    event.preventDefault();
}



$("#status-list").on("click", "#load-more", function () {
    load_more_status();
});