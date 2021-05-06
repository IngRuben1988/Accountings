var invForm = $("#invoiceitemForm")
$(document).ready(function () {
    loadDataNotDependency(PrepareRequestGetCompaniesBySegment("1"), null, "searchForm", "company", null);
    //loadDateTimePermissionSearch(PrepareRequestGetDatePermissionsIncomeInvoice(), "appdate1", new Date(),"applicationDateIni");
    CheckedIsFiscal();
});

$(document).click(function () {
    if ($("#Invoice").val() != 0) {
        $("#btnUploadFile").prop('disabled', false);
        $("#btnSaveComments").prop('disabled', false);
    }
});

$('#appdate1').datetimepicker({
    format: 'DD/MM/YYYY'
    , locale: moment.updateLocale('es-us')
    , useCurrent: true
});


$('#appdate2').datetimepicker({
    format: 'DD/MM/YYYY'
    , locale: moment.updateLocale('es-us')
    , useCurrent: true
});

$('#appdate3').datetimepicker({
    format: 'DD/MM/YYYY'
    , locale: moment.updateLocale('es-us')
    , useCurrent: true
});

$('#appdate4').datetimepicker({
    format: 'DD/MM/YYYY'
    , locale: moment.updateLocale('es-us')
    , useCurrent: true
});


