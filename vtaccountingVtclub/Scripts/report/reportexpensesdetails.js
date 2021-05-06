var frm = $("#frmAvanzadaReportExpenses");
// var _table = "#tblExpensesDetail";
var _table = "";

var JsonData;
var margenOperación = {};

var totalAmmountMonth = 0;
var totalData = 0;
var currrentMonth = 0;


var currentkey = 0;

// var for accountl1,L2,L3

var stringL1StringGlobal = "";
var stringL2StringGlobal = "";
var stringL3StringGlobal = "";


var currentL1 = 0;
var totalAmmountL1 = 0;

var currentL2 = 0;
var totalAmmountL2 = 0;

var currentL3 = 0;
var totalAmmountL3 = 0;
var lastL3 = 0;

var currentL4 = 0;
var totalAmmountL4 = 0;

var globalTrKeys = 0;
/*

*/
var idDivMontlyCuerrency = "";

$(document).ready(function () {
    loadYearsAvailable("frmAvanzadaReportExpenses", "year", null);
    selectMonth();
    loadDataNotDependency(PrepareRequestGetAnyData(Components.companies), null, 'frmAvanzadaReportExpenses', 'Segment', true);
    //loadSegmentsByCompanyUser(null, "frmAvanzadaReportExpenses", "Segment", true);
    //loadFinancialReportType();
    loadDataNotDependency(PrepareRequestGetDataGenericMethod(Components.typefinancialreport), null, "frmAvanzadaReportExpenses", "typeReport", null);

    //$('.popover-dismiss').popover({
    //    trigger: 'focus'
    //});

    //$('[data-toggle="popover"]').popover({
    //    trigger: 'focus'
    //});

});

$(".Segment").on("change", function (event) {
    // loadCompanyBySegment(null, this.value, 'frmAvanzadaReportExpenses', 'company', null);
    loadDataNotDependency(PrepareRequestGetCompaniesBySegment(this.value, Components.companysegment), null, "frmAvanzadaReportExpenses", "Company", null);
});


$('#btngenerateReport').click(function (e) {

    e.preventDefault();
    $("#_lblMonth").text('');
    $("#_lblYear").text('');
    $("#_lblMonth").append($("#month option:selected").text());
    $("#_lblYear").append($("#year option:selected").text());
    if ($('.trexpense').length != 0) $('.trexpense').remove();
    // $("#dialog").dialog("close");

    getReporData(frm);

});


$("#btnExportMontly").click(function () {
    var _html = $("#divExpensesCurrencies").html();
    $("input[name='gridHtml']").val(_html);
    $("input[name='nameFile']").val('Reporte_Mensual_Estado');
});

var dataExport = [];

var currentkeyIndex = 0;
var currentCurrrencyString = "";
var idCurrentDivMonthlyCurrency = "";
var _idCurrentDivMonthlyCurrency = "";
var _reportType = "";

function getReporData(frm) {


    axios.post('/reports/expenseDetails', frm.serialize())
        .then(function (response) {
            JsonData = response.data.data
            dataExport = response.data.data;
            addDivCurrency(dataExport);            
        })
        .catch(function (error) {
            notifyMessageGral("Se produjo un error al realizar el reporte. ", "warn", null, null);
            console.error(error);
            // console.error(" Stack" + data.stackTrace);            
        });

    /*
    $.ajax({
        method: "POST"
        , url: "/reports/expenseDetails"
        , data: frm.serialize() //{ year: year, month: month }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

            if (data.success = true) {
                JsonData = data.data
                dataExport = data.data;
                addDivCurrency(dataExport);
            }
            else {
                notifyMessageGral("Se produjo un error al realizar el reporte. ", "warn", null, null);
                console.error(data.message);
                console.error(" Stack" + data.stackTrace);
            }

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            notifyMessageGral("Se produjo un error al realizar el reporte. ", "warn", null, null);
            console.error(jqXHR.message);
        });
    $('.xls').remove();
    // setTimeout(function () { generateButtonsExport(); }, 3000);
    // return dataExport;
    */
}

