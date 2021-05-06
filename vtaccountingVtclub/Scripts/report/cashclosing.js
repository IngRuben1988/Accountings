var frm = $("#frmAReportCashClosing");
var _reportData;
$(document).ready(function () {
    loadDataNotDependency(PrepareRequestGetSegmentsbyUserBankAccount(Components.getsegmentbyuser), null, "frmAReportCashClosing", "Segment", true)
    //loadSegmentsByUserBankAccount(null, "frmAReportCashClosing", "Segment");
    //dateTimePickerFrom("appdate1", null, null);
    loadDateTimeLoad("appdate1")
    loadDateTimeLoad("appdate2")
    $(".ammount").ForceNumericOnly();
});
/*
$('#dateReportStart' ).on("blur", function () {
    $("#appdate2").datetimepicker('destroy');
    $("#appdate2").datetimepicker({
        format: 'YYYY-MM-DD',
        minDate: new Date($('#dateReportStart').val())
    });
});*/

$(".datetimeinput").keypress(function () {
    return false;
});

$(".Segment").on("change", function (event) {
    loadDataNotDependency(PrepareRequestGetCompaniesbyUserBAccount(Components.getCompaniesbySegmentUser, this.value), null, "frmAReportCashClosing", "Company", null);
    //loadCompaniesBySegmentUserBAccount(null, this.value, 'frmAReportCashClosing', 'Company');
});

$(".CompanyDependecyPayment").on('change', function (e) {

    var form = this.form;
    var child = this.getAttribute("tochild");

    loadDataNotDependency(PrepareRequestGetBankAccountbyCompanyUserBanAccount(this.value, formControl.BankAccountByCompany), null, form.name, child, true);

});


$("#btnExportCashClosing").click(function () {
    // tableToExcel('tblfinancialStateAccountTimeLine', 'Reporte_Corte_de_Caja');
    window.open(Reportes.accountsClosingExportExcel + "?" + frm.serialize());
});


$('#btngenerateReport').click(function (e) {
    e.preventDefault();
    $('.xls').remove();
    if ($('#PaymentMethod').val() != 0) {
        $("#_lblDate").text('');
        $("#_lblBAccount").text('');
        $("#_lblDate").append($("#dateReport").val());
        $("#_lblBAccount").append($("#PaymentMethod option:selected").text());

        // if ($('.financialstateitem').length != 0) $('.financialstateitem').remove();
        $("#divToExportExcel_container").remove();
        getReporData(frm);
        // setTimeout(function () { generateButtonsExport(); }, 3000);
    } else {
        notifyMessageGral("Debe seleccionar una cuenta.", "warn", null, null);
    }
});

function generateButtonsExport() {
    $('.xls').remove();
    $('#tblfinancialStateAccountTimeLine').tableExport({
        bootstrap: true,
        formats: ['xlsx'],
        filename: $("#dateReport").val() + "-" + $("#PaymentMethod option:selected").text()
    });
    // });
};

var _container = "<div id='divToExportExcel_container' class='' style='padding-left: 100px; padding-right: 100px;'> </div>";
var _table = "<table class='' id='tblfinancialStateAccountTimeLine' name='tblfinancialStateAccountTimeLine' style='width:100%; border-color: #2c3e50; border-style: solid;'>" +
    "<caption class='tablecaption' style='background-color: #2c3e50; color: #ffffff;'>  &nbsp;&nbsp; Historial Detallado   &nbsp;&nbsp;<label id='_lblDate'>  </label> - <label id='_lblBAccount'></label> &nbsp;&nbsp;   &nbsp; [&nbsp; Saldo anterior: $ &nbsp; <label id='_lblBalancebefore'></label> &nbsp;]</caption>" +
    "<thead>" +
    "<tr>" +
    "<th style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; padding: 1px 5px 1px 5px !important; width:200px; text-align:center' class='reorder sorting_desc'>Fecha</th>" +
    "<th style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; padding: 1px 5px 1px 5px !important; width:50px; text-align:center'></th>" +
    "<th style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; padding: 1px 5px 1px 5px !important; width:700px; max-width:700px;'>Descripci&oacute;n</th>" +
    "<th style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; padding: 1px 5px 1px 5px !important; width:200px; text-align:center'>Monto</th>" +
    "<th style='color: #ffffff !important; background-color: #2c3e50 !important; font-weight: bold !important; padding: 1px 5px 1px 5px !important; width:200px; text-align:center'>Saldo</th>" +
    "</tr>" +
    "</thead>" +
    "<tbody style='font-size: 11px;'></tbody>" +
    "</table>";

