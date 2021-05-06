var needtpvdata = false;
$(document).ready(function () {
    loadDataNotDependency(PrepareRequestGetAnyData(Components.companies), null, 'incomeForm', 'segment', true);
    loadDateTimePermissionIncomeInvoice(PrepareRequestGetDatePermissionsIncomeInvoice(), "datetimepicker1", new Date());
    loadDataNotDependency(PrepareRequestGetAnyData(Components.suppliers), null, "incomeForm", "supplier", true);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.bugetypes), null, "incomeForm", "budgetType", true);
    incomeRegisterEdit = false;
    incomeitemEdit = false;
    $('.modal-dialog').draggable({ handle: ".modal-header" });

    AttachmentsTypes()
});

$(document).click(function () {
    if (incomeObject.item != 0) {
        $("#btnUploadFile").attr("disabled", false);
        $("#btnSaveComments").attr("disabled", false);
    }
});

$(".forcenumericonly").ForceNumericOnly();

$("input.authcode").bind('keypress', function (event) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

/********************************************************************************************************************/
/********************************************************************************************************************/
/******************************************** I N C O M E **********************************************************/
/********************************************************************************************************************/
/********************************************************************************************************************/

$('.segment').on('change', function (e) {
    $('.accountl1').find('option').remove().end(); $('.accountl2').find('option').remove().end(); $('.accountl3').find('option').remove().end(); $('.accountl4').find('option').remove().end();
    loadDataNotDependency(PrepareRequestGetCompaniesBySegment(this.value), null, "incomeForm", "company", true);
    loadDataNotDependency(PrepareRequestGetAccl1(this.value, 1), null, "incomeitemForm", "accountl1", true);
});

$('.company').on('change', function (e) {
    loadDataNotDependency(PrepareRequestGetCurrenciesbyCompanies(this.value), null, "incomeForm", "currency", true);
});

$('.currency').on('change', function (e) {
    var currency = this.value;
    if (currency > 0) { loadDepenceSelect($('.BAClass'), currency, Components.bankclasific); }
    else { $('.BAClass').empty(); }
    validateincomemovementForm();
});

$('.BAClass').on("change", function (event) {
    loadAccountTypeProductbyBAClass(null, $("#currency").val(), this.value, 'incomeMovementsForm', 'bankaccnttype');
});

$('.bankaccnttype').on("change", function (event) {
    var name = '#' + this.form.name + '';
    var form = $(name);
    var baclass = form.find("select[name=BAClass]").find(":selected").val();
    _value = this.value;

    if (_value == 2 || _value == 3) {
        $('.tpv,.ccard,.authcode').show();
        loadDataNotDependency(PrepareRequestGetBankAccbyClas($("#currency").val(), baclass, this.value, $("#company").val()), null, "incomeMovementsForm", "bankaccount", true)
        //loadBAccountbyAccClass(null, $("#currency").val(), baclass, this.value, "incomeMovementsForm", "bankaccount", true);
        needtpvdata = true;
    }
    else {
        needtpvdata = false;
        loadDataNotDependency(PrepareRequestGetBankAccbyClas($("#currency").val(), baclass, this.value, $("#company").val()), null, "incomeMovementsForm", "bankaccount", null)
        //loadBAccountbyAccClass(null, $("#currency").val(), baclass, this.value, "incomeMovementsForm", "bankaccount", null);
        $('.tpv,.ccard,.authcode').hide();
        $('.tpv').find('option').remove().end();
        $('.ccard').val("");
        $('.authcode').val("");
    }
});

$('.bankaccount').on("change", function (event) {
    loadDataNotDependency(PrepareRequestGetTpvbyBAccount("/Formcontrol/getTpvUserBankAccount", this.value), null, "incomeMovementsForm", "tpv", null)
    //loadTpvUserBAccount(null, this.value, "incomeMovementsForm", "tpv", null);
})

function GetAccountByCurrencyProfile(currency, BankAccntType) {
    $.ajax({
        method: "GET",
        url: Components.accountmoney,
        data: { idCurrency: currency, idBankAccntType: BankAccntType },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            $('.bankaccount').find('option').remove().end();
            $.each(data.data, function (key, value) {
                $('.bankaccount').append($("<option></option>").attr("value", value.value).text(value.text));
            });
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $("#popup2").notify("No se puede obtener la cuenta.",
                {
                    position: "bottom right",
                    className: "error",
                    hideDuration: 500
                });
        });
}

function PrepareRequestGetBankAccbyClas(currency, baclass, type, company) {

    getBAccountClassRequest = $.ajax({
        method: "GET"
        , url: Components.bankcurrency
        , data: { idCurrency: currency, idClasification: baclass, idBankAccntType: type, company: company}
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getBAccountClassRequest;
}

function PrepareRequestSaveIncomeMovement(item) {
    var Request = $.ajax({
        method: "POST",
        url: incomeApp.saveincomemovitem,
        datatype: 'json',
        data: { item: item },
    })
        .done(function (response, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return Request;
}

/************************************* Preparing Income Save Request  ******************************/
function PrepareRequestSaveIncome() {
    var urlAction; var incomeDataSend = {};
    if (incomeObject.item == undefined) { urlAction = incomeApp.saveincome; }
    else { urlAction = incomeApp.updateincome; }

    $.each(frmIncome.serializeArray(), function (index, value) {
        incomeDataSend[value.name] = value.value;
    });

    if (incomeObject.item != 0)
        incomeDataSend.item = incomeObject.item

    var saveDocumentRequest = $.ajax({
        method: "POST",
        url: urlAction,
        datatype: 'json',
        data: { item: incomeDataSend },
    })
        .done(function (response, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return saveDocumentRequest;
}

/*********************** Saving document Only *****************************/
function saveIncomeOnly() {
    // Guardar DATO GENERAL 
    if (!$("#incomeForm").valid()) {
        notifyMessageGral("Debe capturar la información general - unidad de negocio, fecha, moneda", "warn", 2000, '.info');
        return;
    }
    else {
        OpenModalProcessing(".processingModal");
        var jsPromiseSaveIncome = Promise.resolve(PrepareRequestSaveIncome());
        OpenModalProcessing(".processingModal");
        jsPromiseSaveIncome.then(function (response) {
            notifyMessageGral("Se ha añadido correctamente solo la información general de gastos.", 'success', 1500, '.info');
            incomeObject = response.data;
            $(".incomeIdentifier").text(incomeObject.identifier);
            // populateToEdit("#incomeForm", incomeObject);
            frmIncome.find("input[name=item]").val(incomeObject.item);
        })
            .catch(function (error) {
                console.log("Failed Request! -> Fallo al realizar la acción de guardado/edición con boton.", error);
                notifyMessageGral("No se puede agregar el ingreso general.", 'error', 1500, '.info');
            })
            .finally(function () {
                ForceCloseModal(".processingModal");
            });
    }
}

function prepareEdit(formName, income) {
    OpenModalProcessing(".processingModal");
    populateToEdit(formName, income);
    setTimeout(function () {
        adapterInvoice()
        ForceCloseModal(".processingModal");
    }, 2000);
}

function adapterInvoice() {
    $("input[name=applicationdatestring]").val(incomeObject.applicationdatestring);
}

function populateToEdit(formName, income) {
    var form = $(formName);
    form.find("input[name=item]").val(income.item);
    $('.incomeIdentifier').text(income.identifier);
    loadDataNotDependency(PrepareRequestGetAnyData(Components.companies), income.segment, 'incomeForm', 'segment', null);
    loadDataNotDependency(PrepareRequestGetCompaniesBySegment(income.segment), income.company, "incomeForm", "company", null);
    loadDataNotDependency(PrepareRequestGetAccl1(income.segment, 1), income.accountl1, "incomeitemForm", "accountl1", null);
    loadDataNotDependency(PrepareRequestGetCurrenciesbyCompanies(income.company), income.currency, "incomeForm", "currency", null);
    loadDateTimePermissionIncomeInvoice(PrepareRequestGetDatePermissionsIncomeInvoice(), "datetimepicker1", new Date());
    printJSGridIncomeItem(income.item)
    incomeitemEdit = true;
    EditJSGriIncomeMovements(income.item)

    //AttachmentsTypes();
    getAttachments(income.item, incomeApp.getincomeattach)
    getComments(income.item, incomeApp.getcommentinc)
}

function populateMovementToEdit() {
    var currency = $('.currency').val();
    loadDepenceSelect($('.BAClass'), currency, Components.bankclasific);
}

function incomeObjectSaved() {
    if (incomeObject.item != undefined && incomeObject.item != 0)
        return true
    else return false
}

/************************************* Preparing Income Delete Request  ******************************/
function PrepareRequestDeleteIncome(id) {
    var Request = $.ajax({
        method: "POST",
        url: incomeApp.deleteincome,
        datatype: 'json',
        data: { id: id },
    })
        .done(function (response, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return Request;
}

/*********************** Saving document Only *****************************/
function deleteIncomeOnly(index) {
    var _income = incomesObjectsList[index]; //   identifier
    var text = '¿Esta seguro que desea eliminar el ingreso ' + _income.identifier + ' ?';
    $.confirm({
        title: 'Eliminación de ingreso',
        content: text,
        theme: 'material',
        type: 'orange',
        buttons: {
            si: function () {
                OpenModalProcessing(".processingModal");
                var jsPromiseSaveIncome = Promise.resolve(PrepareRequestDeleteIncome(_income.item));
                jsPromiseSaveIncome.then(function (response) {
                    notifyMessageGral("Se ha eliminado correctamente el ingreso.", 'success', 500, '.info');
                    printgridIncomes();
                })
                    .catch(function (error) {
                        console.log("Failed Request! -> error al borrar registro income.", error);
                        notifyMessageGral("No se puede eliminar el ingreso ingreso.", 'error', 500, '.info');
                    })
                    .finally(function () {
                        ForceCloseModal(".processingModal");
                    });
            },
            no: function () {
                result = false;
            }
        }
    });
}
var jsPromiseOperationIncomeItem;
/********************************************************************************************************************/
/********************************************************************************************************************/
/******************************************** I N C O M E   I T E M S  **********************************************/
/********************************************************************************************************************/
/********************************************************************************************************************/

$('.accountl1').on('change', function (e) {
    $('.accountl2,.accountl3,.accountl3').find('option').remove().end();
    loadDataNotDependency(PrepareRequestGetAccl2(this.value, $("#segment").val(), 1), null, "incomeitemForm", "accountl2", true);
});

$('.accountl2').on('change', function (e) {
    $('.accountl3,.accountl4').find('option').remove().end();
    loadDataNotDependency(PrepareRequestGetAccl3(this.value, $("#segment").val(), 1), null, "incomeitemForm", "accountl3", true);
});

$('.accountl3').on('change', function (e) {
    $('.accountl4').find('option').remove().end();
    loadDataNotDependency(PrepareRequestGetAccl4(this.value, $("#segment").val(), 1), null, "incomeitemForm", "accountl4", true);
});

// Adding Income/IncomeItem
$('.btnAddIncomeItem').click(function () {
    event.preventDefault();
    validateincometemForm();
    validateincomeForm();
    // Getting form
    var frm = this.form;
    var currentincomeItem = generateIncomeItembyForm("#" + frm.name);
    if ($("form[name='incomeitemForm']").valid()) {
        if (incomeObjectSaved() == true) {
            if (incomeItemObjectSaved() == true) {
                $.confirm({
                    title: 'Edición de ingreso',
                    content: '¿Esta seguro que desea editar el ingreso actual?',
                    theme: 'material',
                    type: 'orange',
                    buttons: {
                        si: function () {
                            OpenModalProcessing(".processingModal");
                            currentincomeItem.parent = incomeObject.item;
                            jsPromiseOperationIncomeItem = Promise.resolve(PrepareRequestSaveIncomeItem(currentincomeItem)); // Preparing incomeItem Promise
                            jsPromiseOperationIncomeItem.then(function (response) {
                                notifyMessageGral("Se ha añadido correctamente el ingreso.", 'success', 1500, '.modal-header');
                                printJSGridIncomeItem(incomeObject.item);
                                evaluateUpload(incomeObject.item);
                                evaluateComments(incomeObject.item);
                            })
                                .catch(function (error) {
                                    console.log("Failed Request! -> Editing IncomeItem ", error);
                                    notifyMessageGral("No se puede agregar el ingreso.", 'error', 1500, '.modal-header');
                                })
                                .finally(function () {
                                    ForceCloseModal(".processingModal");
                                });
                        },
                        no: function () { result = false; }
                    }
                });
            } else {
                OpenModalProcessing(".processingModal");
                currentincomeItem.parent = incomeObject.item;
                jsPromiseOperationIncomeItem = Promise.resolve(PrepareRequestSaveIncomeItem(currentincomeItem)); // Preparing incomeItem Promise
                jsPromiseOperationIncomeItem.then(function (response) {
                    notifyMessageGral("Se ha añadido correctamente el ingreso.", 'success', 1500, '.modal-header');
                    printJSGridIncomeItem(incomeObject.item);
                    evaluateUpload(incomeObject.item);
                    evaluateComments(incomeObject.item);
                })
                    .catch(function (error) {
                        console.log("Failed Request! -> Save incomeItem ", error);
                        notifyMessageGral("No se puede agregar el ingreso.", 'error', 1500, '.modal-header');
                    })
                    .finally(function () {
                        ForceCloseModal(".processingModal");
                    });
            }
        }
        else {
            if (!$("#incomeForm").valid()) {
                notifyMessageGral("Debe capturar la información general - unidad de negocio, fecha, moneda", "warn", 2000, '.modal-header');
                return;
            }
            else {
                //OpenModalProcessing(".processingModal");
                var jsPromiseSaveIncome = Promise.resolve(PrepareRequestSaveIncome());
                OpenModalProcessing(".processingModal");
                jsPromiseSaveIncome.then(function (response) {
                    notifyMessageGral("Se ha añadido correctamente solo la información general de gastos.", 'success', 1500, '.modal-header');
                    // Adding idIncome
                    incomeObject = response.data;
                    //console.log(incomeObject)
                    //$("#incomeForm #item").val(response.data.item);                 
                    currentincomeItem.parent = response.data.item;
                    jsPromiseOperationIncomeItem = Promise.resolve(PrepareRequestSaveIncomeItem(currentincomeItem)); // Preparing incomeItem Promise
                    $(".incomeIdentifier").text(incomeObject.identifier);
                    //evaluateUpload(incomeObject.item);
                    //evaluateComments(incomeObject.item);
                }).then(response => jsPromiseOperationIncomeItem)
                    .then(response => {
                        notifyMessageGral("Se ha añadido correctamente el gasto.", 'success', 1500, '.modal-header');
                        printJSGridIncomeItem(response.data.parent);
                    })
                    .catch(function (error) {
                        console.log("Failed Request! ->  save Income & IncomeItem FIrst Time", error);
                        notifyMessageGral("No se puede agregar el ingreso.", 'error', 1500, '.modal-header');
                    })
                    .finally(function () {
                        ForceCloseModal(".processingModal");
                    });
            }
        }
    }
    else {
        notifyMessageGral("Debe capturar los datos de gasto correctamente.", 'info', 1500, '.modal-header');
    }
});

$('.addIncomeMovement').click(function (event) {
    event.preventDefault();

    if (incomeObject.item == 0) {
        notifyMessageGral("Debe guradar la información general del ingreso .", 'info', 500, '.btnClosefrm1');
        return;
    }

    if (calculateTotalIncomeItemList() <= 0) {
        notifyMessageGral("Debe ingresar conceptos de ingresos.", 'info', 500, '.btnClosefrm1');
        return;
    }
    if (needtpvdata == true) {
        var _form = $("#incomeMovementsForm");
        if (Number(_form.find("select[name=tpv]").find(":selected").val()) == 0 || (_form.find("input[name=ccard]").val()).length < 4){
            notifyMessageGral("Debe seleccionar la terminal y los 4 últimos digitos del plastico.", 'info', 500, '.btnClosefrm1');
            return;
        }
        if (Number(_form.find("select[name=tpv]").find(":selected").val()) == 0 || (_form.find("input[name=authcode]").val()).length < 4) {
            notifyMessageGral("Debe agregar el código de autorización o referencia que generó la terminal.", 'info', 500, '.btnClosefrm1');
            return;
        }
    }

    var IConceptual = parseFloat($('#lbltoTalCost').text().replace(/[$,]+/g, ""));
    var Ireal = parseFloat($('.toTalCostMovements').text().replace(/[$,]+/g, ""));

    OpenModalProcessing(".processingModal");

    var identifierform = $(this).closest('form').attr('name');
    if (Ireal < IConceptual) {

        var incomemovementcurrent = {};
        incomemovementcurrent = paymentInsert('#incomeForm', '#incomeMovementsForm');
        if (incomemovementcurrent.parent == 0) {
            incomemovementcurrent.parent = incomeObject.item;

        } else {
            incomeObject.item = incomemovementcurrent.parent;
            incomeObject.applicationdatestring = incomemovementcurrent.aplicationdatestring;
        }
        var valueCurrentMov = parseFloat(incomemovementcurrent.ammounttotal);
        var valueCurrentGridMov = parseFloat($(".lbltoTalCost").text());
        var valueGridER = parseFloat($(".toTalCostMovements").text());

        if ((valueCurrentMov + valueCurrentGridMov) >= valueGridER) {
            jsPromiseOperationIncomeMovements = Promise.resolve(PrepareRequestSaveIncomeMovement(incomemovementcurrent));
            jsPromiseOperationIncomeMovements.then(function (response) {
                notifyMessageGral("Se ha añadido correctamente el movimiento de ingreso.", 'success', 500, '.btnClosefrm1');
                EditJSGriIncomeMovements(incomeObject.item);
            })
                .catch(function (error) {
                    console.log("Failed Request! ->", error);
                    notifyMessageGral("No se puede agregar el movimiento de ingreso.", 'error', 1500, '.btnClosefrm1');
                });
        }
        else {
            notifyMessageGral("El monto actual sobrepasa el total de los conceptos", 'info', 500, '.btnClosefrm1');
        }
    } else {
        notifyMessageGral("Debe completar el formulario para poder guardar un movimiento.", 'info', 500, '.btnClosefrm1');
        return false;
    }
    ForceCloseModal(".processingModal");
    console.log(incomemovementcurrent);
    this.form
});

/************************************* Validating Income Form  ******************************/
function validateincomeForm() {
    $("form[name='incomeForm']").validate({
        rules: {
            company: {
                required: true,
                notEqualTo: 0
            },
            currency: {
                required: true,
                notEqualTo: 0
            },
            applicationdatestring: {
                required: true
            }
        },
        messages: {
            company: "Seleccione una empresa.",
            applicationdatestring: "Seleccione fecha.",
            currency: 'Debe seleccionar un moneda.'
        },
        errorElement: 'em',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
    });
    $.validator.addMethod(
        "notEqualTo",
        function (elementValue, element, param) {
            return elementValue != param;
        },
        "Value cannot be {0}"
    );
}
/************************************* Validating income item Form ***************************/
function validateincometemForm() {
    $("form[name='incomeitemForm']").validate({
        rules: {
            accountl3: {
                required: true,
                notEqualTo: 0
            },
            accountl4: {
                required: true,
                notEqualTo: 0
            },
            ammounttotal: {
                required: true,
                min: 1
            }
        },
        messages: {
            accountl3: "Seleccione una categoria",
            accountl4: "Seleccione un tipo de gasto",
            ammounttotal: "Capture un monto"
        },
        errorElement: 'em',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
        submitHandler: function (form) { }
    });

    $.validator.addMethod(
        "notEqualTo",
        function (elementValue, element, param) {
            return elementValue != param;
        },
        "Value cannot be {0}");
}
/************************************* Validating income move Form ***************************/

function validateincomemovementForm() {
    _identifier = "form[name=incomeMovementsForm]";
    $(_identifier).validate({
        rules: {
            ammounttotal: {
                required: true,
                min: 1
            },
            bankaccnttype: {
                required: true,
                notEqualTo: 0
            },
            bankaccount: {
                required: true,
                notEqualTo: 0
            }
        },
        messages: {
            ammounttotal: "Debe ingresar un monto diferente de cero.",
            bankaccnttype: "Seleccione un tipo de pago",
            bankaccount: 'seleccione una cuenta.'
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


function PrepareRequestSaveIncomeItem(incomeitem) {
    var url = incomeItemObjectSaved() == true ? incomeApp.updateincomeitem : incomeApp.saveincomeitem;
    var saveDocumentItemRequest = $.ajax({
        method: "POST",
        url: url,
        datatype: 'json',
        data: { item: incomeitem },
    })
        .done(function (response, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
    return saveDocumentItemRequest;
}

function incomeItemObjectSaved() {
    if (incomeItemObject.item != undefined && incomeItemObject.item != 0)
        return true
    else return false
}

/*********************** SHOW INCOME ITEM MODAL TO EDIT ***************************************/

function prepareToEditIncomeItem(index) {
    incomeItemObject = incomeItemsObjectList[index];
    $("#incomeitemModal").modal({ backdrop: "static" });
    populateToEditIncomeItem("#incomeitemForm", incomeItemObject)
}

function populateToEditIncomeItem(formName, incomeItem) {
    var form = $(formName);
    form.find("input[name=item]").val(incomeItem.item);
    form.find("input[name=parent]").val(incomeItem.parent);
    form.find("input[name=ammounttotal]").val(incomeItem.ammounttotal);
    form.find("textarea[name=description]").val(incomeItem.description);

    loadDataNotDependency(PrepareRequestGetAccl1(incomeItem.segment, 1), incomeItem.accountl1, "incomeitemForm", "accountl1", null);
    loadDataNotDependency(PrepareRequestGetAccl2(incomeItem.accountl1, incomeItem.segment, 1), incomeItem.accountl2, "incomeitemForm", "accountl2", null);
    loadDataNotDependency(PrepareRequestGetAccl3(incomeItem.accountl2, incomeItem.segment, 1), incomeItem.accountl3, "incomeitemForm", "accountl3", null);
    loadDataNotDependency(PrepareRequestGetAccl4(incomeItem.accountl3, incomeItem.segment, 1), incomeItem.accountl4, "incomeitemForm", "accountl4", null);
}

/******************************** OPEN MODAL TO EDIT INCOME ITEM *******************************************/
function openModalIncomeItem() {
    $("#incomeitemModal").modal({ backdrop: "static" });
}

function openModalIncomeMovement() {
    $("#incomemovementModal").modal({ backdrop: "static" });
}

$('.btnClosefrmIncomeItem').on('click', function () {
    incomeItemObject = {};
})

/**
 * ********INCOME ITEM GRID ACTIOS ***********************************************************
 */

function printJSGridIncomeItem(parent) {
    deleteDataTableIncomeItemTR();
    axios.get("/Income/GetIncomeItemsDetails", { params: { id: parent } })
        .then(function (response) {
            // console.log(response.data.data)
            incomeItemsObjectList = response.data.data;
            printDataTableIncomeItemTR(response.data.data)
            // $('[data-toggle="tooltip"]').tooltip();
        }).catch(function (error) {
            console.error("No se puede obtener la lista de los detalles de gasto " + parent);
            notifyMessageGral("No se puede obtener la lista de ingresos.", 'error', 1500, '.info');
        }).finally(function () {
            ForceCloseModal(".processingModal");
            calculateTotalIncomeItemList();
        });
}

function printDataTableIncomeItemTR(data) {
    var tr = ""; var endtr = "</tr>"; var resulttr = "";
    if (data.length != 0) {
        $.each(data, function (key, value) {
            tr = "<tr class='incomeitem_class_tr' id='financialstateitem_" + value.item + "'>";
            // if (value.length != 0) {
            resulttr = (tr + printDataTableIncomeItemTD(value) + endtr);
            $('#jsGridIncomeItems > tbody:last-child').append(resulttr);
            // }
        });
    }
}

function printDataTableIncomeItemTD(data) {
    var actions = "";
    if (incomeRegisterEdit == true) {
        actions = "<td style='width: 100px;'><input type='button' class='tablegrid-button-edit tablegrid-edit-button' data-toggle='tooltip' data-placement='top' title='Editar' onclick='prepareToEditIncomeItem(" + data.index + ")' style='margin-right:15px;'> <input type='button' class='tablegrid-button tablegrid-delete-button' data-toggle='tooltip' data-placement='top' title='Borrar' onclick='deleteIncomeItem(" + data.item + ")'></td>";
    }
    else {
        actions = "<td><input type='button' class='tablegrid-button tablegrid-delete-button' data-toggle='tooltip' data-placement='top' title='Borrar' onclick='deleteIncomeItem(" + data.item + ")'></td>";
    }
    var result = "<td> " + data.row + " </td><td>" + data.accountl3name + " </td> <td>" + data.accountl4name + " </td> <td>" + data.ammounttotalstring + " </td>" + "<td>" + data.description + " </td>" + actions;
    return result;
}

function calculateTotalIncomeItemList() {
    if (incomeItemsObjectList.length != undefined && incomeItemsObjectList.length != 0) {
        var ammountdataarray = incomeItemsObjectList.map(e => e.ammounttotal).reduce(function (a, b) { return a + b });
        $(".lbltoTalCost").text(covertToNatural(ammountdataarray)); return ammountdataarray;
    }
    else {
        $(".lbltoTalCost").text(covertToNatural(0)); return 0;
    }
}

function deleteDataTableIncomeItemTR() {
    $('.incomeitem_class_tr').remove();
}

function deleteIncomeItem(id) {
    $.confirm({
        title: 'Eliminar ingreso',
        content: '¿Esta seguro que desea eliminar el ingreso actual?',
        theme: 'material',
        type: 'orange',
        buttons: {
            si: function () {
                OpenModalProcessing(".processingModal");
                axios.post(incomeApp.deleteincomeitem, { id: id })
                    .then(function (response) {
                        printJSGridIncomeItem(incomeObject.item);
                        notifyMessageGral("Se ha eliminado correctamente la información del ingreso.", 'success', 800, '#info');
                    }).catch(function (error) {
                        console.error("No se puede borrar el registro de ingreso." + id);
                        notifyMessageGral("No se puede borrar el registro de ingreso.", 'error', 1500, '.info');
                    }).finally(function () {
                        ForceCloseModal(".processingModal");
                        calculateTotalIncomeItemList();
                    });
            },
            no: function () {
                result = false;
            }
        }
    });
}

function deleteincomemovement(item) {
    $.confirm({
        title: 'Eliminar movimiento de ingreso',
        content: '¿Esta seguro que desea eliminar el movimiento de ingreso actual?',
        theme: 'material',
        type: 'orange',
        buttons: {
            si: function () {
                OpenModalProcessing(".processingModal");
                var jsPromiseDeleteIncomeItem = Promise.resolve(PrepareRequestDeleteIncomeMovement(item));
                jsPromiseDeleteIncomeItem.then(function (response) {
                    notifyMessageGral("Se ha eliminado correctamente la información del movimiento de ingreso.", 'success', 800, '.modal-header');
                    EditJSGriIncomeMovements(incomeObject.item);
                })
                    .catch(function (error) {
                        notifyMessageGral("No se puede eliminar el movimineto de ingreso.", 'error', 1500, '.modal-header');
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

function PrepareRequestDeleteIncomeMovement(item) {
    var Request = $.ajax({
        method: "POST",
        url: incomeApp.deleteincomemovitem,
        data: { id: item },
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) { });

    return Request;
}
