
function paymentInsert(objects,payment) {
    editingModelItem = true;
    var head = $(objects);
    var body = $(payment);

    incomePayments.item               = 0
    incomePayments.parent             = head.find("input[id=item]").val();
    incomePayments.identifier         = null;
    incomePayments.segment            = head.find("select[id=segment]").val();
    incomePayments.segmentname        = null;
    incomePayments.company            = head.find("select[id=company]").val();
    incomePayments.companyname        = null;
    incomePayments.companyorder       = null;
    incomePayments.bankaccount        = body.find("select[id=bankaccount]").val();
    incomePayments.bankaccountname    = null;
    incomePayments.bankaccnttype      = body.find("select[id=bankaccnttype]").val();
    incomePayments.bankaccnttypename  = null;
    incomePayments.currency           = body.find("select[id=currency]").val();
    incomePayments.currencyname       = null;
    incomePayments.tpv                = body.find("select[id=tpv]").val();
    incomePayments.tpvname            = null;
    incomePayments.card               = body.find("input[id=ccard]").val();
    incomePayments.ammounttotal       = body.find("input[id=ammounttotal]").val();
    incomePayments.ammounttotalstring = null;
    incomePayments.description        = body.find("textarea[name=description]").val();
    incomePayments.creationdate       = null;
    incomePayments.creationdatestring = null;
    incomePayments.aplicationdate     = head.find("input[id=applicationdatestring]").val();
    incomePayments.aplicationdatestring = head.find("input[id=applicationdatestring]").val();
    incomePayments.authcode           = body.find("input[id=authcode]").val();

    return incomePayments;
}


function editModelItem(_form, arg) {
    editingModelItem = true;
    var form = $(_form);
    form.find("input[name=ammounttotal]").val(arg.item.ammounttotal);
    form.find("textarea[name=description]").val(arg.item.description);
    form.find("select[name=BAClass]").val(arg.item.index);
    $('.BAClass').change();
    form.find("select[name=bankaccnttype]").val(arg.item.bankaccnttype);
    form.find("select[name=bankaccount]").val(arg.item.bankaccount);
}