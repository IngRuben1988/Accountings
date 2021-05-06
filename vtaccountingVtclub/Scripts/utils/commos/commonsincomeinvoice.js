//----------------------------------------------------------------------------------------------------------------------
//-------------------------- Actions By User-Companies------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------

function PrepareRequestGetCompaniesBySegment(idSegment) {
    getDocumentCompaniesRequest = $.ajax({
        method: "GET",
        url: "/Formcontrol/getCompaniesBySegment",
        data: { id: idSegment },
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });

    return getDocumentCompaniesRequest;
}

function PrepareRequestGetCurrenciesbyCompanies(idCompany) {
    var Request = $.ajax({
        method: "GET",
        url: "/Formcontrol/getCompaniesCurrencies",
        data: { id: idCompany },
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });

    return Request;
}

//------------------ TBLACCOUNTS by Segment ----------------------------------------------------------------------------

function PrepareRequestGetAccl1(idSegment, accl1) {
    var getAccl1Request = $.ajax({
        method: "GET",
        url: "/Formcontrol/getAccountL1ByProfileSegment",
        data: { id: idSegment, accl1: accl1 },
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });

    return getAccl1Request;
}

function PrepareRequestGetAccl2(id, idSegment, accl1) {
    getAccl1Request = $.ajax({
        method: "GET",
        url: "/Formcontrol/getAccountLayer2byIdndSegment",
        data: { id: id, idSegment: idSegment, accl1: accl1 },
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });

    return getAccl1Request;
}

function PrepareRequestGetAccl3(id, idSegment, accl1) {
    getAccl1Request = $.ajax({
        method: "GET",
        url: "/Formcontrol/getAccountLayer3byIdndSegment",
        data: { id: id, idSegment: idSegment, accl1: accl1 },
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });

    return getAccl1Request;
}

function PrepareRequestGetAccl4(id, idSegment, accl1) {
    getAccl1Request = $.ajax({
        method: "GET",
        url: "/Formcontrol/getAccountLayer4byIdndSegment",
        data: { id: id, idSegment: idSegment, accl1: accl1 },
        datatype: 'json'
    })
    .done(function (data, textStatus, jqXHR) { })
    .fail(function (jqXHR, textStatus, errorThrown) { });

    return getAccl1Request;
}

//------------------ Creattin object Income and  Items ----------------------------------------------------------------------------

var incomeItemObjectStruct = {
    item: 0,
    parent: 0,
    accountl1: 0,
    accountl2: 0,
    accountl3: 0,
    accountl3name: "",
    accountl4: 0,
    accountl4name: "",
    ammounttotal: 0,
    description: ""
}

function generateIncomeItembyForm(_stringfrm) {
    var _this = incomeItemObjectStruct; var form = $(_stringfrm);
    _this.item          = Number(form.find("input[name=item]").val());
    _this.parent        = Number(form.find("input[name=parent]").val());
    _this.accountl1     = Number(form.find("select[name=accountl1]").find(":selected").val());
    _this.accountl2     = Number(form.find("select[name=accountl2]").find(":selected").val());
    _this.accountl3     = Number(form.find("select[name=accountl3]").find(":selected").val());
    _this.accountl3name = form.find("select[name=accountl3]").find(":selected").text();
    _this.accountl4     = Number(form.find("select[name=accountl4]").find(":selected").val());
    _this.accountl4name = form.find("select[name=accountl4]").find(":selected").text();
    _this.ammounttotal  = parseFloat(form.find("input[name=ammounttotal]").val());
    _this.description   = form.find("textarea[name=description]").val();
    return _this;
}

function incomeObjectSaved() {
    if (incomeObject.item != undefined && incomeObject.item != 0)
        return true
    else return false
}


//------------------ Creattin object Invoice and  Items ----------------------------------------------------------------------------

var invoiceItemObject = {
    item                  : 0,
    invoice               : 0,
    accountl1             : 0,
    accountl2             : 0,
    category              : 0,
    categoryname          : "",
    type                  : 0,
    typename              : "",
    istax                 : false,
    ammount               : 0,
    taxesammount          : 0,
    billidentifier        : "",
    supplier              : 0,
    suppliername          : "",
    supplierother         : "",
    description           : "",
    othertaxesammount     : 0,
    singleexibitionpayment: false,
    budgettype            : 0,
    budgettypename        : ""
}

function OInvoiceItemForm(_stringfrm) {
    var _this = invoiceItemObject;
    var form = $(_stringfrm);
    _this.item                   = Number(form.find("input[name=Item]").val());
    _this.invoice                = Number(form.find("input[name=Invoice]").val());
    _this.accountl1              = Number(form.find("select[name=accountl1]").find(":selected").val());
    _this.accountl2              = Number(form.find("select[name=accountl2]").find(":selected").val());
    _this.category               = Number(form.find("select[name=category]").find(":selected").val());
    _this.categoryname           = form.find("select[name=category]").find(":selected").text();
    _this.type                   = Number(form.find("select[name=Type]").find(":selected").val());
    _this.typename               = form.find("select[name=Type]").find(":selected").text();
    _this.ammount                = parseFloat(form.find("input[name=Ammount]").val());
    _this.istax                  = form.find("input:radio[name=isTax]:checked").val() == "true" ? true : false;
    _this.taxesammount           = parseFloat(form.find("input[name=taxesAmmount]").val());
    _this.billidentifier         = form.find("input[name=billIdentifier]").val();
    _this.supplier               = Number(form.find("select[name=supplier]").find(":selected").val());
    _this.suppliername           = form.find("select[name=supplier]").find(":selected").text();
    _this.supplierother          = form.find("input[name=supplierOther]").val();
    _this.description            = form.find("textarea[name=description]").val();
    _this.othertaxesammount      = Number(form.find("input[name=OthertaxesAmmount]").val());
    _this.singleexibitionpayment = form.find("select[name=singleExibitionPayment]").find(":selected").val() == "true" ? true : false;
    _this.budgettype             = Number(form.find("select[name=budgetType]").find(":selected").val());
    _this.budgettypename         = form.find("select[name=budgetType]").find(":selected").text();
    return _this;
}

function invoiceItemObjectSaved() {
    if (invoiceItemObject.item != undefined && invoiceItemObject.item != 0)
        return true
    else return false
}

var searchObject = {
    idparent      :0,
    identifier    :0,
    currency      :0,
    company       :0,
    segment       :0,
    number        :0,
    user          :0,
    ammountini    :0,
    ammountend    :0,
    paymentini    :0,
    paymentend    :0,
    costini       :0,
    costend       :0, 
    createdateini :"", 
    createdateend :"",
    updatedateini :"",
    updatedateend :"",
    appdateini    :"",
    appdateend: ""
}

function OnSearchForm() {
    var _this = invoiceItemObject;
    _this.idparent = 0;
    _this.identifier=0;
    _this.currency=0;
    _this.company = Number($('.Company').val());
    _this.segment = 0;
    _this.number = Number($("#InvoiceNumber").val());
    _this.user = 1;
    _this.ammountini = $("#ammountIni").val();
    _this.ammountend = $("#ammountEnd").val();
    _this.paymentini = 0;
    _this.paymentend = 0;
    _this.costini = 0;
    _this.costend = 0;
    _this.createdateini = $("#creationDateFin").val();
    _this.createdateend = $("#creationDateIni").val();
    _this.updatedateini = "";
    _this.updatedateend = "";
    _this.appdateini = $("#applicationDateIni").val();
    _this.appdateend= $("#applicationDateFin").val();
    return _this;
}