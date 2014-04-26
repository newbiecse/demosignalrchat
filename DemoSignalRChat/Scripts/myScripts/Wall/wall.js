

$("#btn-wall").click(function () {

    $("#img-loader-wall").show("fast");

        var request = $.ajax({
            url: "/Wall/Index",
            type: "POST"
        })// end ajax
            .done(function (response, textStatus, jqXHR) {
                
                $("#status-list").hide().html(response).fadeIn('slow');
                //alert("success");
                //console.log(response);

            })// end done
            .fail(function (response, textStatus, jqXHR) {
                alert("error");
            })// end fail
    	   .always(function () {
    	       $("#img-loader-wall").hide("fast");
    	    });

        event.preventDefault();
}); // end keyup