function addDivCurrency(items) {
    $('.divExpenseMonthlyCurrenciesClass').remove();
    $('.popover').remove();

    var _results = 0;
    $.each(items, function (key, value) {
        if (value.expensesreport.length != 0) {
            _reportType = value.typereportname;
            // console.log("Key [" + key + "]  - value ["+value.CurrencyName+"]");

            currentCurrrencyString = value.currencyname;
            idCurrentDivMonthlyCurrency = "divMonthlyExpenses" + currentCurrrencyString;
            _idCurrentDivMonthlyCurrency = "#" + idCurrentDivMonthlyCurrency;
            $("#divExpensesCurrencies").append("<div id='" + idCurrentDivMonthlyCurrency + "' class='divExpenseMonthlyCurrenciesClass'> </div>");
            $(_idCurrentDivMonthlyCurrency).append("<td style=' background-color: #2c3e50 !important; color: #ffffff !important; font-size: 14pt; font-weight: bold; padding: 5px 0px 0px 5px !important;'> Moneda : " + currentCurrrencyString + ".&nbsp; </td>");
            // $(_idCurrentDivMonthlyCurrency).append('<br>')            
            $(_idCurrentDivMonthlyCurrency).append("<td style=' background-color: #2c3e50 !important; color: #ffffff !important; font-size: 14pt; font-weight: bold; padding: 5px 0px 0px 5px !important;'> Año : " + value.year + " &nbsp;.</td>");
            // $(_idCurrentDivMonthlyCurrency).append('</br>')
            createTableMonth(key, value.expensesreport);
            totalAmmountMonth = 0;
            _results = _results += value.expensesreport.length;
        }
        else {
            currentCurrrencyString = "";
            idCurrentDivMonthlyCurrency = "";
            _idCurrentDivMonthlyCurrency = "";
        }

    });

    if (_results == 0) {

        notifyMessageGral("La búsqueda no arrojo resultados", "info", null, null);
    }

}


function createTableMonth(keyCurrency, values) {
    globalTrKeys = 0;
    $.each(values, function (key, value) {

        // console.log("Key [" + key + "]  - expensesReport value [" + value.HotelName + "]");
        totalData = value.expensereporitem.length; // Total items

        if (currrentMonth == value.month) {

            createTr(keyCurrency, key, value, idTable);
        }
        else {
            totalAmmountMonth = 0;

            idTable = "tblExpensesDetail" + value.monthname + "" + currentCurrrencyString;
            // _table = "#" + "tblExpensesDetail" + value.monthName + "" + currentCurrrencyString;
            _table = "#" + idTable;
            var table = "<table class='' id='" + idTable + "' style=' border-color: #2c3e50; border-style: solid;'> <td colspan='4' class='' style='background-color: #2c3e50; color: #ffffff;'>  Reporte " + _reportType + " &nbsp;&nbsp;[ &nbsp;&nbsp;<label id=''> " + value.monthname + " </label> - <label id=''> " + value.year + " </label> &nbsp;&nbsp; <label id=''>" + value.companyname + "</label> ] </caption>" +
                "<thead>" +
                "<tr>" +
                "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important; max-width: 400px !important; '></th>" +
                "<th scope='col' style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; padding: 1px 5px 1px 5px !important; max-width: 100px !important;'></th>" +
                "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important; max-width: 100px !important;'></th>" +
                "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important; max-width: 100px !important;'></th>" +
                "</tr>" +
                "</thead>" +
                "<tbody style='font-size: 11px;'></tbody>" +
                "</table> </br>";
            $(_idCurrentDivMonthlyCurrency).append(table);

            createTr(keyCurrency, key, value, idTable);
            globalTrKeys = 0;
        }
        currrentMonth = value.month; // asiganando el mes para evaluar si es el mismo en la siguiente iteración;
    });

    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='font-size: 12pt';> &nbsp;&nbsp;&nbsp;  Utilidad : " + covertToNatural(values[0].totalincome + values[0].totalexpense) + " </td>" + "<td> </td>" + "<td> &nbsp; </td>" + "<td> &nbsp; </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    /****************************************/
    currrentMonth = 0; // Inicializando a cero global;

}

