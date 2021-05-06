var incomeitemEdit = false; // TO determinate some action in TBLIncome items
var incomeRegisterEdit = false;
var incomeDelete = false;
var needtpvdata = false;
var SegmentCompanyEdit = false;
var subtotalpagado = 0;
// Objects

incomeObject = {};
incomeItemObject = {};
incomesObjectsList = {};
incomeItemsObjectList = {};

// Forms
var frmIncome = $("#incomeForm");
var frmIncomeItem = $("#incomeitemForm");

var Components = {};
Components.suppliers = "/Formcontrol/getSuppliersbyUserHotels";
Components.companies = "/Formcontrol/getSegmentsbyCompanyUser";
Components.bugetypes = "/Formcontrol/getBudgetTypes";
Components.bankaccon = "/Formcontrol/getBudgetTypes";
Components.bankclasific = "/Formcontrol/getBankAccountClasification";
Components.bankcurrency = "/Formcontrol/getAccountByCurrencyClasficationProfile";
Components.accountmoney = "/Formcontrol/getAccountByCurrencyProfile";
Components.currencyuser = "/Formcontrol/getCurrencyUser";
Components.companysegment = "/Formcontrol/getCompaniesBySegment";
Components.bankproducts = "/Formcontrol/getBAccountProductsbyUser";
Components.bankproductclass = "/Formcontrol/getBAccountProductsbyUserClass";

//------------------ Income APP URL's ------------------------------------------------------------------------------------
incomeApp = {};
incomeApp.getincomes          = '/Income/GetIncomes';
incomeApp.getincome;
incomeApp.saveincome          = '/Income/SaveIncome';
incomeApp.updateincome        = '/Income/UpdateIncome';
incomeApp.deleteincome        = '/Income/DeleteIncome';
incomeApp.getincomeitems      = '/Income/GetIncomeitemsDetails';
incomeApp.getincomeitem       = '';
incomeApp.saveincomeitem      = '/Income/SaveIncomeItem';
incomeApp.updateincomeitem    = '/Income/UpdateIncomeItem';
incomeApp.deleteincomeitem    = '/Income/DeleteIncomeItem';
incomeApp.getincomemovitems   = '/Income/GetIncomeMovements';
incomeApp.getincomemovitem    = '';
incomeApp.saveincomemovitem   = '/Income/SaveIncomeMovement';
incomeApp.updateincomemovitem = '';
incomeApp.deleteincomemovitem = '/Income/DeleteIncomeMovement';
incomeApp.uploadfile = '/Income/fileuploadincome';
incomeApp.saveattach = '/Income/AttachFileIncomeAjax';
incomeApp.getincomeattach = '/Income/getAttachmentsIncome'
incomeApp.deleteattachinc = '/Income/DeleteAttachmentIncome'
incomeApp.getcommentinc = '/Income/getCommentIncome'
incomeApp.addcommentinc = '/Income/addCommentIncome'

var incomePayments =
{
    item:0,
    parent:0,
    identifier:0,
    segment:0,
    segmentname:0,
    company :0,
    companyname :'',
    companyorder: 0,
    bankaccount :0,
    bankaccountname :'',
    bankaccnttype :0,
    bankaccnttypename:'',
    currency :0,
    currencyname:'',
    tpv :0,
    tpvname:'',
    card:'',
    ammounttotal :0,
    ammounttotalstring :'',
    description :'',
    creationdate: null,
    creationdatestring:null,
    aplicationdate: null,
    aplicationdatestring:null,
}