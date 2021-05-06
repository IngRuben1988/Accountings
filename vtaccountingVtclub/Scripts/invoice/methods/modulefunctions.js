/*************** INVOICE METHODS *****************/

function clearJSGridDocumentItem() {
    $("#jsGridInvoiceItems").jsGrid("option", "data", []);
}

function reloadGrid() {
    //loadVTAGridItem("jsGridInvoiceItems", InvoiceItem.load, invoicesids);
    $("#jsGridInvoiceItems").jsGrid("loadData");
}

function reloadGridPayments() {
    $("#jsGridInvoicePayment").jsGrid("loadData");
}

$('#printitemform').click(function () {
    CheckedIsFiscal('0');
});

function CheckedIsFiscal(yesno) {
    if (yesno == '1') {
        $('.taxesAmmount').show();
        $('.OthertaxesAmmount').val("0");
        $('.billIdentifier').val("");
        _checkVal = true;
        $('.billIdentifier').show();
        calculateTotalDocitem();
    }
    else {
        $('.taxesAmmount').hide();
        $('.OthertaxesAmmount').val("0");
        _checkVal = false;
        $('.billIdentifier').hide();
        $('.taxesAmmount').val("0")
        calculateTotalDocitem();
    }

}

function calculateTotalDocitem() {
    var Totals = $('.calcTotal');
    var _values = [];
    Totals.each(function (s, b) { _values.push(parseFloat(b.value.replace(/[$,]+/g, ""))) });
    $('.taxesAmmountTotalBill').text(_values.reduce(function (a, b) { return a + b }));
}

function prepareToEditInvoiceItem(index) {
    invoiceItemObject = invoiceItemsObjectList[index];
    $("#invoiceitemModal").modal({ backdrop: "static" });
    populateToEditInvoiceeItem("#invoiceitemForm", invoiceItemObject)
}

function populateToEditInvoiceItem(formName, invoiceItem) {
    var form = $(formName);
    form.find("input[name=item]").val(invoiceItem.item);
    form.find("input[name=parent]").val(invoiceItem.parent);
    form.find("input[name=ammounttotal]").val(invoiceItem.ammounttotal);
    form.find("textarea[name=description]").val(invoiceItem.description);
    loadDataNotDependency(PrepareRequestGetAccl1(invoiceItem.segment, 1), invoiceItem.accountl1, "invoiceitemForm", "accountl1", true);
    loadDataNotDependency(PrepareRequestGetAccl2(invoiceItem.accountl1, invoiceItem.segment, 1), invoiceItem.accountl2, "invoiceitemForm", "accountl2", true);
    loadDataNotDependency(PrepareRequestGetAccl3(invoiceItem.accountl2, invoiceItem.segment, 1), invoiceItem.accountl3, "invoiceitemForm", "accountl3", true);
    loadDataNotDependency(PrepareRequestGetAccl4(invoiceItem.accountl3, invoiceItem.segment, 1), invoiceItem.accountl4, "invoiceitemForm", "accountl4", true);
}

