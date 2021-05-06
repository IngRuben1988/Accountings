/************************** Ready *******************/
$(document).ready(function () {

    loadDataNotDependency(PrepareRequestGetReconciliationStatus(formControl.ReconciliationStatus), null, "frmSearchBAReconciliations", "status", true);
    loadDataNotDependency(PrepareRequestGetExternalGroup(formControl.ExternalGroup), null, "frmSearchBAReconciliations", "externalgroup", true);
    //loadDataNotDependency(PrepareRequestGetHotelsbySegmentsUserCompany(0,formControl.HotelsBySegment),null, 'frmSearchBAReconciliations', 'hotel', null);

    loadDateTimeLoad("appdate1")
    loadDateTimeLoad("appdate2")

    if (searchSCTKPDetailsbyFormActive == false) {
        loadDataNotDependency(PrepareRequestGetSourceData(Components.getSourceData), null, "frmSearchBAReconciliations", "typeSourceData", null);

    } else {

        loadDataNotDependency(PrepareRequestGetOperationType(Components.getOperationType), null, "frmSearchBAReconciliations", "typeSourceData", null);
    }
});

/************************* URL*****************************/
bankStatementsUrl = {};
bankStatementsUrl.GetStat2CBKPos = "/Bankreconciliation/GetStatement2CBANKACCNTPos";
bankStatementsUrl.GetS2CBKPosbyItemsId = "/Bankreconciliation/GetStatement2CBANKACCNTPosByItemsId";
bankStatementsUrl.SaveS2CBKPosItems = "/Bankreconciliation/SaveStatement2CBANKACCNTPosItem";
bankStatementsUrl.DeleteS2CBKPos = "/Bankreconciliation/DeleteStatement2CBANKACCNTPosById";
bankStatementsUrl.DeleteArrayS2CBKPos = "/Bankreconciliation/DeleteStatement2CBANKACCNTPosByIds";
bankStatementsUrl.DeleteS2CBKPosItems = "/Bankreconciliation/DeleteStatement2CBANKACCNTPosItem";
bankStatementsUrl.GetFinancialState2Item = "/Bankreconciliation/GetSearchFinancialState2ItemList";
//bankStatementsUrl.fileSend = "/Bankreconciliation/bankStatementsUpFilevsctps";



/************************** Global Data and Reference *******************/

var frm = $("#frmSearchBAReconciliations");
var dataSearchingStatement;
var dataCurrentStatement;
var divtoErase = "";
var jsGridCurrent = "";
var searchSCTKPDetailsbyFormActive = false;
var CCTE_tblsourcedata_fondo = 1;
/************************** General Actions and Functions **********************************************/
$(".externalgroup").on("change", function (event) {
    // loadCompaniesBySegmentUserBAccount(null, this.value, 'frmSearchBAReconciliations', 'Company'); /// crear el trigger
    // console.log("externalgroup"); formControl.BAccountByGroup
    loadDataNotDependency(PrepareRequestGetExternalGroupCompanies(this.value, formControl.BAccountByGroup), null, 'frmSearchBAReconciliations', 'Company', true);

});

$(".Company").on("change", function (event) {
    loadDataNotDependency(PrepareRequestGetBankAccountbyCompanyUserBanAccount(this.value, formControl.BankAccountByCompany), null, "frmSearchBAReconciliations", "PaymentMethod", true);

    //loadDataNotDependency(PrepareRequestGetHotelsbySegmentsUserCompany(this.value, formControl.HotelsBySegment), null, 'frmSearchBAReconciliations', 'hotel', null);

});

$(".PaymentMethod").on("change", function (event) {//loadDataNotDependency(PrepareRequestGetTpvbyBAccount(Components.getTpvUserBAccount, this.value), null, "frmSearchBAReconciliations", "Tpv", true)
    //loadTpvUserBAccount(null, this.value, "frmSearchBAReconciliations", "Tpv", true);
    // loadHotelUserBAccount(null, this.value, "frmSearchBAReconciliations", "hotel", true);
});

