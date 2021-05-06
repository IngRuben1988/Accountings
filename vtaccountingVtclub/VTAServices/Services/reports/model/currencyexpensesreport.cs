using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.bankreconciliation;
using VTAworldpass.VTAServices.Services.bankreconciliation.implements;
using VTAworldpass.VTAServices.Services.bankreconciliation.model;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.invoices.model;
using static VTAworldpass.VTACore.Collections.CollectionsUtils;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTAServices.Services.reports.model
{
    public class currencyexpensesreport
    {

        private readonly UnitOfWork unity = new UnitOfWork();

        public int currency { get; set; }
        public string currencyname { get; set; }
        public int year { get; set; }
        public int typereport { get; set; }
        public string typereportname { get; set; }
        public int countexpensereport { get; set; }
        public List<expensesreport> expensesreport { get; set; } // Reportes de E-R en monedas, año, mes y empresa solicitadas
        public List<tblcurrencyexchange> currencyexchange { get; set; } // tios de cambios utilizados en caso que no se haya capturado el tipo de cambio en UPSCL y RESERV


        public currencyexpensesreport()
        { }

        public currencyexpensesreport(int currency, int year, int typeReport, bool inilist)
        {
            this.currency = currency;
            this.year = year;
            this.typereport = typereport;
            if (inilist == true) { this.expensesreport = new List<expensesreport>(); }
            this.currencyexchange = unity.CurrenciesExchangeRepository.Get().Select(v => new tblcurrencyexchange() { currencyexchangefrom = v.currencyexchangefrom, currencyexchangeto = v.currencyexchangeto, currencyexchangerate = v.currencyexchangerate }).ToList();
        }

        public currencyexpensesreport(int currency, string currencyname, int year, int typereport, string typeReportname, bool inilist)
        {
            this.currency = currency;
            this.currencyname = currencyname;
            this.year = year;
            this.typereport = typereport;
            this.typereportname = typeReportname;
            if (inilist == true) { this.expensesreport = new List<expensesreport>(); }
            this.currencyexchange = unity.CurrenciesExchangeRepository.Get().Select(v => new tblcurrencyexchange() { currencyexchangefrom = v.currencyexchangefrom, currencyexchangeto = v.currencyexchangeto, currencyexchangerate = v.currencyexchangerate }).ToList();
        }

        // public

        public void addExpenseReport(expensesreport expensesReport)
        {
            var x = this.searchExpenseReport(this.expensesreport, expensesReport.month);
            if (x == null)
            {
                if (this.expensesreport.Count == 0)
                {
                    this.expensesreport.Add(expensesReport);
                }
                else if (this.expensesreport == null)
                {
                    this.expensesreport = new List<expensesreport>();
                    this.expensesreport.Add(expensesReport);
                }
                else
                {
                    this.expensesreport.Add(expensesReport);
                }
            }
            else
            {
            }
        }

        public void addExpenseReportGroup(expensesreport expensesReport)
        {
            var x = this.searchExpenseReport(this.expensesreport, expensesReport.month, expensesReport.year, expensesReport.currency, expensesReport.typereport);

            if (x == null)
            {
                if (this.expensesreport.Count == 0)
                {
                    this.expensesreport = (List<expensesreport>)IListUtils.AddSingle(this.expensesreport, new expensesreport(expensesReport.year, expensesReport.month, expensesReport.monthname, 0, "", "", expensesReport.currency, expensesReport.currencyname, expensesReport.typereport, expensesReport.expensereporitem.ToList()));
                }
                else if (this.expensesreport == null)
                {
                    this.expensesreport = (List<expensesreport>)IListUtils.AddSingle(this.expensesreport, new expensesreport(expensesReport.year, expensesReport.month, expensesReport.monthname, 0, "", "", expensesReport.currency, expensesReport.currencyname, expensesReport.typereport, expensesReport.expensereporitem.ToList()));
                }
                else
                {
                    this.expensesreport = (List<expensesreport>)IListUtils.AddSingle(this.expensesreport, new expensesreport(expensesReport.year, expensesReport.month, expensesReport.monthname, 0, "", "", expensesReport.currency, expensesReport.currencyname, expensesReport.typereport, expensesReport.expensereporitem.ToList()));
                }
            }
            else
            {
                x.addExpenseReporItem(expensesReport.expensereporitem);
            }
        }

        public void addExpenseReportGroup(List<expensesreport> list)
        {
            foreach (expensesreport model in list)
            {
                addExpenseReportGroup(model);
            }
        }

        public void AddExpenseReporttoExchange(expensesreport expensesReport, int idToExchangeCurrency)
        {
            var currencyToExchange = unity.CurrencyRepository.Get(c => c.idCurrency == idToExchangeCurrency).FirstOrDefault();
            if (currencyToExchange != null)
            {
                expensesReport.currency = currencyToExchange.idCurrency;
                expensesReport.currencyname = currencyToExchange.currencyAlphabeticCode;
                ///////////////////////////////////////////////////////////////////////////////////////////////
                // Processing ---> docpaymentresevations 
                ///////////////////////////////////////////////////////////////////////////////////////////////
                /*
                var tmpreserv = expensesReport.docpaymentreservexpreport.ToArray();
                for (int i = 0; i <= expensesReport.docpaymentreservexpreport.Count() - 1; i++)
                {
                    if (tmpreserv[i].CurrencytoExchangeRate != 0 && tmpreserv[i].CurrencytoExchangeRate != 1.0m)
                    {
                        tmpreserv[i].CurrencyPay = currencyToExchange.idCurrency;
                        tmpreserv[i].CurrencyPayName = currencyToExchange.currencyAlphabeticCode;
                        tmpreserv[i].reservationPaymentQuantity = tmpreserv[i].reservationPaymentQuantity * tmpreserv[i].CurrencytoExchangeRate;
                        tmpreserv[i].reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(tmpreserv[i].reservationPaymentQuantity);
                        tmpreserv[i].CurrencytoExchange = 0;
                        tmpreserv[i].CurrencytoExchangeName = "";
                    }
                    else
                    {
                        var exchangeRateGral = this.currencyexchange.Where(x => x.currency_from == tmpreserv[i].CurrencyPay && x.currency_to == currencyToExchange.idCurrency).FirstOrDefault();
                        if (exchangeRateGral != null)
                        {
                            tmpreserv[i].CurrencyPay = currencyToExchange.idCurrency;
                            tmpreserv[i].CurrencyPayName = currencyToExchange.currencyAlphabeticCode;
                            tmpreserv[i].reservationPaymentQuantity = tmpreserv[i].reservationPaymentQuantity * (decimal)exchangeRateGral.currency_rate;
                            tmpreserv[i].reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(tmpreserv[i].reservationPaymentQuantity);
                            tmpreserv[i].CurrencytoExchange = 0;
                            tmpreserv[i].CurrencytoExchangeName = "";
                        }
                    }
                }

                expensesReport.docpaymentreservexpreport = tmpreserv;
                expensesReport.expenseReporItem.Clear();
                expensesReport.convertingAllDatasourceReportItem();
                */

                ///////////////////////////////////////////////////////////////////////////////////////////////
                // Processing ---> docpaymentresevations 
                ///////////////////////////////////////////////////////////////////////////////////////////////

                this.addExpenseReportGroup(expensesReport);

            }
            else
            {
                throw new Exception(string.Format("No se puede transformar a la moneda solicitada [{0}].", idToExchangeCurrency));
            }
        }

        public void AddExpenseReporttoExchange(List<expensesreport> expensesReport, int idToExchangeCurrency)
        {
            foreach (expensesreport helper in expensesReport)
            {
                this.AddExpenseReporttoExchange(helper, idToExchangeCurrency);
            }

            this.expensesreport.ElementAt(0).DeleteSourcedata();
            this.expensesreport.ElementAt(0).DeleteReporItemDetails();
        }

        // private
        private expensesreport searchExpenseReport(List<expensesreport> lstexpenses, int month)
        {
            return lstexpenses.Where(y => y.month == month).FirstOrDefault();
        }

        private expensesreport searchExpenseReport(List<expensesreport> lstexpenses, int month, int year)
        {
            return lstexpenses.Where(y => y.month == month && y.year == year).FirstOrDefault();
        }

        private expensesreport searchExpenseReport(List<expensesreport> lstexpenses, int month, int year, int company)
        {
            return lstexpenses.Where(y => y.month == month && y.year == year && y.company == company).FirstOrDefault();
        }

        private expensesreport searchExpenseReport(List<expensesreport> lstexpenses, int month, int year, int currency, int typereport)
        {
            return lstexpenses.Where(y => y.month == month && y.year == year && y.currency == currency && y.typereport == typereport).FirstOrDefault();
        }

        private expensesreport searchExpenseReport_(List<expensesreport> lstexpenses, int month, int year, int company, int currency)
        {
            return lstexpenses.Where(y => y.month == month && y.year == year && y.company == company && y.currency == currency).FirstOrDefault();
        }

        private expensesreport searchExpenseReport(List<expensesreport> lstexpenses, int month, int year, int company, int currency, int typereport)
        {
            return lstexpenses.Where(y => y.month == month && y.year == year && y.company == company && y.currency == currency && y.typereport == typereport).FirstOrDefault();
        }

    }

    public class expensesreport
    {
        public int year { get; set; }
        public int month { get; set; }
        public string monthname { get; set; }
        public int company { get; set; }
        public string companyname { get; set; }
        public string companynamegroup { get; set; }
        public int currency { get; set; }
        public string currencyname { get; set; }
        public int typereport { get; set; }
        public string typereportname { get; set; }
        public decimal totalincome { get; set; }
        public string totalincomestring { get; set; }
        public decimal totalexpense { get; set; }
        public string totalexpensestring { get; set; }
        public virtual IList<int> hoteldevelopment { get; set; }
        public IList<expensereporitem> expensereporitem { get; set; }

        private IList<accountl4methodspay> accountl4methodspay { get; set; }
        private IList<expensereportsourcedata> expensereportsourcedata { get; set; }



        public IList<incomeitem> incomeitems { get; set; }
        public IList<invoiceitems> invoiceitems { get; set; }
        public IList<docpaymentpurchasexpreport> docpaymentpurchasexpreport { get; set; }
        public IList<docpaymentreservexpreport> docpaymentreservexpreport { get; set; }
        private IList<bankreconciliationModel> bankreconciliation { get; set; }

        private readonly UnitOfWork unity = new UnitOfWork();
        private readonly IBankReconciliationServices bankreconciliationService = new BankReconciliationServices();


        public expensesreport()
        { }

        public expensesreport(int year, int month, string monthname, int company, string companyname, string companynamegroup, int currency, string currencyname, int typereport, List<expensereporitem> expensereporitem)
        {
            this.year = year;
            this.month = month;
            this.monthname = monthname;
            this.company = company;
            this.companyname = companyname;
            this.currency = currency;
            this.currencyname = currencyname;
            this.typereport = typereport;
            this.typereportname = typereportname;
            this.expensereporitem = expensereporitem;
            this.calculatetotalincomeexpenses(expensereporitem);

            this.companynamegroup = companynamegroup;
            this.addOrderExpenseReporItem();
        }


        public expensesreport(int year, int month, string monthname, int company, string companyname, int currency, string currencyname, int typereport, string typereportname, bool keepdatasource, bool keepreporitemdetails, ExpenseReport typexpense)
        {
            // Initializing data
            this.year = year;
            this.month = month;
            this.monthname = monthname;
            this.company = company;
            this.companyname = companyname;
            this.currency = currency;
            this.currencyname = currencyname;
            this.typereport = typereport;
            this.typereportname = typereportname;

            this.invoiceitems = new List<invoiceitems>();
            this.docpaymentpurchasexpreport = new List<docpaymentpurchasexpreport>();
            this.docpaymentreservexpreport = new List<docpaymentreservexpreport>();
            this.incomeitems = new List<incomeitem>();
            this.expensereporitem = new List<expensereporitem>();
            this.hoteldevelopment = new List<int>();
            this.expensereportsourcedata = new List<expensereportsourcedata>();
            this.accountl4methodspay = new List<accountl4methodspay>();
            this.bankreconciliation = new List<bankreconciliationModel>();

            // Getting data Company to configurations
            var _company = this.unity.CompaniesRepository.Get(t => t.idcompany == company).FirstOrDefault();

            // Get TPV´S for query
            var _baccount = _company.tblbankaccount.Where(ba => ba.idcompany == company).ToList();
            var _tpv = _baccount.SelectMany(tp => tp.tblbaccounttpv.Select(ban => ban.idtpv).ToList()).ToArray();

            //var _companygroupdevelopment = this.unity.CompanyGroupDevelopmentRepository.Get(de => de.idCompanyParent == company).ToList();
            // Getting configurations
            this.companyname = _company.companyname;
            // Getting Companies-Hotel configurations
            var grouplist = _company.tblcompanygroupdevelopment.Select(c => c.idCompanyChild).ToList();
            var _companydevelopment = this.unity.CompanyDevelopmentRepository.Get(de => grouplist.Contains(de.idCompany)).ToList();
            var lst = _companydevelopment.Select(ch => ch.idHotelChain).ToList();
            //var partnerlist = _companydevelopment.Select(c => c.tblhotelchains.idHotelChain).ToList();
            //var list = partner.Select(i => i.Select(y => y.idPartner).ToArray()).ToList();
            //List<int> partnerlist = list.Cast<int>().ToList();
            this.hoteldevelopment = IListUtils.AddList(this.hoteldevelopment, lst/*_company.tblcompanydevelopment.Select(d => d.idHotelChain).ToList()*/);
            // Getting GroupName
            //var groupcompany = unity.GroupCompaniesRepository.Get(x => x.tblcompanies1.idCompany == _company, null, "").FirstOrDefault();
            // this.companynamegroup = groupcompany != null ? groupcompany.groupcompaniesDescription : "";
            // Getting Sorcedata Configurations
            this.expensereportsourcedata = IListUtils.AddListtoList(this.expensereportsourcedata, (_company.tblexpensereportsourcedata.Select(x => new expensereportsourcedata(x.idexpensereportsourcedata, x.idcompany, x.idsourcedata, x.tblexpensereportsourcedatatypes.Select(y => y.idtype).ToList()))).ToList());
            // Getting Accl4-PaymentMethods [by TypeReport] configurations
            this.accountl4methodspay = IListUtils.AddListtoList(this.accountl4methodspay, unity.Accl4PaymentsRepository.Get(x => (x.tblaccountsl4.tblsegmentaccl4.Select(c => c.idsegment).Contains((int)_company.idsegment)) && (x.tblaccountsl4.tblaccountl4accounttype.Select(y => y.idaccounttypereport).ToList()).Contains(typereport)).Select(x =>
              new accountl4methodspay(x.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1, x.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl2, x.tblaccountsl4.tblaccountsl3.idaccountl3,
              x.tblaccountsl4.idAccountl4, x.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1description, x.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2description, x.tblaccountsl4.tblaccountsl3.accountl3description,
              x.tblaccountsl4.accountl4description, x.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1order, x.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2order, x.tblaccountsl4.tblaccountsl3.accountl3order, x.tblaccountsl4.accountl4order,
              x.tblPaymentForms.idPaymentForm, x.tblPaymentForms.paymentFormName)).ToList());


            // Getting Sourcedata from DB
            foreach (expensereportsourcedata helper in this.expensereportsourcedata)
            {
                switch (helper.sourcedata)
                {
                    case 1: // Ingresos 
                        {
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualIncomeItems(year, month, company, currency, typereport).ToList()));
                        }
                        break;
                    case 2: // Ingresos Movimientos
                        {

                        }
                        break;
                    case 3: // Facturas  / Invoice
                        {
                            // this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualDocitems(year, month, Company, Currency, TypeReport)));
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualDocitems(year, month, company, currency, typereport)));
                        }
                        break;
                    case 4: // Pagos / Payments
                        {
                            // this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPayments(year, month, currency, helper.Types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), this.AccountL4MethosPay));
                        }
                        break;
                    case 5: // Fondos
                        {
                            // this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualFondos(year, month, currency, helper.Types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), this.AccountL4MethosPay));
                        }
                        break;
                    case 6: // Conciliaciones
                        {

                            var _hotel = this.hoteldevelopment.FirstOrDefault() != 0 ? this.hoteldevelopment.First() : 0;
                            var _Company = this.hoteldevelopment.FirstOrDefault() != 0 ? 0 : this.company;

                            var _a = this.bankreconciliationService.getBakReconcilitions(month, year, 0, this.currency, 0, _Company, 0, _hotel, BankAccountReconcilitionStatus.Completo, true, false);
                            this.addSource(_a);

                        }
                        break;
                    case 7: // Reservations Payments
                        {
                            // int year, int month, int idCurrency, int[] idPaymentMethodReserv
                            /*var _X = this.getAnualPaymentsRESERV(year, month, currency, helper.types.ToArray(), this.hoteldevelopment.ToArray()).ToList();
                            var _Y = GeneralModelHelper.ApplyPaymethodtoAccl4Convert(_X.ToList(), accountl4methodspay);
                            this.addSource(_Y);*/
                            //Reservations Payments
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsRESERVAS(year, month, currency, _tpv, helper.types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), accountl4methodspay));                            
                            // Reservas WEB
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsRESERVAS_WEB(year, month, currency, _tpv, helper.types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), accountl4methodspay));
                            //Reservations Parent Payments
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsRESERVAS_PARENT(year, month, currency, _tpv, helper.types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), accountl4methodspay));

                            Log.Info("Se realizó la conulta this.getAnualPaymentsRESERVAS, this.getAnualPaymentsRESERVAS_WEB, this.getAnualPaymentsRESERVAS_PARENT (year, month, currency, helper.types.ToArray(),this.hoteldevelopment.ToArray()).ToList(); ");
                        }
                        break;
                    case 8: // Pagos Membresias
                        {

                            // Purchases Nuevas
                            //this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsPurchaseNew(year, month, currency, _tpv, helper.types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), accountl4methodspay));

                            // Purchases Renovadas
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsPurchase(year, month, currency, _tpv, helper.types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), accountl4methodspay));

                            //Post-batch
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsPurchaseBatchDetail(year, month, company, currency, helper.types.ToArray()).ToList(), accountl4methodspay));
                            // Pre-batch
                            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsPurchaseBatchDetailPre(year, month, company, currency, helper.types.ToArray()).ToList(), accountl4methodspay));

                            // Purchases Upgrades
                            //this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.getAnualPaymentsPurchaseUpgrade(year, month, currency, _tpv, helper.types.ToArray(), this.hoteldevelopment.ToArray()).ToList(), accountl4methodspay));

                        }
                        break;
                    default:
                        break;
                }
            }

            switch ((int)typexpense)
            {

                case (int)ExpenseReport.Expenses:
                    {
                        break;
                    }
                case (int)ExpenseReport.ExpensesConliationsIn:
                    {

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GETTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        // IList<docpaymentupsclexpreport> toDepTranslUpscl = new List<docpaymentupsclexpreport>();
                        // toDepTranslUpscl = IListUtils.AddList(toDepTranslUpscl, docpaymentupsclexpreport.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        // toDepTranslUpscl = IListUtils.AddList(toDepTranslUpscl, docpaymentupsclexpreport.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());

                        // IList<docpaymentreservexpreport> toDepTransReserv = new List<docpaymentreservexpreport>();
                        // toDepTransReserv = IListUtils.AddList(toDepTransReserv, docpaymentreservexpreport.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        // toDepTransReserv = IListUtils.AddList(toDepTransReserv, docpaymentreservexpreport.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());

                        // this.docpaymentupsclexpreport = toDepTranslUpscl;
                        // this.docpaymentreservexpreport = toDepTransReserv;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////// DELETING UPSCL AND RESERVATIONS FROM RECONCILIATIONS ////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        // Tomando las conciliaciones actuales
                        bankreconciliationModel[] reconciliations = new bankreconciliationModel[] { };
                        reconciliations = this.bankreconciliation.ToArray();

                        /*List<docpaymentupsclexpreport> toEvalUpsc = new List<docpaymentupsclexpreport>();
                        toEvalUpsc = this.docpaymentupsclexpreport.ToList();

                        List<docpaymentreservexpreport> toEvalReserv = new List<docpaymentreservexpreport>();
                        toEvalReserv = this.docpaymentreservexpreport.ToList();

                        // Getting bankreconciliations by Hotel, currency, month, year, complete
                        //var _hotel = this.HotelVTH.FirstOrDefault() != 0 ? this.HotelVTH.First() : 0;
                        //reconciliations = this.bankreconciliationService.getBakReconcilitions(month, year, 0, this.Currency, 0, this.Company,0, _hotel, BankAccountReconcilitionStatus.Completo, true, false).ToArray();

                        // Deleting bank reconciliations Upscales and Reservations

                        for (int i = 0; i <= reconciliations.Count() - 1; i++)
                        {
                            if (reconciliations[i].docpaymentupsclitems != null || reconciliations[i].docpaymentupsclitems.Count() != 0)
                            {
                                foreach (docpaymentupscl model in reconciliations[i].docpaymentupsclitems)
                                {
                                    toEvalUpsc.RemoveAll(c => c.Payment == model.Payment);
                                }
                            }

                            if (reconciliations[i].docpaymentreservitems != null || reconciliations[i].docpaymentreservitems.Count() != 0)
                            {
                                foreach (docpaymentreserv model in reconciliations[i].docpaymentreservitems)
                                {
                                    toEvalReserv.RemoveAll(v => v.ReservationPayment == model.ReservationPayment);
                                }
                            }
                        }*/


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////// GENERATING CONCILIATIONS FEES to DOCITEM INVOICE  ///////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                        // Generando arreglo de todas las cuentas bancarias en el arreglo de reconciliations
                        int[] _bankaccountint = new int[] { };
                        _bankaccountint = reconciliations.Select(c => c.baccount).Distinct().ToArray();

                        // lista vacia para generar los DOcitems  de egresos de comisiones de las conciliaciones
                        List<invoiceitems> bankAccountsDocItems = new List<invoiceitems>();

                        // Recorriendo las cuentas bancarias para buscar todos los registro de la misma en las conciliaciones
                        foreach (int i in _bankaccountint)
                        {
                            // Inicializando objeto docitems //Corresponden a gastos 
                            invoiceitems item = new invoiceitems();

                            //decimal interchange = 0.0m; interchange = reconciliations.Where(c => c.baccount == i).Sum(v => v.bankstatementInterchangesCharges).Value;
                            decimal fee = 0.0m; fee = reconciliations.Where(c => c.baccount == i).Sum(v => v.bankstatementBankFee).Value;
                            //decimal charguespayments = 0.0m; charguespayments = reconciliations.Where(c => c.baccount == i).Sum(v => v.bankstatementChargesPayments).Value;

                            // Registro para tomar oslo detalles, nombres
                            bankreconciliationModel details = reconciliations.Where(f => f.baccount == i).First();
                            // Diccionario donde se encuentra las cuentas contable a donde se direccionaran las comisiones de conciliaciones por segmentos
                            var _accl4conciliations = unity.ConciliationsFeeAccountRepository.Get(x => x.tblsegment.idsegment == _company.idsegment, null, "").FirstOrDefault();

                            if (_accl4conciliations != null) // Si se encuentra el registor en el paso anterior se genera los datos de cuenta contable y seran contabilizados en el E-R
                            {
                                item.accountl1 = _accl4conciliations.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1;
                                item.accountl1name = _accl4conciliations.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1description;
                                item.accountl1order = _accl4conciliations.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1order;
                                item.accountl2 = _accl4conciliations.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl2;
                                item.accountl2name = _accl4conciliations.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2description;
                                item.accountl2order = _accl4conciliations.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2order;
                                item.category = _accl4conciliations.tblaccountsl4.tblaccountsl3.idaccountl3;
                                item.categoryName = _accl4conciliations.tblaccountsl4.tblaccountsl3.accountl3description;
                                item.type = _accl4conciliations.tblaccountsl4.idAccountl4;
                                item.typename = _accl4conciliations.tblaccountsl4.accountl4description;
                            }
                            else // de lo contrario no se le generan datos de cuenta contable y no será contabilizada en el E-R
                            {
                                item.accountl1 = 0; item.accountl1name = ""; item.accountl1order = 0;
                                item.accountl2 = 0; item.accountl2name = ""; item.accountl2order = 0;
                                item.category = 0; item.categoryName = ""; item.accountl3order = 0;
                                item.type = 0; item.typename = ""; item.accountl4order = 0;
                            }

                            // Se termina de generar los datos del Docitem
                            item.ammounttotal =  (fee * -1) ;
                            item.ammounttotalstring = MoneyUtils.ParseDecimalToString(item.ammounttotal);
                            item.identifier = string.Format("Conciliaciones {0}", this.monthname);
                            item.description = string.Format("Comisión bancaria CTA {0} mes de {1}", details.baccountname, this.monthname);

                            // Agregando a la lista de Docitems
                            bankAccountsDocItems.Add(item);
                        }


                        // Adding conciliation items to docitems gral
                        this.invoiceitems = IListUtils.AddList(this.invoiceitems, bankAccountsDocItems);

                        break;


                    }
            }


            /// Converting Sources Datas 
            this.addExpenseReporitemIList(this.invoiceitems);
            this.addExpenseReporitemPurchaseIList(this.docpaymentpurchasexpreport);
            this.addExpenseReporitemReservIList(this.docpaymentreservexpreport);
            // this.addExpenseReporItem(this.bankreconciliation);
            this.addExpenseReporitemList(this.incomeitems);


            /// Adding Order
            this.addOrderExpenseReporItem(); // addOrderExpenseReporItem

            /// CLearing General Data and Source Datas

            if (keepdatasource == false)
            {
                this.DeleteSourcedata();
            }

            if (keepreporitemdetails == false)
            {
                this.DeleteReporItemDetails();
            }

        }


        private void initializeReportItem(int company, int typereport)
        {

            var _segment = unity.CompaniesRepository.Get(x => x.idcompany == company).Select(c => c.tblsegment).FirstOrDefault();
            // var _accl4 = unity.Accountl4TypeRepository.Get(x => x.tblaccountsl4.tblsegmentaccl4.FirstOrDefault().idSegment == _segment.idSegment && x.tblaccounttype.idAccountType == TypeReport, y => y.OrderBy(t => t.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1Description).ThenBy(t => t.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2Description).ThenBy(t => t.tblaccountsl4.tblaccountsl3.accountl3Description).ThenBy(t => t.tblaccountsl4.accountl4Description), "tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1").Select(x => x.tblaccountsl4).ToList();

            /*
             * Añade todas cuentas Accl4 activas por segemnto y por tipo de reporte
             * */

            /*
            foreach (tblaccountsl4 model in _accl4)
            {
                expenseReporItem _helperExpReportItem = new expenseReporItem(model.idAccountl4,model.accountl4Description,model.tblaccountsl3.idAccountl3, model.tblaccountsl3.accountl3Description,model.tblaccountsl3.tblaccountsl2.idAccountl2,model.tblaccountsl3.tblaccountsl2.accountl2Description,model.tblaccountsl3.tblaccountsl2.tblaccountsl1.idAccountl1,model.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1Description,0);

                this.expenseReporItem.Add(_helperExpReportItem);
            }
            */
        }

        private void SearchAllDataSource() { }


        #region Deleting source data
        public void DeleteSourcedata()
        {
            this.hoteldevelopment.Clear();
            this.accountl4methodspay.Clear();
            this.expensereportsourcedata.Clear();
        }

        public void DeleteReporItemDetails()
        {
            this.invoiceitems.Clear();
            this.docpaymentpurchasexpreport.Clear();
            this.docpaymentreservexpreport.Clear();
            this.incomeitems.Clear();
            this.bankreconciliation.Clear();
        }


        #endregion

        #region Get SourcesDatas

        private IEnumerable<tblinvoiceditem> getAnualDocitems(int year, int month, int idcompany, int currency, int TypeReport)
        {
            int segment = (int)unity.CompaniesRepository.Get(x => x.idcompany == idcompany).Select(v => v.idsegment).FirstOrDefault();
            List<tblinvoiceditem> invoicesitems = unity.InvoiceItemRepository.Get(t => t.tblinvoice.idcompany == idcompany && t.tblinvoice.invoicedate.Year == year && t.tblinvoice.invoicedate.Month == month && t.tblinvoice.tblcurrencies.idCurrency == currency && (t.tblaccountsl4.tblsegmentaccl4.Select(c => c.idsegment).ToList()).Contains(segment) && t.tblinvoice.tblcompanies.tblsegment.idsegment == segment && ((t.tblaccountsl4.tblaccountl4accounttype.Select(y => y.idaccounttypereport).ToList()).Contains(TypeReport)), null, "tblinvoice.tblcurrencies,tblinvoice.tblcompanies,tblinvoice.tblusers.tblprofilesaccounts,tblaccountsl4").ToList();
            return invoicesitems;
        }
        private IEnumerable<tblinvoiceditem> getAnualDocitems(int year, int month, int[] idcompany, int currency, int TypeReport)
        {
            List<tblinvoiceditem> invoicesitems = unity.InvoiceItemRepository.Get(t => idcompany.Contains(t.tblinvoice.tblcompanies.idcompany) && t.tblinvoice.invoicedate.Year == year && t.tblinvoice.invoicedate.Month == month && t.tblinvoice.tblcurrencies.idCurrency == currency && ((t.tblaccountsl4.tblaccountl4accounttype.Select(y => y.idaccounttypereport).ToList()).Contains(TypeReport)), null, "tblinvoice.tblcurrencies,tblinvoice.tblcompanies,tblinvoice.tblusers.tblprofilesaccounts").ToList();
            return invoicesitems;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchase(int year, int month, int idCurrency, int[] tpv ,int idPaymentMethodPurchase, int idHotelPartner)
        {
            List<tblpaymentspurchases> paymentsPurchase = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate.Value.Year == year && t.paymentDate.Value.Month == month && t.idCurrency == idCurrency && t.idPaymentType == idPaymentMethodPurchase /*&& t.tblpurchases.tblpartners.idHotelChain == idHotelPartner*/, null, "tblpurchases,tblcurrencies,tblpurchases.tblpartners").ToList();
            return paymentsPurchase;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchase(int year, int month, int idCurrency, int[] tpv, int[] idPaymentMethodPurchase, int[] idHotelPartner)
        {
            List<tblpaymentspurchases> paymentsPurchase = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate.Value.Year == year && t.paymentDate.Value.Month == month && t.idCurrency == idCurrency && tpv.Contains((int)t.terminal) && idPaymentMethodPurchase.Contains((int)t.idPaymentType) /*&& idHotelPartner.Contains(t.tblpurchases.tblpartners.idHotelChain)*/, null, "tblpurchases,tblcurrencies,tblpurchases.tblpartners").ToList();
            return paymentsPurchase;
        }

        private IEnumerable<tblpurchases> getAnualPaymentsPurchaseNew(int year, int month, int idCurrency, int[] tpv, int[] idPaymentMethodPurchase, int[] idHotelPartner)
        {
            List<tblpurchases> Purchase = new List<tblpurchases>();
            if (idCurrency == (int)Currencies.US_Dollar)
            {
                Purchase = unity.PurchasesRepository.Get(t => t.purchaseDate.Value.Year == year && t.purchaseDate.Value.Month == month && t.saleType == (int)PurchaseSaleType.New /*&& idHotelPartner.Contains(t.tblpurchases.tblpartners.idHotelChain)*/, null, "tblpartners,tblpartners.tblhotelchains").ToList();
            }
            return Purchase;
        }

        private IEnumerable<tblpurchases> getAnualPaymentsPurchaseUpgrade(int year, int month, int idCurrency, int[] tpv, int[] idPaymentMethodPurchase, int[] idHotelPartner)
        {
            List<tblpurchases> Purchase = new List<tblpurchases>();
            if (idCurrency == (int)Currencies.US_Dollar)
            {
                Purchase = unity.PurchasesRepository.Get(t => t.purchaseDate.Value.Year == year && t.purchaseDate.Value.Month == month && t.saleType == (int)PurchaseSaleType.Upgrade /*&& idHotelPartner.Contains(t.tblpurchases.tblpartners.idHotelChain)*/, null, "tblpartners,tblpartners.tblhotelchains").ToList();
            }
            return Purchase;
        }

        private IEnumerable<tblbatchdetail> getAnualPaymentsPurchaseBatchDetail(int year, int month, int idcompany, int currency, int[] idPaymentMethodPurchase)
        {
            List<tblbatchdetail> purchaseBatch = new List<tblbatchdetail>();

            purchaseBatch = unity.BatchDetailRepository.Get(b => b.tblbatch.datePay.Value.Year >= year && b.tblbatch.datePay.Value.Month <= month && b.tblbatch.tblbankaccount.idcompany == idcompany && b.tblbatch.tblbankaccount.idcurrency == currency && idPaymentMethodPurchase.Contains((int)b.tblbatch.idPaymentForm), null, "tblpurchases,tblpurchases.tblmemberships,tblpurchases.tblpartners,tblbatch.tblbankaccount").ToList();

            return purchaseBatch;
        }

        private IEnumerable<tblbatchdetailpre> getAnualPaymentsPurchaseBatchDetailPre(int year, int month, int idcompany, int currency, int[] idPaymentMethodPurchase)
        {
            List<tblbatchdetailpre> purchaseBatch = new List<tblbatchdetailpre>();

            purchaseBatch = unity.BatchDetailPreRepository.Get(b => b.tblbatch.datePay.Value.Year >= year && b.tblbatch.datePay.Value.Month <= month && b.tblbatch.tblbankaccount.idcompany == idcompany && b.tblbatch.tblbankaccount.idcurrency == currency && idPaymentMethodPurchase.Contains((int)b.tblbatch.idPaymentForm), null, "tblpartners,tblprefixes,tblbatch.tblbankaccount").ToList();

            return purchaseBatch;
        }

        public IEnumerable<tblreservationspayment> getAnualPaymentsRESERVAS(int year, int month, int idCurrency,int[] tpv, int[] idPaymentMethodReserv, int[] idHotelPartner)
        {
            List<tblreservationspayment> paymentsReserv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate.Value.Year >= year && t.reservationPaymentDate.Value.Month == month && t.idCurrency == idCurrency && tpv.Contains((int)t.terminal) && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& idHotelPartner.Contains(t.tblreservations.tblpartners.tblhotelchains.idHotelChain)*/, null, "tblreservations,tblreservations.tblmemberships").ToList();
            return paymentsReserv;
        }

        public IEnumerable<tblreservationsparentpayment> getAnualPaymentsRESERVAS_PARENT(int year, int month, int idCurrency, int[] tpv, int[] idPaymentMethodReserv, int[] idHotelPartner)
        {
            List<tblreservationsparentpayment> paymentsReserv = unity.ReservationsParentPaymentsRepository.Get(t => t.reservationPaymentDate.Value.Year >= year && t.reservationPaymentDate.Value.Month == month && t.idCurrency == idCurrency && tpv.Contains((int)t.terminal) && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& idHotelPartner.Contains(t.tblreservations.tblpartners.tblhotelchains.idHotelChain)*/, null, "tblreservationsparent").ToList();
            return paymentsReserv;
        }

        public IEnumerable<tblreservations> getAnualPaymentsRESERVAS_WEB(int year, int month, int idCurrency, int[] tpv, int[] idPaymentMethodReserv, int[] idHotelPartner)
        {
            List<tblreservations> Reserv = new List<tblreservations>();
            if (idCurrency == (int)Currencies.US_Dollar)
            {
                Reserv = unity.ReservationsRepository.Get(t => t.purchaseDate.Value.Year >= year && t.purchaseDate.Value.Month == month && (t.idSubcategory == (int)ReservaWeb.Reserva_Web) && tpv.Contains((int)t.memberCharge) /*&& idHotelPartner.Contains(t.tblpartners.tblhotelchains.idHotelChain)*/, null, "tblmemberships").ToList();
            }
            return Reserv;
        }


        public IEnumerable<docpaymentreservexpreport> getAnualPaymentsRESERV(int year, int month, int idCurrency, int[] idPaymentMethodReserv, int[] idHotel)
        {
            List<docpaymentreservexpreport> paymentsReserv = new List<docpaymentreservexpreport>();
            List<docpaymentreservexpreport_db> paymentsReserv_db = new List<docpaymentreservexpreport_db>();

            try
            {
                using (var db = new vtclubdbEntities())
                {
                    var r = (from reservationspayment in db.tblreservationspayment
                             join currency in db.tblcurrencies on reservationspayment.idCurrency equals currency.idCurrency
                             join reservations in db.tblreservations on reservationspayment.idReservation equals reservations.idReservation
                             join paymenmethod in db.tblPaymentForms on reservationspayment.idPaymentType equals paymenmethod.idPaymentForm
                             join membership in db.tblmemberships on reservations.idMembership equals membership.idMembership

                             where reservationspayment.reservationPaymentDate.Value.Year == year
                             && reservationspayment.reservationPaymentDate.Value.Month == month
                             && reservationspayment.idCurrency == idCurrency
                             && idPaymentMethodReserv.Contains((int)reservationspayment.idPaymentType)
                             && idHotel.Contains(reservationspayment.tblreservations.tblpartners.tblhotelchains.idHotelChain)



                             select new docpaymentreservexpreport_db()
                             {
                                 ReservationPayment = reservationspayment.idReservationPayment,
                                 Reservation = reservations.idReservation,
                                 PaymentMethod = paymenmethod.idPaymentForm,
                                 PaymentMethodName = paymenmethod.paymentFormName,
                                 reservationPaymentQuantity = reservationspayment.reservationPaymentCost,
                                 reservationPaymentDate = reservationspayment.reservationPaymentDate,
                                 tarjeta = reservationspayment.cardDigits,
                                 CurrencyPay = reservationspayment.idCurrency,
                                 CurrencyPayName = currency.currencyName,
                                 authRef = membership.membershipNumberPref + "-" + membership.membershipNumberFolio,
                                 CurrencytoExchange = 4,
                                 CurrencytoExchangeName = "",
                                 CurrencytoExchangeRate = reservationspayment.exchangeRate
                             }
                               ).ToList().Distinct();
                    paymentsReserv_db = r.ToList();
                }
            }
            catch (Exception e)
            {
                Log.Error("No se puede realizar la consulta.", e);
            }

            foreach (docpaymentreservexpreport_db item in paymentsReserv_db)
            {
                paymentsReserv.Add(new docpaymentreservexpreport().docpaymentreservexpreport_db_TO_docpaymentreservexpreport(item));
            }


            // List<tblreservationpayments> paymentsReserv = unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate.Value.Year >= year && t.reservationPaymentDate.Value.Month == month && t.idCurrency == idCurrency && idPaymentMethodReserv.Contains(t.idPaymentMethod) && t.tblreservations.idHotel == idHotel, null, "tblpaymentmethods,tblreservations,tblreservations.Currency,tblreservations.tblhotels").ToList();
            return paymentsReserv;
        }


        private IEnumerable<tblincomeitem> getAnualIncomeItems(int year, int month, int idcompany, int currency, int TypeReport)
        {
            int segment = (int)unity.CompaniesRepository.Get(x => x.idcompany == idcompany).Select(v => v.idsegment).FirstOrDefault();
            List<tblincomeitem> incomeitems = unity.IncomeItemRepository.Get(t => t.tblincome.idcompany == idcompany && t.tblincome.incomeapplicationdate.Year == year && t.tblincome.incomeapplicationdate.Month == month && t.tblincome.tblcurrencies.idCurrency == currency && (t.tblaccountsl4.tblsegmentaccl4.Select(c => c.idsegment).ToList()).Contains(segment) && t.tblincome.tblcompanies.tblsegment.idsegment == segment && ((t.tblaccountsl4.tblaccountl4accounttype.Select(y => y.idaccounttypereport).ToList()).Contains(TypeReport)), null, "tblincome.tblcurrencies,tblincome.tblcompanies,tblincome.tblusers.tblprofilesaccounts,tblaccountsl4").ToList();
            return incomeitems;
        }
        private IEnumerable<tblincomeitem> getAnualIncomeItems(int year, int month, int[] idcompany, int currency, int TypeReport)
        {
            List<tblincomeitem> incomeitems = unity.IncomeItemRepository.Get(t => idcompany.Contains(t.tblincome.tblcompanies.idcompany) && t.tblincome.incomeapplicationdate.Year == year && t.tblincome.incomeapplicationdate.Month == month && t.tblincome.tblcurrencies.idCurrency == currency && ((t.tblaccountsl4.tblaccountl4accounttype.Select(y => y.idaccounttypereport).ToList()).Contains(TypeReport)), null, "tblinvoice.tblcurrencies,tblinvoice.tblcompanies,tblinvoice.tblusers.tblprofilesaccounts").ToList();
            return incomeitems;
        }
        #endregion

        #region Adding SourceData


        private void addSource(invoiceitems helper)
        {
            if (this.invoiceitems == null || this.invoiceitems.Count() == 0) { this.invoiceitems = IListUtils.AddSingle<invoiceitems>(helper); }
            else { IListUtils.AddSingle<invoiceitems>(this.invoiceitems, helper); }
        }
        private void addSource(List<invoiceitems> listhelper)
        {
            if (this.invoiceitems == null || this.invoiceitems.Count() == 0) { this.invoiceitems = IListUtils.AddList<invoiceitems>(listhelper); }
            else { this.invoiceitems = IListUtils.AddList<invoiceitems>(this.invoiceitems, listhelper); }
        }

        /* private void addSource(docpaymentupsclexpreport helper)
         {
             if (this.docpaymentupsclexpreport == null || this.docpaymentupsclexpreport.Count() == 0) { this.docpaymentupsclexpreport = IListUtils.AddSingle<docpaymentupsclexpreport>(helper); }
             else { IListUtils.AddSingle<docpaymentupsclexpreport>(this.docpaymentupsclexpreport, helper); }
         }
         private void addSource(List<docpaymentupsclexpreport> listhelper)
         {

             if (this.docpaymentupsclexpreport == null || this.docpaymentupsclexpreport.Count() == 0) { this.docpaymentupsclexpreport = IListUtils.AddList<docpaymentupsclexpreport>(listhelper); }
             else { this.docpaymentupsclexpreport = IListUtils.AddList<docpaymentupsclexpreport>(this.docpaymentupsclexpreport, listhelper); }
         }
         */

        private void addSource(docpaymentreservexpreport helper)
        {
            if (this.docpaymentreservexpreport == null || this.docpaymentreservexpreport.Count() == 0) { this.docpaymentreservexpreport = IListUtils.AddSingle<docpaymentreservexpreport>(helper); }
            else { IListUtils.AddSingle<docpaymentreservexpreport>(this.docpaymentreservexpreport, helper); }
        }
        private void addSource(List<docpaymentreservexpreport> listhelper)
        {
            if (this.docpaymentreservexpreport == null || this.docpaymentreservexpreport.Count() == 0) { this.docpaymentreservexpreport = IListUtils.AddList<docpaymentreservexpreport>(listhelper); }
            else { this.docpaymentreservexpreport = IListUtils.AddListtoList<docpaymentreservexpreport>(this.docpaymentreservexpreport, listhelper); }
        }

        private void addSource(docpaymentpurchasexpreport helper)
        {
            if (this.docpaymentpurchasexpreport == null || this.docpaymentpurchasexpreport.Count() == 0) { this.docpaymentpurchasexpreport = IListUtils.AddSingle<docpaymentpurchasexpreport>(helper); }
            else { IListUtils.AddSingle<docpaymentpurchasexpreport>(this.docpaymentpurchasexpreport, helper); }
        }

        private void addSource(List<docpaymentpurchasexpreport> listhelper)
        {
            if (this.docpaymentpurchasexpreport == null || this.docpaymentpurchasexpreport.Count() == 0) { this.docpaymentpurchasexpreport = IListUtils.AddList<docpaymentpurchasexpreport>(listhelper); }
            else { this.docpaymentpurchasexpreport = IListUtils.AddListtoList<docpaymentpurchasexpreport>(this.docpaymentpurchasexpreport, listhelper); }
        }


        private void addSource(bankreconciliationModel helper)
        {
            if (this.bankreconciliation == null || this.bankreconciliation.Count() == 0) { this.bankreconciliation = IListUtils.AddSingle<bankreconciliationModel>(helper); }
            else { IListUtils.AddSingle<bankreconciliationModel>(this.bankreconciliation, helper); }
        }

        private void addSource(List<bankreconciliationModel> listhelper)
        {
            if (this.bankreconciliation == null || this.bankreconciliation.Count() == 0) { this.bankreconciliation = IListUtils.AddList<bankreconciliationModel>(listhelper); }
            else { this.bankreconciliation = IListUtils.AddList<bankreconciliationModel>(this.bankreconciliation, listhelper); }

        }

        private void addSource(incomeitem helper)
        {
            if (this.incomeitems == null || this.incomeitems.Count() == 0) { this.incomeitems = IListUtils.AddSingle<incomeitem>(helper); }
            else { IListUtils.AddSingle<incomeitem>(this.incomeitems, helper); }
        }

        private void addSource(List<incomeitem> listhelper)
        {
            if (this.incomeitems == null || this.incomeitems.Count() == 0) { this.incomeitems = IListUtils.AddList<incomeitem>(listhelper); }
            else { this.incomeitems = IListUtils.AddListtoList<incomeitem>(this.incomeitems, listhelper); }
        }


        #endregion

        #region  Convertig SourceData To ExpenseReportItem [Processing SourceDatas]

        public void convertingAllDatasourceReportItem()
        {
            this.addExpenseReporitemIList(this.invoiceitems);
            this.addExpenseReporitemPurchaseIList(this.docpaymentpurchasexpreport);
            this.addExpenseReporitemReservIList(this.docpaymentreservexpreport);
            // this.addExpenseReporItem(this.bankreconciliation);
            this.addExpenseReporitemList(this.incomeitems);
        }


        // Adding Docitems
        public void addExpenseReporItem(invoiceitems helper)
        {
            var item = this.ConverToExpenseReporItem(helper);
            if (this.expensereporitem == null)
            {
                this.expensereporitem = IListUtils.AddSingle(item);
            }
            else if (this.expensereporitem.Count == 0)
            {
                this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
            }
            else
            {
                var _itemResult = this.searchExpenseReportItem(this.expensereporitem, item);

                if (_itemResult == null)
                {
                    this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
                }
                else
                {
                    _itemResult.ammounttotal = _itemResult.ammounttotal += item.ammounttotal;
                    _itemResult.ammounttotalstring = MoneyUtils.ParseDecimalToString(_itemResult.ammounttotal);
                    _itemResult.invoiceitems = IEnumerableUtils.AddSingle(_itemResult.invoiceitems, item.invoiceitems.First());
                }
            }
            this.calculatetotalincomeexpenses(item);
        }
        /*
                public void addExpenseReporitemList(List<docitems> listhelper)
                {
                    foreach (docitems helper in listhelper)
                    {
                        this.addExpenseReporItem(helper);
                    }
                }

                public void addExpenseReporitemEnum(IEnumerable<docitems> listhelper)
                {
                    foreach (docitems helper in listhelper)
                    {
                        this.addExpenseReporItem(helper);
                    }
                }*/

        public void addExpenseReporitemIList(IList<invoiceitems> listhelper)
        {
            foreach (invoiceitems helper in listhelper)
            {
                this.addExpenseReporItem(helper);
            }
        }

        // Adding  PURCHASE
        public void addExpenseReporitemPurchase(docpaymentpurchasexpreport helper)
        {
            var item = this.ConverToExpenseReporItem(helper);
            if (this.expensereporitem == null)
            {
                this.expensereporitem = IListUtils.AddSingle(item);
                // _itemResult.docitems = IEnumerableUtils.AddSingle(item);
            }
            else if (this.expensereporitem.Count == 0)
            {
                this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
            }
            else
            {
                var _itemResult = this.searchExpenseReportItem(this.expensereporitem, item);

                if (_itemResult == null)
                {
                    this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
                }
                else
                {
                    _itemResult.ammounttotal = _itemResult.ammounttotal += item.ammounttotal;
                    _itemResult.ammounttotalstring = MoneyUtils.ParseDecimalToString(_itemResult.ammounttotal);
                    _itemResult.invoiceitems = IEnumerableUtils.AddSingle(_itemResult.invoiceitems, item.invoiceitems.First());
                }
            }
            this.calculatetotalincomeexpenses(item);
            // this.addOrderExpenseReporItem();
        }

        /*public void addExpenseReporitemUpsclList(List<docpaymentupsclexpreport> listhelper)
        {
            foreach (docpaymentupsclexpreport helper in listhelper)
            {
                this.addExpenseReporitemUpscl(helper);
            }
        }

        public void addExpenseReporitemUpsclEnum(IEnumerable<docpaymentupsclexpreport> listhelper)
        {
            foreach (docpaymentupsclexpreport helper in listhelper)
            {
                this.addExpenseReporitemUpscl(helper);
            }
        }
*/
        public void addExpenseReporitemPurchaseIList(IList<docpaymentpurchasexpreport> listhelper)
        {
            foreach (docpaymentpurchasexpreport helper in listhelper)
            {
                this.addExpenseReporitemPurchase(helper);
            }
        }

        // Adding Reservations
        public void addExpenseReporitemReserv(docpaymentreservexpreport helper)
        {
            var item = this.ConverToExpenseReporItem(helper);
            if (this.expensereporitem == null)
            {
                this.expensereporitem = IListUtils.AddSingle(item);
                // _itemResult.docitems = IEnumerableUtils.AddSingle(item);
            }
            else if (this.expensereporitem.Count == 0)
            {
                this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
            }
            else
            {
                var _itemResult = this.searchExpenseReportItem(this.expensereporitem, item);

                if (_itemResult == null)
                {
                    this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
                }
                else
                {
                    _itemResult.ammounttotal = _itemResult.ammounttotal += item.ammounttotal;
                    _itemResult.ammounttotalstring = MoneyUtils.ParseDecimalToString(_itemResult.ammounttotal);

                    _itemResult.invoiceitems = IEnumerableUtils.AddSingle(_itemResult.invoiceitems, item.invoiceitems.First());
                }
            }

            // this.addOrderExpenseReporItem();
            this.calculatetotalincomeexpenses(item);
        }

        public void addExpenseReporitemReservList(List<docpaymentreservexpreport> listhelper)
        {
            foreach (docpaymentreservexpreport helper in listhelper)
            {
                this.addExpenseReporitemReserv(helper);
            }
        }

        public void addExpenseReporitemReservEnum(IEnumerable<docpaymentreservexpreport> listhelper)
        {
            foreach (docpaymentreservexpreport helper in listhelper)
            {
                this.addExpenseReporitemReserv(helper);
            }
        }

        public void addExpenseReporitemReservIList(IList<docpaymentreservexpreport> listhelper)
        {
            foreach (docpaymentreservexpreport helper in listhelper)
            {
                this.addExpenseReporitemReserv(helper);
            }
        }
        /*

        // Adding BankConciliations
        public void addExpenseReporItem(bankreconciliation helper)
        {
            var item = this.ConverToExpenseReporItem(helper);
            if (this.expenseReporItem == null)
            {
                this.expenseReporItem = IListUtils.AddSingle(item);
            }
            else if (this.expenseReporItem.Count == 0)
            {
                this.expenseReporItem = IListUtils.AddSingle(this.expenseReporItem, item);
            }
            else
            {
                var _itemResult = this.searchExpenseReportItem(this.expenseReporItem, item);

                if (_itemResult == null)
                {
                    this.expenseReporItem = IListUtils.AddSingle(this.expenseReporItem, item);
                }
                else
                {
                    _itemResult.AmmountTotal = _itemResult.AmmountTotal += item.AmmountTotal;
                    _itemResult.AmmountTotalString = MoneyUtils.ParseDecimalToString(_itemResult.AmmountTotal);
                    _itemResult.docitems = IEnumerableUtils.AddSingle(_itemResult.docitems, item.docitems.First());
                }
            }
            this.calculatetotalincomeexpenses(item);
        }

        public void addExpenseReporItem(IList<bankreconciliation> listhelper)
        {
            foreach (bankreconciliation helper in listhelper)
            {
                this.addExpenseReporItem(helper);
            }
        }
        */
        // Adding IncomeItems
        public void addExpenseReporItem(incomeitem helper)
        {
            var item = this.ConverToExpenseReporItem(helper);
            if (this.expensereporitem == null)
            {
                this.expensereporitem = IListUtils.AddSingle(item);
            }
            else if (this.expensereporitem.Count == 0)
            {
                this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
            }
            else
            {
                var _itemResult = this.searchExpenseReportItem(this.expensereporitem, item);

                if (_itemResult == null)
                {
                    this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
                }
                else
                {
                    _itemResult.ammounttotal = _itemResult.ammounttotal += item.ammounttotal;
                    _itemResult.ammounttotalstring = MoneyUtils.ParseDecimalToString(_itemResult.ammounttotal);
                    _itemResult.invoiceitems = IEnumerableUtils.AddSingle(_itemResult.invoiceitems, item.invoiceitems.First());
                }
            }

            this.calculatetotalincomeexpenses(item);
        }

        public void addExpenseReporitemList(List<incomeitem> listhelper)
        {
            foreach (incomeitem helper in listhelper)
            {
                this.addExpenseReporItem(helper);
            }
        }

        public void addExpenseReporitemList(IList<incomeitem> listhelper)
        {
            foreach (incomeitem helper in listhelper)
            {
                this.addExpenseReporItem(helper);
            }
        }


        // Adding ExpenseReporItem
        public void addExpenseReporItem(expensereporitem item)
        {
            if (this.expensereporitem == null)
            {
                this.expensereporitem = IListUtils.AddSingle(item);
            }
            else if (this.expensereporitem.Count == 0)
            {
                this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
            }
            else
            {
                var _itemResult = this.searchExpenseReportItem(this.expensereporitem, item);

                if (_itemResult == null)
                {
                    this.expensereporitem = IListUtils.AddSingle(this.expensereporitem, item);
                }
                else
                {
                    _itemResult.ammounttotal = _itemResult.ammounttotal += item.ammounttotal;
                    _itemResult.ammounttotalstring = MoneyUtils.ParseDecimalToString(_itemResult.ammounttotal);


                    if (_itemResult.invoiceitems == null)
                    {
                        //IEnumerableUtils.AddList(_itemResult.docitems = new List<invoiceitems>(), item.docitems);
                    }
                    else if (_itemResult.invoiceitems.Count() == 0)
                    {
                        IEnumerableUtils.AddList(_itemResult.invoiceitems, item.invoiceitems);
                    }
                    else
                    {
                        _itemResult.invoiceitems = IEnumerableUtils.AddList(_itemResult.invoiceitems, item.invoiceitems);
                    }
                    // _itemResult.docitems = _itemResult.docitems.Count() == 0 ? _itemResult.docitems.Count() == 1 ? IEnumerableUtils.AddSingle(_itemResult.docitems, item.docitems.First()) : IEnumerableUtils.AddList(_itemResult.docitems, item.docitems): 
                }
            }
            this.calculatetotalincomeexpenses(item);
            this.addOrderExpenseReporItem();
        }


        public void addExpenseReporItem(List<expensereporitem> list)
        {
            foreach (expensereporitem model in list)
            {
                this.addExpenseReporItem(model);
            }
        }

        public void addExpenseReporItem(IList<expensereporitem> list)
        {
            foreach (expensereporitem model in list)
            {
                this.addExpenseReporItem(model);
            }
        }

        public void addExpenseReporItem(IEnumerable<expensereporitem> list)
        {
            foreach (expensereporitem model in list)
            {
                this.addExpenseReporItem(model);
            }
        }


        #endregion


        #region Calculating  expenses 
        private void calculatetotalincomeexpenses(expensereporitem item)
        {
            switch (item.accountl1)
            {
                case 1:
                    {
                        this.totalincome = totalincome += (item.ammounttotal * 1);
                        this.totalincomestring = MoneyUtils.ParseDecimalToString(totalincome);
                        break;
                    }
                case 2:
                    {
                        this.totalexpense = totalexpense += (item.ammounttotal * -1);
                        this.totalexpensestring = MoneyUtils.ParseDecimalToString(totalexpense);
                        break;
                    }
            }
        }

        private void calculatetotalincomeexpenses(List<expensereporitem> list)
        {
            for (int i = 0; i <= list.Count - 1; i++)
            {
                calculatetotalincomeexpenses(list.ElementAt(i));
            }
        }

        #endregion


        /// <summary>
        /// Ordering Expense Report Item
        /// </summary>
        private void addOrderExpenseReporItem()
        {
            if (this.expensereporitem != null || this.expensereporitem.Count != 0)
            {
                this.expensereporitem = this.expensereporitem.OrderBy(x => x.accountl1order).ThenBy(x => x.accountl2order).ThenBy(x => x.accountl3order).ThenBy(x => x.accountl4order).ToList();
            }
        }


        /// <summary>
        /// Search Expense ReporItem in List<> / IList<>
        /// </summary>
        /// <param name="lstexpenses"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        private expensereporitem searchExpenseReportItem(List<expensereporitem> lstexpenses, expensereporitem entry)
        {
            return lstexpenses?.Where(y => entry.accountl1 == y.accountl1 && entry.accountl2 == y.accountl2 && entry.accountl3 == y.accountl3 && entry.accountl4 == y.accountl4).FirstOrDefault();
        }

        private expensereporitem searchExpenseReportItem(IList<expensereporitem> lstexpenses, expensereporitem entry)
        {
            return lstexpenses?.Where(y => entry.accountl1 == y.accountl1 && entry.accountl2 == y.accountl2 && entry.accountl3 == y.accountl3 && entry.accountl4 == y.accountl4).FirstOrDefault();
        }

        /// <summary>
        /// Convert SourcesData to ReportItem
        /// </summary>
        /// <param name="docitems"></param>
        /// <returns></returns>
        private expensereporitem ConverToExpenseReporItem(invoiceitems docitems)
        {
            expensereporitem helper = new expensereporitem();
            helper.accountl1 = docitems.accountl1;
            helper.accountl1name = docitems.accountl1name;
            helper.accountl1order = docitems.accountl1order;
            helper.accountl2 = docitems.accountl2;
            helper.accountl2name = docitems.accountl2name;
            helper.accountl2order = docitems.accountl2order;
            helper.accountl3 = docitems.accountl3;
            helper.accountl3name = docitems.accountl3name;
            helper.accountl3order = docitems.accountl3order;
            helper.accountl4 = docitems.accountl4;
            helper.accountl4name = docitems.accountl4name;
            helper.accountl4order = docitems.accountl4order;


            helper.ammounttotal = docitems.ammounttotal;
            helper.ammounttotalstring = docitems.ammounttotalstring;
            // helper.AmmountL1 = docitems.Ammount;
            // helper.AmmountTotalL1String = docitems.AmmountTotalL1String;
            // helper.AmmountL2 = helper.AmmountL2;
            // helper.AmmountTotalL2String = helper.AmmountTotalL2String;
            // helper.AmmountL3 = helper.AmmountL3;
            // helper.AmmountTotalL3String = helper.AmmountTotalL3String;
            // helper.AmmountL4 = helper.AmmountL4;
            // helper.AmmountTotalL4String = helper.AmmountTotalL4String;
            helper.taxes = docitems.taxesammount;
            helper.taxeststring = docitems.taxesammountstring;
            helper.othertaxes = docitems.othertaxesammount;
            helper.othertaxeststring = docitems.othertaxesammountstring;
            helper.invoiceitems = IEnumerableUtils.AddSingle(docitems);
            return helper;
        }

        private expensereporitem ConverToExpenseReporItem(docpaymentpurchasexpreport docpaymentpurchasexpreport)
        {
            expensereporitem helper = new expensereporitem();
            helper.accountl1 = docpaymentpurchasexpreport.accountl1;
            helper.accountl1name = docpaymentpurchasexpreport.accountl1Name;
            helper.accountl1order = docpaymentpurchasexpreport.accountl1Order;
            helper.accountl2 = docpaymentpurchasexpreport.accountl2;
            helper.accountl2name = docpaymentpurchasexpreport.accountl2Name;
            helper.accountl2order = docpaymentpurchasexpreport.accountl2Order;
            helper.accountl3 = docpaymentpurchasexpreport.accountl3;
            helper.accountl3name = docpaymentpurchasexpreport.accountl3Name;
            helper.accountl3order = docpaymentpurchasexpreport.accountl3Order;
            helper.accountl4 = docpaymentpurchasexpreport.accountl4;
            helper.accountl4name = docpaymentpurchasexpreport.accountl4Name;
            helper.accountl4order = docpaymentpurchasexpreport.accountl4Order;

            helper.ammounttotal = docpaymentpurchasexpreport.paymentAmount;
            helper.ammounttotalstring = docpaymentpurchasexpreport.paymentAmountString;
            helper.invoiceitems = IEnumerableUtils.AddSingle(new invoiceitems { identifier = string.Concat("Purchase: ", docpaymentpurchasexpreport.Purchase == 0 ? " " : docpaymentpurchasexpreport.Purchase.ToString(), " - Pago ", docpaymentpurchasexpreport.Payment.ToString()), ammounttotalstring = docpaymentpurchasexpreport.paymentAmountString, description = docpaymentpurchasexpreport.authRef });

            return helper;
        }


        private expensereporitem ConverToExpenseReporItem(docpaymentreservexpreport docpaymentreservexpreport)
        {
            expensereporitem helper = new expensereporitem();
            helper.accountl1 = docpaymentreservexpreport.accountl1;
            helper.accountl1name = docpaymentreservexpreport.accountl1name;
            helper.accountl1order = docpaymentreservexpreport.accountl1order;
            helper.accountl2 = docpaymentreservexpreport.accountl2;
            helper.accountl2name = docpaymentreservexpreport.accountl2name;
            helper.accountl2order = docpaymentreservexpreport.accountl2order;
            helper.accountl3 = docpaymentreservexpreport.accountl3;
            helper.accountl3name = docpaymentreservexpreport.accountl3name;
            helper.accountl3order = docpaymentreservexpreport.accountl3order;
            helper.accountl4 = docpaymentreservexpreport.accountl4;
            helper.accountl4name = docpaymentreservexpreport.accountl4name;
            helper.accountl4order = docpaymentreservexpreport.accountl4order;

            helper.ammounttotal = docpaymentreservexpreport.reservationPaymentQuantity;
            helper.ammounttotalstring = docpaymentreservexpreport.reservationPaymentQuantityString;
            helper.invoiceitems = IEnumerableUtils.AddSingle(new invoiceitems { identifier = string.Concat("Reserva: ", docpaymentreservexpreport.Reservation == 0 ? " " : docpaymentreservexpreport.Reservation.ToString()), ammounttotalstring = docpaymentreservexpreport.reservationPaymentQuantityString, description = docpaymentreservexpreport.authRef });
            return helper;
        }


        /*
        private expensereporitem ConverToExpenseReporItem(bankreconciliation bankreconciliation)
        {
            var _result = this.AccountL4MethosPay.Where(c => c.PaymentMethod == bankreconciliation.paymentmethod).FirstOrDefault();
            expensereporitem helper = new expensereporitem();

            if (_result != null)
            {
                helper.accountl1 = _result.accountl1;
                helper.accountl1name = _result.accl1Name;
                helper.accountl1order = _result.accountl1Order;
                helper.accountl2 = _result.accountl2;
                helper.accountl2name = _result.accl2Name;
                helper.accountl2order = _result.accountl2Order;
                helper.accountl3 = _result.accountl3;
                helper.accountl3name = _result.accl3Name;
                helper.accountl3order = _result.accountl3Order;
                helper.accountl4 = _result.accountl4;
                helper.accountl4name = _result.accl4Name;
                helper.accountl4order = _result.accountl4Order;
            }

            helper.ammountyotal = (decimal)bankreconciliation.bankstatementAppliedAmmountFinal;
            helper.ammounttotalstring = bankreconciliation.bankstatementappliedammountfinalstring;
            helper.docitems = IEnumerableUtils.AddSingle(new docitems { InvoiceIdentifier = "Conciliación bancaria", AmmountTotalString = bankreconciliation.bankstatementappliedammountfinalstring, description = string.Format("Conciliación bancaria ScotiaPos, fecha aplicación {0}, origen: {1}", bankreconciliation.bankstatementaplicationdatestring, bankreconciliation.hotelname) });
            return helper;
        }
        */

        private expensereporitem ConverToExpenseReporItem(incomeitem income)
        {
            expensereporitem helper = new expensereporitem();
            helper.accountl1 = income.accountl1;
            helper.accountl1name = income.accountl1name;
            helper.accountl1order = income.accountl1order;
            helper.accountl2 = income.accountl2;
            helper.accountl2name = income.accountl2name;
            helper.accountl2order = income.accountl2order;
            helper.accountl3 = income.accountl3;
            helper.accountl3name = income.accountl3name;
            helper.accountl3order = income.accountl3order;
            helper.accountl4 = income.accountl4;
            helper.accountl4name = income.accountl4name;
            helper.accountl4order = income.accountl4order;
            helper.ammounttotal = income.ammounttotal;
            helper.ammounttotalstring = income.ammounttotalstring;
            helper.invoiceitems = IEnumerableUtils.AddSingle(new invoiceitems
            {
                identifier = income.identifier,
                ammounttotal = income.ammounttotal,
                ammounttotalstring = income.ammounttotalstring,
                description = income.description
            });
            return helper;
        }

        public void addExpenseReportItemL4Resume(expensereporitem expenseReporItem)
        {
            var x = this.searchExpenseReportItem((List<expensereporitem>)this.expensereporitem, expenseReporItem);

            if (x == null)
            {
                if (this.expensereporitem == null)
                {
                    this.expensereporitem = new List<expensereporitem>();
                    this.expensereporitem.Add(expenseReporItem);
                }
                else if (this.expensereporitem.Count == 0)
                {
                    this.expensereporitem.Add(expenseReporItem);
                }
                else
                {
                    this.expensereporitem.Add(expenseReporItem);
                }
            }
            else
            {
                var result = this.expensereporitem.Where(y => x.accountl1 == y.accountl1 && x.accountl2 == y.accountl2 && x.accountl3 == y.accountl3 && x.accountl4 == y.accountl4).FirstOrDefault();

                result.ammounttotal = result.ammounttotal += expenseReporItem.ammounttotal;
                result.ammounttotalstring = MoneyUtils.ParseDecimalToString(result.ammounttotal);
                result.invoiceitems = IEnumerableUtils.AddSingle(result.invoiceitems, expenseReporItem.invoiceitems.First());
                //result.addDocitem()
            }
        }

        //public void addExpenseReportItemL4Resume(tblinvoiceditem model)
        //{
        //    var reporitem = GeneralModelHelper.ConvertTbltoHelper(model);
        //    var x = this.searchExpenseReportItem(this.expensereporitem.ToList(), reporitem);

        //    if (x == null)
        //    {

        //        if (this.expensereporitem == null)
        //        {
        //            this.expensereporitem = new List<expensereporitem>();
        //            this.expensereporitem.Add(reporitem);
        //        }
        //        else if (this.expensereporitem.Count == 0)
        //        {
        //            this.expensereporitem.Add(reporitem);
        //        }
        //        else
        //        {
        //            this.expensereporitem.Add(reporitem);
        //        }
        //    }
        //    else
        //    {
        //        var result = this.expensereporitem.Where(y => x.accountl1 == y.accountl1 && x.accountl2 == y.accountl2 && x.accountl3 == y.accountl3 && x.accountl4 == y.accountl4).FirstOrDefault();
        //        result.ammounttotal = result.ammounttotal += reporitem.ammounttotal;
        //        result.ammounttotalstring = MoneyUtils.ParseDecimalToString(result.ammounttotal);
        //    }
        //}


        public void addExpenseReportItemL4Details(expensereporitem expenseReporItem)
        {
            if (this.expensereporitem == null)
            {
                this.expensereporitem = new List<expensereporitem>();
                this.expensereporitem.Add(expenseReporItem);
            }
            else if (this.expensereporitem.Count == 0)
            {
                this.expensereporitem.Add(expenseReporItem);
            }
            else
            {
                this.expensereporitem.Add(expenseReporItem);
            }
        }

        /* private expenseReporItem searchExpenseReportItem(List<expenseReporItem> lstexpenses, expenseReporItem entry)
            {
                return lstexpenses?.Where(y => entry.Accountl1 == y.Accountl1 && entry.Accountl2 == y.Accountl2 && entry.Accountl3 == y.Accountl3 && entry.Accountl4 == y.Accountl4).FirstOrDefault();
            }
            */
    }

    public class expensereporitem
    {

        public int accountl4 { get; set; }
        public string accountl4name { get; set; }
        public int accountl4order { get; set; }
        public int accountl3 { get; set; }
        public string accountl3name { get; set; }
        public int accountl3order { get; set; }
        public int accountl2 { get; set; }
        public string accountl2name { get; set; }
        public int accountl2order { get; set; }
        public int accountl1 { get; set; }
        public string accountl1name { get; set; }
        public int accountl1order { get; set; }
        public decimal ammounttotal { get; set; }
        public string ammounttotalstring { get; set; }
        public decimal ammountl1 { get; set; }
        public string ammounttotall1string { get; set; }
        public decimal ammountl2 { get; set; }
        public string ammounttotall2string { get; set; }
        public decimal ammountl3 { get; set; }
        public string ammounttotall3string { get; set; }
        public decimal ammountl4 { get; set; }
        public string ammounttotall4string { get; set; }
        public decimal ammount { get; set; }
        public string ammountstring { get; set; }
        public decimal taxes { get; set; }
        public string taxeststring { get; set; }
        public decimal othertaxes { get; set; }
        public string othertaxeststring { get; set; }
        public List<int> typereport { get; set; }
        public IEnumerable<invoiceitems> invoiceitems { get; set; }



        public expensereporitem()
        { }

        public expensereporitem(int Accountl4, string accountl4Name, int Accountl3, string accountl3Name, int Accountl2, string accountl2Name, int Accountl1, string accountl1Name, decimal AmmountTotal)
        {
            this.accountl4 = Accountl4;
            this.accountl4name = accountl4Name;

            this.accountl3 = Accountl3;
            this.accountl3name = accountl3Name;

            this.accountl2 = Accountl2;
            this.accountl2name = accountl2Name;

            this.accountl1 = Accountl1;
            this.accountl1name = accountl1Name;

            this.ammounttotal = AmmountTotal;


        }




        public decimal calculateAmmountL1(decimal ammount)
        {
            return this.ammountl1 = this.ammountl1 + ammount;
        }
        public decimal calculateAmmountL2(decimal ammount)
        {
            return this.ammountl2 = this.ammountl2 + ammount;
        }
        public decimal calculateAmmountL3(decimal ammount)
        {
            return this.ammountl3 = this.ammountl3 + ammount;
        }
        public decimal calculateAmmountL4(decimal ammount)
        {
            return this.ammountl4 = this.ammountl4 + ammount;
        }

        public void generateStringAmmounts()
        {

            this.ammounttotalstring = MoneyUtils.ParseDecimalToString(this.ammounttotal);
            this.ammounttotall4string = MoneyUtils.ParseDecimalToString(this.ammountl4);
            this.ammounttotall3string = MoneyUtils.ParseDecimalToString(this.ammountl3);
            this.ammounttotall2string = MoneyUtils.ParseDecimalToString(this.ammountl2);
            this.ammounttotall1string = MoneyUtils.ParseDecimalToString(this.ammountl1);
            this.ammounttotall1string = MoneyUtils.ParseDecimalToString(this.ammountl1);
            this.ammountstring = MoneyUtils.ParseDecimalToString(this.ammount);
            this.taxeststring = MoneyUtils.ParseDecimalToString(this.taxes);
            this.othertaxeststring = MoneyUtils.ParseDecimalToString(this.othertaxes);
        }

        public void addDocitem(tblinvoiceditem model)
        {

            if (this.invoiceitems == null)
            {
                this.invoiceitems = new List<invoiceitems>();

                invoiceitems doc = new invoiceitems();

                doc.identifier = AccountsUtils.IdentifierComplete(model.tblinvoice.tblcompanies.companyshortname, model.tblinvoice.tblusers.tblprofilesaccounts.profileaccountshortame, model.tblinvoice.invoicenumber);
                //doc.description = model.description;
                //doc.suppliername = model.idsupplier == 1 ? model.description : model.tblsupplier.suppliername;
                //doc.ammount = model.invoiceditemsubtotal;
                //doc.taxesammount = model.invoiceditemtaxes;
                //doc.othertaxesammount = model.invoiceditemothertaxesammount;
                //doc.ammounttotal = (decimal)model.invoiceditemsubtotal + (decimal)model.invoiceditemtaxes + (decimal)model.invoiceditemothertaxesammount;
                doc.creationdatestring = DateTimeUtils.ParseDatetoString(model.tblinvoice.invoicedate);
                doc.aplicationdatestring = DateTimeUtils.ParseDatetoString(model.tblinvoice.invoicedate);
                doc.generateStringAmmounts();
                this.invoiceitems = IEnumerableUtils.AddSingle(doc);
            }
            else
            {
                invoiceitems doc = new invoiceitems();
                doc.identifier = AccountsUtils.IdentifierComplete(model.tblinvoice.tblcompanies.companyshortname, model.tblinvoice.tblusers.tblprofilesaccounts.profileaccountshortame, model.tblinvoice.invoicenumber);
                //doc.description = model.description;
                //doc.suppliername = model.idsupplier == 1 ? model.description : model.tblsupplier.suppliername;
                //doc.ammount = model.invoiceditemsubtotal;
                //doc.taxesammount = model.invoiceditemtaxes;
                //doc.othertaxesammount = model.invoiceditemothertaxesammount;
                //doc.ammounttotal = (decimal)model.invoiceditemsubtotal + (decimal)model.invoiceditemtaxes + (decimal)model.invoiceditemothertaxesammount;
                doc.creationdatestring = DateTimeUtils.ParseDatetoString(model.tblinvoice.invoicedate);
                doc.aplicationdatestring = DateTimeUtils.ParseDatetoString(model.tblinvoice.invoicedate);
                doc.generateStringAmmounts();
                this.invoiceitems = IEnumerableUtils.AddSingle(doc);


            }

            this.generateStringAmmounts();
        }

    }
}