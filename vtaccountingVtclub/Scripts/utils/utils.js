// prepare general actions
function frmodals(frm) {
    var component = $("#" + frm);
    component.removeClass("secret");
    component.dialog({
        autoOpen: false,
        modal: true,
        width:  720,
        height: 600,
    });
    component.dialog("open");
}

function prepareRequestSearch(datas, urls) {
    getObjectSearchRequest = $.ajax({
        method: "GET",
        url: urls,
        data: { parameter: datas },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) { })
        .fail(function (jqXHR, textStatus, errorThrown) { });
}

function PrepareRequestGetOnlySelect(datas,urls) {
    getDocumentCompaniesRequest = $.ajax({
        method: "GET",
        url: urls,
        data: { id: datas },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) { })
        .fail(function (jqXHR, textStatus, errorThrown) { });

    return getDocumentCompaniesRequest;
}

function PrepareActionInvoice(frmObject, varObjects, urlAction) {
    $.each(frmObject.serializeArray(), function (index, value) {
        varObjects[value.name] = value.value;
    });
    saveInvoiceRequest = $.ajax({
        method: "POST",
        url: urlAction,
        datatype: 'json',
        data: { parameter: varObjects },
    })
    .done(function (response, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });
    return saveInvoiceRequest;
}

function PrepareActionItems(frmObject, varObjects, urlAction) {
    var objectItem={ };
    $.each(frmObject.serializeArray(), function (index, value) {
        objectItem[value.name] = value.value;
    });
    objectItem.id = varObjects;
    saveInvoiceRequest = $.ajax({
        method: "POST",
        url: urlAction,
        datatype: 'json',
        data: { parameter: objectItem },
    })
    .done(function (response, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });
    return saveInvoiceRequest;
}

function PrepareDeleteItems(varObjects, urlAction) {
    saveInvoiceRequest = $.ajax({
        method: "POST",
        url: urlAction,
        datatype: 'json',
        data: { parameter: varObjects },
    })
        .done(function (response, textStatus, jqXHR) { })
        .fail(function (jqXHR, textStatus, errorThrown) { });
    return saveInvoiceRequest;
}

function PrepareRequestGetAnyData(urlAction) {
    getGetSupplierbyCompanyRequest = $.ajax({
        method: "GET",
        url: urlAction,
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });
    return getGetSupplierbyCompanyRequest;
}

function CallSelectInformation(varObejct, urlAction) {
    var Controller = $('#' + varObejct);
    $.ajax({
        method: "GET",
        url: urlAction,
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) {
        $.each(data.data, function (key, value) {
            Controller.append($("<option></option>").attr("value", value.value).text(value.text));
        });
    })
    .fail(function (jqXHR, textStatus, errorThrown) { });
}

function loadDepenceSelect(component,data,url) {
    $.ajax({
        method: "GET",
        url: url,
        data: { parameter: data },
        datatype: 'json',
    })
    .done(function (data, textStatus, jqXHR) {
        component.removeAttr("disabled");
        component.find('option').remove().end();
        $.each(data.data, function (key, value) {
            component.append($("<option></option>").attr("value", value.value).text(value.text));
        });
        if (data) {
            component.val(data);
        }
    })
    .fail(function (jqXHR, textStatus, errorThrown) { });
    return true;
}

function loadDataNotDependency(promise, selected, formName, tochild, triggerchange) {
    jsPromise = Promise.resolve(promise);
    jsPromise.then(function (response) {
        
        var _search = "select[name=" + tochild + "]"; // form removing options

        var ctrl = $('#' + formName).find(_search).length 
        _search = ctrl == 0 ? $("." + tochild) : _search;
        var _formName = "#" + formName + "";
        var form = $(_formName);
        var valueSelect = "0";
        form.find(_search).find('option').remove().end();

        $.each(response.data, function (key, value) {
            form.find(_search).append($("<option></option>").attr("value", value.value).text(value.text));
            if (value.selected != null) valueSelect = value.selected;
        });
        if (selected != null) {
            if (triggerchange != null)
            { form.find(_search).val(selected).trigger("change"); }
            else
            { form.find(_search).val(selected) }
        }
        else {
            if (triggerchange != null)
            { form.find(_search).val(valueSelect).trigger("change"); }
            else
            { form.find(_search).val(valueSelect); }
        }
    })
    .catch(function (error) {
        console.log("Failed Request! ->", error);
        notifyMessageGral("No se pueden cargar los datos." + error.messagge, "error", null, null);
    });
}