$(".datetimeinput").keypress(function () {
    return false;
});

$(".btnGenerateSearchBankStatements").on("click", function () {

    if (searchSCTKPDetailsbyFormActive == false) {
        searchSCTKP(frm); // Searching ScotiaPos
    }
    else {
        searchSCTKPDetailsbyForm(); // Searching BankAccount Details
    }
});

$(".btnPrevExcel").on("click", function (event) {
    ShowUpExcel();
});

$(".btnCreateExcel").click(function () {
    window.open("/Bankreconciliation/GetStatement2CBANKACCNTPosExcel?" + frm.serialize());
});


function searchSCTKP(form) {
    OpenModalProcessing(".processingModal");

    var _form = $(form).serialize();
    var jsPromiseGetSCTP;

    jsPromiseGetSCTP = Promise.resolve(PrepareRequestGetSCTP(_form));
    jsPromiseGetSCTP.then(function (response) {
        // alert(response.data);
        // return response.data;
        generateTr(response.data);
        //$('#datatableconciliations').DataTable();
    }).catch(function (error) {
        console.log("Failed Request! jsPromiseGetSCTP ->", error);
        console.error("No se puede procesar la solicitud jsPromiseGetSCTP. --> ");
        notifyMessageGral("No se puede realizar la acción ", 'error', 1500, '.info');
        ForceCloseModal(".processingModal");
    });
}

