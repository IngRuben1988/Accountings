using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.budgets.model;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.Models.commons;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;
using VTAworldpass.VTAServices.Services.reports.model;

namespace VTAworldpass.VTAServices.Services.budgets.helpers
{
    public abstract class budgetHelper
    {
        private int[] WeekDays = { 6, 0, 1, 2, 3, 4, 5 };
        private static UnitOfWork UnitOfWork = new UnitOfWork();

        protected DateTime calculateDateIni(DateTime date)
        {
            DateTime currentdate = date.Subtract(new TimeSpan(WeekDays[DateTimeUtils.dayOfWeek(date)], 0, 0, 0));
            return currentdate;
        }
        protected DateTime calculateDateEnd(DateTime date)
        {
            DateTime currentdate = date.Add(new TimeSpan(6 - WeekDays[DateTimeUtils.dayOfWeek(date)], 0, 0, 0));
            return currentdate;
        }

        protected DateTime calculateDateIniMonth(DateTime dateSelected)
        {
            int mothini = DateTimeUtils.MonthOfDate(dateSelected);
            int monthEnd = DateTimeUtils.MonthOfDate(dateSelected);

            DateTime dinamic = dateSelected;

            do
            {
                if (DateTimeUtils.dayOfWeek(dinamic) == Globals.Lunes)
                {
                    break;
                }
                else if (DateTimeUtils.dayOfMonth(dinamic) == Globals.PrimerdiaMes)
                {
                    break;
                }
                else
                {
                    dinamic = dinamic.AddDays(Globals.OnedayNegative);
                    monthEnd = DateTimeUtils.MonthOfDate(dinamic);
                }

            }
            while (mothini == monthEnd);

            return dinamic;
        }

        protected DateTime calculateDateEndMonth(DateTime dateSelected)
        {
            int mothini = DateTimeUtils.MonthOfDate(dateSelected);
            int monthEnd = DateTimeUtils.MonthOfDate(dateSelected);

            DateTime dinamic = dateSelected;

            int TotalDaysMomnth = DateTimeUtils.TotalDaysMonth(dateSelected);

            do
            {
                if (DateTimeUtils.dayOfWeek(dinamic) == Globals.Domingo)
                {
                    break;

                }
                else if (DateTimeUtils.dayOfMonth(dinamic) == TotalDaysMomnth)
                {
                    break;
                }
                else
                {
                    dinamic = dinamic.AddDays(Globals.Oneday);
                    monthEnd = DateTimeUtils.MonthOfDate(dinamic);
                }

            }
            while (mothini == monthEnd);

            /*
            while (mothini == monthEnd)
            {

                if (DatetimeUtils.dayOfWeek(dinamic) != 1)
                {
                    dinamic = dinamic.AddDays(Constantes.OnedayNegative);
                    monthEnd = DateTimeUtils.MonthOfDate(dinamic);
                }
            }
            */
            return dinamic;
        }
        protected DateTime calculateDateEndWeek(DateTime date)
        {
            DateTime currentdate = date.Add(new TimeSpan(WeekDays[DateTimeUtils.dayOfWeek(date)], 0, 0, 0));
            return currentdate;
        }
        protected decimal calculaSaldo(decimal cargos, decimal abonos)
        {
            return cargos - abonos;
        }

        /****************************************************************/
        protected void convertTbltoHelper(tblfondos model, fondoModel helper)
        {
            helper.id = model.idFondos;

            // Payment Method
            helper.PaymentMethod = model.idPaymentMethod;
            helper.PaymentMethodName = helper.generateAccountName(model.tblbankaccount);
            helper.Company = model.tblbankaccount.tblcompanies.idcompany;
            helper.CompanyOrder = (int)model.tblbankaccount.tblcompanies.companyorder;
            helper.CompanyName = model.tblbankaccount.tblcompanies.companyname;
            helper.Currency = model.tblbankaccount.tblcurrencies.idCurrency;
            helper.CurrencyName = model.tblbankaccount.tblcurrencies.currencyName;

            /// Common
            helper.fechaEntrega = model.fondofechaEntrega;
            helper.FechaInicio = model.fondoFechaInicio;
            helper.FechaInicioString = DateTimeUtils.ParseDatetoString(model.fondoFechaInicio);
            helper.FechaFin = model.fondoFechaFin;
            helper.FechaFinString = DateTimeUtils.ParseDatetoString(model.fondoFechaFin);
            helper.MontoCargo = model.fondoMonto;
            helper.MontoCargoString = MoneyUtils.ParseDecimalToString(model.fondoMonto);
            helper.fechaEntregaString = DateTimeUtils.ParseDatetoString(model.fondofechaEntrega);
            helper.comments = model.fondoComments;
            helper.editable = true;

            // Financial Method 
            helper.FinancialMethod = model.tblbankaccount1.idbaccount;
            helper.FinancialMethodName = helper.generateAccountName(model.tblbankaccount1);
            helper.CompanyFinancial = model.tblbankaccount1.tblcurrencies.idCurrency;
            helper.CompanyFinancialOrder = (int)model.tblbankaccount1.tblcompanies.companyorder;
            helper.CompanyFinancialName = model.tblbankaccount1.tblcompanies.companyname;
            helper.CurrencyFinancial = model.tblbankaccount1.tblcurrencies.idCurrency;
            helper.CurrencyFinancialName = model.tblbankaccount1.tblcurrencies.currencyName;

            // Invoice If Apply
            helper.Invoice = model.fondoInvoice != null ? model.fondoInvoice : null;
            helper.FondoFee = model.fondoFee;

        }

