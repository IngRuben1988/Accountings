app = {};
app.fondosurl = "/Budget/getBudgets";
app.fondosUp = "/Budget/budgetUp";
app.fondosUpdate = "/Budget/budgetUpdate";
app.fondosDelete = "/Budget/budgetDelete";
app.fondosDateFinalDate = "/Budget/getBudgetFinalDate";
app.fondosfinalState = "/Budget/getTotalFinanceState";



financialstate = {};

var frmFondo = $('#frmFondos');

$(document).ready(function () {

    loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType($("#frmAvanzadaFondos")[0].Type.value, formControl.BankAccountByCompanyGive), null, "frmAvanzadaFondos", "Company")
    // compañia origen
    loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType($("#frmFondos")[0].FinanceType.value, formControl.BankAccountByCompanyGive), null, "frmFondos", "CompanyDependecyFinancial", false);
    // compañia destino
    loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType($("#frmFondos")[0].FinanceType.value, formControl.BankAccountByCompanyReceive), null, "frmFondos", "CompanyDependecyPayment", false);


    // loadPayMethods();
    loadDataNotDependency(PrepareRequestGetCurrencies(formControl.Currencies), null, "frmFondos", "Currency");
    fondosgrid();
    //$('[data-toggle="tooltip"]').tooltip();
    //loadDateTime('datetimepicker1')
    loadDateTime('datetimepicker2', new Date())
    loadDateTime('datetimepicker3', new Date())
    loadDateTimeLoad('datetimepicker_1')
    loadDateTimeLoad('datetimepicker_2')
    loadDateTimeLoad('datetimepicker_3')

    setTimeout(function () {
        if ($(".jsgrid-load-shader").is(":visible") && $(".jsgrid-load-panel").is(":visible")) {
            $(".jsgrid-load-shader").css("display:none");
            $(".jsgrid-load-panel").css("display:none");
        }
    }, 2000);
});


function preventNumberInput(e) {
    var keyCode = (e.keyCode ? e.keyCode : e.which);
    if (keyCode > 47 && keyCode < 58 || keyCode > 95 && keyCode < 107) {
        e.preventDefault();
    }
}

$('#fechaEntregaString').keypress(function (e) {
    return false;
});

$('#FechaInicioString').keypress(function (e) {
    return false;
});


$('#FechaFinString').keypress(function (e) {
    return false;
});

$('#btnAdvanceSearchFondo').click(function () {
    reloadGridFondos();
});


$('.PaymentMethod').change(function () {
    if (frmFondo[0].id.value == "") {
        $(frmFondo[0].fechaEntregaString).val("");
        $('#datetimepicker1').datetimepicker('destroy');
        $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY', useCurrent: false });
    }
});

// Calcular fecha fin
$(".getfondoFechaFin").change(function (e) {
    var _that = this;
    if (_that.value != "") {
        getDateFinal();
    }
});

function calculateBudget(date) {
    var pm = frmFondos[0].form.PaymentMethod.value;
    var htl = frmFondos[0].form.Hotel.value;

    $.ajax({
        method: "POST",
        url: app.fondosfinalState,
        datatype: 'json',
        data: { idPaymentMethod: pm, startDate: '', endDate: date },

    }).done(function (data, textStatus, jqXHR) {
        if (data.success === true) {
            financialstate = data.data;
            printMaxbalance(financialstate.balance, financialstate.hasMaxLimit, financialstate.maxBalance, date);
        } else {
            $("#formItemInfo2").notify(data.message, {
                position: "right top",
                className: "warn",
                hideDuration: 500
            });
            printMaxbalanceClean();
        }

    })
        .fail(function (jqXHR, textStatus, errorThrown) {
            printMaxbalanceClean();
        });
}
function printMaxbalance(balance, hasMaxBalance, MaxBalance, date) {
    var totalmax = "0";

    if (hasMaxBalance == true) {

        $("#balance").text(covertToNatural(balance));
        $("#maxBalance").text(covertToNatural(MaxBalance));
        $("#fechaSelect").text(date);
        frmFondos[0].form.MontoCargo.value = MaxBalance;

    } else {
        $("#balance").text("0.00");
        $("#maxBalance").text("0.00");
        $("#fechaSelect").text(date);
        notifyMessageGral("No cuenta con un límite para calcular el máximo a otorgar", "info", null, "#formItemInfo2");
    }

}
function printMaxbalanceClean(balance, MaxBalance) {
    $("#balance").text(covertToNatural(0));
    $("#maxBalance").text(covertToNatural(0));
    $("#fechaSelect").text("");
}