function loadVTAInvoiceGridHead(component, urls,para) {
    var Component = $('#' + component);
    Component.jsGrid({
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
                    url: urls,
                    dataType: "json",
                    data: $('#searchForm').serializeArray()
                    //data: { id: 0, InvoiceNumber: para.number, company: para.company, ammountIni: para.ammountini, ammountEnd: para.ammountend, applicationDateIni : para.appdateini, applicationDateFin : para.appdatefin, creationDateIni:para.appdateini, creationDateFin : para.appdatefin },
                })
                .done(function (response) {
                    d.resolve(response.data);
                });
                return d.promise();
            },
            deleteItem: function (item) {
                if (_Invoice != null || _Invoice != "") {
                    OpenModalProcessing(".processingModal");
                    jsPromiseSaveDocumentPayment = Promise.resolve(PrepareRequestDeletePayment(item));
                    jsPromiseSaveDocumentPayment.then(function (response) {
                        notifyMessageGral("Se ha eliminado correctamente el pago.", 'success', 800, '.modal-header');
                        reloadGridPayments();
                        CloseModalProcessing(".processingModal");
                    })
                    .catch(function (error) {
                        notifyMessageGral("No se puede eliminar el pago.", 'error', 1500, '.modal-header');
                        console.log("Failed Request! ->", error);
                        reloadGridPayments();
                        CloseModalProcessing(".processingModal");
                    });
                } else {
                }
                calculateDocumentValuePayment();
            }
        },
        fields: [{
            title: "",
            name: "Payment",
            type: "number",
            width: 70,
            visible: false,
            align: "left"
        },
        {
            title: "Invoice",
            name: "document",
            type: "number",
            width: 70,
            visible: false,
            align: "left"
        },
        {
            title: "BankAccntType",
            name: "BankAccntType",
            type: "number",
            width: 70,
            visible: false,
            align: "left"
        },
        {
            title: "PaymentMethod",
            name: "PaymentMethod",
            type: "number",
            width: 70,
            visible: false,
            align: "left"
        },
        {
            title: "Método de pago",
            name: "BankAccntTypeName",
            type: "text",
            width: 120,
            align: "center"
        },
        {
            title: "Cuenta",
            name: "PaymentMethodName",
            type: "text",
            width: 120,
            align: "center"
        },
        {
            title: "Pago",
            name: "chargedAmount",
            type: "number",
            width: 80,
            align: "right"
        },
        {
            title: "Descripción",
            name: "authRef",
            type: "text",
            width: 260,
            align: "left"
        },
        ],
        onDataLoaded: function (grid, data) {
            calculateDocumentValuePayment();
        },
    });
}

function loadVTAInvoiceGridItem(component, urls, ids) {
    var jsPromiseInvoiceItem;
    var Component = $('#' + component);
    Component.jsGrid({
        height: "auto",
        width:  "100%",
        sorting:  true,
        autoload: true,
        paging:   true,
        autoload: true,
        loadMessage: "Cargando, espere...",
        noDataContent: "Sin datos...",
        deleteConfirm: "¿ Desea borrar el registro ?",
        confirmDeleting: false,
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    url: urls,
                    dataType: "json",
                    data: { parameter: ids },
                })
                .done(function (response) {
                     d.resolve(response.data);
                });
                return d.promise();
            },
            deleteItem: function (item, val) {
                DeleteItem(item.item)
            },
            updateItem: function (val, item) {
                UpdateItem(item)
            }
        },

        rowDoubleClick: function (arg) {
            openModalItems();
            editModelItem("#invoiceitemForm", arg);
        },
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
            name:  "accountl3name",
            type:  "text",
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
        onDataLoaded: function (grid, data) {
            //calculateDocumentValue();
        }
    });


}