        protected List<fondoModel> convertTbltoHelper(IEnumerable<tblfondos> list)
        {

            List<fondoModel> listHelper = new List<fondoModel>();

            foreach (tblfondos model in list)
            {
                fondoModel helper = new fondoModel();

                // Payment method
                helper.id = model.idFondos;
                helper.PaymentMethod = model.idPaymentMethod;
                helper.PaymentMethodName = helper.generateAccountName(model.tblbankaccount);
                helper.Company = model.tblbankaccount.tblcompanies.idcompany;
                helper.CompanyOrder = (int)model.tblbankaccount.tblcompanies.companyorder;
                helper.CompanyName = model.tblbankaccount.tblcompanies.companyname;
                helper.Currency = model.tblbankaccount.tblcurrencies.idCurrency;
                helper.CurrencyName = model.tblbankaccount.tblcurrencies.currencyName;

                // Common
                helper.fechaCaptura = model.fondoCreationDate;
                helper.fechaCapturaString = DateTimeUtils.ParseDatetoString(model.fondoCreationDate);
                helper.fechaEntrega = model.fondofechaEntrega;
                helper.FechaInicio = model.fondoFechaInicio;
                helper.FechaInicioString = DateTimeUtils.ParseDatetoString(model.fondoFechaInicio);
                helper.FechaFin = model.fondoFechaFin;
                helper.FechaFinString = DateTimeUtils.ParseDatetoString(model.fondoFechaFin);
                helper.MontoCargo = model.fondoMonto;
                helper.MontoCargoString = MoneyUtils.ParseDecimalToString(model.fondoMonto);
                helper.fechaEntregaString = DateTimeUtils.ParseDatetoString(model.fondofechaEntrega);
                helper.comments = model.fondoComments;


                // Financial Method 
                helper.FinancialMethod = model.tblbankaccount1.idbaccount;
                helper.FinancialMethodName = helper.generateAccountName(model.tblbankaccount1);
                helper.FinancialMethodName = helper.generateAccountName(model.tblbankaccount1);
                helper.CompanyFinancial = model.tblbankaccount1.tblcompanies.idcompany;
                helper.CompanyFinancialOrder = (int)model.tblbankaccount1.tblcompanies.companyorder;
                helper.CompanyFinancialName = model.tblbankaccount1.tblcompanies.companyname;
                helper.CurrencyFinancial = model.tblbankaccount1.tblcurrencies.idCurrency;
                helper.CurrencyFinancialName = model.tblbankaccount1.tblcurrencies.currencyName;
                helper.FinanceType = model.idFinanceType;

                // Invoice If Apply
                helper.Invoice = model.fondoInvoice != null ? model.fondoInvoice : null;
                helper.FondoFee = model.fondoFee;


                helper.editable = true;
                listHelper.Add(helper);

            }

            return listHelper;
        }

        protected void PrepareToSave(fondoModel helper, tblfondos model)
        {
            model.idPaymentMethod = helper.PaymentMethod;
            model.idFinancialMethod = helper.FinancialMethod;
            // model.idCurrency = helper.Currency;
            // model.idCompany = helper.Hotel;
            model.fondoFee = (decimal)helper.FondoFee;
            model.fondoInvoice = helper.Invoice;
            model.fondofechaEntrega = (DateTime)DateTimeUtils.ParseStringToDate(helper.fechaEntregaString);
            model.fondoFechaInicio = (DateTime)DateTimeUtils.ParseStringToDate(helper.FechaInicioString);
            model.fondoFechaFin = (DateTime)DateTimeUtils.ParseStringToDate(helper.FechaFinString);
            model.fondoMonto = (decimal)helper.MontoCargo;
            model.fondoCreationDate = DateTime.Now;
            model.fondoComments = helper.comments;
            model.idFinanceType = helper.FinanceType;
        }