//var _defaultDate = formatDate(new Date());
//$('#datetimepicker1').datetimepicker({
//    format: 'DD/MM/YYYY'
//    , locale: moment.updateLocale('es-us')
//    , useCurrent: false
//});

$('#datetimepicker1').on('change.datetimepicker', function (ev) {
    calculateBudget($(this).find("#fechaEntregaString").first().val());
});
//$('#fechaEntregaString').on('change', function (ev) {
//    calculateBudget($(this).find("#fechaEntregaString").first().val());
//});

function getDateFinal() {
    $.ajax({
        method: "POST",
        url: app.fondosDateFinalDate,
        datatype: 'json',
        data: frmFondo.serialize(),

    }).done(function (data, textStatus, jqXHR) {
        if (data.success === true) {

            frmFondo.notify("Se ha calculado la fecha de finalización.", {
                position: "top right",
                className: "success",
                hideDuration: 500
            });

            $(frmFondo[0].FechaInicioString).val(data.data.FechaInicioString);
            $(frmFondo[0].FechaFinString).val(data.data.FechaFinString);
            // reloadGridFondos();
        } else {
            frmFondo.notify("No se puede calcular la fecha de finalización. " + data.message, {
                position: "top right",
                className: "error",
                hideDuration: 2000
            });
        }
    })
        .fail(function (jqXHR, textStatus, errorThrown) {
            frmFondo.notify("No se puede calcular la fecha de finalización. " + jqXHR.message, {
                position: "top right",
                className: "error",
                hideDuration: 500
            });
        });
}

// Reload GRid
function reloadGridFondos() {
    $("#jsGridFondos").jsGrid("loadData");
}

// Submit form
$('#btnCreaFondo').click(function (event) {
    $(this).prop("disabled", true);
    var url = "";
    validarFormFondos();
    if (frmFondo[0].id.value != "") {
        url = app.fondosUpdate
    } else {
        url = app.fondosUp
    }

    if (frmFondo.valid()) {

        if (financialstate.hasMaxLimit == true) {

            var _tempVal = (Number(frmFondo[0].MontoCargo.value) + Number(frmFondo[0].FondoFee.value));
            if (_tempVal <= 0 || isNaN(parseInt(_tempVal))) {
                frmFondo.notify("Debe ingresar un monto diferente de cero.", {
                    position: "top right", className: "info", hideDuration: 800
                });
                return;
            }
            if (_tempVal > financialstate.maxBalance || isNaN(parseInt(_tempVal))) {
                frmFondo.notify("La suma de monto mas comisiones debe ser inferior  a $ " + financialstate.maxBalance + ".", {
                    position: "top right", className: "info", hideDuration: 800
                });

                return;
            }
        }

        $.ajax({
            method: "POST",
            url: url,
            datatype: 'json',
            data: frmFondo.serialize(),
        }).done(function (data, textStatus, jqXHR) {
            if (data.success === true) {
                // $('#Invoice').val(data.data.Invoice);

                frmFondo.notify("Se ha añadido correctamente la información. Folio :" + data.data.id, {
                    position: "top right",
                    className: "success",
                    hideDuration: 3500
                });
                //$(frmFondo[0].id).val(data.data.id);
                ResetForm();
                reloadGridFondos();

            } else {
                frmFondo.notify("No se puede guardar la inforación. " + data.message, {
                    position: "top right",
                    className: "error",
                    hideDuration: 2000
                });
            }
            //reloadGrid();
        })
            .fail(function (jqXHR, textStatus, errorThrown) {
                frmFondo.notify("No se puede guardar la información.", {
                    position: "top right",
                    className: "error",
                    hideDuration: 500
                });
                //reloadGrid();
            });
    }
    $(this).prop("disabled", false);

    //else { console.log("No se puede guardar"); }

});

