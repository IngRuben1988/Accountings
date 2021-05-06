/********************************************************************************************************************/
/********************************************************************************************************************/
/******************************************** I N V O I C E  ********************************************************/
/********************************************************************************************************************/
/********************************************************************************************************************/
var invForm = $("#invoiceitemForm")
$(document).ready(function () {
    loadDataNotDependency(PrepareRequestGetAnyData(Components.companies), null, 'invoiceForm', 'Segment', true);
    loadDateTimePermissionIncomeInvoice(PrepareRequestGetDatePermissionsIncomeInvoice(), "datetimepicker1", new Date());
    loadDataNotDependency(PrepareRequestGetAnyData(Components.suppliers), null, "invoiceitemForm", "supplier", true);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.bugetypes), null, "invoiceitemForm", "budgetType", true);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.bankaccon), null, "invoiceitemForm", "BankAccntType", true);

    //Company
    invoiceRegisterEdit = false;
    invoiceitemEdit = false;
    $('.modal-dialog').draggable({ handle: ".modal-header" });
    CheckedIsFiscal('0');
    AttachmentsTypes();
});

setTimeout(function () { }, 2000);
//$(".forcenumericonly").ForceNumericOnly();

$('.Segment').on('change', function (e) {
    $('.accountl1').find('option').remove().end();
    $('.accountl2').find('option').remove().end();
    $('.accountl3').find('option').remove().end();
    $('.accountl4').find('option').remove().end();
    loadDataNotDependency(PrepareRequestGetCompaniesBySegment(this.value), null, "invoiceForm", "Company", true);
    populateaccountl1();
    
});

$('.Company').on('change', function (e) {
    loadDataNotDependency(PrepareRequestGetCurrenciesbyCompanies(this.value), null, "invoiceForm", "Currency", true);
});

$('.Currency').on('change', function (e) {
    var currency = this.value;
    if (currency > 0) { loadDepenceSelect($('.BAClass'), currency, Components.bankclasific); }
    else {  }
});

/************************************* Validating invoice Form  ******************************/
function validateinvoiceForm() {
    $("form[name='invoiceForm']").validate({
        rules: {
            company: {
                required: true,
                notEqualTo: 0
            },
            currency: {
                required: true,
                notEqualTo: 0
            },
            applicationdatestring: {
                required: true
            }
        },
        messages: {
            company: "Seleccione una empresa.",
            applicationdatestring: "Seleccione fecha.",
            currency: 'Debe seleccionar un moneda.'
        },
        errorElement: 'em',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
    });
    $.validator.addMethod(
        "notEqualTo",
        function (elementValue, element, param) {
            return elementValue != param;
        },
        "Value cannot be {0}"
    );
}

function validateinvoicetemForm() {
    $("form[name='invoiceitemForm']").validate({
        rules: {
            accountl3: {
                required: true,
                notEqualTo: 0
            },
            accountl4: {
                required: true,
                notEqualTo: 0
            },
            ammounttotal: {
                required: true,
                min: 1
            }
        },
        messages: {
            accountl3: "Seleccione una categoria",
            accountl4: "Seleccione un tipo de gasto",
            ammounttotal: "Capture un monto"
        },
        errorElement: 'em',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
        submitHandler: function (form) { }
    });

    $.validator.addMethod(
        "notEqualTo",
        function (elementValue, element, param) {
            return elementValue != param;
        },
        "Value cannot be {0}");
}

function openModalItems() {
    $("#myModal").modal({
        backdrop: "static"
    });
}

/********************************************************************************************************************/
/********************************************************************************************************************/
/******************************************* I N V O I C E   I T E M S  *********************************************/
/********************************************************************************************************************/
/********************************************************************************************************************/

$(".accountl1").on("change", function (e) {
    $('#accountl2,#category,#Type').find('option').remove().end();
    populateaccountl2(null, this.value);
});

$(".accountl2").on("change", function (e) {
    populateAccuntL3(null, this.value);
});

$(".category").on("change", function (e) {
    updateTypeCallBack(null, this.value, $("#Company").val(), 2);
});

$('#Ammount').focusout(function (event) {
    var el = this;
    if (_checkVal) {
        $.ajax({
            url: "/utilsapp/calculateTax",
            datatype: 'json',
            data: { value: this.value }
        }).done(function (data) {
            el.form.taxesAmmount.value = covertToNatural(data.data);
            calculateTotalDocitem();
        });
    } else {

    }
});

$("#taxesAmmount,#OthertaxesAmmount").focusout(function (event) {
    $('#Ammount').focusout();
    //CheckedIsFiscal()
});

$("#supplier").on("change", function (e) {
    var _this = this;
    if (_this.value == 1) {
        $('.supplierOther').removeClass('hide');
        $('.supplierOther').val("");
    } else {
        $('.supplierOther').addClass('hide');
    }
});


$('.BAClass').on("change", function (event) {
    loadAccountTypeProductbyBAClass(null, $("#Currency").val(), this.value, 'docPaymentsForm', 'BankAccntType');
});

$('.BankAccntType').on("change", function (event) {
    $.ajax({
        method: "GET",
        url: "/Formcontrol/getAccountByCurrencyProfile",
        data: { idCurrency: $('#Currency').val(), idBankAccntType: this.value, idCompany: $('#Company').val() },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            $('.PaymentMethod').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('.PaymentMethod').append($("<option></option>").attr("value", value.value).text(value.text));
            });
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $("#popup2").notify("No se puede obtener la cuenta.",
                {
                    position: "bottom right",
                    className: "error",
                    hideDuration: 500
                });
        });
});