        protected void PreparetoSaveAccountFinancial(fondoModel helper, tblfondos model)
        {
            // Se realiza un movimiento para descontar de la cuenta Financiadora el montoa tranferir a la pagadora
            model.idPaymentMethod = helper.FinancialMethod;
            model.idFinancialMethod = helper.PaymentMethod;
            // model.idCurrency = helper.Currency;
            // model.idCompany = helper.Hotel;
            model.fondofechaEntrega = (DateTime)DateTimeUtils.ParseStringToDate(helper.fechaEntregaString);
            model.fondoFechaInicio = (DateTime)DateTimeUtils.ParseStringToDate(helper.FechaInicioString);
            model.fondoFechaFin = (DateTime)DateTimeUtils.ParseStringToDate(helper.FechaFinString);
            model.fondoMonto = (decimal)helper.MontoCargo * -1;
            model.fondoCreationDate = DateTime.Now;
            model.fondoComments = helper.comments;
        }

        protected List<invoiceitems> convertTbltoHelper(List<tblinvoiceditem> listmodel)
        {
            List<invoiceitems> list = new List<invoiceitems>();

            foreach (tblinvoiceditem model in listmodel)
            {
                invoiceitems helper = new invoiceitems();
                helper.item = model.idinvoiceitem;
                helper.id = model.idinvoice;
                helper.category = model.tblaccountsl4.tblaccountsl3.idaccountl3 == 0 ? 0 : model.tblaccountsl4.tblaccountsl3.idaccountl3;
                helper.categoryName = model.tblaccountsl4.tblaccountsl3.accountl3name == null ? "" : model.tblaccountsl4.tblaccountsl3.accountl3name;
                helper.type = model.idaccountl4;
                helper.typename = model.tblaccountsl4.accountl4name == null ? "" : model.tblaccountsl4.accountl4name;
                helper.ammount = (decimal)model.itemsubtotal;
                helper.taxesammount = model.itemtax;
                helper.description = model.itemdescription;
                helper.status = model.tblinvoiceitemstatus.idinvoiceItemstatus;
                helper.statusname = model.tblinvoiceitemstatus.invoicestatusname;
                helper.isverified = model.idinvoiceitemstatus == 1 ? false : model.idinvoiceitemstatus == 2 ? true : true;
                list.Add(helper);
            }
            return list;
        }

        protected invoiceitems convertTbltoHelper(tblinvoiceditem model)
        {
            invoiceitems helper = new invoiceitems();
            helper.item = model.idinvoiceitem;
            helper.id = model.idinvoice;
            helper.category = model.tblaccountsl4.tblaccountsl3.idaccountl3 == 0 ? 0 : model.tblaccountsl4.tblaccountsl3.idaccountl3;
            helper.categoryName = model.tblaccountsl4.tblaccountsl3.accountl3name == null ? "" : model.tblaccountsl4.tblaccountsl3.accountl3name;
            helper.type = model.idaccountl4;
            helper.typename = model.tblaccountsl4.accountl4name == null ? "" : model.tblaccountsl4.accountl4name;
            helper.ammount = (decimal)model.itemsubtotal;
            helper.taxesammount = model.itemtax;
            helper.description = model.itemdescription;
            helper.status = model.tblinvoiceitemstatus.idinvoiceItemstatus;
            helper.statusname = model.tblinvoiceitemstatus.invoicestatusname;
            helper.isverified = model.idinvoiceitemstatus == 1 ? false : model.idinvoiceitemstatus == 2 ? true : true;
            return helper;
        }

        protected List<invoiceitems> parseHelperToHelper(List<invoiceitems> listHelper)
        {
            List<invoiceitems> _new = new List<invoiceitems>();

            foreach (invoiceitems helper in listHelper)
            {
                var result = convertTbltoHelper(UnitOfWork.InvoiceItemRepository.Get(y => y.idinvoiceitem == helper.item, null, "tblaccountsl4, tblaccountsl4.tblaccountsl3, tblinvoiceitemstatus").FirstOrDefault());
            }

            return _new;
        }