function PrepareRequestGetSCTP(_form) {
    getFondoRequest = $.ajax({
        method: "GET"
        , url: bankStatementsUrl.GetStat2CBKPos
        , data: _form
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getFondoRequest;
}

function generateTr(data) {
    $("._sctp").remove();
    if (data.length > 0) {
        dataSearchingStatement = data;
        $.each(data, function (key, value) {
            // var classState = value.state == 1 ? "highlight-green" : "highlight-yellow";
            var iMethodBuild = value.methodconciliation == 1 ? "<i class='fas fa-exclamation-triangle'></i>" : value.methodconciliation == 2 ? "<i class='fas fa-hands-helping'></i>" : value.methodconciliation == 3 ? "<i class='fas fa-cogs'></i>" : "";
            var classStateLink = value.statusconciliation == 1 ? "bank_statements_success_i" : value.statusconciliation == 2 ? "bank_statements_danger_i" : value.statusconciliation == 3 ? "bank_statements_error_i" : "bank_statements_nostatus_i";
            var row = "<tr class='_sctp'><td style='width: 1em !important;'><input type='checkbox' class='chkstatChild _unchkstatChild' value='" + value.idBankStatements2 + "' id='chkstatChild_" + value.idBankStatements2 + "' onclick='chkStatementsClicked(this)'/></td>" +
                "<td style='width: 3em !important;text-align: center;'>" + value.idBankStatements2 + "</td>" +
                //"<td style='width: 4em !important;text-align: center;'>" + value.companyname + "</td>" +
                "<td style='width: 4em !important;text-align: center;'>" + value.bankstatementsAplicationDateString + "</td>" +
                "<td style='width: 4em !important;text-align: center;'>" + value.currencyName + "</td>" +
                "<td style='width: 4em !important;text-align: center;'>" + value.baccountName + "</td>" +
                "<td style='width: 10em !important;text-align: right;'>" + value.bankstatements2ComisionString + "</td> " +
                "<td style='width: 10em !important;text-align: right;'>" + value.bankstatementsAbonoString + "</td> " +
                "<td style='width: 10em !important;text-align: right;'>" + value.bankstatementsCargoString + "</td>" +
                "<td style='width: 10em !important;text-align: right;'>" + value.bankstatementsdescriptionstring + "</td>" +
                "<td style='width: 10em !important;text-align: right;'>" + value.bankstatementsamountfinalstring + "</td>" +
                "<td style='width: 1em !important;text-align: center;'> <a class='' data-toggle='tooltip' data-original-title='Conciliar' onclick='showsearchSCTKPDetail(" + key + ")'> <i class='fas fa-circle " + classStateLink + "' style='font-size: 13px;'></i></a></td>" +
                "<td style='font-size: 13px; width: 1em !important; text-align: center;'>" + iMethodBuild + "</td>" +
                "<td style='width: 1em !important;text-align: center;'><input type='button' class='tablegrid-button tablegrid-delete-button' style='font-size: 13px;' data-toggle='tooltip' title='Eliminar' onclick='financialStateDelete(" + value.idBankStatements2 + ")' /></td></tr>";
            $("#tblSearchSCTP> tbody:last-child").append(row);
            //$("#datatableconciliations > tbody:last-child").append(row);

        });

    } else {
        notifyMessageGral("No se encontraron datos", 'info', 1500, null);
    }
    ForceCloseModal(".processingModal");
}

function showsearchSCTKPDetail(key) {
    ShowDetail(); // Show HTML
    dataCurrentStatement = dataSearchingStatement[key]; // Getting data from Search
    searchSCTKPDetailGrid(dataCurrentStatement);
    loadDataNotDependency(PrepareRequestGetSourceData(Components.getSourceData), null, "frmSearchBAReconciliations", "typeSourceData", null);
}

function validartoNull(str_val) {
    var result = "";

    if (str_val != null) {
        result = str_val;
    }

    return result;
}


/************************** Details Actions and Functions **********************************************/

function searchSCTKPDetailGrid(data) {

    first = data.tpvname;
    second = "_" + data.tpvname;
    divtoErase = "#" + first;
    // $(".toAddBankStatements").append("<div id='" + first + "' style='padding-top: 2em;'> <div id='" + JsgridDetailSCTP + "'></div></div>");
    $(".lblSearchDetailSCTPInfo").text("Conciliación | Hotel/Empresa: " + dataCurrentStatement.companyname + "   | Cuenta: " + dataCurrentStatement.baccountName + " | Monto: $ " + dataCurrentStatement.bankstatementsamountfinalstring + " | Fecha : " + dataCurrentStatement.bankstatementsAplicationDateString);
    $(".toAddBankStatements").append("<div id='" + first + "' > <h5> D e s t i n o </h5><div id='" + second + "'></div></div>");

    var _name = "#" + second;
    jsGridCurrent = _name;
    // fondosgrid(_name);
    // $("#JsgridDetailSCTP").jsGrid({
    $(_name).jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        autoload: true,
        filtering: false,
        // editing: true,
        noDataContent: "Sin datos...",
        loadMessage: "Cargando datos, espere ...",
        deleteConfirm: "¿Desea borrar el registro ?",
        loadIndicationDelay: 500,
        rowDoubleClick: function (args) {
        },
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    url: bankStatementsUrl.GetS2CBKPosbyItemsId,
                    dataType: "json",
                    data: { id: dataCurrentStatement.idBankStatements2 }
                }).done(function (response) {
                    d.resolve(response.data);
                });
                return d.promise();
            }
        },
        rowClass: function (item, itemIndex) {
            return item.bankStatementLinked == true ? 'highlight-green' : item.statusExpenses == false ? 'highlight-red' : "";
        },
        fields: [{
            title: "#",
            name: "rowNumber",
            type: "text",
            width: 30,
            validate: "required",
            visible: true
        },
        {
            title: "Fecha",
            name: "aplicationDateString",
            type: "text",
            width: 70,
            visible: true
        },
        {
            title: "Origen",
            name: "SourceDataName",
            type: "text",
            align: "center",
            width: 120,
            visible: true,
            itemTemplate: function (value, item) {
                var r = value + " - " + item.Reference;
                return r;
            }
        },
        {
            title: "Descripción",
            name: "description",
            type: "text",
            align: "center",
            width: 120,
            visible: true
        },
        {
            title: "Tipo de Pago",
            name: "PaymentMethodName",
            type: "text",
            width: 100,
            visible: false
        },
        {
            title: "Monto",
            name: "appliedAmmountString",
            type: "text",
            width: 100,
            align: 'right',
            itemTemplate: function (value) {
                return " $ " + value;
            }
        },
        {
            title: "Monto",
            name: "appliedAmmount",
            type: "number",
            width: 100,
            visible: false,
        }, {
            title: "Saldo",
            name: "SaldoString",
            type: "text",
            width: 100,
            align: "right",
            visible: false,
            //cellRenderer: function (value, item) { return $(this).addClass("jsgrid-td-background"); },
            itemTemplate: function (value, item) {
                //return "<span class='highlight-green'>$ " + value + "</span>";
                var r = ""; //return " $ " + value;
                r = value == 0 ? r = "<span class=''> $ " + value + "</span>" : value < 0 ? r = "<span class='highlight-red'> $ " + value + "</span>" : r = "<span class='highlight-green'> $ " + value + "</span>";
                return r;
            }
        }, {
            name: "Reference",
            type: "number",
            visible: false
        },
        {
            name: "ReferenceItem",
            type: "number",
            visible: false
        }, {
            title: "",
            name: "rowIndex",
            type: "text",
            width: 24,
            itemTemplate: function (value, item) {
                if (item.bankStatementLinked == false) {
                    fondo = item;
                    return $("<div>").addClass("rating").append("<i class='fa fa-save fa-lg' data-toggle='tooltip' title='Guardar' data-original-title='Guardar' onclick='financialStateitemSave(" + 1 + ",0," + item.rowIndex + ")'></i>");
                }
            }
        },
        {
            title: "",
            name: "id",
            type: "text",
            width: 24,
            itemTemplate: function (value, item) {
                if (item.bankStatementLinked == true) {
                    return $("<div>").addClass("rating").append("<i class='fa fa-trash fa-lg' data-toggle='tooltip' title='Eliminar' data-original-title='Eliminar' onclick='financialStateitemDelete(" + item.rowIndex + ")'></i>");
                }
            }
        }
            /*,
            {
              type: "control"
            }*/
        ],
        onDataLoaded: function (grid, data) {
            //$('[data-toggle="tooltip"]').tooltip();
            $('#SCTPSValue').text("Conciliado: $ " + covertToNatural(getTotalValueSCTPSItems()));
            $('#SCTPSValueRemaing').text("X Conciliar: $ " + covertToNatural(getTotalValueSCTPSRemaing()));
            $('#SCTPSValueSuggestion').text("Propuesta: $ " + covertToNatural(getTotalValueSCTPSSuggestion()));

        }
    });
}