function calculateRemain2(cost,eval) {
    var num = parseFloat(cost) - parseFloat(eval);
    return num.toFixed(2);
}
/*************** INVOICE METHODS MODULE *****************/
function printJSGridInvoice(parent) {
    $("#SearchGrid").jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        autoload: true,
        paging: true,
        pageIndex: 1,
        pageSize: 15,
        pageButtonCount: 30,
        pagerFormat: "Páginas: {first} {prev} {pages} {next} {last}   |   {pageIndex} de {pageCount}",
        pagePrevText: "Ant",
        pageNextText: "Sig",
        pageFirstText: "Primera",
        pageLastText: "Última",
        pageNavigatorNextText: "...",
        pageNavigatorPrevText: "...",
        noDataContent: "Sin datos...",
        loadMessage: "Espere, obteniendo ...",
        deleteConfirm: "Desea borrar el registro?",
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    url: App.loadUrl,
                    dataType: "json",
                    data: $('#searchForm').serializeArray()
                }).done(function (response) {
                    d.resolve(response.data);
                });
                return d.promise();
            },
            deleteItem: function (item, val) {
                var r = confirm("¿Desea eliminar este elemento?");
                if (r == true) {
                    $.ajax({
                        method: "POST",
                        url: App.itemDelete,
                        data: {
                            id: item.Invoice
                        },
                        datatype: 'json'
                    })
                        .done(function (data, textStatus, jqXHR) {
                            if (data.success === true) {
                                notifyMessageGral("Se ha eliminado correctamente el concepto.", "success", 500, null);
                            }
                            else {
                                notifyMessageGral("No se completo la acción eliminar.", "error", 500, null);
                            }
                        })
                        .fail(function (jqXHR, textStatus, errorThrown) { notifyMessageGral("No se puede realizar la petición de eliminar", "error", 500, null); });
                }
            },
            updateItem: function (item, val) {

            }
        },
        rowClass: function (item) { return item.statusExpenses === 2 ? 'highlight-green' : item.statusExpenses === 3 ? 'highlight-red' : ""; },
        fields: [
            { title: "#", name: "InvoiceIdentifier", type: "text", width: 150, validate: "required" },
            { title: "Unidad de negocio", name: "CompanyName", type: "text", width: 150 },
            {
                title: "Costo gasto", name: "CostString", type: "number", width: 100,
                itemTemplate: function (value) {
                    return " $ " + value;
                }
            },
            {
                title: "Pagos", name: "PaymentsString", type: "number", width: 100,
                itemTemplate: function (value) {
                    return " $ " + value;
                }
            },
            { title: "Fecha captura", name: "CreactionDateString", type: "text", width: 80, align: "center" },
            { title: "Fecha aplicación", name: "ApplicationDateString", type: "text", width: 80, align: "center" },
            // { title : "Descripción", name: "Description", type: "text", width: 300 },
            {
                title: " ", name: "attachments", type: "text", width: 15,
                itemTemplate: function (value, index) {
                    if (value != null) {
                        var documentshtml = "";
                        $.each(value, function (key, value) {
                            documentshtml = documentshtml + " " + value.nombrearchivo + "<br>";
                        });
                        return $("<div>").addClass("seeDocstooltip").append("<i class='fas fa-paperclip fa-lg' style=''></i> <div class='seeDocstooltiptext'>" + documentshtml + "</div>");
                    }
                }
            },
            {
                title: " ", name: "Invoice", type: "text", width: 15,
                itemTemplate: function (value, item) {
                    if (item.statusExpenses === 2) { return ""; } else {
                        return $("<div>").addClass("rating").append("<i class='fa fa-edit fa-lg editInvoice' data-toggle='tooltip' title='Editar' data-original-title='Editar' onclick='HideworkingSearch(" + value + ")'></i>");
                    }
                }
            }
        ],
        onDataLoaded: function (grid, data) {
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function printJSGridInvoiceItem(parent) {
    deleteDataTableInvoiceItemTR();
    axios.get("/invoice/getinvoiceitemsdetails", { params: { id: parent } })
        .then(function (response) {
            ForceCloseModal(".processingModal");

            invoiceItemsObjectList = response.data.data;
            printDataTableInvoiceItemTR(response.data.data)
        }).catch(function (error) {
            ForceCloseModal(".processingModal");
            console.error("No se puede obtener la lista de los detalles de gasto " + parent);
            notifyMessageGral("No se puede obtener la lista de ingresos.", 'error', 1500, '.info');
        }).finally(function () {
            ForceCloseModal(".processingModal");
            calculateTotalInvoiceItemList();
        });
}

function DeleteHead(ids) {
    if (editingModelItem != true) {
        if (Invoice != null || Invoice != "") {
            OpenModalProcessing(".processingModal");
            jsPromiseDeleteInvoiceItem = Promise.resolve(PrepareDeleteItems(ids, InvoiceItem.Delete));//PrepareRequestDeleteDocumentItem(item));
            jsPromiseDeleteInvoiceItem.then(function (response) {
                notifyMessageGral("Se ha eliminado correctamente la información del gasto.", 'success', 800, '.modal-header');
                reloadGrid();
                //reloadGridPayments();
                //CloseModalProcessing(".processingModal");
            })
            .catch(function (error) {
                notifyMessageGral("No se puede eliminar el gasto.", 'error', 1500, '.modal-header');
                console.log("Failed Request! ->", error);
                reloadGrid();
                //reloadGridPayments();
                ForceCloseModal(".processingModal");
            });
        }
    }
}

function DeleteItem(ids) {

        if (editingModelItem != true) {
            if (Invoice != null || Invoice != "") {
                OpenModalProcessing(".processingModal");
                jsPromiseDeleteInvoiceItem = Promise.resolve(PrepareDeleteItems(ids, InvoiceItem.Delete));//PrepareRequestDeleteDocumentItem(item));
                jsPromiseDeleteInvoiceItem.then(function (response) {
                    notifyMessageGral("Se ha eliminado correctamente la información del gasto.", 'success', 800, '.modal-header');
                    reloadGrid();
                    //reloadGridPayments();
                    //CloseModalProcessing(".processingModal");
                    ForceCloseModal(".processingModal");
                })
                    .catch(function (error) {
                        notifyMessageGral("No se puede eliminar el gasto.", 'error', 1500, '.modal-header');
                        console.log("Failed Request! ->", error);
                        reloadGrid();
                        //reloadGridPayments();
                        ForceCloseModal(".processingModal");
                    });
            }
        }
    //calculateDocumentValue();
}

function UpdateHead(parameter) {
    invoiceItemDataSend = initems; // Adding to global variable
    OpenModalProcessing(".processingModal");
    jsPromiseSaveDocumentItem = Promise.resolve(PrepareActionItems(frmInvoiceItem, invoicesids, InvoiceItem.Update)); // Preparing to Save sending current Document
    jsPromiseSaveDocumentItem.then(function (response) {
        notifyMessageGral("Se ha añadido correctamente la información del gasto.", 'success', 800, '.modal-header');
        reloadGrid();
        //reloadGridPayments();
        ForceCloseModal(".processingModal");
    })
        .catch(function (error) {
            notifyMessageGral("No se puede capturar el gasto.", 'error', 1500, '.modal-header');
            console.log("Failed Request! ->", error);
            reloadGrid();
            //reloadGridPayments();
            CloseModalProcessing(".processingModal");
        });
}

function UpdateItem(parameter) {
    if (editingModelItem != true) {
        if (Invoice != null || Invoice != "") {
            OpenModalProcessing(".processingModal");
            jsPromiseDeleteInvoiceItem = Promise.resolve(PrepareDeleteItems(parameter, InvoiceItem.Update));//PrepareRequestDeleteDocumentItem(item));
            jsPromiseDeleteInvoiceItem.then(function (response) {
                notifyMessageGral("Se ha eliminado correctamente la información del gasto.", 'success', 800, '.modal-header');
                reloadGrid();
                //reloadGridPayments();
                ForceCloseModal(".processingModal");
            })
                .catch(function (error) {
                    notifyMessageGral("No se puede eliminar el gasto.", 'error', 1500, '.modal-header');
                    console.log("Failed Request! ->", error);
                    reloadGrid();
                    //reloadGridPayments();
                    ForceCloseModal(".processingModal");
                });
        }
    }
    //calculateDocumentValue();
}

function editModelItem(_form, arg) {
    docItemODataSelected = arg.item;
    var check;
    editingModelItem = true;
    var form = $(_form);
    if (arg.item.istax == true) {
        $("#Tax1").prop('checked', true);
        check = '1';
    } else {
        $("#Tax2").prop('checked', true);
        check = '0';
    }
    CheckedIsFiscal(check);

    form.find("input[id=document]").val(arg.item.id);
    form.find("input[id=Item]").val(arg.item.item);
    form.find("select[id=accountl1]").val(arg.item.accountl1);
    $(".accountl1").change();
    form.find("input[name=Ammount]").val(arg.item.ammount);
    form.find("input[name=taxesAmmount]").val(arg.item.taxesammount);
    form.find("input[name=OthertaxesAmmount]").val(arg.item.othertaxesammount);
    form.find("label[name=taxesAmmountTotalBill]").text(arg.item.ammounttotalstring);
    form.find("input[name=billIdentifier]").val(arg.item.identifier);
    form.find("select[name=supplier]").val(arg.item.supplier);
    form.find("select[name=budgetType]").val(arg.item.budgettype);
    form.find("input[name=supplierOther]").val(arg.item.supplierother);
    form.find("input[name=singleExibitionPayment]").val(arg.item.singlexibitionpayment == true ? "true" : "false");
    form.find("textarea[name=description]").val(arg.item.description);

    setTimeout(function () {
        form.find("select[name=accountl2]").val(arg.item.accountl2);
        $(".accountl2").change();
    }, 2000);

    setTimeout(function () {
        form.find("select[name=category]").val(arg.item.accountl3);
        $(".category").change();
    }, 6000);

    setTimeout(function () {
        form.find("select[name=accountl4]").val(arg.item.accountl4);
    }, 10000);
}


function paymentInsert(objects, payment) {
    var head = $(objects);
    var body = $(payment);

    invoicePayments.Payment             = 0;
    invoicePayments.Invoice             = invoicesids;
    invoicePayments.InvoiceIdentifier   = body.find("select[name=BAClass]").val();
    invoicePayments.PaymentMethod       = body.find("select[name=BankAccntType]").val();
    invoicePayments.PaymentMethodNam    = '';
    invoicePayments.BankAccntType       = body.find("select[name=BankAccntType]").val();
    invoicePayments.BankAccntTypeName   = '';
    invoicePayments.CurrencyPay         = body.find("select[name=Currency]").val();
    invoicePayments.CurrencyPayName     = '';
    invoicePayments.Segment             = head.find("select[id=Segment]").val();
    invoicePayments.SegmentName         = '';
    invoicePayments.Company             = head.find("select[id=Company]").val();
    invoicePayments.CompanyOrder        = 0;
    invoicePayments.CompanyName         = '';
    invoicePayments.chargedAmount       = body.find("input[name=chargedAmount]").val();
    invoicePayments.chargedAmountString = '';
    invoicePayments.taxesAmmount        = 0;
    invoicePayments.taxesAmmountString  = '';
    invoicePayments.authRef             = body.find("textarea[name=authRef]").val();
    invoicePayments.creationDate        = null; 
    invoicePayments.creationDateString  = null;
    invoicePayments.aplicationDate      = head.find("input[id=applicationdatestring]").val();
    invoicePayments.aplicationDateString = null;
    invoicePayments.description         = body.find("textarea[name=authRef]").val();
    invoicePayments.tblcurrency         = 0;
    invoicePayments.tblinvoice          = 0;

    return invoicePayments;
}

var paymentObject = {

    Payment: 0,
    document: 0,
    chargedAmount: 0,
    authRef: "",
    BankAccntType: 0,
    BankAccntTypeName: "",
    PaymentMethod: 0,
    PaymentMethodName: ""

}

function OPaymentForm(_stringfrm) {
    var _this = paymentObject;
    var form = $(_stringfrm);

    _this.Payment = 0;
    _this.document = 0;
    _this.PaymentMethod = Number(form.find("select[name=PaymentMethod]").find(":selected").val());
    _this.PaymentMethodName = form.find("select[name=PaymentMethod]").find(":selected").text();
    _this.BankAccntType = Number(form.find("select[name=BankAccntType]").find(":selected").val());
    _this.BankAccntTypeName = form.find("select[name=BankAccntType]").find(":selected").text();
    _this.chargedAmount = parseFloat(form.find("input[name=chargedAmount]").val());
    _this.authRef = form.find("textarea[name=authRef]").val();
    return _this;
}