        protected List<invoicepayment> convertTbltoHelper(IEnumerable<tblpayment> listmodel)
        {
            List<invoicepayment> list = new List<invoicepayment>();

            foreach (tblpayment model in listmodel)
            {
                invoicepayment helper = new invoicepayment();
                helper.Payment = model.idpayment;
                helper.Invoice = model.tblinvoice.idinvoice;
                helper.PaymentMethod = model.tblbankaccount.idbaccount;
                helper.PaymentMethodName = string.Concat(model.tblbankaccount.baccountname, " - ", model.tblbankaccount.tblcurrencies.currencyAlphabeticCode);
                helper.CurrencyPay = model.tblbankaccount.tblcurrencies.idCurrency;
                helper.CurrencyPayName = string.Concat("[", model.tblbankaccount.tblcurrencies.currencyAlphabeticCode, "]-", model.tblbankaccount.tblcurrencies.currencyName);
                helper.chargedAmount = model.paymentamount;
                helper.chargedAmountString = MoneyUtils.ParseDecimalToString(model.paymentamount);
                helper.authRef = model.paymentauthref;

                try

                {
                    helper.description = model.tblinvoice.tblinvoiceditem.Count > 0 ? string.Concat(model.tblinvoice.tblinvoiceditem.First().tblaccountsl4.tblaccountsl3.accountl3description, " - ", model.tblinvoice.tblinvoiceditem.First().tblaccountsl4.accountl4description) : helper.authRef;
                }
                catch (Exception e)
                {
                    helper.description = helper.authRef;
                    Log.Info("budgetHelper-List<invoicepayment> convertTbltoHelper(IEnumerable<tblpayment> listmodel -> " + "Obteniendo Descripción de registro y no de Cuenta contable", e);
                }

                helper.creationDate = model.paymentcreateon;
                helper.creationDateString = DateTimeUtils.ParseDatetoString(model.paymentcreateon);
                helper.aplicationDate = model.tblinvoice.invoicedate;
                helper.aplicationDateString = DateTimeUtils.ParseDatetoString(helper.aplicationDate);
                helper.Company = model.tblbankaccount.tblcompanies.idcompany;
                helper.CompanyName = model.tblbankaccount.tblcompanies.companyname;
                helper.CompanyOrder = (int)model.tblbankaccount.tblcompanies.companyorder;
                helper.InvoiceIdentifier = AccountsUtils.IdentifierComplete(model.tblinvoice.tblcompanies.companyshortname, model.tblinvoice.tblusers.tblprofilesaccounts.profileaccountshortame, model.tblinvoice.invoicenumber);
                //helper.invoiceitems = model.tblinvoice.tblinvoiceditem.Count() != 0 ? convertTbltoHelper(model.tblinvoice.tblinvoiceditem.ToList()) : null;
                list.Add(helper);
            }
            return list;
        }

        protected invoice makeInvoicebyFondo(tblbankaccount model, fondoModel helper)
        {
            invoice dochelper = new invoice();
            dochelper.currency = model.idcurrency;
            dochelper.company = model.idcompany;
            dochelper.applicationdatestring = helper.fechaEntregaString;
            return dochelper;
        }

        protected invoiceitems makeInvoicedItembyFondo(fondoModel helper, int BankAccntType)
        {
            invoiceitems docitemHelper = new invoiceitems();
            docitemHelper.type = BankAccntType;
            docitemHelper.ammount = helper.FondoFee;
            docitemHelper.description = helper.comments;
            docitemHelper.istax = false;
            docitemHelper.taxesammount = 0;
            docitemHelper.billidentifier = helper.comments;
            docitemHelper.supplier = 1;
            docitemHelper.supplierother = null;
            docitemHelper.singlexibitionpayment = true;

            return docitemHelper;
        }

        protected invoicepayment makePaymentbyFondo(fondoModel helper)
        {
            invoicepayment docpaymentHelper = new invoicepayment();
            docpaymentHelper.PaymentMethod = helper.FinancialMethod;
            docpaymentHelper.BankAccntType = (int)BankAccounType.Transferencias;
            docpaymentHelper.aplicationDateString = helper.fechaEntregaString;
            docpaymentHelper.chargedAmount = (decimal)helper.FondoFee;
            docpaymentHelper.authRef = helper.comments;

            return docpaymentHelper;
        }



        /************************ BUDGET LIMITS **************************************************/

        protected fondosmaxammountModel convertTbltoHelper(tblfondosmaxammount model)
        {
            // if (model == null) throw new Exception("No existe un limite definido para los criterios Seleccionados");
            fondosmaxammountModel obj = new fondosmaxammountModel();
            obj.FondosMax = model.idFondosMax;
            // obj.Hotel = model.idCompany;
            // obj.HotelName = model.tblcompanies.companyName;
            obj.PaymentMethod = model.idBAccount;
            obj.PaymentMethodName = model.tblbankaccount.baccountname;
            obj.Currency = model.tblbankaccount.tblcurrencies.idCurrency;
            obj.CurrencyName = model.tblbankaccount.tblcurrencies.currencyName;
            obj.fondosmaxDescription = model.fondosmaxDescription;
            obj.fondosmaxAmmount = model.fondosmaxAmmount;
            return obj;
        }