function showsearchSCTKPGeneral(e) {
    ShowGeneral();
    loadDataNotDependency(PrepareRequestGetOperationType(Components.getOperationType), null, "frmSearchBAReconciliations", "typeSourceData", null);
}

// Save
function financialStateitemSave(jsGrid, rowIndexGrid, rowIndex) {
    OpenModalProcessing(".processingModal");

    var row;
    if (jsGrid == 1) { row = $(jsGridCurrent).jsGrid("option", "data")[rowIndex]; }
    else { row = $("#JsGridSCTKPDetailsbyForm").jsGrid("option", "data")[rowIndexGrid]; }

    var data = dataCurrentStatement;

    var gridValueRow = (getTotalValueSCTPSItems() + row.appliedAmmount).toFixed(2);
    var sign1 = Math.sign(gridValueRow)
    var sign2 = Math.sign(data.bankstatementsamountfinal)
    if (row.SourceData == CCTE_tblsourcedata_fondo) {
        if (sign1 == sign2) {
            if (Math.abs(gridValueRow) == Math.abs(data.bankstatementsamountfinal)) {
                var jsPromiseSaveSCTPItem;
                jsPromiseSaveSCTPItem = Promise.resolve(PrepareRequestFinancialStateItemSave(data.idBankStatements2, row));
                jsPromiseSaveSCTPItem.then(function (response) {
                    reloadGrid("#JsGridSCTKPDetailsbyForm");
                    reloadGrid(jsGridCurrent);
                    CloseModalProcessing(".processingModal");

                }).catch(function (error) {
                    // console.log("Failed Request! jsPromiseSaveSCTPItem ->", error);
                    console.error("No se puede procesar la solicitud jsPromiseSaveSCTPItem. --> ");
                    CloseModalProcessing(".processingModal");
                });
            } else {
                CloseModalProcessing(".processingModal");
                notifyMessageGral('La conciliación de fondo solo deben ser en una exhibición, igual al monto solicitado.', "info", 500, null);
            }
        } else {
            CloseModalProcessing(".processingModal");
            notifyMessageGral('El registro excede el máximo de la conciliación.', "info", 500, null);
        }
    }
    else if ((Math.abs(gridValueRow) <= Math.abs(data.bankstatementsamountfinal)) && (sign1 == sign2)) {

        var jsPromiseSaveSCTPItem;
        jsPromiseSaveSCTPItem = Promise.resolve(PrepareRequestFinancialStateItemSave(data.idBankStatements2, row));
        jsPromiseSaveSCTPItem.then(function (response) {
            reloadGrid("#JsGridSCTKPDetailsbyForm");
            reloadGrid(jsGridCurrent);
            ForceCloseModal(".processingModal");
        }).catch(function (error) {
            // console.log("Failed Request! jsPromiseSaveSCTPItem ->", error);
            console.error("No se puede procesar la solicitud jsPromiseSaveSCTPItem. --> ");
            ForceCloseModal(".processingModal");
        });
    }
    else {
        ForceCloseModal(".processingModal");
        notifyMessageGral('El registro excede el máximo de la conciliación', "info", 500, null);
    }

}

