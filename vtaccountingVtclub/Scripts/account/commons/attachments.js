var invoiceform = $("#invoiceForm")
var incomeform = $("#incomeForm")
var URL_ACTION = '', ATTS = 0, IDS = 0, TYPES;
// Botton action UploadFile
$('#btnUploadFile').on('click', function (e) {
    e.preventDefault();
    var data = new FormData();
    // var files = $('#fileUpload').get(0).files;
    var files = $('#fileUpload').get(0).files;
    // Add the uploaded image content to the form data collection 
    if (files.length > 0) {
        // data.append('UploadedImage', files[0]);
        $.each(files, function (index, value) {
            data.append('itemsFiles', files[index]);
        });
    }

    if ($('#idAttach').val() == 0 || $('#fileUpload').val() == "") {
        $("#info").notify("Debe seleccionar un tipo de archivo para cargar y seleccionar un archivo.",
            { position: "top right", className: "warn", hideDuration: 500 }
        );
    }
    else { // Make Ajax request with the contentType = false, and procesDate = false 
        var element = validateObjectSavedAttach();

        ATTS = $('#idAttach').val();
        if (element == false) {
            URL_ACTION = incomeApp.saveattach;
            IDS = incomeObject.item;
        } else {
            URL_ACTION = Invoice.SaveAttach;
            IDS = $("#Invoice").val();
        }

        ActionFile(URL_ACTION, ATTS, IDS, data);
    }
});

function validateObjectSavedAttach() {
    if (invoiceform[0] != undefined && invoiceform[0] != 0) {

        return true
    }
    else  {
        return false
    }
 }

function ActionFile(URL, ATTS, IDS, data) {
    var urlAction = URL + "?idAttach=" + ATTS + "&id=" + IDS + ""
    $.ajax({
        type: 'POST',
        url: urlAction,
        contentType: false,
        processData: false,
        data: data,
        beforeSend: function () {
            OpenModalProcessing(".processingModal");
        }
    })
        .done(function (xhr, textStatus) {
            if (xhr.success === false) {
                notifyMessageGral(xhr.message, "warn", 1500, null);
            }
            else {
                var type = validateObjectSavedAttach();
                notifyMessageGral("Se ha cargado correctamente los archivos.", "success", 500, null);
                $('#idAttach').val('0');
                $('#fileUpload').val('');
                type == false ? URL_ACTION = incomeApp.getincomeattach : URL_ACTION = Invoice.GetAttach
                getAttachments(IDS, URL_ACTION);
            }
            ForceCloseModal(".processingModal");
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 500) {
                myObj = JSON.parse(this.responseText);
                notifyMessageGral(myObj.title, "warn", 1500, null);
                btnUploadFileACtive(true);
            }
            if (jqXHR.status == 404) {
                notifyMessageGral("Los archivos exceden el limite maximo de 10 mb.", "warn", 1500, null);
                btnUploadFileActive(true);
                ForceCloseModal(".processingModal");
            }
        });

}

function btnUploadFileActive(val) {

    if (!val) {
        $('.btnUploadFile').addClass('disabled');
    }
    else {
        $('.btnUploadFile').removeClass('disabled');
    }
}


/********* ADDING ATTACHMENTS TO TABLE*********/
function getAttachments(ID,URL) {

    var count_elements = $('.attach').length;;
    if (count_elements == 0) {
        AddElemtsTableAttach(ID, URL);
    }
    else {
        for (i = 0; i <= count_elements; i++) {
            $('#trAttach').remove();
        }
        AddElemtsTableAttach(ID, URL);
    }
}

function AddElemtsTableAttach(ID, URL) {
    jsPromiseSaveDocumentPayment = Promise.resolve(getFilesUpload(ID, URL));
    jsPromiseSaveDocumentPayment.then(function (response) {
        if (response.data.success = true) {
            var items = response.data;
            $.each(items, function (key, value) {
                var tdId = 'tdAtt' + key + '';
                $('#keysAttach> tbody:last-child').append('<tr id=trAttach ><td>' + value.row + '</td><td>' + value.typefilename + '</td><td> ' + value.filename + '</td><td>' + value.datechange + '</td><td id="' + tdId + '"></td><td><i class="fas fa-trash fa-1x" data-toggle="tooltip" data-original-title="Eliminar" onClick="deleteAtachment(' + value.item + ')"></i></td></tr>');
                var arefId = '#' + tdId + '';
                var thelink = $('<a>', {
                    // text: value.nombrearchivo,
                    html: "<i class='fas fa-download fa-1x ' data-toggle='tooltip' data-original-title='Bajar archivo'></i>",
                    title: "Bajar " + value.nombrearchivo,
                    href: "http://" + value.url,
                    class: 'attach',
                    target: '_blank'
                }).appendTo(arefId);

            });
        }
        else {
            $("#info").notify("No se pueden cargar los archivos adjuntos: " + response.data.message,
                {
                    position: "button right",
                    className: "error",
                    hideDuration: 500
                });
        }
    }).catch(function (error) {
        notifyMessageGral("No se puedo carga los archivos.", 'error', 1500, '.modal-header');
        console.log("Failed Request! ->", error);
    })
        .finally(function () {
        });

}