function validatesearchForm() {
    $("form[name='searchForm']").validate({
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


$('#btnAdvanceSearch').click(function () {
    printgridInvoice();
});

function printgridInvoice() {
    OpenModalProcessing(".processingModal");
    deleteDataTableInvoiceTR();
    var searchHead = {
        id: $('input[name=id]').val(),
        number: $("input[name=number]").val(),
        company: $("select[name=company]").val(),
        ammountIni: $("input[name=ammountIni]").val(),
        ammountEnd: $("input[name=ammountEnd]").val(),
        applicationDateIni: $("input[name=applicationDateIni]").val(),
        applicationDateFin: $("input[name=applicationDateFin]").val(),
        creationDateIni: $("input[name=creationDateIni]").val(),
        creationDateFin: $("input[name=creationDateFin]").val()
    }
    axios.post(Invoice.load,
        {
            id: searchHead.id,
            InvoiceNumber: searchHead.number,
            company: searchHead.company,
            ammountIni: searchHead.ammountIni,
            ammountEnd: searchHead.ammountEnd,
            applicationDateIni: searchHead.applicationDateIni,
            applicationDateFin: searchHead.applicationDateFin,
            creationDateIni: searchHead.creationDateIni,
            creationDateFin: searchHead.creationDateFin
        })
        .then(function (response) {
            invoiceObject = response.data.data;
            printDataTableInvoiceTR(invoiceObject);
        }).catch(function (error) {
            console.error("No se puede obtener la lista de los ingresos " + parent);
            notifyMessageGral("No se puede obtener la lista de ingresos.", 'error', 1500, '.info');
        }).finally(function () {
            ForceCloseModal(".processingModal");
        });

}

function printDataTableInvoiceTR(data) {
    var tr = "";
    var endtr = "</tr>";
    var resulttr = "";
    $('#tblInvoices > tbody').empty();
    if (data.length != 0) {
        $.each(data, function (key, value) {
            tr = "<tr class='invoices_class_tr' id='invoices_" + value.id + "'>";
            // if (value.length != 0) {
            resulttr = (tr + printDataTableInvoiceTD(value) + endtr);
            $('#tblInvoices > tbody:last-child').append(resulttr);
            // }
        });
    }
}

function printDataTableInvoiceTD(data) {
    var actions = "",editar="", eliminar="";
    editar = "<input type='button' class='tablegrid-button-edit tablegrid-edit-button' data-toggle='tooltip' data-placement='top' title='Editar' onClick='HideworkingSearch(" + data.id + ")' style='margin-right:15px;'>";
    eliminar = "<input type='button' class='tablegrid-button tablegrid-delete-button' data-toggle='tooltip' data-placement='top' title='Borrar' onclick='deleteInvoiceOnly(" + data.id + ")'>";
        
    actions = "<td style='width: 100px;'>" + editar + eliminar +"</td>";
    var result =
        "<td style='text-align: center !important;'>" + data.id + "</td>" +
        "<td style='text-align: center !important;'>" + data.identifier + " </td>" +
        "<td style='text-align: right; padding-right: 4rem !important;'>" + data.costString + "</td>" +
        "<td style='text-align: center !important;'>" + data.creactiondatestring + "</td>" +
        "<td style='text-align: center !important;'>" + data.applicationdatestring + "</td>" + actions;
    return result;
}

function deleteDataTableInvoiceTR() {
    $('.invoices_class_tr').remove();
}

// Working Search
function HideworkingSearch(index) {
    $(".workingSearch").hide();
    document.title = "VTA - Edición de gasto";
    invoicesids = index;
    var invoice 
    $.each(invoiceObject, function (i, value) {
        if (value.id == index) {
            invoiceObjectList = value
        }
    });

    // Get DOcuments type to Attach
    prepareEdit('#invoiceForm', invoiceObjectList);

    ShowworkingInvoice();

    setTimeout(function () {
        printJSGriInvoiceMovements()
    }, 2000);
}

function ShowworkingSearch() {
    $(".workingSearch").show();
    SegmentCompanyEdit = false;
}
// Working Invoice
function HideworkingIncome() {
    $(".divInvoiceGral").hide();
    ShowworkingSearch();
    document.title = "VTA - Detalles de gasto";
    // clearAllData();
    incomeObject = {};
}

function deleteInvoiceOnly(index) {
    //var _income = invoiceObject[index]; //   identifier
    var text = '¿Esta seguro que desea eliminar el gasto ' + index + ' ?';
    $.confirm({
        title: 'Eliminación de ingreso',
        content: text,
        theme: 'material',
        type: 'orange',
        buttons: {
            si: function () {
                OpenModalProcessing(".processingModal");
                var jsPromiseSaveIncome = Promise.resolve(PrepareRequestDeleteInvoice(index));
                jsPromiseSaveIncome.then(function (response) {
                    notifyMessageGral("Se ha eliminado correctamente el ingreso.", 'success', 500, '.info');
                    printgridInvoice();
                })
                    .catch(function (error) {
                        console.log("Failed Request! -> error al borrar registro income.", error);
                        notifyMessageGral("No se puede eliminar el ingreso ingreso.", 'error', 500, '.info');
                    })
                    .finally(function () {
                        ForceCloseModal(".processingModal");
                    });
            },
            no: function () {
                result = false;
            }
        }
    });
}

function PrepareRequestDeleteInvoice(id) {
    var Request = $.ajax({
        method: "POST",
        url: Invoice.Delete,
        datatype: 'json',
        data: { id: id },
    })
        .done(function (response, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return Request;
}


function prepareEdit(formName, income) {
    populateToEdit(formName, income);
    setTimeout(function () {
        adapterInvoice()
        CloseModalProcessing(".processingModal");
    }, 2000);

}

function adapterInvoice() {
    invoiceItemObject;
    invoicePaymentsObject;
    //$('.Segment').change();
    //$('.Company').change();
    //$('.Currency').change();
    var invoice = invoiceObject.find(data => data.id === invoicesids);
    $('#invoiceIdentifier').text(invoice.identifier);
    $("#Invoice").val(invoicesids);
    var invoiceform = $('#invoiceForm');
    //$('select[name=Segment]').val(invoice.Segment);
    $('select[name=Company]').val(invoice.company);
    //$('.Company').change();
    $('input[name=ApplicationDateString0]').val(invoice.applicationdatestring);
    $('select[name=Currency]').val(invoice.currency);
    $('.Currency').change();
}

function populateToEdit(formName, income) {
    OpenModalProcessing(".processingModal");
    var form = $(formName);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.companies), income.segment, 'invoiceForm', 'Segment', true);
    //loadDataNotDependency(PrepareRequestGetCompaniesBySegment(income.segment), null, "invoiceForm", "Company", true);
    loadDataNotDependency(PrepareRequestGetCurrenciesbyCompanies(income.company), income.currency, "invoiceForm", "Currency", true);
    loadDateTimePermissionIncomeInvoice(PrepareRequestGetDatePermissionsIncomeInvoice(), "datetimepicker1", new Date());
    loadDataNotDependency(PrepareRequestGetAnyData(Components.suppliers), null, "invoiceitemForm", "supplier", true);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.bugetypes), null, "invoiceitemForm", "budgetType", true);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.bankaccon), null, "invoiceitemForm", "BankAccntType", true);
    //loadVTAInvoiceGridItem("jsGridInvoiceItems", InvoiceItem.load, invoicesids);
    loadJSGridDocumentItem()
    AttachmentsTypes();
    getAttachments(invoicesids, Invoice.GetAttach)
    getComments(invoicesids, Invoice.getComment)
}


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
    else { /*$(this).empty();*/ }
});

$(".accountl1").change(function (e) {
    $('#accountl2,#category,#Type').find('option').remove().end();
    populateaccountl2(null, this.value);
});

$(".accountl2").change(function (e) {
    populateAccuntL3(null, this.value);
});

