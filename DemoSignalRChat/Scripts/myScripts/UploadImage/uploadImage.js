$("#btn-ajax-upload").click(function () {

    //alert("running...");

    UploadFile();

    alert("running...");

    //console.log(arrImageNames);

    });

    function readURLAjax(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $("#images").append("<img src='" + e.target.result + "' />");
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#formupload").on("change", ".etlfileToUpload", function () {
        readURLAjax(this);
        //alert($(this).attr("class"));
        $(".browse-wrap").append("<input type='file' name='etlfileToUpload' class='etlfileToUpload' />");
        $(this).hide('fast');
    });

        // Call this function on upload button click after user has selected the file
        function UploadFile() {
            var eles = document.getElementsByClassName('etlfileToUpload');

            

            for(var i = 0; i < eles.length; i++)
            {
                console.log(eles[i].files[0]);
            }

            //console.log(files);

            for(var i = 0; i < eles.length; i++)
            {
                var file = eles[i].files[0]

                var fileName = file['name'];

                //arrImageNames.push(fileName);

                var fd = new FormData();
                fd.append("fileData", file);
                var xhr = new XMLHttpRequest();
                xhr.upload.addEventListener("progress", function (evt) { UploadProgress(evt); }, false);
                xhr.addEventListener("load", function (evt) { UploadComplete(evt); }, false);
                xhr.addEventListener("error", function (evt) { UploadFailed(evt); }, false);
                xhr.addEventListener("abort", function (evt) { UploadCanceled(evt); }, false);
                xhr.open("POST", "/Home/Upload", true);
                xhr.send(fd);
            }
        }


function UploadProgress(evt) {
    if (evt.lengthComputable) {

        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        while (percentComplete < 100)
        {
            UploadProgress(evt);
        }
        $("#uploading").text(percentComplete + "% ");
    }
}

function UploadComplete(evt) {
    if (evt.target.status == 200)
        //alert(evt.target.responseText);
        console.log("complete");
    else {
        alert("Error Uploading File");
    }
}

function UploadFailed(evt) {
    alert("There was an error attempting to upload the file.");
}
