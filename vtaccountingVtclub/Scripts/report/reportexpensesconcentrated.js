

$(document).ready(function () {
    //loadSegmentsByCompanyUser(null, "frmAvanzadaReportExpenses", "Segment", true);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.companies), null, 'frmAvanzadaReportExpenses', 'Segment', true);

    loadDataNotDependency(PrepareRequestGetDataGenericMethod(Components.bugetypes), null, "frmAvanzadaReportExpenses","budgetType",null)
    //loadBudgetType(null, "frmAvanzadaReportExpenses", "budgetType", null);

    loadDateTimeLoad('appdate1');
    loadDateTimeLoad('appdate2');
    loadDateTimeLoad('appdate3');
    loadDateTimeLoad('appdate4');
    
});

$(".datetimeinput").keypress(function () {
    return false;
});


$(".Segment").on("change", function (event) {
    loadDataNotDependency(PrepareRequestGetCompaniesBySegment(this.value), null, "frmAvanzadaReportExpenses", "Company", null);
    loadDataNotDependency(PrepareRequestGetAccl3bySegment(Components.getaccount3bysegment, this.value, 0), null, "frmAvanzadaReportExpenses", "category", null)
    //loadCompanyBySegment(null, this.value, 'frmAvanzadaReportExpenses', 'Company', null);
    //loadAccl3BySegmentOnly(null, this.value, 0, "frmAvanzadaReportExpenses", "category", null);
});

$(".category").on("change", function (event) {
    loadDataNotDependency(PrepareRequestGetAccl4(this.value, $(".Segment")[0].value, 0), null, "frmAvanzadaReportExpenses", "Type", null);
    //loadAccl4BySegment(null, this.value, $(".Segment")[0].value, 0, "frmAvanzadaReportExpenses", "Type");
});

var JsonData = [];
var idDocItems = [];
var searchValue = 0.00;


$('#btngeneratereport').click(function (e) {
    e.preventDefault();
    $(".expenseReportItem").remove();
    var frm = $("#frmAvanzadaReportExpenses");

    //obtenemos la promesa
    var promise = getReporData(frm);
    //la ejecutamos y accedemos al evento success
    promise.done(function (response) {
        if (response.data.length != 0) {
            idDocItems = [];
            createTr(response.data);
        }
        else { notifyMessageGral("No existen datos con los criterios de búsqueda seleccionado", "warn", null, null); $('#lblTotalSearch').text('0.00');}
    }).fail(function (jqXHR, textStatus, errorThrown) { notifyMessageGral("Falló la consulta de datos.", "warn", null, null); });
});

$('#btnReportExportExcel').click(function (e) {
    // tableToExcel('tblExpenseReportconcentrated', 'Concentrado Gastos');
    if (idDocItems.length == 0) {
        e.preventDefault();
    }
    else {
        $("input[name='gridHtml']").val($("#divToExportExcel").html());
        $("input[name='nameFile']").val('Historial Detallado');
        $("input[name='items']").val(idDocItems);
        $("input[name='typeReport']").val($("input:radio[name=isTax]:checked").val());
    }
});


function getReporData(frm) {
    getRepor = $.ajax({
        method: "POST"
        , url: Reportes.expenseconcentrated
        , data: frm.serialize() //{year: year, month: month }
        , datatype: 'json'
    });

    return getRepor;
}

function createTr(data) {
    var tr = ""; var endtr = "</tr>";
    var resulttr = "";
    var is_odd = true;
    var odd_class = "odd";

    $('#lblTotalSearch').text('0.00');
    searchValue = 0.00;

    $.each(data, function (key, value) {

        odd_class = is_odd ? "odd" : "even";

        tr = "<tr class='expenseReportItem exprep_tr-" + odd_class + "' id='financialstateitem" + key + "'>";
        resulttr = (tr + createTd(value) + endtr);
        $('#tblExpenseReportconcentrated> tbody:last-child').append(resulttr);
        $('#tblExpenseReportconcentrated0> tbody:last-child').append(resulttr);
        searchValue = parseFloat(searchValue) + parseFloat(value.ammounttotal);

        is_odd = !is_odd;
    });

    $('#lblTotalSearch').text(covertToNatural(searchValue));
}

