var frmFondoLimit = $('#frmLimitBudget');

$(document).ready(function () {
    loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType(null, formControl.BankAccountByCompanyGive), null, "frmAvanzadaFondosLimit", "Company")
    loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType(null, formControl.BankAccountByCompanyGive), null, "frmLimitBudget", "Company")

    //getCompaniesbyUserBankAccount(null, "frmAvanzadaFondosLimit", "Company");
    //getCompaniesbyUserBankAccount(null, "frmLimitBudget", "Company");


    // loadPayMethods();
    //loadCurrency();
    loadDataNotDependency(PrepareRequestGetCurrencies(formControl.Currencies), null, "frmAvanzadaFondosLimit", "Currency");
    $("._money").ForceNumericOnly();
    validateFormLimit();
    fondosLimitGrid();

    setTimeout(function () {
        if ($(".jsgrid-load-shader").is(":visible") && $(".jsgrid-load-panel").is(":visible")) {
            $(".jsgrid-load-shader").css("display:none");
            $(".jsgrid-load-panel").css("display:none");
        }
    }, 2000);

});

/***************** MODAL FUNCTIONS *************************/

// Add Fondo Modal button
$("#btnAddModalLimite").click(function () {
    openModalLimit();
});

// Open Modal
function openModalLimit() {
    $("#modalLimitBudget").modal({
        backdrop: "static"
    });
}

// close Fondo Modal button
$("#btnCloseLimitBudget").click(function () {
    closeModalLimitBudget();
});

// Close modal
function closeModalLimitBudget() {
    $('#modalLimitBudget').modal('hide');
}

/************** FUNCTIOND APP ****************************/
appMAx = {};
appMAx.getData = "/Budget/LimitbudgetGet";
appMAx.postData = "/Budget/LimitbudgetSet";
appMAx.updateData = "/Budget/LimitbudgetUpdate";
appMAx.delete = "/Budget/LimitbudgetDelete";

var frmLimit = $('#frmLimitBudget');
var frmLimitSeacrh = $('#frmAvanzadaFondosLimit');
var urlAction = "";
var limitDataSend = {};

//var gridFondosLimit = $("#jsGridFondosLimit");

$('#btnCreteLimitFondo').click(function () {
    saveLimit();
});

$('#btnAdvanceSearchFondo').click(function () {
    reloadGridFondosLimit();
});