function getReporData(frm) {

    OpenModalProcessing(".processingModal");
    $("#divToExportExcel").append(_container);

    axios.post(Reportes.accountsClosing, frm.serialize())
        .then(function (response) {

            $("#divToExportExcel_container").append(_table);

            _reportData = response.data.data;
            $('#_lblBalancebefore').html('');
            $('#tblfinancialStateAccountTimeLine> tbody:last-child').append("<tr class='financialstateitem'> <td></td> <td></td> <td></td> <td><p style='margin: 5px 0 5.5px;font-weight: bold;'>Saldo Inicial</p></td> <td style='text-align: right; padding-right: 15px;'>" + _reportData.balanceBeforeString + "</td> </tr>");
            createTr(_reportData.financialstateitemlist);
            $("#_lblBalancebefore").append(_reportData.balanceBeforeString);
            $('#tblfinancialStateAccountTimeLine> tbody:last-child').append("<tr class='financialstateitem'> <td></td> <td></td> <td></td> <td> <p class='thicker' style='font-weight: bold;'>Saldo Final </p> </td> <td style='text-align: right; padding-right: 15px;'> <p class='thicker'>" + _reportData.balanceString + " </p> </td> </tr>");
            ForceCloseModal(".processingModal");
        }).then(function () {
            
            if ($.fn.dataTable.isDataTable('#tblfinancialStateAccountTimeLine')) {
                table = $('#tblfinancialStateAccountTimeLine').DataTable({
                    paging: false,
                    "language": {
                        "search": "Búscar:",
                        "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                        "infoFiltered": " <span class='quickApproveTable_info_filtered_span'>(filtered from _MAX_ total entries)</span>",
                        "infoEmpty": "Mostrando 0 a 0 de 0 registros",
                        "emptyTable": "Sin datos disponibles en la tabla",
                        "zeroRecords": "No se encontraron coincidencias"
                    },

                });
            }
            else {
                table = $('#tblfinancialStateAccountTimeLine').DataTable({
                    paging: false,
                    "language": {
                        "search": "Búscar:",
                        "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                        "infoFiltered": " <span class='quickApproveTable_info_filtered_span'>(filtados de un total de _MAX_  registros)</span>",
                        "infoEmpty": "Mostrando 0 a 0 de 0 registros",
                        "emptyTable": "Sin datos disponibles en la tabla",
                        "zeroRecords": "No se encontraron coincidencias"
                    },

                });
            }

            $("#tblfinancialStateAccountTimeLine_wrapper").css("display","inline");

            $("#tblfinancialStateAccountTimeLine_wrapper").find('div').removeClass('row')
            $("#tblfinancialStateAccountTimeLine_wrapper").find('div').addClass('col-lg-12')

        })
        .catch(function (error) {

            ForceCloseModal(".processingModal");
            /*$.alert({
                title: ' ¡ Error !',
                content: 'No se puede realizar la consulta.',
            });*/
        });

}

function createTr(datas) {
    var tr = "";
    var endtr = "</tr>"
    var resulttr = "";

    $.each(datas, function (key, value) {
        tr = "<tr class='financialstateitem' id='financialstateitem" + key + "'>";
        if (value.length != 0) {
            resulttr = (tr + createTd(value) + endtr);
            $('#tblfinancialStateAccountTimeLine> tbody:last-child').append(resulttr);
        }
    });
}