function createTd(data) {
    var _istax = data.istax == true ? "Fiscal" : "No Fiscal";
    var _single = data.singlexibitionpayment == true ? "SI" : "NO";
    var categoryname = data.categoryName == null ? "" : data.categoryName;
    var _typename = data.typename == null ? "" : data.typename;
    //var result = "<td style='text-align:left; padding: 1px 5px 1px 5px !important; width:120px' >" + data.InvoiceIdentifier + "</td> <td style='text-align:center; width:50px;'> " + data.CompanyName + "</td> <td style='text-align:center; padding: 1px 5px 1px 5px !important; '>" + data.aplicationDateString + " </td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.billIdentifier + " </td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.categoryName + " </td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.TypeName + " </td> <td style='text-align:center; padding: 1px 5px 1px 5px !important;'>" + _istax + " </td> <td style='text-align:center; padding: 1px 5px 1px 5px !important;'> " + _single + "</td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.supplierName + " </td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'>" + data.AmmountString + " </td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'>" + data.taxesAmmountString + " </td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'> " + data.OthertaxesAmmountString + "</td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'>" + data.AmmountTotalString + "</td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'>" + data.invoiceBalanceString + " </td> <td style='padding: 1px 5px 1px 5px !important; '>" + data.description + " </td>";
    // var result = "<td style='text-align:left; padding: 1px 5px 1px 5px !important; width:120px' >" + data.InvoiceIdentifier +"</td> <td style='text-align:center; width:50px;'> " + data.CompanyName + "</td> <td style='text-align:center; padding: 1px 5px 1px 5px !important; '>" + data.aplicationDateString + " </td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.billIdentifier + " </td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.categoryName + " </td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.TypeName + " </td> <td style='text-align:center; padding: 1px 5px 1px 5px !important;'>" + _istax + " </td> <td style='text-align:center; padding: 1px 5px 1px 5px !important;'> " + _single + "</td> <td style='padding: 1px 5px 1px 5px !important;'>" + data.supplierName + " </td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'>" + data.AmmountString + " </td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'>" + data.taxesAmmountString + " </td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'> " + data.OthertaxesAmmountString + "</td> <td style='padding: 1px 5px 1px 5px !important; text-align: right;'>" + data.AmmountTotalString + "</td> <td style='padding: 1px 5px 1px 5px !important; '>" + data.description + " </td>";
    var result = "<td class=\"exprep_cell exprep_tlef exprep_col_01\">" + data.identifier + "</td><td class=\"exprep_cell exprep_tcen exprep_col_02\">" + data.companyname + "</td><td class=\"exprep_cell exprep_tcen exprep_col_03\">" + data.aplicationdatestring + "</td><td class=\"exprep_cell exprep_tlef exprep_col_04\">" + data.billidentifier + "</td><td class=\"exprep_cell exprep_tlef exprep_col_05\">" + data.budgettypename + "</td><td class=\"exprep_cell exprep_tlef exprep_col_06\">" + categoryname + "</td><td class=\"exprep_cell exprep_tlef exprep_col_07\">" + _typename + "</td><td class=\"exprep_cell exprep_tcen exprep_col_08\">" + _istax + "</td><td class=\"exprep_cell exprep_tcen exprep_col_09\">" + _single + "</td><td class=\"exprep_cell exprep_tlef exprep_col_10\">" + data.suppliername + "</td><td class=\"exprep_cell exprep_trig exprep_col_11\">" + data.ammountstring + "</td><td class=\"exprep_cell exprep_trig exprep_col_12\">" + data.taxesammountstring + "</td><td class=\"exprep_cell exprep_trig exprep_col_13\">" + data.othertaxesammountstring + "</td><td class=\"exprep_cell exprep_trig exprep_col_14\">" + data.ammounttotalstring + "</td><td class=\"exprep_cell exprep_trig exprep_col_15\">" + data.invoicebalancestring + "</td><td class=\"exprep_cell exprep_tlef exprep_col_16\">" + data.description + "</td>";
    idDocItems.push(data.id);
    return result;
}