        protected void PrepareToSave(tblfondosmaxammount model, fondosmaxammountModel helper)
        {
            //model.idCompany = helper.Hotel;
            model.idBAccount = helper.PaymentMethod;
            model.fondosmaxAmmount = (decimal)helper.fondosmaxAmmount;
            model.fondosmaxDescription = helper.fondosmaxDescription;
            model.fondosmaxCreationDate = DateTime.Now;
        }

        protected void PrepareToUpdate(tblfondosmaxammount model, fondosmaxammountModel helper)
        {
            //model.idCompany = helper.Hotel;
            model.idBAccount = helper.PaymentMethod;
            model.fondosmaxAmmount = (decimal)helper.fondosmaxAmmount;
            model.fondosmaxDescription = helper.fondosmaxDescription;
        }

        protected IEnumerable<fondosmaxammountModel> convertTbltoHelper(IEnumerable<tblfondosmaxammount> list)
        {
            List<fondosmaxammountModel> listModel = new List<fondosmaxammountModel>();

            foreach (tblfondosmaxammount model in list)
            {
                fondosmaxammountModel obj = new fondosmaxammountModel();
                obj.FondosMax = model.idFondosMax;
                obj.Company = model.tblbankaccount.tblcompanies.idcompany;
                obj.CompanyName = model.tblbankaccount.tblcompanies.companyname;
                obj.PaymentMethod = model.idBAccount;
                obj.PaymentMethodName = string.Concat(model.tblbankaccount.tblbank.bankshortname, " ", model.tblbankaccount.baccountshortname);
                obj.Currency = model.tblbankaccount.tblcurrencies.idCurrency;
                obj.CurrencyName = model.tblbankaccount.tblcurrencies.currencyName;
                obj.fondosmaxDescription = model.fondosmaxDescription;
                obj.fondosmaxAmmount = model.fondosmaxAmmount;

                obj.editable = true;

                listModel.Add(obj);
            }
            return listModel;
        }


        protected IEnumerable<docpaymentpurchase> convertTbltoHelper(IEnumerable<tblpaymentspurchases> list)
        {
            List<docpaymentpurchase> listModel = new List<docpaymentpurchase>();

            foreach (tblpaymentspurchases model in list)
            {
                docpaymentpurchase obj = new docpaymentpurchase();
                var paymentTypeName = getPaymentMethodName((int)model.idPaymentType);
                var currencyName = getCurrencyName((int)model.idCurrency);
                var partnerName = getHotelPartenerName(model.tblpurchases.idPartner);
                obj.Payment = model.idPaymentPurchase;
                obj.Purchase = (int)model.tblpurchases.idPurchase;
                obj.Memberships = model.tblpurchases.idMembership;
                obj.PaymentMethod = (int)model.idPaymentType;
                obj.PaymentMethodName = paymentTypeName;
                obj.CurrencyPay = (int)model.idCurrency;
                obj.CurrencyPayName = currencyName;
                obj.paymentDate = (DateTime)model.paymentDate;
                obj.paymentDateString = DateTimeUtils.ParseDatetoString((DateTime)model.paymentDate);
                obj.paymentAmount = (decimal)model.paymentCost;
                obj.paymentAmountString = MoneyUtils.ParseDecimalToString((decimal)model.paymentCost);
                obj.Hotel = model.tblpurchases.idPartner;
                obj.HotelName = partnerName;
                obj.authRef = model.authCode;
                obj.bankStatementLinked = false;

                listModel.Add(obj);
            }
            return listModel;
        }

        protected IEnumerable<docpaymentpurchase> convertTbltoHelper(IEnumerable<tblbatchdetail> list)
        {
            List<docpaymentpurchase> listModel = new List<docpaymentpurchase>();

            foreach (tblbatchdetail item in list)
            {
                docpaymentpurchase obj = new docpaymentpurchase();
                var paymentTypeName = getPaymentMethodName((int)item.tblbatch.idPaymentForm);
                //var memberName = getMemberName(model.idMembership);
                //var currencyName = "USD";
                //var partnerName = getHotelPartenerName(item.tblpurchases.idPartner);
                obj.Payment = item.tblbatch.idBatch;
                obj.Purchase = (int)item.idPurchase;
                obj.Memberships = item.tblpurchases.idMembership;
                obj.PaymentMethod = (int)item.tblbatch.idPaymentForm;
                obj.PaymentMethodName = paymentTypeName;
                obj.CurrencyPay = (int)Currencies.US_Dollar;
                obj.CurrencyPayName = item.tblbatch.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
                obj.paymentDate = item.tblbatch.datePay == null ? (DateTime)item.tblpurchases.purchaseDate : (DateTime)item.tblbatch.datePay;
                obj.paymentDateString = DateTimeUtils.ParseDatetoString(obj.paymentDate);
                obj.paymentAmount = item.batchmonto == null ? getPrefixAmmount(item.tblpurchases.tblmemberships.membershipNumberPref, item.tblpurchases.tblpartners.idHotelChain) : (decimal)item.batchmonto;
                obj.paymentAmountString = MoneyUtils.ParseDecimalToString(obj.paymentAmount);
                obj.Hotel = item.tblpurchases.idPartner;
                obj.HotelName = item.tblpurchases.tblpartners.partnerName;
                obj.authRef = "Post-Batch";//string.Format("Datos de Purchase: Cliente: {0}, Hotel Partner: {1}", memberName, partnerName);
                obj.bankStatementLinked = false;

                listModel.Add(obj);
            }




            return listModel;
        }