// Validating Form Payments
function validarFormFondos() {
    $("form[name='frmFondos']").validate({
        rules: {
            PaymentMethod: {
                required: true,
                notEqualTo: 0
            },
            FinancialMethod: {
                required: true,
                notEqualTo: 0
            },
            Company: {
                required: true,
                notEqualTo: 0
            },
            Currency: {
                required: true,
                notEqualTo: 0
            },
            fechaEntregaString: {
                required: true

            },
            FechaInicioString: {
                required: true
            },
            FechaFinString: {
                required: true
            }

        },
        messages: {
            PaymentMethod: "Seleccione un destino de recursos.",
            FinancialMethod: "Seleccione origen de origen.",
            Currency: "Seleccione la moneda de pago.",
            fechaEntregaString: "Seleccione una fecha",
            FechaInicioString: "Seleccione una fecha",
            FechaFinString: "Seleccione la fecha de inicio.",
            Company: "Seleccione una empresa.",
            MontoCargo: "Capture un monto o un valor diferente de cero."
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("help-block");
            element.parents(".reqs").addClass("has-feedback");
            if (element.prop("type") === "checkbox") {
                error.insertAfter(element.parent("label"));
            } else {
                error.insertAfter(element);
            }
            if (!element.next("span")[0]) {
                $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
            }
        },
        success: function (label, element) {
            if (!$(element).next("span")[0]) {
                $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).parents(".reqs").addClass("has-error").removeClass("has-success");
            $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents(".reqs").addClass("has-success").removeClass("has-error");
            $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
        },

        submitHandler: function (form) { }
    });

    $.validator.addMethod(
        "notEqualTo",
        function (elementValue, element, param) {
            return elementValue != param;
        },
        "El valor no puede ser {0}"
    );

}

$(".closeModal").click('click', function () {
    $("#tblpaymentsDetails").remove();
});

// Add Fondo Modal button
$("#btnAddModal").click(function () {
    openModal();
});

// close Fondo Modal button
$("#btnCloseFondo").click(function () {
    closeModal();
    resetDatapickes();
    frmFondo[0].reset();
    $(frmFondo[0].id).val("");
    printMaxbalanceClean();
});

function ResetForm() {
    resetDatapickes();
    frmFondo[0].reset();
    $(frmFondo[0].id).val("");
    printMaxbalanceClean();
};

// Open Modal
function openModal() {
    $("#myModal").modal({
        backdrop: "static"
    });
}

// Close modal
function closeModal() {
    $('#myModal').modal('hide');
}

function deleteFondo(value) {
    $("#delete-modal").modal('show');
    var datos = $("#jsGridFondos").jsGrid("option", "data");
    var item;
    $.each(datos, function (v,x) {
        if (value == x.id) {
            item = x
        }
    })

    var data = item.FinanceType != null ? "Company: " + item.CompanyName + " | Tipo: " + item.PaymentMethodName + " | Financiamiento por: " + item.MontoCargoString + ""
        : "Hotel: " + item.CompanyName + " | Tipo: " + item.PaymentMethodName + " | Cargo: " + item.MontoCargoString + "";
    $("#lblDataErase").text(data);

    modalConfirm(function (confirm) {
        if (confirm) {
            //Acciones si el usuario confirma
            //alert("CONFIRMADO");
            $.ajax({
                method: "POST",
                url: app.fondosDelete,
                datatype: 'json',
                data: item,
            }).done(function (data, textStatus, jqXHR) {
                if (data.success == true) {
                    // $('#Invoice').val(data.data.Invoice);

                    $("#formTargetDiv").notify("Se ha eliminado correctamente", {
                        position: "bottom right",
                        className: "success",
                        hideDuration: 500
                    });

                    reloadGridFondos();
                    $("#delete-modal").modal('hide');
                } else {
                    $("#lblDataErase").notify("No se puede realizar la acción. " + data.message, {
                        position: "bottom right",
                        className: "error",
                        hideDuration: 2000
                    });
                }
            })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $("#lblDataErase").notify("No se puede realizar acción.", {
                        position: "bottom right",
                        className: "error",
                        hideDuration: 500
                    });
                });
        } else {
            //Acciones si el usuario no confirma
            //alert("NO CONFIRMADO");
        }
    });
}

var modalConfirm = function (callback) {

    $("#modal-btn-si").on("click", function () {
        callback(true);
    });

    $("#modal-btn-no").on("click", function () {
        callback(false);
        $("#delete-modal").modal('hide');
    });
};

$('.xls').remove();



function generateButtonsExport() {
    // $('#btnExportExpenses').click(function () {
    /* $("#tblExpensesDetail").excelexportjs({
        containerid: "tblExpensesDetail",
        datatype: 'table',
        worksheetName: $("#month option:selected").text() + "-" + $("#year option:selected").text()
    });
    */
    // $('.xls').remove();
    /* $('#tblpaymentsDetails').tableExport({
        bootstrap: true,
        formats: ['xlsx'],
        filename: $('.caption-custom').html()
    });
    */
    // });
};

$('#btnPrintDetails').click(function () {
    $("#tblpaymentsDetails").excelexportjs({
        containerid: "tblpaymentsDetails",
        datatype: 'table'
    });

});

