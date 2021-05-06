//----------------------------------------------------------------------------------------------------------------------
//-------------------------- YEAR AVIALABLE by INCOEM ITEMS  -----------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

// Loading YEar Aviable by Incomes
function loadYearsAvailable(formName, tochild, triggerchange) {
    var request = $.ajax({
        method: "GET",
        url: "/Formcontrol/getapppyearsavaliables",
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) { })
        .fail(function (jqXHR, textStatus, errorThrown) { });
    loadDataNotDependency(request, null, formName, tochild, triggerchange);
}

//----------------------------------------------------------------------------------------------------------------------
//-------------------------- Notify ------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

function notifyMessageGral(message, type, duration, place) {
    var selector = place == null ? "#info" : place; var hide = duration == null ? 500 : duration;
    var classType = type == null ? "success" : type; var msg = message == null ? "No message" : message;
    $(selector).notify(
        msg,
        { position: "bottom right", className: classType, hideDuration: hide }
    );
}

//----------------------------------------------------------------------------------------------------------------------
//-------------------------- Loading Data No Dependency ----------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

function OpenModalProcessing(identifier) {
    $(identifier).modal(
        { backdrop: "static" }
    );
}

function CloseModalProcessing(identifier) {
    $(identifier).modal('hide');
}

function ForceCloseModal(identifier) {
    setTimeout(function () {
        $(identifier).modal('hide');//ocultamos el modal
        $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
        $('.modal-backdrop').remove();//eliminamos el backdrop del modal
    }, 3000);
}
//----------------------------------------------------------------------------------------------------------------------
//-------------------------- Conver To Natural Notation ---------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

// return a float number width 2 decimal
function covertToNatural(val) {
    return val.toFixed(2).replace(/./g, function (c, i, a) {
        return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c;
    });
}
//----------------------------------------------------------------------------------------------------------------------
//-------------------------- Force to numeric only ---------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------
jQuery.fn.ForceNumericOnly = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
            // home, end, period, and numpad decimal
            return (
                key == 8 ||
                key == 9 ||
                key == 13 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};

//----------------------------------------------------------------------------------------------------------------------
//-------------------------- Type Report  ------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

function loadTypeReport(formName, tochild, triggerchange) {
    var request = $.ajax({
        method: "GET",
        url: "/Formcontrol/gettypefinancialreport",
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) { })
        .fail(function (jqXHR, textStatus, errorThrown) { });
    loadDataNotDependency(request, null, formName, tochild, triggerchange);
}

//----------------------------------------------------------------------------------------------------------------------
//-------------------------- Companies by User  ------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

function loadCompaniesbyUser(selected, formName, tochild, triggerchange) {
    var request = $.ajax({
        method: "GET",
        url: "/Formcontrol/getCompaniesByUser",
        datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) { })
        .fail(function (jqXHR, textStatus, errorThrown) { });
    loadDataNotDependency(request, selected, formName, tochild, triggerchange);
}

//----------------------------------------------------------------------------------------------------------------------
//-------------------------- RESET FORM  -------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

// ResetForm
$('.resetform').click(function (e) {
    //console.log("click to restar");
    var element = "#" + this.getAttribute("value");
    $(element)[0].reset();
    if (typeof resetDatapickes === "function") {
        resetDatapickes();
    }
});

function calculateIncomeGridValue() {
    var data = getGridData("#jsGridIncomeItems");
    if (data.length != 0) {
        var ammountdataarray = data.map(e => e.ammounttotal);
        const AmmItems = ammountdataarray.reduce(function (a, b) { return a + b });
        var ammount = AmmItems;
        $(".toTalCost").text(covertToNatural(AmmItems));
        return ammount;
    } else {
        $(".toTalCost").text("0.00");
        return 0;
    }
}
//$(".lbltoTalCost").text(covertToNatural(AmmItems));//
function getGridData(gridId) {
    return $(gridId).jsGrid("option", "data");
}

function getGridDataIndex(gridId, index) {
    return $(gridId).jsGrid("option", "data")[index];

}

function getDateNowString() {

    var fullDate = new Date();
    var twoDigitMonth = fullDate.getMonth() + 1 + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;
    var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
    var currentDate = twoDigitDate + "/" + twoDigitMonth + "/" + fullDate.getFullYear();

    return currentDate;
}

function calculateBudget(eval) {
    // var el = this;
    var el = eval;
    var element = "#" + el.name;
    var pm = el.value;
    // if ($(element).val() != 0) {
    if (pm != 0) {
        var text = el.selectedOptions[0].text;
        getBudget(el.form);
        $(".remaingBudgetName").text("Saldo " + text + " :");
    }
    else {
        $(".remaingBudgetName").text("Saldo :");
        $(".remaingBudget").text(covertToNatural(0.0));
    }
}

function getBudget(form) {

    var saldofondo = {};
    var _frm = "#" + form.name;
    var _payment = OPaymentForm(_frm);

    var currentDate = getDateNowString();

    $.ajax({
        method: "GET",
        url: "/budget/getBudgetTypeMethodPay",
        datatype: 'json',
        // async: true,
        data: {
            Company: $('#Company').val(),
            fecha: currentDate,
            idpaymentMethod: _payment.PaymentMethod
            , BankAccntType: _payment.BankAccntType
        }
    })
        .done(function (response, textStatus, jqXHR) {

            saldofondo = response.data;
            _budget = saldofondo;
            $(".remaingBudget").text(covertToNatural(saldofondo.balance) != 0 ? saldofondo.balance : "0.00");
            return response.data;
            //}
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
}