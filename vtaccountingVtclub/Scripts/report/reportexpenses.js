var frm = $("#frmAvanzadaReportExpenses");
var _table = "";

var JsonData;

var totalAmmountMonth = 0;
var totalData = 0;
var currrentMonth = 0;


var currentkey = 0;

// var for AccountL1,L2,L3

var stringL1StringGlobal = "";
var stringL2StringGlobal = "";
var stringL3StringGlobal = "";


var currentL1 = 0;
var totalAmmountL1 = 0;

var currentL2 = 0;
var totalAmmountL2 = 0;

var currentL3 = 0;
var totalAmmountL3 = 0;

var globalTrKeys = 0;
/*

*/
var idDivMontlyCuerrency = "";


$(document).ready(function () {
    loadYearsAvailable("frmAvanzadaReportExpenses", "year", null);
    selectMonth();
    loadDataNotDependency(PrepareRequestGetAnyData(Components.companies), null, 'frmAvanzadaReportExpenses', 'Segment', true);
    //loadSegmentsByCompanyUser(null, "frmAvanzadaReportExpenses", "Segment", true);

    loadDataNotDependency(PrepareRequestGetDataGenericMethod(Components.typefinancialreport), null, "frmAvanzadaReportExpenses", "typeReport", null);
    //loadFinancialReportType();
});

$(".Segment").on("change", function (event) {

    loadDataNotDependency(PrepareRequestGetCompaniesBySegment(this.value, Components.companysegment), null, 'frmAvanzadaReportExpenses', 'Company', null);
    //loadCompanyBySegment(null, this.value, 'frmAvanzadaReportExpenses', 'Company', null);
});