        protected IEnumerable<docpaymentpurchase> convertTbltoHelper(IEnumerable<tblbatchdetailpre> list)
        {
            List<docpaymentpurchase> listModel = new List<docpaymentpurchase>();


                foreach (tblbatchdetailpre item in list)
                {
                    docpaymentpurchase obj = new docpaymentpurchase();
                    var paymentTypeName = getPaymentMethodName((int)item.tblbatch.idPaymentForm);
                    //var memberName = getMemberName(model.idMembership);
                    //var currencyName = "USD";
                    //var partnerName = getHotelPartenerName((int)item.idPartner);
                    obj.Payment = (int)item.idBatch;
                    obj.Purchase = 0;
                    obj.Memberships = 0;
                    obj.PaymentMethod = (int)item.tblbatch.idPaymentForm;
                    obj.PaymentMethodName = paymentTypeName;
                    obj.CurrencyPay = (int)Currencies.US_Dollar;
                    obj.CurrencyPayName = item.tblbatch.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
                    obj.paymentDate = (DateTime)item.tblbatch.datePay;
                    obj.paymentDateString = DateTimeUtils.ParseDatetoString((DateTime)item.tblbatch.datePay);
                    obj.paymentAmount = (decimal)item.tblbatch.batchMonto;
                    obj.paymentAmountString = MoneyUtils.ParseDecimalToString(obj.paymentAmount);
                    obj.Hotel = (int)item.idPartner;
                    obj.HotelName = item.tblpartners.partnerName;
                    obj.authRef = "Pre-Batch";//string.Format("Datos de Purchase: Cliente: {0}, Hotel Partner: {1}", memberName, partnerName);
                    obj.bankStatementLinked = false;

                    listModel.Add(obj);
                }


            
            return listModel;
        }

        protected IEnumerable<docpaymentpurchase> convertTbltoHelper(IEnumerable<tblpurchases> list)
        {
            List<docpaymentpurchase> listModel = new List<docpaymentpurchase>();

            foreach (tblpurchases model in list)
            {
                docpaymentpurchase obj = new docpaymentpurchase();
                var paymentTypeName = getPaymentMethodName((int)PaymentMethods_Bank_Report.Transfer);
                //var memberName = getMemberName(model.idMembership);
                var currencyName = "USD";
                var partnerName = getHotelPartenerName(model.idPartner);
                obj.Payment = model.idPurchase;
                obj.Purchase = (int)model.idPurchase;
                obj.Memberships = model.idMembership;
                obj.PaymentMethod = (int)PaymentMethods_Bank_Report.Transfer;
                obj.PaymentMethodName = paymentTypeName;
                obj.CurrencyPay = (int)Currencies.US_Dollar;
                obj.CurrencyPayName = currencyName;
                obj.paymentDate = (DateTime)model.purchaseDate;
                obj.paymentDateString = DateTimeUtils.ParseDatetoString((DateTime)model.purchaseDate);
                obj.paymentAmount = getPrefixAmmount(model.tblmemberships.membershipNumberPref, model.tblpartners.idHotelChain);
                obj.paymentAmountString = MoneyUtils.ParseDecimalToString(obj.paymentAmount);
                obj.Hotel = model.idPartner;
                obj.HotelName = partnerName;
                obj.authRef = "";//string.Format("Datos de Purchase: Cliente: {0}, Hotel Partner: {1}", memberName, partnerName);
                obj.bankStatementLinked = false;

                listModel.Add(obj);
            }
            return listModel;
        }