function PrepareRequestFinancialStateItemSave(id, row) {
    var saveFInancialStateItemRequest = $.ajax({
        method: "POST"
        , url: bankStatementsUrl.SaveS2CBKPosItems
        , data: { idBankStatement: id, financialstateitem: row }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return saveFInancialStateItemRequest;
}

// Delete
function financialStateDelete(id) {

    $.confirm({
        title: 'Eliminar ScotiaPos',
        content: '<div style="font-size: 0.90em !important;"> ¿Desea eliminar la conciliacion seleccionada.?</div>',
        containerFluid: true,
        boxWidth: '30%',
        useBootstrap: false,
        theme: 'material',
        type: 'orange',
        // lazyOpen: true,
        buttons: {
            confirm: {
                text: 'Eliminar',
                action: function () {
                    axios.post(bankStatementsUrl.DeleteS2CBKPos, { id: id })
                        .then(function (response) {
                            // CloseModalProcessing(".processingModal");
                            notifyMessageGral("Se ha borrado el registro.", 'success', 1500, '.info');
                            searchSCTKP(frm); // Searching ScotiaPos
                        })
                        .catch(function (error) {
                            // CloseModalProcessing(".processingModal");
                            notifyMessageGral("No se ha completado la acción de borrado.", 'error', 1500, '.info');
                        });
                    //}
                }
            },
            cancel: {
                text: 'Cancelar',
                action: function () {
                }
            }
        }
    });

}

function financialStateDeleteArray() {

    $.confirm({
        title: 'Eliminar ScotiaPos',
        content: '<div style="font-size: 0.90em !important;"> ¿Desea eliminar las conciliaciones seleccionadas?</div>',
        containerFluid: true,
        boxWidth: '50%',
        useBootstrap: false,
        theme: 'material',
        type: 'orange',
        // lazyOpen: true,
        buttons: {
            confirm: {
                text: 'Eliminar',
                action: function () {
                    var arrayStatements = [].map.call($("._chkstatChild"), function (obj) { return obj.value; });
                    if (arrayStatements.length != 0) {
                        // OpenModalProcessing(".processingModal");
                        axios.post(bankStatementsUrl.DeleteArrayS2CBKPos, arrayStatements)
                            .then(function (response) {
                                // CloseModalProcessing(".processingModal");
                                notifyMessageGral("Se han borrado los registros.", 'success', 1500, '.info');
                                $(".chkstatParent").prop("checked", false);
                                $(".btn_delete_sctp").addClass("hide");
                                $(".btn_delete_sctp").removeClass("show");
                            }).then(function () {
                                searchSCTKP(frm); // Searching ScotiaPos
                            })
                            .catch(function (error) {
                                // CloseModalProcessing(".processingModal");
                                notifyMessageGral("No se ha completado la acción de borrado.", 'error', 1500, '.info');

                            }).finally(function () {
                                searchSCTKP(frm); // Searching ScotiaPos
                            });
                    }
                }
            },
            cancel: {
                text: 'Cancelar',
                action: function () {
                }
            }
        }
    });
}



function PrepareRequestFinancialStateDelete(id) {
    var deleteFInancialStateRequest = $.ajax({
        method: "POST"
        , url: bankStatementsUrl.DeleteS2CBKPos
        , data: { id: id }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return deleteFInancialStateRequest;
}

// Delete Item
function financialStateitemDelete(rowIndex) {
    OpenModalProcessing(".processingModal");
    var row = $(jsGridCurrent).jsGrid("option", "data")[rowIndex];
    var data = dataCurrentStatement;

    var jsPromiseDeleteSCTPItem;
    jsPromiseDeleteSCTPItem = Promise.resolve(PrepareRequestFinancialStateItemDelete(data.idBankStatements2, row));
    jsPromiseDeleteSCTPItem.then(function (response) {
        reloadGrid("#JsGridSCTKPDetailsbyForm");

        reloadGrid(jsGridCurrent);
        ForceCloseModal(".processingModal");
    }).catch(function (error) {
        console.log("Failed Request! financialStateitemDelete ->", error);
        console.error("No se puede procesar la solicitud financialStateitemDelete. --> ");
        ForceCloseModal(".processingModal");
    });

}

function PrepareRequestFinancialStateItemDelete(id, row) {
    var saveFInancialStateItemRequest = $.ajax({
        method: "POST"
        , url: bankStatementsUrl.DeleteS2CBKPosItems
        , data: { idBankStatement: id, financialstateitem: row }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return saveFInancialStateItemRequest;
}


// searchDetailsbyForm

function searchSCTKPDetailsbyForm() {
    OpenModalProcessing(".processingModal");
    $("#JsGridSCTKPDetailsbyForm").jsGrid({
        height: "auto",
        width: "100%",
        sorting: true,
        autoload: true,
        filtering: false,
        // editing: true,
        noDataContent: "Sin datos...",
        loadMessage: "Cargando datos, espere ...",
        confirmDeleting: false,
        deleteConfirm: "¿Desea borrar el registro ?",
        loadIndicationDelay: 500,
        rowDoubleClick: function (args) {
        },
        controller: {
            loadData: function () {
                var d = $.Deferred();
                $.ajax({
                    // method: "POST",
                    url: bankStatementsUrl.GetFinancialState2Item,
                    dataType: "json",
                    data: frm.serialize()
                }).done(function (response) {
                    d.resolve(response.data);
                });
                return d.promise();
            }
        },
        rowClass: function (item, itemIndex) {
            return item.bankStatementLinked == true ? 'highlight-green' : item.statusExpenses == false ? 'highlight-red' : "";
        },
        fields: [
            {
                title: "#",
                name: "rowNumber",
                type: "text",
                width: 30,
                validate: "required",
                visible: true
            },
            {
                title: "Fecha",
                name: "aplicationDateString",
                type: "text",
                width: 70
            },
            {
                title: "Origen",
                name: "SourceDataName",
                type: "text",
                align: "center",
                width: 120,
                itemTemplate: function (value, item) {
                    var r = value + " - " + item.Reference;
                    return r;
                }
            },
            {
                title: "Descripción",
                name: "description",
                type: "text",
                align: "center",
                width: 120
            },
            {
                title: "Tipo de Pago",
                name: "PaymentMethodName",
                type: "text",
                width: 100,
                visible: false
            },
            {
                title: "Monto",
                name: "appliedAmmountString",
                type: "text",
                width: 100,
                align: 'right',
                itemTemplate: function (value) {
                    return " $ " + value;
                }
            },
            {
                title: "Monto",
                name: "appliedAmmount",
                type: "number",
                width: 100,
                visible: false,
            }, {
                title: "Saldo",
                name: "SaldoString",
                type: "text",
                width: 100,
                align: "right",
                visible: false,
                //cellRenderer: function (value, item) { return $(this).addClass("jsgrid-td-background"); },
                itemTemplate: function (value, item) {
                    //return "<span class='highlight-green'>$ " + value + "</span>";
                    var r = ""; //return " $ " + value;
                    r = value == 0 ? r = "<span class=''> $ " + value + "</span>" : value < 0 ? r = "<span class='highlight-red'> $ " + value + "</span>" : r = "<span class='highlight-green'> $ " + value + "</span>";
                    return r;
                }
            }, {
                name: "Reference",
                type: "number",
                visible: false
            },
            {
                name: "ReferenceItem",
                type: "number",
                visible: false
            }, {
                title: "",
                name: "rowIndex",
                type: "text",
                width: 24,
                itemTemplate: function (value, item) {
                    return $("<div>").addClass("rating").append("<i class='fas fa-plus-square fa-lg' data-toggle='tooltip' title='Añadir' data-original-title='Añadir' onclick='financialStateitemSave(" + 2 + "," + value + "," + item.rowIndex + ")'></i>");
                }
            },
            {
                name: "typeRsv",
                type: "number",
                visible: false
            }
            /*,
            {
              type: "control"
            }*/
        ],
        onDataLoaded: function (grid, data) {
            //$('[data-toggle="tooltip"]').tooltip();
        }
    });

    ForceCloseModal(".processingModal");
}

function getTotalValueSCTPSItems() {
    var data = $(jsGridCurrent).jsGrid("option", "data");
    var result = data.filter(item => item.bankStatementLinked == true)
        .reduce(function (accumulator, pilot) {
            return accumulator + pilot.appliedAmmount;
        }, 0);
    return result;
}

function getTotalValueSCTPSRemaing() {
    var data = $(jsGridCurrent).jsGrid("option", "data");
    var result = data.filter(item => item.bankStatementLinked == true)
        .reduce(function (accumulator, pilot) {
            return accumulator + pilot.appliedAmmount;
        }, 0);
    if (result == 0) {
        result = data.filter(item => item.bankStatementLinked == false)
            .reduce(function (accumulator, pilot) {
                return accumulator + pilot.appliedAmmount;
            }, 0);
    }

    if (dataCurrentStatement.bankstatementsamountfinal < 0) {
        value = Math.abs(dataCurrentStatement.bankstatementsamountfinal) - Math.abs(result);
        value = value * -1
    }
    else {
        value = dataCurrentStatement.bankstatementsamountfinal - result;
    }

    return value;
}

function getTotalValueSCTPSSuggestion() {
    var data = $(jsGridCurrent).jsGrid("option", "data");
    var result = data.filter(item => item.bankStatementLinked == false)
        .reduce(function (accumulator, pilot) {
            return accumulator + pilot.appliedAmmount;
        }, 0);
    return result;
}




// Utilitarias
/******************************************/
function ShowDetail() {
    $(".divSearchSCTP").hide();
    $(".divSearchDetailSCTP").show();
    searchSCTKPDetailsbyFormActive = true;
    $("#JsGridSCTKPDetailsbyForm").jsGrid("option", "data", []);
}

function ShowGeneral() {
    $(".divSearchSCTP").show();
    $(".divSearchDetailSCTP").hide();
    $(divtoErase).remove();
    dataCurrentStatement = {};
    divtoErase = "";
    jsGridCurrent = "";
    searchSCTKP(frm);
    searchSCTKPDetailsbyFormActive = false;


}

$('#ItemChkAllDocItems').click(function () {
    var c = this.checked;
    $(':checkbox').prop('checked', c);
});

//
/*************** File Upload ***************/

/*
$('#fileUpload').on("change", function (event) {
    $(".lblFileAdd").remove();
    var files = $('#fileUpload').get(0).files;
    var filesCount = files.length;
    if (filesCount > 0) {
        $('#btnUploadFileConciliation').removeClass('disabled');
        $('#btnUploadFileConciliation').attr('disabled', false);
        $.each(files, function (key, value) {
            $(".divFilesSCTPS").append("<li class='lblFileAdd margin-left-md' style='font-size: 10px;'>" + value.name + "</li>")
        });
    }
    else {
        $('#btnUploadFileConciliation').addClass('disabled');
        $('#btnUploadFileConciliation').attr('disabled', true);
    }
});


$('#btnUploadFileConciliation').on('click', function (e) {

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
        // console.log(response.data);
        notifyMessageGral('Se ha cargado correctamente el archivo, vuelva a la pantalla anterior.', "success", 500, null);
        $(".lblFileAdd").remove();
        $('#fileUpload').val("");
        ForceCloseModal(".processingModal");
    }).catch(function (error) {
        // console.log("Failed Request! SCTPSUp ->", error);
        // console.error("No se puede procesar la solicitud SCTPSUp. --> ");
        notifyMessageGral('No se ha cargado el archivo.', "error", 500, null);
        ForceCloseModal(".processingModal");
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
}*/



/******************************************/

function ShowUpExcel() {
    $(".divAddExcelStatements").show();
    $(".divSearchStatementsGral").hide();
}

function HideUpExcel() {
    $(".divAddExcelStatements").hide();
    $(".divSearchStatementsGral").show();
}



function chkStatementsClicked(e) {

    var _id = "#" + e.id + "";
    if ($(_id).is(':checked')) {
        // alert("Está activado");

        $(_id).addClass("_chkstatChild");
        $(_id).removeClass("_unchkstatChild");

        $("tr > td ").children(".fa-trash-alt").addClass("hide");
        $("tr > td ").children(".fa-trash-alt").removeClass("show");

        $(".chkstatParent").prop("checked", true);

        $(".btn_delete_sctp").addClass("show");
        $(".btn_delete_sctp").removeClass("hide");

    } else {
        // alert("No está activado");
        $(_id).addClass("_unchkstatChild");
        $(_id).removeClass("_chkstatChild");

        var _totalChildren = $(".chkstatChild").length;
        var _totalunChecked = $("._unchkstatChild").length;

        if (_totalunChecked == _totalChildren) {

            $(".chkstatParent").prop("checked", false);
            $("tr > td ").children(".fa-trash-alt").addClass("show");
            $("tr > td ").children(".fa-trash-alt").removeClass("hide");

            $(".btn_delete_sctp").addClass("hide");
            $(".btn_delete_sctp").removeClass("show");
        }
    }
}

function chkCheckUncheckStatement(e) {

    if (e.checked == true) {
        var childName = "." + e.getAttribute("childs") + ""; // check boxes
        e.setAttribute("childs", "_chkstatChild"); // check boxes

        $(childName).prop("checked", true);

        $("tr > td ").children(".fa-trash-alt").addClass("hide");
        $("tr > td ").children(".fa-trash-alt").removeClass("show");

        $(childName).addClass("_chkstatChild");
        $(childName).removeClass("_unchkstatChild");

        $(".btn_delete_sctp").addClass("show");
        $(".btn_delete_sctp").removeClass("hide");

    } else {
        var childName = "." + e.getAttribute("childs") + ""; // check boxes

        //alert("No está activado");
        $(childName).prop("checked", false);

        $("tr > td ").children(".fa-trash-alt").addClass("show");
        $("tr > td ").children(".fa-trash-alt").removeClass("hide");

        $(childName).addClass("_unchkstatChild");
        $(childName).removeClass("_chkstatChild");

        $(".btn_delete_sctp").addClass("hide");
        $(".btn_delete_sctp").removeClass("show");

        e.setAttribute("childs", "_unchkstatChild");
    }
}