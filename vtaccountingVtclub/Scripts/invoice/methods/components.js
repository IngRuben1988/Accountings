function loadJSGridDocumentItem() {
    $("#jsGridInvoiceItems").jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        autoload: true,
        paging: true,
        autoload: true,
        loadMessage: "Cargando, espere...",
        noDataContent: "Sin datos...",
        confirmDeleting: false,
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    url: InvoiceItem.load,
                    dataType: "json",
                    data: {
                        parameter: invoicesids
                    },
                }).done(function (response) {
                    d.resolve(response.data);
                    var resp = response.data;
                    if (resp) {
                        subtotalpagado = 0.00;
                        $.each(resp[0], function (key, value) {
                            if (key == "ammount") {
                                subtotalpagado = subtotalpagado + parseFloat(value);
                            }
                        });
                        $('#toTalCost').text(covertToNatural(parseFloat(subtotalpagado)));
                    }

                });
                return d.promise();
            },
            deleteItem: function (item, val) {

                $.confirm({
                    title: 'Eliminar concepto de gasto',
                    content: '¿Esta seguro que desea eliminar la información de gasto actual?',
                    theme: 'material',
                    type: 'orange',
                    buttons: {
                        si: function () {
                            DeleteItem(item.item);
                        },
                        no: function () { loadJSGridDocumentItem(); result = false; }
                    }
                });
            },
            updateItem: function (val, item) {
                UpdateItem(item)
            }
        },

        //rowDoubleClick: function (arg) {
        //    openModalItems();
        //    editModelItem("#invoiceitemForm", arg);
        //},
        rowClass: function (item) {
            return item.status == 2 ? 'highlight-green' : item.status == 3 ? 'highlight-red' : ""
        },
        fields: [{
            title: "",
            name: "Item",
            type: "number",
            width: 70,
            visible: false,
            align: "center",
            editing: false
        }, {
            title: "Invoice",
            name: "id",
            type: "number",
            width: 70,
            visible: false,
            editing: false
        }, {
            title: "accountl1",
            name: "accountl1",
            type: "number",
            width: 70,
            visible: false
        }, {
            title: "accountl2",
            name: "accountl2",
            type: "number",
            width: 70,
            visible: false
        }, {
            title: "category",
            name: "accountl3",
            type: "select",
            width: 70,
            visible: false
        }, {
            title: "Categoria",
            name: "accountl3name",
            type: "text",
            width: 120,
            align: "center"
        }, {
            title: "Type",
            name: "accountl4",
            type: "int",
            width: 70,
            visible: false
        }, {
            title: "Tipo",
            name: "accountl4name",
            type: "text",
            width: 120,
            align: "center"
        },
        {
            title: "SubTotal",
            name: "ammount",
            type: "number",
            width: 50,
            validate: {
                validator: "range",
                param: [0, 999999999.00],
                title: "Valor incorrecto",
                message: "Capture un valor de entre 0 y 999999999.00",
            }
        },
        {
            title: "IVA",
            name: "taxesammount",
            type: "number",
            width: 50,
            validate: {
                validator: "range",
                param: [0, 999999999.00],
                title: "Valor incorrecto",
                message: "Capture un valor de entre 0 y 999999999.00",
            }
        },
        {
            title: "Otros Impuestos",
            name: "othertaxesammount",
            type: "number",
            width: 50,
            validate: {
                validator: "range",
                param: [0, 999999999.00],
                title: "Valor incorrecto",
                message: "Capture un valor de entre 0 y 999999999.00",
            }
        }, {
            title: "Total",
            name: "AmmountTotal",
            type: "number",
            width: 50,

        }, {
            title: "Una exibición",
            name: "singlexibitionpayment",
            type: "checkbox",
            width: 50,
            align: "center",
            itemTemplate: function (value, item) {
                return item.singleExibitionPayment == true ? 'Si' : 'No';
            }
        },
        {
            title: "Descripción",
            name: "description",
            type: "text",
            width: 150,
            align: "left"
        },
        {
            title: "Fiscal",
            name: "istax",
            type: "bool",
            width: 50,
            align: "center",
            itemTemplate: function (value, item) {
                return item.isTax == true ? 'Si' : 'No';
            }
        },
        {
            title: "# Factura",
            name: "identifier",
            type: "text",
            width: 50,
            align: "center"
        },
        {
            title: "supplier",
            name: "supplier",
            type: "number",
            width: 50,
            align: "center",
            visible: false
        },
        {
            title: "Proveedor",
            name: "suppliername",
            type: "text",
            width: 110,
            align: "center"
        }, {
            title: "Prov. Otro",
            name: "supplierother",
            type: "text",
            width: 100,
            align: "center"
        }, {
            type: "control", /*editButton: true,*/ deleteButton: true,
            itemTemplate: function (value, item) {
                var $result = $([]);
                /*$result = $result.add(this._createEditButton(item));*/
                $result = $result.add(this._createDeleteButton(item));
                return $result;
            }
        }],
        /*fields: [
            { title: "", name: "Item", type: "number", width: 70, visible: false, align: "center", editing: false },
            { title: "document", name: "document", type: "number", width: 70, visible: false, editing: false },
            { title: "accountl1", name: "accountl1", type: "number", width: 70, visible: false },
            { title: "accountl2", name: "accountl2", type: "number", width: 70, visible: false },
            { title: "category", name: "category", type: "select", width: 70, visible: false },
            { title: "Categoria", name: "categoryName", type: "text", width: 120, align: "center" },
            { title: "Type", name: "Type", type: "int", width: 70, visible: false },
            { title: "Tipo", name: "TypeName", type: "text", width: 120, align: "center" },
            { title: "SubTotal", name: "Ammount", type: "number", width: 50, validate: { validator: "range", param: [0, 999999999.00], title: "Valor incorrecto", message: "Capture un valor de entre 0 y 999999999.00", } },
            { title: "IVA", name: "taxesAmmount", type: "number", width: 50, validate: { validator: "range", param: [0, 999999999.00], title: "Valor incorrecto", message: "Capture un valor de entre 0 y 999999999.00", } },
            { title: "Otros Impuestos", name: "OthertaxesAmmount", type: "number", width: 50, validate: { validator: "range", param: [0, 999999999.00], title: "Valor incorrecto", message: "Capture un valor de entre 0 y 999999999.00", } },
            { title: "Total", name: "ammounttotal", type: "number", width: 50, },
            {
                title: "Una exibición", name: "singleExibitionPayment", type: "checkbox", width: 50, align: "center",
                itemTemplate: function (value, item) {
                    return item.singleExibitionPayment == true ? 'Si' : 'No';
                }
            },
            { title: "Descripción", name: "description", type: "text", width: 150, align: "left" },
            {
                title: "Fiscal", name: "isTax", type: "bool", width: 50, align: "center",
                itemTemplate: function (value, item) {
                    return item.isTax == true ? 'Si' : 'No';
                }
            },
            { title: "# Factura", name: "billIdentifier", type: "text", width: 50, align: "center" },
            { title: "supplier", name: "supplier", type: "number", width: 50, align: "center", visible: false },
            { title: "Proveedor", name: "supplierName", type: "text", width: 110, align: "center" },
            { title: "Prov. Otro", name: "supplierOther", type: "text", width: 100, align: "center" },
            { title: "budgetType", name: "budgetType", type: "number", width: 50, align: "center", visible: false },
            { title: "Tipo Pres.", name: "budgetTypeName", type: "text", width: 100, align: "center" },
            {
                title: " ", name: "Item", type: "text", width: 20,
                itemTemplate: function (value, item) {
                    if (item.statusExpenses === 2) { return ""; } else {
                        return $("<div>").addClass("rating").append("<i class='fa fa-trash fa-lg ' data-toggle='tooltip' title='Eliminar' data-original-title='Eliminar' onclick='deleteDocitem(" + item.Item + ")'></i>");
                    }
                }
            }
        ],*/
        onDataLoaded: function (grid, data) {
            //calculateDocumentValue();
        }
    });
}