$('#btngeneratereport').click(function (e) {

    e.preventDefault();
    $("#_lblMonth").text('');
    $("#_lblYear").text('');
    $("#_lblMonth").append($("#month option:selected").text());
    $("#_lblYear").append($("#year option:selected").text());
    if ($('.trexpense').length != 0) $('.trexpense').remove();

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
    $.ajax({
        method: "POST"
        , url: Reportes.expense
        , data: frm.serialize()//{ year: year, month: month }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            JsonData = data.data
            dataExport = data.data;
            addDivCurrency(dataExport);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            notifyMessageGral(jqXHR.message, "warn", null, null);
        });
    $('.xls').remove();
    // setTimeout(function () { generateButtonsExport(); }, 3000);
    // return dataExport;
}

function addDivCurrency(items) {
    $('.divExpenseMonthlyCurrenciesClass').remove();

    if (items != null) {
        if (items.length > 0) {
            $.each(items, function (key, value) {

                if (value.expensesreport.length != 0) {
                    _reportType = value.typereportname;
                    currentCurrrencyString = value.currencyname;
                    idCurrentDivMonthlyCurrency = "divMonthlyExpenses" + currentCurrrencyString;
                    _idCurrentDivMonthlyCurrency = "#" + idCurrentDivMonthlyCurrency;
                    $("#divExpensesCurrencies").append("<div id='" + idCurrentDivMonthlyCurrency + "' class='divExpenseMonthlyCurrenciesClass'> </div>");
                    $(_idCurrentDivMonthlyCurrency).append("<td style=' background-color: #2c3e50 !important; color: #ffffff !important; font-size: 14pt; font-weight: bold; padding: 5px 0px 0px 5px !important;'> Moneda : " + currentCurrrencyString + ".&nbsp; </td>");
                    // $(_idCurrentDivMonthlyCurrency).append('<br>')            
                    $(_idCurrentDivMonthlyCurrency).append("<td style=' background-color: #2c3e50 !important; color: #ffffff !important; font-size: 14pt; font-weight: bold; padding: 5px 0px 0px 5px !important;'> Año : " + value.year + " &nbsp;.</td>");
                    // $(_idCurrentDivMonthlyCurrency).append('</br>')
                    createTableMonth(value.expensesreport);
                    totalAmmountMonth = 0;
                }
                else {
                    currentCurrrencyString = "";
                    idCurrentDivMonthlyCurrency = "";
                    _idCurrentDivMonthlyCurrency = "";
                    _reportType = "";

                }
            });
        } else {
            frm.notify("No se encontro información. ",
                {
                    position: "top right",
                    className: "error",
                    hideDuration: 500
                });
        }
    }

}


function createTableMonth(values) {
    globalTrKeys = 0;
    $.each(values, function (key, value) {
        totalData = value.expensereporitem.length; // Total items

        if (currrentMonth == value.month) {

            createTr(value, idTable);
        }
        else {
            totalAmmountMonth = 0;

            idTable = "tblExpensesDetail" + value.monthname + "" + currentCurrrencyString;
            // _table = "#" + "tblExpensesDetail" + value.monthName + "" + currentCurrrencyString;
            _table = "#" + idTable;
            var table = "<table class='' id='" + idTable + "' style=' border-color: #2c3e50; border-style: solid;'> <td colspan='4' class='' style='background-color: #2c3e50; color: #ffffff;'>  Reporte " + _reportType + " &nbsp;&nbsp;[ &nbsp;&nbsp;<label id=''> " + value.monthname + " </label> - <label id=''> " + value.year + " </label> &nbsp;&nbsp; <label id=''>" + value.companyname + "</l<bel> ] </caption>" +
                "<thead>" +
                "<tr>" +
                "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important; max-width: 400px !important; '></th>" +
                "<th scope='col' style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; padding: 1px 5px 1px 5px !important; max-width: 100px !important;'></th>" +
                "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important; max-width: 100px !important;'></th>" +
                "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important; max-width: 100px !important;'></th>" +
                // "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important;'></th>" +
                // "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important;'></th>" +
                // "<th scope='col' style='color: #ffffff !important;background-color: #2c3e50 !important;font-weight: bold !important;padding: 1px 5px 1px 5px !important;'></th>" +
                // "<th></th>" +
                "</tr>" +
                "</thead>" +
                "<tbody style='font-size: 11px;'></tbody>" +
                "</table> </br>";
            $(_idCurrentDivMonthlyCurrency).append(table);

            createTr(value, idTable);
            globalTrKeys = 0;
        }
        currrentMonth = value.month; // asiganando el mes para evaluar si es el mismo en la siguiente iteración;
    });

    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='font-size: 12pt';> &nbsp;&nbsp;&nbsp;  Utilidad : " + covertToNatural(values[0].totalincome + values[0].totalexpense) + " </td>" + "<td> </td>" + "<td> &nbsp; </td>" + "<td> &nbsp; </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    /****************************************/
    currrentMonth = 0; // Inicializando a cero global;



}

function createTr(data, idTable) {
    var resulttr = "";
    totalData = data.expensereporitem.length;
    var row = "";

    $.each(data.expensereporitem, function (key, value) {

        currentkey = key;
        if (value.length != 0) {

            if (currentL3 != value.accountl3) {
                row = "tr#trexpense" + (globalTrKeys - 1) + "";
                $(_table).find(row).find('td:eq(1)').html("" + covertToNatural(totalAmmountL3) + '');
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
                    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td class='' style='font-size: 12pt;'> " + "Total " + stringL2StringGlobal + "</td>" + "<td style=''>  </td>" + "<td class='tdbold' style='width: 150px !important; font-size: 12pt;'>" + covertToNatural(totalAmmountL2) + "<td> " + "" + " </td>" + "</tr>");
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
                    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 14pt; font-weight: bold;'> " + "Total " + stringL1StringGlobal + "</td>" + "<td class='' > " + "</td>" + "<td>" + "" + " </td>" + "<td class='tdbold' style='width: 100px !important; font-size: 14pt; font-weight: bold;'>" + covertToNatural(totalAmmountL1) + " </td>" + "</tr>");
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
            createTd("", value);
            totalAmmountMonth = totalAmmountMonth + value.ammounttotal;
        }


    });


    row = "tr#trexpense" + (globalTrKeys - 1) + "";
    $(_table).find(row).find('td:eq(1)').html(" " + covertToNatural(totalAmmountL3) + '');

    //AcountL2
    //row = "tr#trexpense" + (globalTrKeys - 1) + "";


    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td class='' style=' border-top-color: #2c3e50;border-top-style: solid;'>  </td>" + "<td> " + "&nbsp;" + " </td>" + "<td class=''> " + "&nbsp;" + "</td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    //row = "tr#trexpense" + (globalTrKeys - 1) + "";
    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='font-size: 12pt;'> " + "Total " + stringL2StringGlobal + " </td>" + "<td>   </td>" + "<td class='tdbold' style='width: 150px !important; font-size: 12pt;'>" + covertToNatural(totalAmmountL2) + " </td>" + "<td> " + "" + " </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td>  </td>" + "<td class='tdbold' style='width: 100px !important;'> " + "&nbsp;" + " </td>" + "<td> " + "&nbsp;" + " </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;
    //AcountL1
    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 14pt; font-weight: bold;'> Total " + stringL1StringGlobal + " </td>" + "<td> </td>" + "<td>  </td>" + "<td class='tdbold' style='width: 150px !important; font-size: 14pt; font-weight: bold;'>" + covertToNatural(totalAmmountL1) + " </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;

    // end empty
    $('' + _table + '> tbody:last-child').append("<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td> &nbsp; </td>" + "<td> </td>" + "<td> &nbsp; </td>" + "<td> &nbsp; </td>" + "</tr>");
    globalTrKeys = globalTrKeys + 1;



    currentL1 = 0;
    currentL2 = 0;
    currentL3 = 0;
    totalAmmountL1 = 0;
    totalAmmountL2 = 0;
    totalAmmountL3 = 0;
    return resulttr;
}

function createTd(currency, data) {
    var tr = "";
    var endtr = "</tr>"
    var result = "";
    var stringL1String = currentL1 != data.accountl1 ? stringL1String = data.accountl1name : "";
    var stringL2String = currentL2 != data.accountl2 ? stringL2String = data.accountl2name : "";
    var stringL3String = currentL3 != data.accountl3 ? stringL3String = data.accountl3name : "";
    stringL2StringGlobal = data.accountl2name;
    stringL1StringGlobal = data.accountl1name;
    if (currentkey == 0) {
        totalAmmountL1 = data.ammounttotal
        totalAmmountL2 = data.ammounttotal
        totalAmmountL3 = data.ammounttotal
    }


    // Evaluando AccountL1 para acumular o mostrar valor de L1
    if (currentL1 != data.accountl1) {
        result = "<tr class='trexpense accountL1' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 14pt; font-weight: bold;'>" + stringL1String + " </td>" + "<td>  </td>" + "<td> </td>" + "<td> </td>" + endtr;
        totalAmmountL1 = data.ammounttotal;
        globalTrKeys = globalTrKeys + 1;
        $('' + _table + '> tbody:last-child').append(result);
    }
    // Evaluando AccountL2 para acumular o mostrar valor de L2
    if (currentL2 != data.accountl2) {
        result = "<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 12pt;'> " + stringL2String + " </td>" + "<td>   </td>" + "<td> </td>" + "<td> </td>" + endtr;
        totalAmmountL2 = data.ammounttotal;
        globalTrKeys = globalTrKeys + 1;
        $('' + _table + '> tbody:last-child').append(result);
    }
    // Evaluando AccountL3 para acumular o mostrar valor de L3
    if (currentL3 != data.accountl3) {
        result = "<tr class='trexpense' id='trexpense" + globalTrKeys + "'>" + "<td style='width: 300px !important; font-size: 10pt;'> " + stringL3String + "</td>" + "<td style='width: 100px !important;'>   </td>" + "<td> " + "" + " </td>" + "<td>" + "" + " </td>" + endtr;
        globalTrKeys = globalTrKeys + 1;
        $('' + _table + '> tbody:last-child').append(result);
    }
    currentL1 = data.accountl1;
    currentL2 = data.accountl2;
    currentL3 = data.accountl3;
    return result;
}

function selectMonth() {
    $('#month').val(new Date().getMonth() + 1);
    //$('#Hotel').val(5);
}
