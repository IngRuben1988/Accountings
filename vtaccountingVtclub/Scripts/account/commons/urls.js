var VTA = {};
VTA.Configs1   = "Account";
VTA.Configs2   = "Config";
VTA.Complement = "Complement";
VTA.CtrlTool   = "Formcontrol";
VTA.InWallet   = "Income";
VTA.OutWallet  = "Invoice";
VTA.Body       = "Home";
VTA.Operation1 = "Bankreconciliation";
VTA.Operation2 = "contables";
VTA.Reports ="Reports"

var VTAComponet = {};
VTAComponet.name    = $(".VTA-UserName");
VTAComponet.profile = $(".VTA-UserProfile");



var Account = {};
Account.LogOut   = '/' + VTA.Configs1 + '/LogOff';
Account.LogOutIn = '../' + VTA.Configs1 + '/Login';
Account.NotWork  = "S0001";
Account.Locate   = $(location).attr('hostname');
Account.Obtain   = "/" + VTA.Configs1 + "/AccountIdentify";
Account.Name     = $('.VTA-UserName');
Account.Profile  = "Bonty Hunter";



var Components = {};
Components.suppliers        = "/" + VTA.CtrlTool + "/getSuppliersbyUserHotels";
Components.companies        = "/" + VTA.CtrlTool + "/getSegmentsbyCompanyUser";
Components.bugetypes        = "/" + VTA.CtrlTool + "/getBudgetTypes";
Components.bankaccon        = "/" + VTA.CtrlTool + "/getBudgetTypes";
Components.bankclasific     = "/" + VTA.CtrlTool + "/getBankAccountClasification";
Components.bankcurrency     = "/" + VTA.CtrlTool + "/getAccountByCurrencyClasficationProfile";
Components.accountmoney     = "/" + VTA.CtrlTool + "/getAccountByCurrencyProfile";
Components.currencyuser     = "/" + VTA.CtrlTool + "/getCurrencyUser";
Components.companysegment   = "/" + VTA.CtrlTool + "/getCompaniesBySegment";
Components.bankproducts     = "/" + VTA.CtrlTool + "/getBAccountProductsbyUser";
Components.bankproductclass = "/" + VTA.CtrlTool + "/getBAccountProductsbyUserClass";
Components.typefinancialreport = "/" + VTA.CtrlTool + "/gettypefinancialreport";
Components.getaccount3bysegment = "/" + VTA.CtrlTool + "/getAccountLayer3bySegment";
Components.getsegmentbyuser = "/" + VTA.CtrlTool + "/getSegmentByUserBankAccount";
Components.getCompaniesbySegmentUser = "/" + VTA.CtrlTool + "/getCompaniesbySegmentUserBankAccount";
Components.getTpvUserBAccount = "/" + VTA.CtrlTool + "/getTpvUserBankAccount";
Components.getSourceData = "/" + VTA.CtrlTool + "/getSourceData";
Components.getOperationType = "/" + VTA.CtrlTool + "/getOperationType";

var accountlevels = {};
accountlevels.level1 = "/" + VTA.CtrlTool + "/getAccountLayerbyProfile";
accountlevels.level2 = "/" + VTA.CtrlTool + "/getAccountLayer2byId/";
accountlevels.level3 = "/" + VTA.CtrlTool + "/getAccountLayer3byId/";
accountlevels.level4 = "/" + VTA.CtrlTool + "/getAccountLayer4byId";

var Invoice    = {};
Invoice.load   = "/" + VTA.OutWallet + "/GetInvoice";
Invoice.Insert = "/" + VTA.OutWallet + "/Sendinvoice";
Invoice.Update = "/" + VTA.OutWallet + "/Sendinvoiceitem";
Invoice.Delete = "/" + VTA.OutWallet + "/DeleteInvoice";
Invoice.GetAttach = "/Invoice/getAttachmentsInvoice";
Invoice.SaveAttach = "/Invoice/AttachFileInvoiceAjax";
Invoice.DeleteAttach = "/Invoice/DeleteAttachment";
Invoice.getComment = "/Invoice/getCommentInvoice";
Invoice.addComment = "/Invoice/addCommentInvoice";