function populateaccountl1() {
    $.ajax({
        method: "GET",
        url: accountlevels.level1,
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            $.each(data.data, function (key, value) {                
                    $('#accountl1').append($("<option></option>").attr("value", value.value).text(value.text));                
            });
            $("#accountl1 option[value='1']").remove();
            invForm.find("select[name=accountl1]").val('2').trigger("change");
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });
}

function populateaccountl2(selected, id) {
    $.ajax({
        method: "GET",
        url: accountlevels.level2,
        data: { id: id },
        datatype: 'json',
    })
        .done(function (data, textStatus, jqXHR) {
            $("#accountl2").removeAttr("disabled");
            $('#accountl2').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('#accountl2').append($("<option></option>").attr("value", value.value).text(value.text));
            });
            if (selected) {
                $('#accountl2').val(selected);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });
    return true;
}

function populateAccuntL3(selected, id) {
    $.ajax({
        method: "GET",
        url: accountlevels.level3,
        data: { id: id },
        datatype: 'json',
    })
        .done(function (data, textStatus, jqXHR) {
            $("#category").removeAttr("disabled");
            $('#category').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('#category').append($("<option></option>").attr("value", value.value).text(value.text));
            });

            if (selected) {
                $('#category').val(selected);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });
}

function updateTypeCallBack(selected, category, Company, accl1) {
    $.ajax({
        url: accountlevels.level4,
        type: "GET",
        datatype: 'json',
        data: { id: category, Company: Company, accl1: accl1 },
        success: function (data, textStatus, jqXHR) {
            $("#Type").removeAttr("disabled");
            $('#Type').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('#Type').append($("<option></option>").attr("value", value.value).text(value.text));
            });
            if (selected) {
                $("#Type").val(selected);
            }
        },
        error: function () {
            $("#info").notify("No se puede obtener los tipos de gastos.", {
                position: "bottom right",
                className: "error",
                hideDuration: 500
            });
        }
    });
}