        protected IEnumerable<docpaymentreserv> convertTbltoHelper(IEnumerable<tblreservationspayment> list)
        {
            List<docpaymentreserv> listModel = new List<docpaymentreserv>();

            foreach (tblreservationspayment model in list)
            {
                docpaymentreserv obj = new docpaymentreserv();
                var currencyName = getCurrencyName((int)model.idCurrency);
                var paymentTypeName = getPaymentMethodName((int)model.idPaymentType);
                var partnerName = getHotelPartenerName((int)model.tblreservations.idPartner);

                obj.ReservationPayment = model.idReservationPayment;
                obj.Reservation = (int)model.idReservation;
                obj.reservationPaymentQuantity = model.reservationPaymentCost == null ? 0 : (decimal)model.reservationPaymentCost;
                obj.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(obj.reservationPaymentQuantity);
                obj.reservationPaymentDate = (DateTime)model.reservationPaymentDate;
                obj.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.reservationPaymentDate);
                obj.CurrencyPay = (int)model.idCurrency;
                obj.CurrencyPayName = currencyName;
                obj.PaymentMethod = (int)model.idPaymentType;
                obj.PaymentMethodName = paymentTypeName;
                obj.Hotel = (int)model.tblreservations.idPartner;
                obj.HotelName = partnerName;
                obj.HotelOrder = (int)model.tblreservations.idReservation;
                obj.authRef = string.Format("Datos de reserva: Cliente: {0}, Hotel Partner: {1}", model.tblreservations.reservationGuestName, partnerName);
                obj.bankStatementLinked = false;
                obj.authcode = model.authCode;

                listModel.Add(obj);
            }
            return listModel;
        }