$('#btnPrintAdvanceSearchFondo').click(function () {

    var data = $("#jsGridFondos").jsGrid("option", "data");
    //  gridData.push(item);

    $("#jsGridFondos").excelexportjs({
        datatype: 'json',
        dataset: data,
        columns: getColumns(data)
    });

});

function fondosgrid() {

    $("#jsGridFondos").jsGrid({
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
            /*
            $("#paymentsDetails").modal({
                backdrop: false
            });
            var header = "<table class='' id='tblpaymentsDetails' name='tblpaymentsDetails'> <caption class='caption-custom'> " + args.item.HotelName + " - [" + args.item.PaymentMethodName + " | " + args.item.FechaInicioString + " - " + args.item.FechaFinString + "]" + " </caption> <thead> <tr> <th style='padding: 1px 15px 1px 15px;'> # Gasto </th> <th style='padding: 1px 15px 1px 15px;'> Método de Pago </th> <th style='padding: 1px 15px 1px 15px; text-align: center;'> Pago </th> <th style='padding: 1px 15px 1px 15px;'> Descripción </th> </tr> </thead> <tbody>";
            var data = "";
            var footer = "</tbody> </table>";
            $.each(args.item.docpaymt, function (index, value) {
                data = data + "<tr>" + "<td style='padding: 1px 15px 1px 15px;'>" + value.InvoiceIdentifier + "</td>" + "<td style='padding: 1px 15px 1px 15px;'>" + value.PaymentMethodName + "</td>" + "<td style='padding: 1px 15px 1px 15px; text-align: right;'> $ " + value.chargedAmountString + "</td>" + "<td style='padding: 1px 15px 1px 15px;'>" + value.authRef + "</td>" + "<tr>"
            });
            var result = header + data + footer;
            $('#modalPaymentsInformation').append(result);
            setTimeout(function () { generateButtonsExport(); }, 3000);
            */
        },
        controller: {
            loadData: function () {
                var d = $.Deferred();

                $.ajax({
                    url: app.fondosurl,
                    dataType: "json",
                    data: $('#frmAvanzadaFondos').serializeArray()
                }).done(function (response) {
                    d.resolve(response.data);
                });

                return d.promise();
            }
        },
        /* rowClass: function (item) {
            return item.statusExpenses == 2 ? 'highlight-green' : item.statusExpenses == 3 ? 'highlight-red' : ""
        },
        */
        fields: [{
            title: "#",
            name: "id",
            type: "text",
            width: 30,
            validate: "required",
            visible: true
        },
        {
            title: "Empresa",
            name: "CompanyName",
            type: "text",
            width: 150
        },
        {
            title: "Cuenta Origen",
            name: "FinancialMethodName",
            type: "text",
            align: "center",
            width: 200
        },
        {
            title: "Cuenta Destino",
            name: "PaymentMethodName",
            type: "text",
            align: "center",
            width: 200
        },
        {
            title: "Moneda",
            name: "CurrencyName",
            type: "text",
            width: 100,
            visible: false
        },
        {
            title: "Autorizado",
            name: "MontoCargoString",
            type: "text",
            width: 100,
            // sorting: true,
            itemTemplate: function (value) {
                return " $ " + value;
            }
        },
        {
            title: "Pagos",
            name: "MontoAbonosString",
            type: "text",
            width: 100,
            visible: false,
            itemTemplate: function (value) {
                return " $ " + value;
            }
        },
        {
            title: "Saldo",
            name: "SaldoString",
            type: "text",
            width: 100,
            align: "right",
            visible: false,
            //cellRenderer: function (value, item) { return $(this).addClass("jsgrid-td-background"); },
            itemTemplate: function (value) {
                //return "<span class='highlight-green'>$ " + value + "</span>";
                var r = ""; //return " $ " + value;
                r = value == 0 ? r = "<span class=''> $ " + value + "</span>" : value < 0 ? r = "<span class='highlight-red'> $ " + value + "</span>" : r = "<span class='highlight-green'> $ " + value + "</span>";
                return r;
            }
        }, {
            title: "Fecha Captura",
            name: "fechaCapturaString",
            type: "text",
            align: "center",
            width: 100
        },
        {
            title: "Fecha Entrega",
            name: "fechaEntregaString",
            type: "text",
            align: "center",
            width: 100
        },
        {
            title: "Fecha Inicio",
            name: "FechaInicioString",
            type: "text",
            align: "center",
            width: 100
        },
        {
            title: "Fecha Fin",
            name: "FechaFinString",
            type: "text",
            align: "center",
            width: 100
        },
        {
            title: "Descripción",
            name: "comments",
            type: "text",
            //cellRenderer: function (value, item) { return $("<div>").addClass("jsgrid-td-background"); },
            width: 180
        },
        {
            title: "",
            name: "id",
            type: "text",
            width: 24,
            itemTemplate: function (value, item) {
                if (item.editable == true) {
                    fondo = item;
                    return $("<div>").addClass("rating").append("<i class='fa fa-edit fa-lg' data-toggle='tooltip' title='Editar' data-original-title='Editar' onclick='filltoedit(" + value + ")'></i>");
                }
            }
        },
        {
            title: "",
            name: "id",
            type: "text",
            width: 60,
            itemTemplate: function (value, item) {
                if (item.editable == true) {
                    return $("<div>").addClass("rating").append("<i class='fa fa-trash fa-lg ' data-toggle='tooltip' title='Eliminar' data-original-title='Eliminar' onclick='deleteFondo(" + value + ")'></i>");
                }
            }
        }
            /*,
                        {
                            type: "control"
                        }*/
        ],
        onDataLoaded: function (grid, data) {
            /*$('[data-toggle="tooltip"]').tooltip()*/;
        }
    });
}

