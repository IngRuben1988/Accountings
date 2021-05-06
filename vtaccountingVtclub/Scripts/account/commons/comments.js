var URL_ACTION = '', IDS = 0;
var invoiceform = $("#invoiceForm");
var incomeform = $("#incomeForm");
$('#btnSaveComments').on("click", function (event) {
    event.preventDefault();

    var element = validateObjectSavedAttach();

    if (element == false) {
        URL_ACTION = incomeApp.addcommentinc;
        IDS = incomeObject.item;
    } else {
        URL_ACTION = Invoice.addComment;
        IDS = $("#Invoice").val();
    }

    saveComments(URL_ACTION,IDS, $("#commentDescription").val());
});

function validateObjectSavedAttach() {
    if (invoiceform[0] != undefined && invoiceform[0] != 0) {

        return true
    }
    else {
        return false
    }
}

function saveComments(urlAction,id, text) {
    $.ajax({
        method: "POST"
        , url: urlAction
        , data: { id: id, comments: text }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            if (data.success == true) {

                notifyMessageGral("Se ha agregado correctamente el segumiento");
                var type = validateObjectSavedAttach();
                type == false ? URL_ACTION = incomeApp.getcommentinc : URL_ACTION = Invoice.getComment

                getComments(id,URL_ACTION);
                $("#commentDescription").val("");
            }
            else {
                notifyMessageGral("No se puede guardar el seguimiento: " + data.message, "error");
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            notifyMessageGral("No se puede guardar el seguimiento: " + jqXHR.message, "error");
        });
}

/********* ADDING COMMENTS TO TABLE*********/
function getComments(Id, UrlAction) {

    var count_elements = $('.trComment').length;;

    if (count_elements == 0) {
        AddElemtsTableComments(Id, UrlAction);
    }
    else {
        for (i = 0; i <= count_elements; i++) {
            $('#trComment').remove();
        }
        AddElemtsTableComments(Id, UrlAction);
    }

}

/*********AJAX REQUEST TO GET COMMENTS FROM BACK********/
function AddElemtsTableComments(Id,UrlAction) {

    jsPromiseSaveDocumentPayment = Promise.resolve(PrepareRequestGetComments(Id, UrlAction));
    jsPromiseSaveDocumentPayment.then(function (response) {

        if (response.data.success = true) {

            var items = response.data;
            $.each(items, function (key, value) {
                var tdId = 'tdAtt' + key + '';
                $('#tblComments> tbody:last-child').append('<tr id=trComment class="trComment"><td>' + value.row + '</td><td>' + value.UserComment + '</td><td> ' + value.CreactionDateString + '</td><td>' + value.Description + '</td></tr>');
            });
        }
        else {

            notifyMessageGral("No se pueden cargar el seguimiento: " + response.data.message, "error");
        }

    }).catch(function (error) {
        notifyMessageGral("No se puedo carga los archivos.", 'error', 1500, '.modal-header');
        console.log("Failed Request! ->", error);
    })
        .finally(function () {
        });
}

/*********AJAX REQUEST TO GET COMMENTS FROM BACK********/
function AddElemtsTableComments2() {
    var inv_id = ($('#Invoice').val()) || 0;
    $.ajax({
        method: "GET",
        url: "/documents/getCommentInvoice",
        data: { id: inv_id },
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) {
        if (data.success = true)
        {
            var items = data.data;
            $.each(items, function (key, value)
            {
                var tdId = 'tdAtt' + key + '';
                $('#tblComments> tbody:last-child').append(
                    '<tr id=trComment class="trComment">' +
                        '<td>' + value.row + '</td>' +
                        '<td>' + value.UserComment + '</td>' +
                        '<td> ' + value.CreactionDateString + '</td>' +
                        '<td>' + value.commentDescription + '</td>' +
                    '</tr>'
                );
            });
        }
        else
        {
            notifyMessageGral("No se pueden cargar el seguimiento: " + data.message, "error");
        }
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
    });
}

//evaluate buttonComments
function evaluateComments(element) {
    if (element != "" || element > 0) {
        $('#btnSaveComments').prop('disabled', false);
    }
}


function checkElements(element) {
    var element = document.getElementById(element);
    if (typeof (element) != 'undefined' && element != null) {
        true
    } else {
        false
    }
}