$(".category").change(function (e) {
    updateTypeCallBack(null, this.value, $("#Company").val(), $(".accountl1").val());
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
        data: { idCurrency: $('#Currency').val(), idBankAccntType: this.value },
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
                //printJSGriInvoiceMovements();
                CloseModalProcessing(".processingModal");
                ForceCloseModal(".processingModal");
            })
                .catch(function (error) {
                    notifyMessageGral("No se puede capturar el gasto.", 'error', 1500, '.modal-header');
                    console.log("Failed Request! ->", error);
                    reloadGrid();
                    //printJSGriInvoiceMovements();
                    CloseModalProcessing(".processingModal");
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
                    Invoice = response.data.id;
                    notifyMessageGral("Se ha añadido correctamente solo la información general de gastos.", 'success', 500, '.modal-header');
                    jsPromiseSaveDocumentItem = Promise.resolve(PrepareActionItems(frmInvoiceItem, invoicesids, InvoiceItem.Insert)); // Preparing DocItem Promise
                    $('#invoiceIdentifier').text(response.data.InvoiceIdentifier);
                })
                    .then(response => jsPromiseSaveDocumentItem)
                    .then(response => {
                        notifyMessageGral("Se ha añadido correctamente el gasto.", 'success', 800, '.modal-header');
                        loadVTAInvoiceGridItem("jsGridInvoiceItems", InvoiceItem.load, invoicesids);
                        //reloadGridPayments();
                        CloseModalProcessing(".processingModal");
                        ForceCloseModal(".processingModal");
                    })
                    .catch(function (error) {
                        console.log("Failed Request! ->", error);
                        CloseModalProcessing(".processingModal");
                        ForceCloseModal(".processingModal");
                        notifyMessageGral("No se puede capturar las facturas.", 'error', 1500, '.modal-header');
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
                    //reloadGridPayments();
                    CloseModalProcessing(".processingModal");
                    ForceCloseModal(".processingModal");
                })
                    .catch(function (error) {
                        notifyMessageGral("No se puede capturar el gasto.", 'error', 1500, '.modal-header');
                        console.log("Failed Request! ->", error);
                        reloadGrid();
                        //reloadGridPayments();
                        CloseModalProcessing(".processingModal");
                        ForceCloseModal(".processingModal");
                    });
            }
            break;
    }
});

$('.btnAddDocPayment').click(function (event) {
    event.preventDefault();

    var identifierform = $(this).closest('form').attr('name');
    // gettin all data from grid
    // //gridDataPayment = getGridDataPayment();
    //var _payment = OPaymentForm(_frm);
    //documentPaymentDataSend = _payment;
    //var r = 0.0;
    //r = calculateRemain2($("#Costpay").val(),_payment.chargedAmount + parseFloat($("#Costpay").val())) /// validando que pagos no sean mayores al saldo.
    ///var _totalPagos = calculatecurrentpay(parseFloat(_payment.chargedAmount));
    //if (r < 0) {
    // notifyMessageGral("No es posible agregar un pago mayor al saldo restante.", "warn", null, ".modal-body");
    //} else {
    //_totalPagos = parseFloat(_totalPagos);
    //var saldo = 0.0;
    //saldo = calculateRemainFondo(_budget.balance, _payment.chargedAmount);
    //if (saldo >= 0) // Validando que el pago no sea mayor al saldo de la cuenta
    //{
    var payments = paymentInsert('#invoiceForm', '#docPaymentsForm')

    OpenModalProcessing(".processingModal");
    jsPromiseSaveDocumentPayment = Promise.resolve(PrepareRequestSavePayment(payments));
    jsPromiseSaveDocumentPayment.then(function (response) {
        notifyMessageGral("Se ha añadido correctamente la información del pago.", 'success', 800, '.modal-header');
        printJSGriInvoiceMovements();
        ForceCloseModal(".processingModal");
    })
        .catch(function (error) {
            notifyMessageGral("No se puede capturar el pago.", 'error', 1500, '.modal-header');
            console.log("Failed Request! ->", error);
            printJSGriInvoiceMovements();
            ForceCloseModal(".processingModal");
        });
    //} else {
    //    // En caso contrario de no haber saldo, si la cuenta por medio del tipo de producto admite negativos se procede a capturar el pago de lo contrario mensaje
    //    if (_budget.allowsNegatives == true) {
    //        OpenModalProcessing(".processingModal");
    //        jsPromiseSaveDocumentPayment = Promise.resolve(PrepareRequestSavePayment(invoiceObject));
    //        jsPromiseSaveDocumentPayment.then(function (response) {
    //            notifyMessageGral("Se ha añadido correctamente la información del pago.", 'success', 800, '.modal-header');
    //            printJSGriInvoiceMovements();
    //            CloseModalProcessing(".processingModal");

    //        })
    //        .catch(function (error) {
    //            notifyMessageGral("No se puede capturar el pago.", 'error', 1500, '.modal-header');
    //            console.log("Failed Request! ->", error);
    //            printJSGriInvoiceMovements();
    //            CloseModalProcessing(".processingModal");
    //        });
    //    }
    //    else {
    //        notifyMessageGral("No es posible agregar el pago ya que no existe fondo disponible o sobrepasa el saldo de este método de pago.", "warn", null, ".modal-body");
    //    }
    //}
    //}
});


function ShowworkingInvoice() {
    $(".divInvoiceGral").show();
}

function openModalItems() {

    $("#myModal").modal({
        backdrop: "static"
    });
}