function loadAccountTypeProductbyBAClass(selected, currency, baclass, formName, tochild) {
    var jsPromiseGetBAccountTypebyClass = Promise.resolve(PrepareRequestGetBankAccTypebyClas(currency, baclass));
    jsPromiseGetBAccountTypebyClass.then(function (response) {
        var _search = "select[name=" + tochild + "]"; // form removing options
        var _formName = "#" + formName + "";
        var form = $(_formName);
        var valueSelect = "0";
        form.find(_search).find('option').remove().end();
        $.each(response.data, function (key, value) {
            form.find(_search)
                .append($("<option></option>").attr("value", value.value).text(value.text));
            if (value.selected != null) valueSelect = value.selected;
        });
        if (selected != null) {
            form.find(_search).val(selected).trigger("change");
        }
        else {
            if (valueSelect != '0') {
                form.find(_search).val(valueSelect).trigger("change");
            }
        }
    })
        .catch(function (error) {
            console.log("Failed Request! loadAccountTypeProductbyBAClass ->", error);
            notifyMessageGral("No se pueden cargar los tipos de productos.", "error", null, null);
        });
}

function PrepareRequestGetBankAccTypebyClas(currency, clasification) {
    getBAccTypebyClassRequest = $.ajax({
        method: "GET",
        url: "/Formcontrol/getBAccountProductsbyUserClass",
        data: { idCurrency: currency, idClassification: clasification },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return getBAccTypebyClassRequest;
}

function PrepareRequestSavePayment(payments) {
    saveInvoicePaymentRequest = $.ajax({
        method: "POST",
        url: PaymentMovs.Insert,
        datatype: 'json',
        data: { parameter: payments },
    })
        .done(function (response, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return saveInvoicePaymentRequest;
}

function printJSGriInvoiceMovements() {
    $("#jsGridInvoicePayment").jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        autoload: true,
        loadMessage: "Cargando, espere...",
        deleteConfirm: "¿ Desea borrar el registro ?",
        noDataContent: "Sin datos...",
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    url: PaymentMovs.load,
                    dataType: "json",
                    data: {
                        id: invoicesids
                    },
                }).done(function (response) {
                    d.resolve(response.data);
                    invoicePaymentsObject = response.data;
                    var resp = response.data;
                    subtotalpagado = 0.00;
                    if (resp) {
                        $.each(resp, function (key, value) {
                            subtotalpagado = subtotalpagado + parseFloat(value.chargedAmount);
                        });
                        $('#totalPay').text(covertToNatural(parseFloat(subtotalpagado)));
                    }

                });
                return d.promise();
            },
            deleteItem: function (item, val) {

            },
            updateItem: function (val, item) {
            }
        },

        rowDoubleClick: function (item) {
        },
        rowClass: function (item) {
            return item.status == 2 ? 'highlight-green' : item.status == 3 ? 'highlight-red' : ""
        },
        fields:
            [
                { title: "", name: "Payment", type: "number", width: 70, visible: false, align: "left" },
                { title: "Invoice", name: "document", type: "number", width: 70, visible: false, align: "left" },
                { title: "BankAccntType", name: "BankAccntType", type: "number", width: 70, visible: false, align: "left" },
                { title: "PaymentMethod", name: "PaymentMethod", type: "number", width: 70, visible: false, align: "left" },
                { title: "Método de pago", name: "BankAccntTypeName", type: "text", width: 120, align: "center" },
                { title: "Cuenta", name: "PaymentMethodName", type: "text", width: 120, align: "center" },
                { title: "Pago", name: "chargedAmount", type: "number", width: 80, align: "right" },
                { title: "Descripción", name: "authRef", type: "text", width: 260, align: "left" },
                {
                    title: " ", name: "Payment", type: "number", width: 20, align: "center",
                    itemTemplate: function (value, item) {
                        return $("<div>").append("<input type='button' class='jsgrid-button jsgrid-delete-button' data-toggle='tooltip' title='Borrar' data-original-title='Borrar' onclick='deleteinvoicemovement(" + item.Payment + ")'>");
                    }
                }
            ],
        onDataLoaded: function (grid, data) {
            calculateRemaininvoiceIn();
        }
    });
}

function calculateRemaininvoiceIn() {
    var total = parseFloat($('#toTalCost').text().replace(/[$,]+/g, ""))
    $(".remaingPay").text(covertToNatural(total - subtotalpagado));
    $('.remaingBudget').text(covertToNatural(subtotalpagado));
    subtotalpagado = 0;
}