function filltoedit(idFondo) {

    var jsPromiseGetFondo;

    jsPromiseGetFondo = Promise.resolve(PrepareRequestGetFondo(idFondo));
    jsPromiseGetFondo.then(function (response) {

        CompanyDependecyFinancialEdit = true;
        CompanyDependecyPaymentEdit = true;

        item = response.data[0];

        openModal();
        
        frmFondo.find("input[name=id]").val(item.id);
        loadDateTimeLoad('datetimepicker1', item.fechaEntregaString)
        loadDateTimeLoad('datetimepicker2', item.FechaInicioString)
        loadDateTimeLoad('datetimepicker3', item.FechaFinString)
        frmFondo.find("input[name=fechaEntregaString]").val(item.fechaEntregaString);
        frmFondo.find("input[name=FechaInicioString]").val(item.FechaInicioString);
        frmFondo.find("input[name=FechaFinString]").val(item.FechaFinString);
        frmFondo.find("input[name=FondoFee]").val(item.FondoFee);
        frmFondo.find("input[name=MontoCargo]").val(item.MontoCargo);
        frmFondo.find("textarea[name=comments]").val(item.comments);
        //origen
        //frmFondo.find("select[class=CompanyDependecyFinancial]").val(item.CompanyFinancial)
        //frmFondo.find("select[name=FinancialMethod]").val(item.FinancialMethod)
        //destino
        //frmFondo.find("select[class=CompanyDependecyPayment]").val(item.Company)
        //frmFondo.find("select[name=PaymentMethod]").val(item.PaymentMethod)

        //origen
        loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType($("#frmFondos")[0].FinanceType.value, formControl.BankAccountByCompanyGive), item.CompanyFinancial, "frmFondos", "CompanyDependecyFinancial", null);
        loadDataNotDependency(PrepareRequestGetBankAccountbyCompanyUserBanAccount(item.CompanyFinancial, formControl.BankAccountByCompany), item.FinancialMethod, "frmFondos", "FinancialMethod");
        //destino
        loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType($("#frmFondos")[0].FinanceType.value, formControl.BankAccountByCompanyReceive), item.Company, "frmFondos", "CompanyDependecyPayment", null);
        loadDataNotDependency(PrepareRequestGetBankAccountbyCompanyUserBanAccount(item.Company, formControl.BankAccountByCompany), item.PaymentMethod, "frmFondos", "PaymentMethod");

    }).catch(function (error) {
        console.log("Failed Request! jsPromiseGetFondo ->", error);
        console.error("No se puede procesar la solicitud jsPromiseGetFondo. --> ")
    });
}


function PrepareRequestGetFondo(idFondo) {

    getFondoRequest = $.ajax({
        method: "GET"
        , url: app.fondosurl + "?id=" + idFondo + "&Type=" + $("#frmFondos")[0].FinanceType.value + ""
        // , data:  myJSON 
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getFondoRequest;
}


$('.CompanyFrmAvanzado').on('change', function () {

    // getBankAccountbyCompanies(this.value, this.form.name, "PaymentMethod", null);
    loadDataNotDependency(PrepareRequestGetBankAccountbyCompanyUserBanAccount(this.value, formControl.BankAccountByCompany), null, this.form.name, "PaymentMethod");
})