function createTr(keyCurrency, keyExpenseReport, data, idTable) {
    var resulttr = "";
    totalData = data.expensereporitem.length;
    var row = "";

    $.each(data.expensereporitem, function (key, value) {
        // console.log("Key [" + key + "] de createTr / expenseReporItem.");
        currentkey = key;

        if (value.length != 0) {

            // AccL3
            if (currentL3 != value.accountl3) {
                row = "tr#trexpense" + (lastL3) + "";
                $(_table).find(row).find('td:eq(1)').html(" " + covertToNatural(totalAmmountL3) + '');
                totalAmmountL3 = value.ammounttotal;
            }
            else {
                totalAmmountL3 = totalAmmountL3 + value.ammounttotal;
            }

            // AccL2
            if (currentL2 != value.accountl2) {

                if (globalTrKeys != 0) {
                    row = "tr#trexpense" + (globalTrKeys - 1) + "";
                    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td class='' style='border-top-color: #2c3e50;border-top-style: solid;'>  </td>" + "<td> " + "&nbsp;" + " </td>" + "<td class=''> " + "&nbsp;" + "</td>" + "</tr>");
                    globalTrKeys = globalTrKeys + 1;
                    row = "tr#trexpense" + (globalTrKeys - 1) + "";
                    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td class='' style='font-size: 12pt;'> &nbsp; &nbsp; &nbsp;" + "Total " + stringL2StringGlobal + "</td>" + "<td style=''>  </td>" + "<td class='tdbold' style='width: 150px !important; font-size: 12pt;'>  " + covertToNatural(totalAmmountL2) + "<td> " + "" + " </td>" + "</tr>");
                    globalTrKeys = globalTrKeys + 1;
                    row = "tr#trexpense" + (globalTrKeys - 1) + "";
                    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td> </td>" + "<td> " + "&nbsp;" + " </td>" + "<td class=''> " + "&nbsp;" + "</td>" + "</tr>");
                    globalTrKeys = globalTrKeys + 1;
                }
                totalAmmountL2 = value.ammounttotal;
            }
            else {
                totalAmmountL2 = totalAmmountL2 + value.ammounttotal;
            }

            // AccL1
            if (currentL1 != value.accountl1) {
                if (globalTrKeys != 0) {
                    row = "tr#trexpense" + (globalTrKeys - 1) + "";
                    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 14pt; font-weight: bold;'> &nbsp; &nbsp; &nbsp;" + "Total " + stringL1StringGlobal + "</td>" + "<td class='' > " + "</td>" + "<td>" + "" + " </td>" + "<td class='tdbold' style='width: 100px !important; font-size: 14pt; font-weight: bold;'> " + covertToNatural(totalAmmountL1) + " </td>" + "</tr>");
                    globalTrKeys = globalTrKeys + 1;

                    // end empty
                    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td>  </td>" + "<td> &nbsp; </td>" + "<td> &nbsp; </td>" + "</tr>");
                    globalTrKeys = globalTrKeys + 1;
                }
                totalAmmountL1 = value.ammounttotal;
            }
            else {
                totalAmmountL1 = totalAmmountL1 + value.ammounttotal;
            }
            createTd(keyCurrency, keyExpenseReport, key, "", value);
            totalAmmountMonth = totalAmmountMonth + value.ammounttotal;
        }
    });


    row = "tr#trexpense" + (lastL3) + "";
    $(_table).find(row).find('td:eq(1)').html("" + covertToNatural(totalAmmountL3) + '');

    //AcountL2
    //row = "tr#trexpense" + (globalTrKeys - 1) + "";

    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td class='' style=' border-top-color: #2c3e50;border-top-style: solid;'>  </td>" + "<td> " + "&nbsp;" + " </td>" + "<td class=''> " + "&nbsp;" + "</td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    //row = "tr#trexpense" + (globalTrKeys - 1) + "";
    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='font-size: 12pt;'> &nbsp; &nbsp; &nbsp;" + "Total " + stringL2StringGlobal + " </td>" + "<td>   </td>" + "<td class='tdbold' style='width: 150px !important; font-size: 12pt;'> " + covertToNatural(totalAmmountL2) + " </td>" + "<td> " + "" + " </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td>  </td>" + "<td class='tdbold' style='width: 100px !important;'> " + "&nbsp;" + " </td>" + "<td> " + "&nbsp;" + " </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;
    //AcountL1
    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 14pt; font-weight: bold;'> Total " + stringL1StringGlobal + " </td>" + "<td> </td>" + "<td>  </td>" + "<td class='tdbold' style='width: 150px !important; font-size: 14pt; font-weight: bold;'> " + covertToNatural(totalAmmountL1) + " </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    // end empty
    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td> </td>" + "<td> &nbsp; </td>" + "<td> &nbsp; </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;



    currentL1 = 0;
    currentL2 = 0;
    currentL3 = 0;
    currentL4 = 0;
    totalAmmountL1 = 0;
    totalAmmountL2 = 0;
    totalAmmountL3 = 0;
    totalAmmountL4 = 0;
    lastL3 = 0;
    return resulttr;
}