var InvoiceItem    = {};
InvoiceItem.load   = "/" + VTA.OutWallet + "/GetInvoiceitemsbyId";
InvoiceItem.Insert = "/" + VTA.OutWallet + "/Sendinvoiceitem";
InvoiceItem.Update = "/" + VTA.OutWallet + "/Sendinvoiceitemupdate";
InvoiceItem.Delete = "/" + VTA.OutWallet + "/SendInvoiceItemDelete";

var Income    = {}
Income.Load   = '/' + VTA.InWallet + '/GetIncomes';
Income.Insert = '/' + VTA.InWallet + '/SaveIncome';
Income.Update = '/' + VTA.InWallet + '/UpdateIncome';
Income.Delete = '/' + VTA.InWallet + '/DeleteIncome';

var IncomeItem     = {};
IncomeItem.Load    = '/' + VTA.InWallet + '/GetIncomeItemsDetails';
IncomeItem.Insert  = '/' + VTA.InWallet + '/SaveIncomeitem';
IncomeItem.Update  = '/' + VTA.InWallet + '/UpdateIncomeItem';
IncomeItem.Delete  = '/' + VTA.InWallet + '/DeleteIncomeItem';

var PaymentMovs = {};
PaymentMovs.InvoiceLoad   = "/" + VTA.OutWallet + "/InvoicepaymentsGetbyId";
PaymentMovs.InvoiceInsert = "/" + VTA.OutWallet + "/SendInvoicepayment";
PaymentMovs.InvoiceDelete = "/" + VTA.OutWallet + "/InvoicepaymentDelete";
PaymentMovs.IncomeLoad    = "/" + VTA.OutWallet + "/GetIncomeMovements";
PaymentMovs.IncomeInsert  = "/" + VTA.OutWallet + "/SaveIncomeMovement";
PaymentMovs.IncomeDelete  = "/" + VTA.OutWallet + "/DeleteIncomeMovement";

var incomeApp = {};
incomeApp.getincomes          = '/' + VTA.InWallet + '/GetIncomes';
incomeApp.saveincome          = '/' + VTA.InWallet + '/SaveIncome';
incomeApp.updateincome        = '/' + VTA.InWallet + '/UpdateIncome';
incomeApp.deleteincome        = '/' + VTA.InWallet + '/DeleteIncome';
incomeApp.getincomeitems      = '/' + VTA.InWallet + '/GetIncomeItemsDetails';
incomeApp.saveincomeitem      = '/' + VTA.InWallet + '/SaveIncomeItem';
incomeApp.updateincomeitem    = '/' + VTA.InWallet + '/UpdateIncomeItem';
incomeApp.deleteincomeitem    = '/' + VTA.InWallet + '/DeleteIncomeItem';
incomeApp.getincomemovitems   = '/' + VTA.InWallet + '/GetIncomeMovements';
incomeApp.saveincomemovitem   = '/' + VTA.InWallet + '/SaveIncomeMovement';
incomeApp.deleteincomemovitem = '/' + VTA.InWallet + '/DeleteIncomeMovement';
incomeApp.uploadfile = '/' + VTA.InWallet + '/fileuploadincome';
incomeApp.saveattach = '/' + VTA.InWallet + '/AttachFileIncomeAjax';
incomeApp.getincomeattach = '/' + VTA.InWallet + '/getAttachmentsIncome';
incomeApp.deleteattachinc = '/' + VTA.InWallet + '/DeleteAttachmentIncome';
incomeApp.getcommentinc = '/' + VTA.InWallet + '/getCommentIncome';
incomeApp.addcommentinc = '/' + VTA.InWallet + '/addCommentIncome';



var Configuration = {};
Configuration.getUsers = "/" + VTA.Configs2 + "/getAllUsers";
Configuration.searchUsers = "/" + VTA.Configs2 + "/searchUsers";
Configuration.getUsersToFill = "/" + VTA.Configs2 + "/getUserToFillSelect";
Configuration.getProfileToFill = "/" + VTA.Configs2 + "/getProfileToFill";
Configuration.getCompaniesToFill = "/" + VTA.Configs2 + "/getCompaniesToFill";
Configuration.getAccountToFill = "/" + VTA.Configs2 + "/getAccountToFill";
Configuration.getPermissionsToFill = "/" + VTA.Configs2 + "/getPermissionsToFill";
Configuration.getCompaniesByIdUser = "/" + VTA.Configs2 + "/getCompaniesByIdUser";
Configuration.getAccountByIdUser = "/" + VTA.Configs2 + "/getAccountByIdUser";
Configuration.getPermissionsByIdUser = "/" + VTA.Configs2 + "/getPermissionsByIdUser";
Configuration.saveCompaniesBy = "/" + VTA.Configs2 + "/saveCompaniesByUser";
Configuration.saveAccountsBy = "/" + VTA.Configs2 + "/saveAccountsByUser";
Configuration.savePermissionsBy = "/" + VTA.Configs2 + "/savePermissionsByUser";
Configuration.saveInfoUserBy = "/" + VTA.Configs2 + "/saveInfoUserByUser";


