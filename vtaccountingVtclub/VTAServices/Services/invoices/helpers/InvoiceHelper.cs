using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using VTAworldpass.VTAServices.Services.utilsapp.model;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTACore;
using VTAworldpass.VTAServices.Services.attachments;
using VTAworldpass.VTAServices.Services.utilsapp;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.invoices.helpers
{
    public abstract class InvoiceHelper
    {
        private readonly UnitOfWork unit = new UnitOfWork();

        /*******************************************/

        protected invoice ConvertTbltoHelper(tblinvoice model)
        {
            invoice helper = new invoice();
            helper.id                     = model.idinvoice;
            helper.currency               = model.tblcurrencies.idCurrency;
            helper.currencyname           = model.tblcurrencies.currencyName;
            helper.segment                = model.tblcompanies.tblsegment.idsegment;
            helper.segmentname            = model.tblcompanies.tblsegment.segmentname;
            helper.company                = model.tblcompanies.idcompany;
            helper.companyname            = model.tblcompanies.companyname;
            helper.applicationdatestring  = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
            helper.applicationdatestring0 = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
            helper.creactiondatestring    = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
            helper.cost                   = (decimal)model.tblinvoiceditem.Sum(x => x.itemsubtotal) + model.tblinvoiceditem.Sum(x => x.itemtax) + model.tblinvoiceditem.Sum(x => x.itemothertax);
            helper.costString             = MoneyUtils.ParseDecimalToString(helper.cost);
            //helper.PaymentsString = MoneyUtils.ParseDecimalToString(model.tblpayment.Sum(x => x.chargedAmount));
            helper.statusexpenses         = this.CalculateDocumentStatus(model.tblinvoiceditem.ToList());
            helper.identifier             = AccountsUtils.IdentifierComplete(model.tblcompanies.companyshortname, "Admin", model.invoicenumber);//model.tblusers.tblprofilesaccounts.profileaccountshortame, model.invoicenumber);
            return helper;
        }

        protected List<invoice> ConvertTbltoHelper(List<tblinvoice> listmodel)
        {
            List<invoice> list = new List<invoice>();
            foreach (tblinvoice model in listmodel)
            {
                invoice helper = new invoice();
                helper.id = model.idinvoice;
                helper.companyname = model.tblcompanies.companyname;
                helper.company = model.idcompany;
                helper.cost = (decimal)model.tblinvoiceditem.Sum(x => x.itemsubtotal) + model.tblinvoiceditem.Sum(x => x.itemtax) + model.tblinvoiceditem.Sum(x => x.itemothertax);
                helper.costString = MoneyUtils.ParseDecimalToString(helper.cost);
                //helper.PaymentsString = MoneyUtils.ParseDecimalToString((decimal)model.tblpayment.Sum(x => x.chargedAmount));
                helper.currencyname = model.tblcurrencies.currencyName;
                helper.creactiondatestring = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
                helper.applicationdatestring = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
                helper.statusexpenses = this.CalculateDocumentStatus(model.tblinvoiceditem.ToList());
                helper.identifier = AccountsUtils.IdentifierComplete(model.tblcompanies.companyshortname, "Admin", model.invoicenumber);
                //model.tblusers.tblprofilesaccounts.profileaccountshortame, model.invoicenumber);
                list.Add(helper);
            }
            return list;
        }

        protected List<invoice> ConvertTbltoHelper(IEnumerable<tblinvoice> listmodel, IAttachmentServices attachmentServices)
        {
            List<invoice> list = new List<invoice>();
            foreach (tblinvoice model in listmodel)
            {
                invoice helper = new invoice();
                helper.id = Convert.ToInt32(model.idinvoice);
                helper.companyname = model.tblcompanies.companyname;
                helper.company = model.idcompany;
                helper.costString = MoneyUtils.ParseDecimalToString((decimal)model.tblinvoiceditem.Sum(x => x.itemsubtotal) + (decimal)model.tblinvoiceditem.Sum(x => x.itemtax) + (decimal)model.tblinvoiceditem.Sum(x => x.itemothertax));
                //helper.paymentsstring = MoneyUtils.ParseDecimalToString((decimal)model.tblpayment.Sum(x => x.chargedAmount));
                helper.currency = model.tblcurrencies.idCurrency;
                helper.currencyname = model.tblcurrencies.currencyName;
                helper.creactiondatestring = model.invoicedate   == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
                helper.applicationdatestring = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
                helper.statusexpenses = this.CalculateDocumentStatus(model.tblinvoiceditem.ToList());
                helper.identifier = AccountsUtils.IdentifierComplete(model.tblcompanies.companyshortname, "Admin", model.invoicenumber);
                //model.tblusers.tblprofilesaccounts.profileaccountshortame, model.invoicenumber);
                //if (model.tblinvoiceattach.Count != 0) helper.attachments = attachmentServices.getAttachmentsByInvoice(Convert.ToInt32(model.idinvoice)); else helper.attachments = null;
                list.Add(helper);
            }
            return list;
        }

        protected List<invoice> ConvertTbltoHelper(IEnumerable<tblinvoice> listmodel)
        {
            List<invoice> list = new List<invoice>();
            foreach (tblinvoice model in listmodel)
            {
                invoice helper = new invoice();
                helper.id = Convert.ToInt32(model.idinvoice);
                helper.companyname = model.tblcompanies.companyname;
                helper.company = model.idcompany;
                helper.segment = (int)model.tblcompanies.idsegment;
                helper.segmentname = model.tblcompanies.tblsegment.segmentname;
                helper.costString = MoneyUtils.ParseDecimalToString((decimal)model.tblinvoiceditem.Sum(x => x.itemsubtotal) + (decimal)model.tblinvoiceditem.Sum(x => x.itemtax) + (decimal)model.tblinvoiceditem.Sum(x => x.itemothertax));
                //helper.paymentsstring = MoneyUtils.ParseDecimalToString((decimal)model.tblpayment.Sum(x => x.chargedAmount));
                helper.currency = model.tblcurrencies.idCurrency;
                helper.currencyname = model.tblcurrencies.currencyName;
                helper.creactiondatestring = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
                helper.applicationdatestring = model.invoicedate == null ? null : DateTimeUtils.ParseDatetoString(model.invoicedate);
                helper.statusexpenses = this.CalculateDocumentStatus(model.tblinvoiceditem.ToList());
                helper.identifier = AccountsUtils.IdentifierComplete(model.tblcompanies.companyshortname, "Admin", model.invoicenumber);
                list.Add(helper);
            }
            return list;
        }

        protected invoiceitems ConvertTbltoHelper(tblinvoiceditem model)
        {
            invoiceitems helper = new invoiceitems();
            helper.identifier = AccountsUtils.IdentifierComplete(model.tblinvoice.tblcompanies.companyshortname, model.tblinvoice.tblusers.tblprofilesaccounts.profileaccountshortame, model.tblinvoice.invoicenumber);
            helper.item = model.idinvoiceitem;
            helper.id = model.idinvoice;
            helper.company = model.tblinvoice.idcompany;
            helper.companyname = model.tblinvoice.tblcompanies.companyshortname;
            helper.aplicationdate = model.tblinvoice.invoicedate;
            helper.istax = model.ditemistax;
            helper.category = model.tblaccountsl4.tblaccountsl3.idaccountl3;
            helper.categoryName = model.tblaccountsl4.tblaccountsl3.accountl3description;
            helper.accountl3 = model.tblaccountsl4.tblaccountsl3.idaccountl3;
            helper.accountl3name = model.tblaccountsl4.tblaccountsl3.accountl3description;
            helper.accountl4 = model.tblaccountsl4.idAccountl4;
            helper.accountl4name = model.tblaccountsl4.accountl4description;
            helper.type = model.tblaccountsl4.idAccountl4;
            helper.typename = model.tblaccountsl4.accountl4description;
            helper.ammount = (decimal)model.itemsubtotal;
            helper.taxesammount = model.itemtax;
            helper.othertaxesammount = model.itemothertax;
            helper.description = model.itemdescription == null ? "" : model.itemdescription;
            helper.billidentifier = model.itemidentifier == null ? "" : model.itemidentifier;
            helper.ammounttotal = (decimal)helper.ammount + helper.taxesammount + helper.othertaxesammount;
            helper.status = model.tblinvoiceitemstatus.idinvoiceItemstatus;
            helper.statusname = model.tblinvoiceitemstatus.invoicestatusname;
            helper.isverified = model.idinvoiceitemstatus == 1 ? false : model.idinvoiceitemstatus == 2 ? true : true;
            helper.suppliername = model.tblSuppliers.idSupplier != 1 ? model.tblSuppliers.supplierName : model.itemsupplierother == null ? " " : model.itemsupplierother;
            helper.budgettypename = model.tblbugettype.budgettypename;
            helper.singlexibitionpayment = model.itemsinglepayment;
            helper.tblinvoiceditem = model.tblinvoice.tblinvoiceditem != null ? model.tblinvoice.tblinvoiceditem : new List<tblinvoiceditem>();
            helper.tblpayment = model.tblinvoice.tblpayment != null ? model.tblinvoice.tblpayment : new List<tblpayment>();
            helper.generateStringAmmounts();
            helper.generateStringDates();
            helper.calculateBalance(true);
            return helper;
        }

        protected List<invoiceitems> ConvertTbltoHelper(List<tblinvoiceditem> listmodel)
        {
            List<invoiceitems> list = new List<invoiceitems>();
            foreach (tblinvoiceditem model in listmodel)
            {
                invoiceitems helper = new invoiceitems();
                helper.item              = model.idinvoiceitem;
                helper.id                = model.idinvoice;
                helper.segment           = model.tblaccountsl4.tblsegmentaccl4.FirstOrDefault().tblsegment != null ? model.tblaccountsl4.tblsegmentaccl4.FirstOrDefault().tblsegment.idsegment : 0;
                helper.company           = model.tblinvoice.tblcompanies.idcompany;
                helper.companyname       = model.tblinvoice.tblcompanies.companyname;
                helper.segmentname       = model.tblaccountsl4.tblsegmentaccl4.FirstOrDefault().tblsegment != null ? model.tblaccountsl4.tblsegmentaccl4.FirstOrDefault().tblsegment.segmentname : "";
                helper.accountl1         = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1;
                helper.accountl2         = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl2;
                helper.accountl3         = model.tblaccountsl4.tblaccountsl3.idaccountl3;
                helper.accountl3name     = model.tblaccountsl4.tblaccountsl3.accountl3description;
                helper.accountl4         = model.tblaccountsl4.idAccountl4;
                helper.accountl4name     = model.tblaccountsl4.accountl4description;
                helper.ammount           = (decimal)model.itemsubtotal;
                helper.taxesammount      = model.itemtax;
                helper.description       = model.itemdescription;
                helper.status            = model.tblinvoiceitemstatus.idinvoiceItemstatus;
                helper.statusname        = model.tblinvoiceitemstatus.invoicestatusname;
                helper.istax             = model.ditemistax;
                helper.isverified        = model.idinvoiceitemstatus == 1 ? false : model.idinvoiceitemstatus == 2 ? true : true;
                helper.supplier          = model.tblSuppliers.idSupplier;
                helper.suppliername      = model.tblSuppliers.supplierName;
                helper.supplierother     = model.itemsupplierother == null ? "" : model.itemsupplierother;
                helper.budgettype        = model.tblbugettype.idbudgettype;
                helper.budgettypename    = model.tblbugettype.budgettypename;
                helper.othertaxesammount = model.itemothertax;
                helper.identifier        = model.itemidentifier;
                helper.singlexibitionpayment = model.itemsinglepayment;
                helper.ammounttotal      = (decimal)model.itemsubtotal + model.itemtax + helper.othertaxesammount;
                helper.generateStringAmmounts();
                helper.generateStringDates();
                list.Add(helper);
            }
            return list;
        }

        /*******************************************/
        protected void prepareToUpdate(invoice helper, tblinvoice model)
        {
            model.idcurrency = helper.currency;
            model.idcompany = helper.company;
            model.invoicedate = (DateTime)DateTimeUtils.ParseStringToDate(helper.applicationdatestring0);
            model.invoiceupdateon = DateTime.Now;
        }

        protected void prepareToUpdate(invoiceitems helper, tblinvoiceditem model)
        {
            model.idinvoice         = helper.id;
            model.idaccountl4       = helper.accountl4;
            model.idbudgettype      = helper.budgettype;
            model.idsupplier        = helper.supplier;
            model.itemsubtotal      = helper.ammount;
            model.itemdescription   = helper.description == null ? null : helper.description.Length > Globals.MaxQuickDescriptionCharacters ? string.Concat((helper.description).Substring(0, Globals.MaxQuickDescriptionCharacters), "...") : helper.description;
            model.ditemistax        = helper.istax;
            model.itemtax           = helper.taxesammount;
            model.itemidentifier    = helper.billidentifier;
            model.itemsupplierother = helper.supplierother;
            model.itemothertax      = helper.othertaxesammount;
            model.itemsinglepayment = helper.singlexibitionpayment;
            model.itemupdatedby     = +1;
            model.itemupdateon      = DateTime.Now;
        }
        /*******************************************/
        protected async Task<tblinvoice> prepareToSave(invoice helper)
        {
            tblinvoice model = new tblinvoice();
            model.idcurrency          = helper.currency;
            model.idcompany           = helper.company;
            model.invoicecreateon = DateTime.Now;
            model.invoiceupdateon = DateTime.Now;
            model.invoicenumber       = await lastNumberIdentifierInvoiceAsync(model.idcompany, unit);
            model.invoicedate = (DateTime)DateTimeUtils.ParseStringToDate(helper.applicationdatestring0);
            return model;
        }

        protected tblinvoiceditem prepareToSave(invoiceitems helper)
        {
            tblinvoiceditem model = new tblinvoiceditem();
            model.idinvoice = helper.id;
            model.idaccountl4       = helper.accountl4;
            model.idbudgettype      = helper.budgettype;
            model.idsupplier        = helper.supplier;
            model.itemsubtotal      = helper.ammount;
            model.itemdescription   = helper.description == null ? null : helper.description.Length > Globals.MaxQuickDescriptionCharacters ? string.Concat((helper.description).Substring(0, Globals.MaxQuickDescriptionCharacters), "...") : helper.description;
            model.ditemistax        = helper.istax;
            model.itemtax           = helper.taxesammount;
            model.itemidentifier    = helper.billidentifier;
            model.itemsupplierother = helper.supplierother;
            model.itemothertax      = helper.othertaxesammount;
            model.itemsinglepayment = helper.singlexibitionpayment;
            model.itemcreateon      = DateTime.Now;
            model.itemupdateon      = DateTime.Now;
            return model;
        }

        protected void prepareToSave(invoiceitems helper, tblinvoiceditem model, tblinvoice doc)
        {
            model.idinvoice = doc.idinvoice;
            model.idaccountl4 = helper.accountl4;
            model.itemsubtotal = helper.ammount;
            model.itemdescription = helper.description == null ? null : helper.description.Length > Globals.MaxQuickDescriptionCharacters ? string.Concat((helper.description).Substring(0, Globals.MaxQuickDescriptionCharacters), "...") : helper.description;
            model.ditemistax = helper.istax;
            model.itemtax = helper.taxesammount;
            model.itemothertax = helper.othertaxesammount;
            model.itemidentifier = helper.billidentifier;
            model.idsupplier = helper.supplier;
            model.itemsupplierother = helper.supplierother;
            model.itemsinglepayment = helper.singlexibitionpayment;
        }

        protected void prepareToSave(invoice helper, tblinvoice model)
        {
            model.idcompany = helper.company;
            model.invoicecreateon = DateTime.Now;
            model.invoiceupdateon = DateTime.Now;
            model.invoicedate = (DateTime)DateTimeUtils.ParseStringToDate(helper.applicationdatestring);
        }

        protected void prepareToSave(invoice helper, tblinvoice model, UnitOfWork unit)
        {
            model.idcurrency = helper.currency;
            model.idcompany = helper.company;
            model.invoicecreateon = DateTime.Now;
            model.invoiceupdateon = DateTime.Now;
            model.invoicenumber = lastNumberIdentifierInvoice(model.idcompany, unit);
            model.invoicedate = (DateTime)DateTimeUtils.ParseStringToDate(helper.applicationdatestring);
        }


        /******************** CALCULOS **************************************/
        protected int CalculateDocumentStatus(List<tblinvoiceditem> items)
        {
            int records = items.Count();
            int status = Globals.invoicedItemStatus_SinRevisar;
            if (records == 0) return status;

            if (items.Find(y => y.idinvoiceitemstatus == Globals.invoicedItemStatus_Rechazado) != null)
            {
                status = Globals.invoicedItemStatus_Rechazado;
            }
            else
            {
                int counter = items.FindAll(y => y.idinvoiceitemstatus == Globals.invoicedItemStatus_Aprobado).Count;
                status = counter == records ? status = Globals.invoicedItemStatus_Aprobado : status = Globals.invoicedItemStatus_SinRevisar;
            }
            return status;
        }

        public int CalculateDociItemStatusToSave(tblinvoice model, tblinvoiceditem modelitem, IUtilsappServices utilsappServices)
        {
            // Finding Monday of current Date
            var MondayApplicationDate = utilsappServices.getMondayCurrentWeek(model.invoicedate);
            var Today = model.invoicedate;
            var direfenceWeeks = (utilsappServices.diferenceDays(MondayApplicationDate, Today) / Globals.WeeksToExtemporal);
            if (direfenceWeeks > Globals.WeeksToExtemporal)
            {
                return Globals.invoicedItemStatus_Extemporal;
            }
            else
            {
                return Globals.invoicedItemStatus_SinRevisar;
            }
        }

        /*******************************************/
        private async Task<int> lastNumberIdentifierInvoiceAsync(int idCompany, UnitOfWork unit)
        {
            var result = await unit.InvoiceRepository.GetAsync(x => x.idcompany == idCompany, t => t.OrderByDescending(y => y.idinvoice), "");
            return result.Count() == 0 ? Globals.OneInt : result.Max(y => y.invoicenumber) + Globals.OneInt;
        }

        private int lastNumberIdentifierInvoice(int idCompany, UnitOfWork unit)
        {
            var result = unit.InvoiceRepository.Get(x => x.idcompany == idCompany, t => t.OrderByDescending(y => y.idinvoice), "").ToList();
            return result.Count() == 0 ? Globals.OneInt : result.Max(y => y.invoicenumber) + Globals.OneInt;
        }
        /*******************************************/

        protected List<invoicepayment> ConvertTbltoHelper(List<tblpayment> listmodel)
        {
            List<invoicepayment> list = new List<invoicepayment>();
            foreach (tblpayment model in listmodel)
            {
                invoicepayment helper = new invoicepayment();
                helper.Payment           = model.idpayment;
                helper.Invoice           = model. tblinvoice.idinvoice;
                helper.PaymentMethod     = model.tblbankaccount.idbaccount;
                helper.PaymentMethodName = string.Concat(model.tblbankaccount.baccountshortname, " ", model.tblbankaccount.tblbank.bankshortname, " ", model.tblbankaccount.tblcurrencies.currencyAlphabeticCode, " ", model.tblbankaccount.tblcompanies.companyshortname);
                helper.BankAccntType     = model.idbankprodttype;
                helper.BankAccntTypeName = model.tblbankprodttype.bankprodttypename;
                helper.CurrencyPay       = model.tblbankaccount.tblcurrencies.idCurrency;
                helper.CurrencyPayName   = string.Concat("[", model.tblbankaccount.tblcurrencies.currencyAlphabeticCode, "]");
                helper.chargedAmount     = model.paymentamount;
                helper.authRef           = model.paymentauthref;
                list.Add(helper);
            }
            return list;
        }

        protected tblpayment prepareToSave(invoicepayment helper)
        {
            tblpayment model = new tblpayment();
            model.idinvoice         = helper.Invoice;
            model.idbaccount        = helper.PaymentMethod;
            model.idbankprodttype   = helper.BankAccntType;
            model.paymentdate       = helper.aplicationDate; 
            model.paymentamount     = helper.chargedAmount;
            model.paymentauthref    = helper.authRef == null ? null : helper.authRef.Length > Globals.QuickDescriptionCharacters128 ? string.Concat((helper.authRef).Substring(0, Globals.QuickDescriptionCharacters128), "...") : helper.authRef;
            model.paymentcreateon   = System.DateTime.Now;
            model.paymentupdatedon  = System.DateTime.Now;

            return model;
        }

        protected invoicepayment ConvertTbltoHelper(tblpayment model)
        {
            invoicepayment helper = new invoicepayment();
            helper.Payment = model.idpayment;
            helper.Invoice = model.tblinvoice.idinvoice;
            helper.PaymentMethod = model.tblbankaccount.idbaccount;
            helper.PaymentMethodName = model.tblbankaccount.baccountname;
            helper.CurrencyPay = model.tblbankaccount.tblcurrencies.idCurrency;
            helper.CurrencyPayName = string.Concat("[", model.tblbankaccount.tblcurrencies.currencyAlphabeticCode, "]-", model.tblbankaccount.tblcurrencies.currencyName);
            helper.chargedAmount = model.paymentamount;
            helper.authRef = model.paymentauthref;

            return helper;
        }
    }
}