function createTd(keyCurrency, keyExpenseReport, keyReportItem, currency, data) {

    // console.log(JsonData[keyCurrency].expensesReport[keyExpenseReport].expenseReporItem[keyReportItem].docitems)
    var tr = "";
    var endtr = "</tr>"
    var result = "";
    var stringL1String = currentL1 != data.accountl1 ? stringL1String = data.accountl1name : "";
    var stringL2String = currentL2 != data.accountl2 ? stringL2String = data.accountl2name : "";
    var stringL3String = currentL3 != data.accountl3 ? stringL3String = data.accountl3name : "";
    var stringL4String = currentL4 != data.accountl4 ? stringL4String = data.accountl4name : "";

    stringL2StringGlobal = data.accountl2name;
    stringL1StringGlobal = data.accountl1name;

    if (currentkey == 0) {
        totalAmmountL1 = data.ammounttotal;
        totalAmmountL2 = data.ammounttotal;
        totalAmmountL3 = data.ammounttotal;
        totalAmmountL4 = data.ammounttotal;
    }


    // Evaluando accountl1 para acumular o mostrar valor de L1
    if (currentL1 != data.accountl1) {
        result = "<tr class='trexpense accountl1' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 14pt; font-weight: bold;'>"+ stringL1String + " </td>" + "<td>  </td>" + "<td> </td>" + "<td> </td>" + endtr;
        totalAmmountL1 = data.ammounttotal;
        globalTrKeys = globalTrKeys + 1;
        $('' + _table + '> tbody:last-child').append(result);
    }
    // Evaluando accountl2 para acumular o mostrar valor de L2
    if (currentL2 != data.accountl2) {
        result = "<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 12pt;'>&nbsp;" + stringL2String + " </td>" + "<td>   </td>" + "<td> </td>" + "<td> </td>" + endtr;
        totalAmmountL2 = data.ammounttotal;
        globalTrKeys = globalTrKeys + 1;
        $('' + _table + '> tbody:last-child').append(result);
    }
    // Evaluando accountl3 para acumular o mostrar valor de L3
    if (currentL3 != data.accountl3) {
        result = "<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 10pt;'>&nbsp;&nbsp;&nbsp;" + stringL3String + "</td>" + "<td style='width: 100px !important; font-weight: bold;'>   </td>" + "<td> " + "" + " </td>" + "<td>" + "" + " </td>" + endtr;
        lastL3 = globalTrKeys;
        globalTrKeys = globalTrKeys + 1;
        $('' + _table + '> tbody:last-child').append(result);

        // result = "<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 10pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + stringL4String + "</td>" + "<td style='width: 100px !important; text-align: right;'>   </td>" + "<td> " + "" + " </td>" + "<td>" + "" + " </td>" + endtr;
        // globalTrKeys = globalTrKeys + 1;
        // $('' + _table + '> tbody:last-child').append(result);

    }
    // Evaluando accountl4
    if (currentL4 != data.accountl4) {
        // result = "<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 10pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + stringL4String + "</td>" + "<td id='tdexpense" + globalTrKeys + "' rel='popover' tabindex='0' class='popover-dismiss' style='width: 100px !important; text-align: right;' onclick='generateAlert(" + keyCurrency + "," + keyExpenseReport + "," + keyReportItem + ",this)'>$ " + data.ammounttotalString + "  </td>" + "<td> " + "" + " </td>" + "<td>" + "" + " </td>" + endtr;
        result = "<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 9pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + stringL4String + "</td>" + "<td id='tdexpense" + globalTrKeys + "'  tabindex='0'  data-toggle='popover'  data-trigger='focus' class='popover-dismiss' style='width: 100px !important; text-align: right;' onclick='generateAlert(" + keyCurrency + "," + keyExpenseReport + "," + keyReportItem + ",this)'> " + data.ammounttotalstring + "  </td>" + "<td> " + "" + " </td>" + "<td>" + "" + " </td>" + endtr;
        globalTrKeys = globalTrKeys + 1;
        $('' + _table + '> tbody:last-child').append(result);
    }

    currentL1 = data.accountl1;
    currentL2 = data.accountl2;
    currentL3 = data.accountl3;
    currentL4 = data.accountl4;

    return result;
}