var Complement = {};
Complement.UploadInvoice    = "/" + VTA.Complement + "AttachFileUploadInvoice";
Complement.UploadIncome     = "/" + VTA.Complement + "AttachFileUploadIncome";
Complement.GetAttachInvoice = "/" + VTA.Complement + "GetAttachmentInv";
Complement.GetAttachIncome  = "/" + VTA.Complement + "GetAttachmentInc";
Complement.DelAttachInvoice = "/" + VTA.Complement + "DeleteAttachmentInv";
Complement.DelAttachIncome  = "/" + VTA.Complement + "DeleteAttachmentInc";
Complement.CommentIncome    = "/" + VTA.Complement + "AddCommentIncome";
Complement.GetCommentIncome = "/" + VTA.Complement + "GetCommentIncome";
Complement.CommentInvoice   = "/" + VTA.Complement + "AddCommentInvoice";
Complement.GetCommentInvoice= "/" + VTA.Complement + "GetCommentInvoice";


var GridView   = {};
GridView.Table = $("#gridView");
GridView.Head  = $("#gridhead");
GridView.Body  = $("#gridbody");
GridView.Class = $(".tblResponse");




var FielGridView = {};


var bankStatementsUrl = {};
bankStatementsUrl.fileSend = "/" + VTA.Operation1 + "/BankStatementsUploadFileConciliations";
bankStatementsUrl.getSCBKPosbyReference = "/" + VTA.Operation1 + "/getSCBKPosbyReferenceReferenceItem";
bankStatementsUrl.deleteSCBKPosItemReference = "/" + VTA.Operation1 + "/deleteSCBKPosItemReferenceReferenceItem";



var formControl = {}
formControl.BAccountByGroup = "/" + VTA.CtrlTool + "/getCompaniesbyGroupUserBankAccount";
formControl.ReconciliationStatus = "/" + VTA.CtrlTool + "/getReconciliationStatus";
formControl.ExternalGroup = "/" + VTA.CtrlTool + "/getExternalGroupUserBank";
formControl.HotelsBySegment = "/" + VTA.CtrlTool + "/getCompaniesBySegmentUser";
formControl.Currencies = "/" + VTA.CtrlTool + "/getCurrencies";
formControl.BankAccountByCompany = "/" + VTA.CtrlTool + "/getBankAccountbyCompaniesUserBankAccount"
formControl.BankAccountByCompanyGive = "/" + VTA.CtrlTool + "/getCompaniesByUserBankAccountGive"
formControl.BankAccountByCompanyReceive = "/" + VTA.CtrlTool + "/getCompaniesByUserBankAccountReceive"


var Reportes = {};

Reportes.expense = "/" + VTA.Reports + "/expense";
Reportes.expensedetails = "/" + VTA.Reports + "/expenseDetails";
Reportes.expenseconcentrated = "/" + VTA.Reports + "/expenseReportConcentrated";
Reportes.accountsClosing = "/" + VTA.Reports + "/accountsClosing";
Reportes.accountsClosingExportExcel = "/" + VTA.Reports + "/accountsClosingGenerateExcel";
Reportes.baccountCashClosing = "/" + VTA.Reports + "/baccountCashClosing";
Reportes.baccountCashExportExcel = "/" + VTA.Reports + "/baccountCashClosingGenerateExcel";
Reportes.accountsClosingReconciliation = "/" + VTA.Reports + "/accountsClosingReconciliation";
Reportes.accountsClosingExportExcelConciliation = "/" + VTA.Reports + "/accountsClosingGenerateExcelConciliations";