function deleteinvoicemovement(item) {
    $.confirm({
        title: 'Eliminar movimiento de gasto',
        content: '¿Esta seguro que desea eliminar el movimiento de gasto actual?',
        theme: 'material',
        type: 'orange',
        buttons: {
            si: function () {
                OpenModalProcessing(".processingModal");
                var jsPromiseDeleteIncomeItem = Promise.resolve(PrepareRequestDeleteInvoiceMovement(item));
                jsPromiseDeleteIncomeItem.then(function (response) {
                    notifyMessageGral("Se ha eliminado correctamente la información del movimiento de gasto.", 'success', 800, '.modal-header');
                    EditJSGriIncomeMovements(incomeObject.item);
                })
                    .catch(function (error) {
                        notifyMessageGral("No se puede eliminar el movimineto de gasto.", 'error', 1500, '.modal-header');
                        console.log("Failed Request! ->", error);
                        EditJSGriIncomeMovements(incomeObject.item);
                        return;
                    })
                    .finally(function () {
                        ForceCloseModal(".processingModal");
                    });
            },
            no: function () { result = false; }
        }
    });
}

function PrepareRequestDeleteInvoiceMovement(item) {
    var Request = $.ajax({
        method: "POST",
        url: PaymentMovs.Delete,
        data: { id: item },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            printJSGriInvoiceMovements()
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });

    return Request;
}

function deleteDocitem(idDocitem) {
    $.confirm({
        title: 'Eliminar Gasto',
        content: '¿Esta seguro que desea eliminar el gasto actual?',
        theme: 'material',
        type: 'orange',
        buttons: {
            si: function () {
                OpenModalProcessing(".processingModal");

                jsPromiseSaveDocumentItem = Promise.resolve(PrepareRequestDeleteDocumentItem(idDocitem));
                jsPromiseSaveDocumentItem.then(function (response) {
                    notifyMessageGral("Se ha eliminado correctamente la información del gasto.", 'success', 800, '.modal-header');
                    reloadGrid();
                    reloadGridPayments();
                    ForceCloseModal(".processingModal");

                })
                    .catch(function (error) {
                        notifyMessageGral("No se puede eliminar el gasto.", 'error', 1500, '.modal-header');
                        console.log("Failed Request! ->", error);
                        reloadGrid();
                        reloadGridPayments();
                        ForceCloseModal(".processingModal");
                        return;

                    });
            },
            no: function () {
                result = false;
            }
        }
    });
    //calculateDocumentValue();
}

function PrepareRequestDeleteDocumentItem(documentItem) {
    deleteDocumentItemRequest = $.ajax({
        method: "POST",
        url: InvoiceItem.Delete,
        data: {
            parameter: documentItem
        },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });

    return deleteDocumentItemRequest;
}

$('#Tax1, #Tax2').on('change', function () {
    if ($('#Tax1').is(':checked')) {
        CheckedIsFiscal('1');
    } else {
        CheckedIsFiscal('0');
    }
});

$('#BankAccntType').change(function () {
    if ($(this).val() == 2 || $(this).val() == 3) {

    }
});


function saveDocument(e) {
    var urlAction;
    if (!$("#invoiceForm").valid()) {
        notifyMessageGral("Debe capturar la información general - unidad de negocio, fecha, moneda", "warn", 2000, '.info');
        return;
    } else {
        if (invoicesids == "" || invoicesids == 0 || invoicesids == null) {// Adding to global variable
            urlAction = Invoice.Insert;
        } else {
            urlAction = Invoice.UpdateInvoice;
            invoiceDataSend.id = invoicesids;
        }

        jsPromiseSaveDocument = Promise.resolve(PrepareActionInvoice(frmInvoice, invoiceDataSend, urlAction));
            OpenModalProcessing(".processingModal");
            jsPromiseSaveDocument.then(function (response) {
                invoicesids = response.data.id;
                InvoiceObject = response.data;
                var invoice = response.data;
                $('#invoiceIdentifier').text(invoice.identifier);
                notifyMessageGral("Se ha añadido correctamente solo la información general de gastos.", 'success', 500, '.modal-header');

                jsPromiseSaveDocumentItem = Promise.resolve(PrepareActionItems(frmInvoiceItem, invoicesids, InvoiceItem.Insert)); // Preparing DocItem Promise

                $('#invoiceIdentifier').text(response.data.InvoiceIdentifier);
            })
                .catch(function (error) {
                    console.log("Failed Request! ->", error);
                    notifyMessageGral("No se puede capturar las facturas.", 'error', 1500, '.modal-header');
                })
                .finally(function () {
                    ForceCloseModal(".processingModal");
                });
        
    }
}