function selectMonth() {
    $('#month').val(new Date().getMonth() + 1);
}


function generateAlert(keyCurrency, keyExpenseReport, keyReportItem, _this) {


    // $('.popover-dismiss_clear').remove();
    // $('.trPopover').remove();

    // $('.tblReportItemDocitemCopy').remove();

    var _reportItem = JsonData[keyCurrency].expensesreport[keyExpenseReport].expensereporitem[keyReportItem];
    var _docitems = JsonData[keyCurrency].expensesreport[keyExpenseReport].expensereporitem[keyReportItem].invoiceitems;
    // $("#modal-body").append('<div id="divTable"></div >');

    var _tablehead = "<table class='tblReportItemDocitemCopy' id='tblReportItemDocitemCopy' style=' border-color: #2c3e50; border-style: solid; width: 100%;'> " +
        "<thead style='display: block; background-color: #2c3e50 !important;'>" +
        "<tr>" +
        "<th scope='col' style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; width: 150px !important; text-align: center;'> Identificador </th>" +
        "<th scope='col' style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; width: 150px !important; text-align: center;'> Costo </th>" +
        "<th scope='col' style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important;'> Referencia </th>" +
        "</tr>" +
        "</thead>";

    var _tablebody = " <tbody style='font-size: 11px; max-height: 500px; overflow-y: auto; overflow-x:hidden; display: block;'> ";

    $.each(_docitems, function (key, value) {
        var descripcionString = value.description == null ? 'N/A' : value.description
        _tablebody = _tablebody + "<tr class='trPopover' id='trexpense'>" + "<td style='font-size: 11px; font-weight: bold; padding: 1px 5px 1px 5px !important; width: 150px !important; text-align: left;'>&nbsp;" + value.identifier + " </td>" + "<td style='text-align: right; padding: 1px 20px 1px 5px !important; width: 150px !important;'>" + value.ammounttotalstring + " </td> <td style='padding: 1px 5px 1px 5px !important; text-align: justify;'> " + descripcionString + " </td> </tr>";
    });

    var _tablebottom = " </tbody> " +
        " </table> ";
    var _table = _tablehead + _tablebody + _tablebottom;
    $.confirm({
        title: _reportItem.accountl4name,
        content: _table,
        useBootstrap: false,
        boxWidth: '50%',
        theme: 'material',
        type: 'dark',
        draggable: true,
        icon: 'fa fa-info-circle',
        containerFluid: true,
        buttons: {
            generateExcel: {
                text: 'XLS',
                btnClass: 'btn-green',
                // keys: ['enter', 'shift'],
                action: function () {
                    // $.alert('Something else?');
                    tableToExcel('tblReportItemDocitemCopy', 'Gastos');
                }
            },
            cancel: {
                text: 'Cerrar',
                btnClass: 'btn-default',
                action: function () {
                }
                // $.alert('Canceled!');
            }
        }
    });
}