function deleteAtachment(idAttachment) {
    var type = validateObjectSavedAttach();
    type == false ? URL_ACTION = incomeApp.deleteattachinc : URL_ACTION = Invoice.DeleteAttach

    var r = confirm("¿Desea eliminar el archivo adjunto?");
    if (r == true) {
        OpenModalProcessing(".processingModal");

        jsPromiseDeleteAttachment = Promise.resolve(PrepareRequestDeleteAttachment(idAttachment, URL_ACTION)); // Preparing to Delete Attachment
        jsPromiseDeleteAttachment.then(function (response) {

            if (response.success === true) {
                ForceCloseModal(".processingModal");
                notifyMessageGral("Se ha eliminado correctamente el archivo adjunto", 'success', 800, '#info');
                getAttachments();
            }
            else {
                ForceCloseModal(".processingModal");
                notifyMessageGral("No se puede eliminar el archivo adjunto. " + response.message, 'error', 1500, '#info');
            }

        })
            .catch(function (error) {
                ForceCloseModal(".processingModal");
                notifyMessageGral("No se puede eliminar el archivo adjunto.", 'error', 1500, '#info');
                console.log("Failed Request! ->", error);
                reloadGrid();
                reloadGridPayments();
            });


    } else {

    }

}

/*********AJAX REQUEST TO GET ATTACHEMENTS FROM BACK********/
function AddElemtsTableAttach2() {

    var inv_id = ($('#Invoice').val()) || 0;

    $.ajax({
        method: "POST"
        , url: "/" + VTA.Complement + "/getAttachmentsdocument"
        , data: { id: inv_id }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            if (data.success = true) {

                var items = data.data;
                $.each(items, function (key, value) {
                    var tdId = 'tdAtt' + key + '';
                    $('#keysAttach> tbody:last-child').append('<tr id=trAttach ><td>' + value.row + '</td><td>' + value.typefilename + '</td><td> ' + value.filename + '</td><td>' + value.datechange + '</td><td id="' + tdId + '"></td><td><i class="fas fa-trash fa-1x" data-toggle="tooltip" data-original-title="Eliminar" onClick="deleteAtachment(' + value.item + ')"></i></td></tr>');
                    var arefId = '#' + tdId + '';
                    var thelink = $('<a>', {
                        html: "<i class='fas fa-download fa-1x ' data-toggle='tooltip' data-original-title='Bajar archivo'></i>",
                        title: "Bajar " + value.filename,
                        href: "http://" + value.url,
                        class: 'attach',
                        target: '_blank'
                    }).appendTo(arefId);

                });
            }
            else {
                $("#info").notify("No se pueden cargar los archivos adjuntos: " + data.message,
                    {
                        position: "top right",
                        className: "error",
                        hideDuration: 500
                    });
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {

        });
}

function deleteAtachment2(idAttachment) {

    var r = confirm("¿Desea eliminar el archivo adjunto?");
    if (r == true) {
        OpenModalProcessing(".processingModal");

        jsPromiseDeleteAttachment = Promise.resolve(PrepareRequestDeleteAttachment(idAttachment)); // Preparing to Delete Attachment
        jsPromiseDeleteAttachment.then(function (response) {

            if (response.success === true) {
                ForceCloseModal(".processingModal");
                notifyMessageGral("Se ha eliminado correctamente el archivo adjunto", 'success', 800, '#info');
                getAttachments();
            }
            else {
                ForceCloseModal(".processingModal");
                notifyMessageGral("No se puede eliminar el archivo adjunto. " + response.message, 'error', 1500, '#info');
            }

        })
            .catch(function (error) {
                ForceCloseModal(".processingModal");
                notifyMessageGral("No se puede eliminar el archivo adjunto.", 'error', 1500, '#info');
                console.log("Failed Request! ->", error);
                reloadGrid();
                reloadGridPayments();
            });


    } else {

    }
}

function PrepareRequestDeleteAttachment(idAttachment) {

    deleteAttachmentRequest = $.ajax({
        method: "POST"
        , url: "/" + VTA.Complement + "/DeleteAttachment"
        , datatype: 'json'
        , data: {
            id: idAttachment
        },
    })
        .done(function (response, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return deleteAttachmentRequest;
}

/*********AJAX REQUEST TO SAVE ATTACHEMENTS ********/

function saveAttachments(_formName, type, parent) {
    OpenModalProcessing(".processingModal");
    var formName = '#' + _formName;
    var data = new FormData();
    var files = $(formName).get(0).files;
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        $.each(files, function (index, value) {
            data.append('itemsFiles', files[index]);
        });
    }
    var jsPromiseSaveAttachment = Promise.resolve(PrepareRequestSaveAttachment(data, type, parent)); // Preparing to Delete Attachment
    jsPromiseSaveAttachment.then(function (response) {
        if (response.success === false) {
            notifyMessageGral(xhr.message, "warn", 1500, null);
        }
        else {
            notifyMessageGral("Se ha cargado correctamente los archivos.", "success", 500, null);
            getAttachments();
            ForceCloseModal(".processingModal");
        }

    })
    .catch(function (error) {
        ForceCloseModal(".processingModal");
        notifyMessageGral("No se puede vargar archivo adjunto.", 'error', 1500, '.info');
        console.log("Failed Request! ->", error);
    });
}

function PrepareRequestSaveAttachment(data, type, parent) {
    // Make Ajax request with the contentType = false, and procesDate = false 
    var request = $.ajax({
        type: 'POST',
        url: "/" + VTA.Complement + "/AttachFilePropertieAjax?idAttach=" + type + "&id=" + parent + "",
        contentType: false,
        processData: false,
        data: data,
    })
    .done(function (xhr, textStatus) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });
    return request;
}

//***** Tipos de archivos *****//
function AttachmentsTypes() {
    $.ajax({
        method: "GET",
        url: "/formcontrol/getAttachmentsTypes",
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) {
        $.each(data.data, function (key, value) {
            $('#idAttach').append($("<option></option>").attr("value", value.value).text(value.text));
        });
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        $("#info").notify("No se puede obtener los hoteles.",{ position: "bottom right", className: "error", hideDuration: 500 });
    });
}


function evaluateUpload(element) {
    if (element != "" || element >0) {
        $('#btnUploadFile').prop('disabled', false);
    }
} 