function createTd(data) {

    var _classStateLink = data.bankStatementStatus == 1 ? "bank_statements_success_i" : data.bankStatementStatus == 2 ? "bank_statements_danger_i" : data.bankStatementStatus == 3 ? "bank_statements_error_i" : "bank_statements_nostatus_i";
    var _linked = data.bankStatementLinked == true ? "<i class='fas fa-link " + _classStateLink + "' id='bslinked_" + data.rowIndex + "' onclick='showActions(" + data.rowIndex + ")'></i>" : "";
    var result = "<td style='text-align:center; padding: 1px 3px 1px 3px !important;'>" + data.aplicationDateString + " </td> <td style='text-align:center; padding: 1px 3px 1px 3px !important;'>" + _linked + "</td> <td style='text-align:left; padding: 1px 3px 1px 3px !important;'>" + data.description + " </td> <td style='text-align: right; padding: 1px 3px 1px 3px !important;'>" + data.appliedAmmountString + " </td>" + "<td style='text-align: right; padding: 1px 3px 1px 3px !important;'>" + data.balanceString + " </td>";
    return result;
}

var _confirm_bsi;

function showActions(rowindex) {

    axios.get(bankStatementsUrl.getSCBKPosbyReference +'?sourcedata=' + _reportData.financialstateitemlist[rowindex].SourceData + '&reference=' + _reportData.financialstateitemlist[rowindex].Reference + "&referenceitem=" + _reportData.financialstateitemlist[rowindex].ReferenceItem)
        .then(function (response) {
            _dataBankStatement = response.data.data;
            var identifier = "# " + _dataBankStatement.idBankStatements + " - " + _dataBankStatement.tpvname + " " + _dataBankStatement.companyname + " " + _dataBankStatement.currencyname + " " + _dataBankStatement.baccountname + " " + _dataBankStatement.bankstatementaplicationdatestring + "  $ " + _dataBankStatement.bankstatementappliedammountstring + "";
            _confirm_bsi = $.confirm({
                title: 'Conciliaciones',
                content: '<div style="font-size: 0.90em !important;"> Este registro está relacionado con la conciliación <b> ' + identifier + ' </b>. <br> ¿Desea eliminarlo de esta conciliacón?</div>',
                containerFluid: true,
                boxWidth: '50%',
                useBootstrap: false,
                theme: 'material',
                type: 'red',
                lazyOpen: true,
                buttons: {
                    confirm: {
                        text: 'Eliminar',
                        action: function () {
                            delete_bsi(rowindex);
                        }
                    },
                    cancel: {
                        text: 'Cancelar',
                        action: function () {
                        }
                    }
                }
            });
            _confirm_bsi.open();
        })
        .catch(function (error) {
            $.alert({
                title: ' ¡ Error !',
                content: 'No se puede consultar la conciliación el registro.',
            });

        })
}

function delete_bsi(rowindex) {
    OpenModalProcessing(".processingModal");
    axios.post(bankStatementsUrl.deleteSCBKPosItemReference, {
        SourceData: _reportData.financialstateitemlist[rowindex].SourceData,
        Reference: _reportData.financialstateitemlist[rowindex].Reference,
        ReferenceItem: _reportData.financialstateitemlist[rowindex].ReferenceItem
    })
        .then(function (response) {
            _confirm_bsi.close();
            _confirm_bsi.toggle();

            var _nameI = "#bslinked_" + rowindex + "";
            $(_nameI).hide();
            ForceCloseModal(".processingModal");
            notifyMessageGral("Se ha eliminado correctamente el registro de conciliación.", 'success', 800, '.info');
        })
        .catch(function (error) {
            ForceCloseModal(".processingModal");

            $.alert({
                title: ' ¡ Error !',
                content: 'No se puede eliminar el registro.',
            });
            return false;
        });
}