$('#btnAddDocItem').click(function (event) {
    event.preventDefault();
    var frm = this.form;
    var _frm = "#" + frm.name;
    var initems = OInvoiceItemForm(_frm);

    switch (editingModelItem) {
        case true:
            // guardar solo DocItem
            invoiceItemDataSend = initems; // Adding to global variable
            OpenModalProcessing(".processingModal");
            jsPromiseSaveDocumentItem = Promise.resolve(PrepareActionItems(frmInvoiceItem, invoicesids, InvoiceItem.Update)); // Preparing to Save sending current Document
            jsPromiseSaveDocumentItem.then(function (response) {
                notifyMessageGral("Se ha añadido correctamente la información del gasto.", 'success', 800, '.modal-header');
                reloadGrid();
                evaluateUpload(invoicesids);
                evaluateComments(invoicesids);
            })
                .catch(function (error) {
                    notifyMessageGral("No se puede capturar el gasto.", 'error', 1500, '.modal-header');
                    console.log("Failed Request! ->", error);
                    reloadGrid();
                })
                .finally(function () {
                    ForceCloseModal(".processingModal");
                });
            break;
        case false:
            if (invoicesids == "" || invoicesids == 0 || invoicesids == null) {
                invoiceItemDataSend = initems; // Adding to global variable
                jsPromiseSaveDocument = Promise.resolve(PrepareActionInvoice(frmInvoice, invoiceDataSend, Invoice.Insert));
                OpenModalProcessing(".processingModal");
                jsPromiseSaveDocument.then(function (response) {
                    invoicesids = response.data.id;
                    InvoiceObject = response.data;
                    var invoice = response.data;
                    $('#invoiceIdentifier').text(invoice.identifier);
                    notifyMessageGral("Se ha añadido correctamente solo la información general de gastos.", 'success', 500, '.modal-header');
                    jsPromiseSaveDocumentItem = Promise.resolve(PrepareActionItems(frmInvoiceItem, invoicesids, InvoiceItem.Insert)); // Preparing DocItem Promise
                    $('#invoiceIdentifier').text(response.data.InvoiceIdentifier);
                    $("#Invoice").val(invoicesids);
                    evaluateUpload(invoicesids);
                    evaluateComments(invoicesids);
                })
                    .then(response => jsPromiseSaveDocumentItem)
                    .then(response => {
                        notifyMessageGral("Se ha añadido correctamente el gasto.", 'success', 800, '.modal-header');
                        //loadVTAInvoiceGridItem("jsGridInvoiceItems", InvoiceItem.load, invoicesids);
                        loadJSGridDocumentItem();
                    })
                    .catch(function (error) {
                        console.log("Failed Request! ->", error);
                        notifyMessageGral("No se puede capturar las facturas.", 'error', 1500, '.modal-header');
                    })
                    .finally(function () {
                        ForceCloseModal(".processingModal");
                    });
            }
            else {
                // guardar solo DocItem
                invoiceItemDataSend = initems; // Adding to global variable
                OpenModalProcessing(".processingModal");
                jsPromiseSaveDocumentItem = Promise.resolve(PrepareActionItems(frmInvoiceItem, invoicesids, InvoiceItem.Insert)); // Preparing to Save sending current Document
                jsPromiseSaveDocumentItem.then(function (response) {
                    notifyMessageGral("Se ha añadido correctamente la información del gasto.", 'success', 800, '.modal-header');
                    reloadGrid();
                    evaluateUpload(invoicesids);
                    evaluateComments(invoicesids);
                })
                    .catch(function (error) {
                        notifyMessageGral("No se puede capturar el gasto.", 'error', 1500, '.modal-header');
                        console.log("Failed Request! ->", error);
                        reloadGrid();
                    })
                    .finally(function () {
                        ForceCloseModal(".processingModal");
                    });
            }
            break;
    }


});

$('.btnAddDocPayment').click(function (event) {
    event.preventDefault();

    var identifierform = $(this).closest('form').attr('name');
    var payments = paymentInsert('#invoiceForm', '#docPaymentsForm')

    OpenModalProcessing(".processingModal");
    jsPromiseSaveDocumentPayment = Promise.resolve(PrepareRequestSavePayment(payments));
    jsPromiseSaveDocumentPayment.then(function (response) {
        notifyMessageGral("Se ha añadido correctamente la información del pago.", 'success', 800, '.modal-header');
        printJSGriInvoiceMovements();
    })
        .catch(function (error) {
            notifyMessageGral("No se puede capturar el pago.", 'error', 1500, '.modal-header');
            console.log("Failed Request! ->", error);
        })
        .finally(function () {
            printJSGriInvoiceMovements();
            ForceCloseModal(".processingModal");
        });
});


/*********************** SHOW INVOICE ITEM MODAL TO EDIT ***************************************/


/******************************** OPEN MODAL TO EDIT invoice ITEM *******************************************/
function openModalInvoiceItem() {
    $("#invoiceitemModal").modal({ backdrop: "static" });
}

$('.btnClosefrmInvoiceItem').on('click', function () {
    invoiceItemObject = {};
})