        protected IEnumerable<docpaymentparentreserv> convertTbltoHelper(IEnumerable<tblreservationsparentpayment> list)
        {
            List<docpaymentparentreserv> listModel = new List<docpaymentparentreserv>();

            foreach (tblreservationsparentpayment model in list)
            {
                docpaymentparentreserv obj = new docpaymentparentreserv();
                var currencyName = getCurrencyName((int)model.idCurrency);
                var paymentTypeName = getPaymentMethodName((int)model.idPaymentType);
                var partnerName = getHotelChainName(model.tblreservationsparent.idPartner);

                obj.ReservationPayment = model.idReservationParentPayment;
                obj.Reservation = (int)model.idReservationParent;
                obj.reservationPaymentQuantity = model.reservationPaymentCost == null ? 0 : (decimal)model.reservationPaymentCost;
                obj.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(obj.reservationPaymentQuantity);
                obj.reservationPaymentDate = (DateTime)model.reservationPaymentDate;
                obj.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.reservationPaymentDate);
                obj.CurrencyPay = (int)model.idCurrency;
                obj.CurrencyPayName = currencyName;
                obj.PaymentMethod = (int)model.idPaymentType;
                obj.PaymentMethodName = paymentTypeName;
                obj.Hotel = model.tblreservationsparent.idPartner != null ? (int)model.tblreservationsparent.idPartner : 0;
                obj.HotelName = partnerName;
                obj.HotelOrder = (int)model.tblreservationsparent.idReservationParent;
                obj.authRef = string.Format("Datos de reserva sin membresia: Cliente: {0}, Hotel Partner: {1}", model.tblreservationsparent.reservationParentGuestName, partnerName);
                obj.bankStatementLinked = false;
                obj.authcode = model.authCode;

                listModel.Add(obj);
            }
            return listModel;
        }

        public static incomepayment convertTbltoHelper(tblincomemovement item)
        {
            incomepayment helper = new incomepayment(item.idincomeMovement, item.idincome, "", item.tblincome.tblcompanies.tblsegment.idsegment, item.tblincome.tblcompanies.tblsegment.segmentname, item.tblincome.idcompany, item.tblincome.tblcompanies.companyshortname, (int)item.tblincome.tblcompanies.companyorder,
                item.tblbankaccount.idbaccount, string.Format("{0} {1} {2} {3}", item.tblbankaccount.baccountshortname, item.tblbankaccount.tblbank.bankshortname, item.tblbankaccount.tblcurrencies.currencyAlphabeticCode, item.tblbankaccount.tblcompanies.companyshortname), item.idbankaccnttype, item.tblbankprodttype.bankprodttypeshortname,
                item.tblbankaccount.tblcurrencies.idCurrency, item.tblbankaccount.tblcurrencies.currencyAlphabeticCode, item.idtpv == null ? 0 : (int)item.idtpv, item.idtpv == null ? "" : item.tbltpv.tpvidlocation, item.incomemovcard, item.incomemovchargedamount, item.incomemovauthref, item.incomemovcreationdate, item.incomemovapplicationdate, item.incomemovauthcode);
            helper.identifier = AccountsUtils.IdentifierComplete(item.tblincome.tblcompanies.companyshortname, item.tblincome.tblusers.tblprofilesaccounts.profileaccountshortame, item.tblincome.incomenumber);
            return helper;
        }

        public static List<incomepayment> convertTbltoHelper(List<tblincomemovement> items)
        {
            List<incomepayment> list = new List<incomepayment>();
            var toread = items.ToArray();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                incomepayment helper = new incomepayment();
                helper = convertTbltoHelper(toread[i]);
                helper.index = i;
                helper.row = i + 1;
                list.Add(helper);
            }
            return list;
        }

        public static decimal getPrefixAmmount(string prefix, int hotelchain)
        {
            var amount = 0m;
            var lst = UnitOfWork.PrefixesRepository.Get(x => x.prefixAbbrev == prefix && x.idHotelChain == hotelchain && x.prefixActive == Globals.ActiveRecord, null, "").FirstOrDefault();
            if (lst != null)
            {
                amount = lst.PartnerPrice != null ? (decimal)lst.PartnerPrice : 0m;
            }
            return amount;
        }

        public static string getHotelPartenerName(int partner)
        {
            var lst = UnitOfWork.PartnersRepository.Get(x => x.idPartner == partner && x.partnerActive == Globals.ActiveRecord, null, "").FirstOrDefault();

            return lst.partnerName;
        }

        public static string getHotelChainName(int? partner)
        {
            var name = "";
            if (partner != null)
            {
                var lst = UnitOfWork.HotelChainsRepository.Get(x => x.idHotelChain == partner && x.hotelChainActive == Globals.ActiveRecord, null, "").FirstOrDefault();
                name = lst.hotelChainName;
            }

            return name;
        }

        public static string getPaymentMethodName(int idPaymentMethod)
        {
            var lst = UnitOfWork.PaymentFormsRepository.Get(x => x.idPaymentForm == idPaymentMethod && x.paymentFormActive == Globals.ActiveRecord, null, "").FirstOrDefault();

            return lst.paymentFormName;
        }

        public static string getCurrencyName(int currency)
        {
            var lst = UnitOfWork.CurrencyRepository.Get(x => x.idCurrency == currency, null, "").FirstOrDefault();

            return lst.currencyAlphabeticCode;
        }

        public static string getMemberName(int? memberships)
        {
            string nameComplet = "";
            var lst = UnitOfWork.MembersRepository.Get(x => x.idMembership == memberships && x.memberActive == Globals.activeRecord && x.memberFolio == (int)Record.Activeted, null, "").FirstOrDefault();

            if (lst != null)
            {
                var name = lst.memberFirstName != null ? lst.memberFirstName : "";
                var lastname = lst.memberLastName != null ? lst.memberLastName : "";

                nameComplet = name + " " + lastname;
            }
            return nameComplet;
        }


        protected IEnumerable<docpaymentreserv> convertTbltoHelper(IEnumerable<tblreservations> listmodel)
        {
            List<docpaymentreserv> list = new List<docpaymentreserv>();
            foreach (tblreservations model in listmodel)
            {
                docpaymentreserv helper = new docpaymentreserv();
                helper = ConvertTbltoHelper(model);
                list.Add(helper);
            }


            return list;
        }

        public static docpaymentreserv ConvertTbltoHelper(tblreservations model)
        {
            UnitOfWork unity = new UnitOfWork();
            var methodPayment = getPaymentMethodName((int)ReservaWeb.PaymentType);
            docpaymentreserv helper = new docpaymentreserv();
            helper.ReservationPayment = model.idReservation;
            helper.Reservation = model.idReservation;
            helper.reservationPaymentQuantity = model.reservationSellPrice == null ? 0m : (decimal)model.reservationSellPrice;
            helper.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(helper.reservationPaymentQuantity);
            helper.reservationPaymentDate = (DateTime)model.purchaseDate;
            helper.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.purchaseDate);
            helper.CurrencyPay = (int)Currencies.US_Dollar;
            helper.Hotel = model.idPartner != null ? (int)model.idPartner : 0;
            helper.HotelName = model.tblpartners.partnerName;
            helper.CurrencyPayName = "USD";
            helper.CurrencytoExchange = (int)Currencies.US_Dollar;
            helper.CurrencytoExchangeName = Currencies.US_Dollar.ToString().Replace('_', ' ');
            helper.CurrencytoExchangeRate = 0m;
            helper.PaymentMethod = (int)ReservaWeb.PaymentType;
            helper.PaymentMethodName = methodPayment;
            helper.authRef = string.Format("Datos de reserva web: Cliente: {0} , Hotel: {1}", model.reservationGuestName, model.tblpartners.partnerName);
            helper.bankStatementLinked = false;
            helper.authcode = "";

            return helper;
        }
    }
}