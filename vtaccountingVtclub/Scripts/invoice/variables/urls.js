var invoiceDomain = $(location).attr('hostname');
var invoicesids = 0;
var subtotalpagado = 0.00;
var invoiceitemEdit = false; // TO determinate some action in TBLinvoice items
var invoiceRegisterEdit = false;
var invoiceDelete = false;
var needtpvdata = false;
var _checkVal = false;

// Objects
invoiceObject = {};
searchObject  = {};
invoiceItemObject = {};
invoicePaymentsObject = {};
invoicesObjectsList = {};
invoiceItemsObjectList = {};

var invoiceDataSend = {};

// Forms
var frmInvoice = $("#invoiceForm");
var frmInvoiceItem = $("#invoiceitemForm");

var saveInvoiceRequest;
var invoiceItemDataSend = {};
var jsPromiseSaveDocument;
var jsPromiseSaveDocumentItem;
var docItemODataSelected = {};

var editingModelItem = false;
var urlAction = "";

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


var Invoice = {};
Invoice.load   = "/Invoice/GetInvoice";
Invoice.Insert = "/Invoice/SendInvoice";
Invoice.UpdateInvoice = "/Invoice/UpdateInvoice";
Invoice.Update = "/Invoice/SendInvoiceItem";
Invoice.Delete = "/Invoice/DeleteInvoice";
Invoice.GetAttach = "/Invoice/getAttachmentsInvoice";
Invoice.SaveAttach = "/Invoice/AttachFileInvoiceAjax";
Invoice.DeleteAttach = "/Invoice/DeleteAttachment";
Invoice.getComment = "/Invoice/getCommentInvoice";
Invoice.addComment = "/Invoice/addCommentInvoice";


var InvoiceItem = {};
InvoiceItem.load   = "/Invoice/GetInvoiceitemsbyId";
InvoiceItem.Insert = "/Invoice/SendInvoiceItem";
InvoiceItem.Update = "/Invoice/SendInvoiceItemUpdate";
InvoiceItem.Delete = "/Invoice/SendInvoiceItemDelete";


var accountlevels = {};
accountlevels.level1 = "/Formcontrol/getAccountLayerbyProfile";
accountlevels.level2 = "/Formcontrol/getAccountLayer2byId/";
accountlevels.level3 = "/Formcontrol/getAccountLayer3byId/";
accountlevels.level4 = "/Formcontrol/getAccountLayer4byId";


var PaymentMovs = {};
PaymentMovs.load   = "/Invoice/InvoicepaymentsGetbyId";
PaymentMovs.Insert = "/Invoice/SendInvoicepayment";
PaymentMovs.Delete = "/Invoice/InvoicepaymentDelete";

var invoicePayments = {
    Payment: 0,
    Invoice: 0,
    InvoiceIdentifier : 0,
    PaymentMethod : 0,
    PaymentMethodName : '',
    BankAccntType : 0,
    BankAccntTypeName : '',
    CurrencyPay : 0,
    CurrencyPayName : '',
    Segment : 0,
    SegmentName : '',
    Company : 0,
    CompanyOrder : 0,
    CompanyName : '',
    chargedAmount : 0,
    chargedAmountString : '',
    taxesAmmount : 0,
    taxesAmmountString : '',
    authRef : 0,
    creationDate : null,
    creationDateString : null,
    aplicationDate : null,
    aplicationDateString : null,
    description : '',
    tblcurrency : 0,
    tblinvoice : 0,
}