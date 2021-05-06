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

function loadTpvUserBAccount(selected, value, formName, tochild, triggerchange) {
    jsPromiseGetTpvByBankAccount = Promise.resolve(PrepareRequestGetTpvbyBAccount(value));
    jsPromiseGetTpvByBankAccount.then(function (response) {
        var _search = "select[name=" + tochild + "]"; // form removing options
        var _formName = "#" + formName + "";
        var form = $(_formName);

        var valueSelect = "0";

        form.find(_search).find('option').remove().end();

        $.each(response.data, function (key, value) {
            form.find(_search)
                .append($("<option></option>")
                    .attr("value", value.value)
                    .text(value.text));
            if (value.selected != null) valueSelect = value.selected;
        });

        if (selected != null) {
            if (triggerchange != null) {
                form.find(_search).val(selected).trigger("change");
            }
            else {
                form.find(_search).val(selected);
            }
        }
        else {
            if (triggerchange != null) {
                form.find(_search).val(valueSelect).trigger("change");
            }
            else {
                form.find(_search).val(valueSelect);
            }
        }
    })
    .catch(function (error) {
        console.log("Failed loadTpvUserBAccount! ->", error);
        notifyMessageGral("No se pueden cargar las TVP .", "error", null, null);
    });
}

function PrepareRequestGetTpvbyBAccount(value) {
    getDocumentCompaniesRequest = $.ajax({
        method: "GET",
        url: "/Formcontrol/getTpvUserBankAccount",
        datatype: 'json',
        data: { idBankAccount: value }
    })
    .done(function (data, textStatus, jqXHR) {
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
    });
    return getDocumentCompaniesRequest;
}



function printJSGriIncomeMovements(idIncome) {
    $("#jsGridIncomeMovements").jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        autoload: true,
        paging: true,
        autoload: true,
        loadMessage: "Cargando, espere...",
        noDataContent: "Sin datos...",
        deleteConfirm: "¿ Desea borrar el registro ?",
        confirmDeleting: false,
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    url: incomeApp.getincomemovitems,
                    dataType: "json",
                    data: {
                        id: idIncome
                    },
                }).done(function (response) {
                    d.resolve(response.data);
                    var resp = response.data;
                    if (resp) {
                        $.each(resp, function (key, value) {
                            subtotalpagado = subtotalpagado + parseFloat(value.ammounttotalstring.replace(/[$,]+/g, "")) ;
                        });
                    }

                });
                return d.promise();
            },
            deleteItem: function (item, val) {

            },
            updateItem: function (val, item) {
            }
        },

        rowDoubleClick: function (arg) {
        },
        rowClass: function (item) {
            return item.status == 2 ? 'highlight-green' : item.status == 3 ? 'highlight-red' : ""
        },
        fields: [
            { title: "item",           name: "item",              type: "number", width: 70,   visible: false, align: "center", editing: false },
            { title: "parent",         name: "parent",            type: "number", width: 70,   visible: false, align: "center", editing: false },
            { title: "Cuenta",         name: "bankaccountname",   type: "text",   width: 170,  align: "center" },
            { title: "Método de pago", name: "bankaccnttypename", type: "text",   width: 140,  align: "center" },
            { title: "Tpv",            name: "tpvname",           type: "text",   width: 150,  align: "center" },
            { title: "Tarjeta",        name: "card",              type: "text",   width: 150,  align: "center",
                itemTemplate: function (value, item) {
                    if (value == null) { return ""; } else {
                        return value;
                    }
                }
            },
            { title: "Total", name: "ammounttotalstring", type: "number", width: 150 },
            { title: "Descripción", name: "description", type: "text", width: 160, align: "left" },
        ],
        onDataLoaded: function (grid, data) {
            calculateRemainincomeIn();
        }
    });
}

function EditJSGriIncomeMovements(idIncome) {
    populateMovementToEdit();
    $("#jsGridIncomeMovements").jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        autoload: true,
        paging: true,
        autoload: true,
        loadMessage: "Cargando, espere...",
        noDataContent: "Sin datos...",
        deleteConfirm: "¿ Desea borrar el registro ?",
        confirmDeleting: false,
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    url: incomeApp.getincomemovitems,
                    dataType: "json",
                    data: {
                        id: idIncome
                    },
                }).done(function (response) {
                    d.resolve(response.data);
                    var resp = response.data;
                    if (resp) {
                        $.each(resp, function (key, value) {
                            subtotalpagado = subtotalpagado + parseFloat(value.ammounttotalstring.replace(/[$,]+/g, ""));
                        });
                    }

                });
                return d.promise();
            },
            deleteItem: function (item, val) {

            },
            updateItem: function (val, item) {
            }
        },

        rowDoubleClick: function (arg) {
            //openModalIncomeMovement();
            //editModelItem("#incomeMovementsForm", arg);
        },
        rowClass: function (item) {
            return item.status == 2 ? 'highlight-green' : item.status == 3 ? 'highlight-red' : ""
        },
        fields: [
            { title: "item", name: "item", type: "number", width: 70, visible: false, align: "center", editing: false },
            { title: "parent", name: "parent", type: "number", width: 70, visible: false, align: "center", editing: false },
            { title: "Cuenta", name: "bankaccountname", type: "text", width: 170, align: "center" },
            { title: "Método de pago", name: "bankaccnttypename", type: "text", width: 140, align: "center" },
            { title: "Tpv", name: "tpvname", type: "text", width: 150, align: "center" },
            {
                title: "Tarjeta", name: "card", type: "text", width: 150, align: "center",
                itemTemplate: function (value, item) {
                    if (value == null) { return ""; } else {
                        return value;
                    }
                }
            },
            { title: "Total", name: "ammounttotalstring", type: "number", width: 150 },
            { title: "Descripción", name: "description", type: "text", width: 160, align: "left" },
            {
                title: " ", name: "item", type: "number", width: 20,
                itemTemplate: function (value, item) {
                    return $("<div>").addClass("rating").append("<input type='button' class='jsgrid-button jsgrid-delete-button' title='Borrar' data-original-title='Borrar' onclick='deleteincomemovement(" + item.item + ")'>");
                }
            }
        ],
        onDataLoaded: function (grid, data) {
            calculateRemainincomeIn();
        }
    });
}

function calculateRemainincomeIn() {
    $(".totalRemaingIncomeMovements").text(
        covertToNatural(
            parseFloat($('.lbltoTalCost').text().replace(/[$,]+/g, "")) - subtotalpagado
        )
    );
    $('.toTalCostMovements').text(covertToNatural(subtotalpagado));
    subtotalpagado = 0;
}