/*********AJAX REQUEST TO GET ATTACHEMENTS FROM BACK********/
function getFilesUpload(id, urlAction) {
   var fileUpload = $.ajax({
        method: "POST"
        , url: urlAction
        , data: { id: id }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            
        })
        .fail(function (jqXHR, textStatus, errorThrown) {

        });

    return fileUpload;
}

function PrepareRequestDeleteAttachment(idAttachment,urlAction) {


  var  deleteAttachmentRequest = $.ajax({
        method: "POST"
        , url: urlAction  
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

function PrepareRequestGetComments(id,urlAction) {
 var getComments =   $.ajax({
        method: "GET"
        , url: urlAction
        , data: { id: id }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getComments;
} 

function PrepareRequestGetBankAccountbyCompanyUserBanAccount(company,urlAction) {
    getBankAccountbyCompany = $.ajax({
        method: "GET"
        , url: urlAction
        , data: { Company: company }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getBankAccountbyCompany;
} 

function PrepareRequestGetCompaniesFinanceByType(paramType, urlAction) {
    getCompaniesRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , data: { Type: paramType }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getCompaniesRequest;
}

function PrepareRequestGetExternalGroupCompanies(value, urlAction) {

    var getExternalGroupCompaniesRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , datatype: 'json'
        , data: { externalgroup: value }
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getExternalGroupCompaniesRequest;
}

function PrepareRequestGetReconciliationStatus(urlAction) {
    var getReconciliationStatus = $.ajax({
        method: "GET"
        , url: urlAction
        //,data: { name: "John", location: "Boston" }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return getReconciliationStatus;
}

function PrepareRequestGetExternalGroup(urlAction) {

    var getExternalGroupRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , datatype: 'json'
        //, data: { idBankAccount: value }
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getExternalGroupRequest;
}


function PrepareRequestGetHotelsbySegmentsUserCompany(value,urlAction) {

    getDocumentCompaniesRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , data: { id: value }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getDocumentCompaniesRequest;
}

function PrepareRequestGetCurrencies(urlAction) {

    getCurrenciesRequest = $.ajax({
        method: "GET"
        , url: urlAction
        //, data: { id: value }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getCurrenciesRequest;
}


///  Populate form to Inputs
function populateForm(frm, data) {
    $.each(data, function (key, value) {
        $('[name=' + key + ']', frm).val(value);
    });
}

function PrepareRequestGetDataGenericMethod(urlAction) {

    getRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getRequest;
}

function PrepareRequestGetAccl3bySegment(urlAction,idSegment, accl1) {

    getAccl3bySegmentRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , data: { idSegment: idSegment, accl1: accl1 }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getAccl3bySegmentRequest;
}

function PrepareRequestGetSegmentsbyUserBankAccount(urlAction) {
    getSegmentsbyCompanyUser = $.ajax({
        method: "GET"
        , url: urlAction
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getSegmentsbyCompanyUser;
}

function PrepareRequestGetCompaniesbyUserBAccount(urlAction,idSegment) {

    getDocumentCompaniesRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , data: { Segment: idSegment }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getDocumentCompaniesRequest;
}

function PrepareRequestGetTpvbyBAccount(urlAction,value) {

    getDocumentCompaniesRequest = $.ajax({
        method: "GET"
        , url: urlAction
        , datatype: 'json'
        , data: { idBankAccount: value }
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getDocumentCompaniesRequest;
}

function PrepareRequestGetSourceData(urlAction) {
    getSourceData = $.ajax({
        method: "GET"
        , url: urlAction
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getSourceData;
}


function PrepareRequestGetOperationType(urlAction) {
    getOperationType = $.ajax({
        method: "GET"
        , url: urlAction
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getOperationType;
}