function validateFormLimit() {
    frmLimit.validate({
        rules: {
            idPaymentMethod: {
                required: true,
                notEqualTo: 0
            },
            fondosmaxAmmount: {
                required: true
            }
        },
        messages: {
            idPaymentMethod: "Seleccione un método de pago.",
            fondosmaxAmmount: "Introduzca un monto límite."
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

        submitHandler: function (form) {
        }
    });

    $.validator.addMethod(
        "notEqualTo",
        function (elementValue, element, param) {
            return elementValue != param;
        },
        "Value cannot be {0}"
    );
}

function saveLimit() {

    if (frmLimit.valid()) {
        if ($(frmLimit[0].FondosMax).val() == "" || $(frmLimit[0].FondosMax).val() == null) {

            urlAction = appMAx.postData;
        }
        else {
            urlAction = appMAx.updateData
        }
        $.ajax({
            method: "POST"
            , url: urlAction
            , data: frmLimit.serialize()
            , datatype: 'json'
        })
            .done(function (data, textStatus, jqXHR) {
                if (data.success == true) {
                    // frmInvoice = data.data;
                    populateForm(frmLimit, data.data);

                    frmLimit.notify("Se ha añadido correctamente la información :" + data.message, {
                        position: "top right",
                        className: "success",
                        hideDuration: 3500
                    });
                }
                else {
                    notifyMessageGral(data.message, 'error', 500, "#frmLimitBudget");
                }
                reloadGridFondosLimit();
                CompanyDependecyPaymentEdit = false;

            })
            .fail(function (jqXHR, textStatus, errorThrown) {

                notifyMessageGral(jqXHR.message, 'error', 500, "#frmLimitBudget");
            });
    }
}

function reloadGridFondosLimit() {
    $("#jsGridFondosLimit").jsGrid("loadData");
}

function fondosLimitGrid() {

    $("#jsGridFondosLimit").jsGrid({
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
                    url: appMAx.getData,
                    dataType: "json",
                    data: $('#frmAvanzadaFondosLimit').serialize()
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
            name: "FondosMax",
            type: "text",
            width: 30,
            validate: "required"
        },
        {
            title: "Empresa",
            name: "CompanyName",
            type: "text",
            width: 100
        },
        {
            title: "Cuenta",
            name: "PaymentMethodName",
            type: "text",
            align: "center",
            width: 100
        },
        {
            title: "Moneda",
            name: "CurrencyName",
            type: "text",
            width: 100
        },
        {
            title: "Lìmite",
            name: "fondosmaxAmmount",
            type: "number",
            width: 100,
            // sorting: true,
            itemTemplate: function (value) {
                return " $ " + value;
            }
        },
        {
            title: "Comentario",
            name: "fondosmaxDescription",
            type: "number",
            align: "left",
            width: 300
        },
        {
            title: "",
            name: "id",
            type: "text",
            width: 24,
            itemTemplate: function (value, item) {
                if (item.editable == true) {
                    return $("<div>").addClass("rating").append("<i class='fa fa-edit fa-lg' data-toggle='tooltip' title='Editar' data-original-title='Editar' onclick='filltoedit(" + item.FondosMax + ")'></i>");
                }

                }            
        },
        {
            title: "",
            name: "id",
            type: "text",
            width: 24,
            itemTemplate: function (value, item) {
                if (item.editable == true) {
                    return $("<div>").addClass("rating").append("<i class='fa fa-trash fa-lg ' data-toggle='tooltip' title='Eliminar' data-original-title='Eliminar' onclick='deleteFondoLimit(" + item.FondosMax + ")'></i>");
                }
                //return $("<i>").addClass("fa fa-trash fa-lg").attr("data-toggle", "tooltip").attr("title", "Eliminar").on("click", function () {
                //    openModalDelete();
                //    deleteFondoLimit(item);
                //});
            }
        }
        ],
        onDataLoaded: function (grid, data) {
            //$('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function filltoedit(idFondoMax) {
    var jsPromiseGetFondo;

    jsPromiseGetFondo = Promise.resolve(PrepareRequestGetFondoMax(idFondoMax));
    jsPromiseGetFondo.then(function (response) {
        item = response.data[0];
        openModalLimit();
        // populateForm(frmLimit, item);
        CompanyDependecyPaymentEdit = true;

        frmFondoLimit.find("input[name=FondosMax]").val(item.FondosMax);
        frmFondoLimit.find("input[name=fondosmaxAmmount]").val(item.fondosmaxAmmount);
        frmFondoLimit.find("textarea[name=fondosmaxDescription]").val(item.fondosmaxDescription);

        loadDataNotDependency(PrepareRequestGetCompaniesFinanceByType(null, formControl.BankAccountByCompanyGive), item.Company, "frmLimitBudget", "Company")
        loadDataNotDependency(PrepareRequestGetBankAccountbyCompanyUserBanAccount(item.Company, formControl.BankAccountByCompany), item.PaymentMethod, "frmLimitBudget", "PaymentMethod")

        //getCompaniesbyUserBankAccount(item.Company, "frmLimitBudget", "Company");
        //getBankAccountbyCompanyUserBaAccount(item.Company, "frmLimitBudget", "PaymentMethod", item.PaymentMethod);

        $("id").val(item.id);
    }).catch(function (error) {
        console.log("Failed Request! jsPromiseGetFondo ->", error);
        console.error("No se puede procesar la solicitud jsPromiseGetFondo. --> ")
    });

}

function PrepareRequestGetFondoMax(idFondo) {
    var fondomax =  "FondosMax="+ idFondo ;
    getFondoRequest = $.ajax({
         url: appMAx.getData 
        , data: fondomax
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getFondoRequest;
}

/******** DELETE ***********/
function openModalDelete() {
    $("#delete-modal").modal('show');
}

function closeModalDelete() { }

function deleteFondoLimit(value) {
    openModalDelete();
    var datos = $("#jsGridFondosLimit").jsGrid("option", "data");
    var item;
    $.each(datos, function (v, x) {
        if (value == x.FondosMax) {
            item = x
        }
    })
    var data = "Unidad de negocio: " + item.HotelName + " | Tipo: " + item.PaymentMethodName + " | Lìmite: " + item.fondosmaxAmmount + "";

    $("#lblDataErase").text(data);

    modalConfirm(function (confirm) {
        if (confirm) {
            //Acciones si el usuario confirma
            //alert("CONFIRMADO");
            $.ajax({
                method: "GET",
                url: appMAx.delete,
                datatype: 'json',
                data: { id: item.FondosMax },
            }).done(function (data, textStatus, jqXHR) {
                if (data.success === true) {
                    notifyMessageGral("Se ha eliminado correctamente", 'success', 500)
                    reloadGridFondosLimit();
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