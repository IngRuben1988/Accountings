$('#fileUpload').on("change", function (event) {
    $(".lblFileAdd").remove();
    var files = $('#fileUpload').get(0).files;
    var filesCount = files.length;
    if (filesCount > 0) {
        $('#btnUploadFile').removeClass('disabled');
        $.each(files, function (key, value) {
            $(".divFilesSCTPS").append("<li class='lblFileAdd margin-left-md' style='font-size: 10px;'>" + value.name + "</li>")
        });
    }
    else {
        $('#btnUploadFile').addClass('disabled');
    }
});

$('#btnUploadFile').on('click', function (e) {

    OpenModalProcessing(".processingModal");
    var files = $('#fileUpload').get(0).files;
    var data = new FormData();

    $.each(files, function (index, value) {
        data.append('itemsFiles', files[index]);
    });

    var jsPromiseSendFileSctPs;
    jsPromiseSendFileSctPs = Promise.resolve(PrepareRequestFileUploadSCTPS(data));
    jsPromiseSendFileSctPs.then(function (response) {

        //reloadGrid(jsGridCurrent);

        notifyMessageGral('Se ha cargado correctamente el archivo, vuelva a la pantalla anterior.', "success", 500, null);
        $(".lblFileAdd").remove();
        $('#fileUpload').val("");
        CloseModalProcessing(".processingModal");
    }).catch(function (error) {
        // var obj = JSON.parse(error);
        // console.log(obj);
        // console.log("Failed Request! SCTPSUp ->", error);
        // console.error("No se puede procesar la solicitud SCTPSUp. --> ");
        notifyMessageGral(error.statusText, "error", 500, null);
        CloseModalProcessing(".processingModal");
    }).finally(function () {
        CloseModalProcessing(".processingModal");
    });

});

function PrepareRequestFileUploadSCTPS(files) {
    var sendFilesRequest = $.ajax({
        method: "POST"
        , url: bankStatementsUrl.fileSend
        , contentType: false
        , processData: false
        , data: files
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return sendFilesRequest;
}
