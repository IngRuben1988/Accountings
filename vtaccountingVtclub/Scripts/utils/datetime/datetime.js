//----------------------------------------------------------------------------------------------------------------------
//-------------------------- DateTime Provider  ------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

function loadDateTimePermissionIncomeInvoice(promise, identifier, defaultdate) {

    jsPromise = Promise.resolve(promise);
    jsPromise.then(function (response) {

        var _identifier = "#" + identifier + "";
        var formatIsValid = moment(defaultdate, 'MM/DD/YYYY', true).isValid();
        var _defaultDate = !formatIsValid ? formatDate(defaultdate) : defaultdate;
        var _ini = formatDate(response.data.datetimeini);
        var _end = formatDate(response.data.datetimeend);
        $('#datetimepicker1').datetimepicker({
            format: 'DD/MM/YYYY'
            , locale: moment.updateLocale('es-us')
            , useCurrent: true
            , defaultDate: _defaultDate
        });

    })
        .catch(function (error) {
            console.log("Failed Request! ->", error);
            notifyMessageGral("No se pueden cargar los datos." + error.messagge, "error", null, null);
        });
}

function loadDateTimePermissionSearch(promise, component1, defaultdate,component2) {

    jsPromise = Promise.resolve(promise);
    jsPromise.then(function (response) {

        var _identifier = "#" + component1 + "";
        var _defaultDate = formatDate(defaultdate);
        var _ini = formatDate(response.data.datetimeini);
        var _end = formatDate(response.data.datetimeend);
        $('#'+component2).datetimepicker({
            format: 'DD/MM/YYYY'
            , locale: moment.updateLocale('es-us')
            , useCurrent: true
            , defaultDate: _defaultDate
            // , minDate: _ini
            // , maxDate: _end
        });

    })
        .catch(function (error) {
            console.log("Failed Request! ->", error);
            notifyMessageGral("No se pueden cargar los datos." + error.messagge, "error", null, null);
        });
}

function loadDateTime(identifier, defaultdate) {

        var _identifier = "#" + identifier + "";
        var _defaultDate = formatDate(defaultdate);

        $(_identifier).datetimepicker({
            format: 'DD/MM/YYYY'
            , locale: moment.updateLocale('es-us')
            , useCurrent: true
            , defaultDate: _defaultDate
        });
}

function loadDateTimeLoad(identifier, defaultdate) {

    var _identifier = "#" + identifier + "";
    var _defaultDate = defaultdate;

    $(_identifier).datetimepicker({
        format: 'DD/MM/YYYY'
        , locale: moment.updateLocale('es-us')
        , useCurrent: false
        , defaultDate: _defaultDate
    });


}

function dateTimePickerFrom(identifier, start, end) {
    var year = new Date().getFullYear() - 1;
    var selector = identifier == null ? ".datetimeinput" : identifier;
    var _start = start == null ? "01/01/" + year : start;
    var _end = start == null ? formatDate(null) : _ends;

    $(identifier).datetimepicker({
        format: 'DD/MM/YYYY',
        minDate: _start,
        maxDate: _end,
        locale: moment.updateLocale('es-us'),        
        useCurrent: true
    });

    if (identifier == null) {
        $(selector).val(formatDate(null));
    }
}

function PrepareRequestGetDatePermissionsIncomeInvoice() {

    var getDateRequest = $.ajax({
        method: "GET"
        , url: "/Utilsapp/getAviableDateIncInvAdd"
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });

    return getDateRequest;
}

//----------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------




function showDatetimebyPermissions(identifier) {

    var datetimeInit = '';

    $.ajax({
        method: "GET"
        //, async : false
        , url: "/utilsapp/getDateAviableDocumentsDates"
        //,data: { name: "John", location: "Boston" }
        , datatype: 'json'
    })
        .done(function (data, textStatus, jqXHR) {
            //datetimeInit = data.success == true ? datetimeInit = data.data : datetimeInit = formatDate(new Date());
            showDatetimebyPermissions(identifier, data.data.datetimeInit, data.data.datetimeEnd, null);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
        });
}









function startdatetime(identifier, datetimeini, datetimeend, defaultdate) {
    var _identifier = "#" + identifier + "";
    var _defaultDate = formatDate(defaultdate);

    $('#datetimepicker1').datetimepicker({
        format: 'DD/MM/YYYY'
        , locale: moment.updateLocale('es-us')
        , useCurrent: true
        , defaultDate: _defaultDate
        , minDate: datetimeini
        , maxDate: datetimeend
    });

}





function showDatetimebyPermissions(identifier, datetimeInit, datetimeEnd, applyDate) {
    $(identifier).datetimepicker({
        language: 'es',
        startDate: datetimeInit,
        endDate: datetimeEnd,
        todayHighlight: true,
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
    });
    if (applyDate == null) {
        $('.inputDateToday').val(formatDate(null));
    } else { $(identifier).val(formatDate(applyDate)); }
}


/*
function showDatetimeDocumentApplyAjax(identifier, datetimeInit, datetimeEnd, applyDate) {
    $(identifier).datetimepicker({
        language: 'es',
        startDate: datetimeInit,
        endDate: datetimeEnd,
        todayHighlight: true,
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
    });
    if (applyDate == null) {
        $('.inputDateToday').val(formatDate(null));
    } else { $(identifier).val(formatDate(applyDate)); }
}
*/

function formatDate(date) {
    var d = date == null ? new Date() : new Date(date), month = '' + (d.getMonth() + 1), day = '' + d.getDate(), year = d.getFullYear();
    if (month == "NaN") {
        var array = date.split("/");
        _DAY_ = Number(array[0]);
        _MONTH_ = Number(array[1]);
        _YEAR_ = Number(array[2]);
        _DATE_ = '' + array[2] + '-' + array[1] + '-' + array[0] + '';
        d = new Date(_DATE_);
        month = '' + (d.getMonth() + 1); day = '' + d.getDate(); year = d.getFullYear();
    }


    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('-');
}

function resetDatapickes() {
    $('.datetimeinput').val("");
}
