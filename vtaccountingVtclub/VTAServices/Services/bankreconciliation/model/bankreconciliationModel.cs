using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.Models.commons;
using VTAworldpass.VTAServices.Services.Models;
using static VTAworldpass.VTACore.Collections.CollectionsUtils;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;
using VTAworldpass.VTACore.Cores.Globales;

namespace VTAworldpass.VTAServices.Services.bankreconciliation.model
{
    public class bankreconciliationModel : tblbankstatements
    {
        public int currency { get; set; }
        public string currencyname { get; set; }
        public int baccount { get; set; }
        public string baccountname { get; set; }
        public string bankstatementappliedammountstring { get; set; }
        public string tpvname { get; set; }
        public string companyname { get; set; }
        public int idhotelpartner { get; set; }
        public string hotelpartnername { get; set; }
        public string bankstatementaplicationdatestring { get; set; }
        public string bankstatementappliedammountfinalstring { get; set; }

        public string bankstatementadjuststring { get; set; }
        public string bankstatementinterchangeschargesstring { get; set; }
        public string bankstatementbankfeestring { get; set; }
        public string bankstatementchargespaymentsstring { get; set; }
        public string bankstatementrefundstring { get; set; }

        public int? paymentmethod { get; set; }
        public string paymentmethodname { get; set; }


        public int statusconciliation { get; set; }
        public string statusconciliationname { get; set; }
        public int methodconciliation { get; set; }
        public int rowIndex { get; set; }
        public int rowNumber { get; set; }
        public bool xlscorrectlyformed { get; set; }

        private IEnumerable<tblreservationsparentpayment> reservaparentpaymentitems { get; set; }
        private IEnumerable<tblreservationspayment> reservapaymentitems { get; set; }
        private IEnumerable<tblpaymentspurchases> purchasepaymentitems { get; set; }

        private IEnumerable<tblincomemovement> tblincomemovementitems { get; set; }
        public IEnumerable<docpaymentparentreserv> docpaymentparentreservitems { get; set; }
        public IEnumerable<docpaymentreserv> docpaymentreservitems { get; set; }
        public IEnumerable<docpaymentpurchase> docpaymentpurchaseitems { get; set; }
        public IEnumerable<docpaymentincome> docpaymentincomitems { get; set; }
        public virtual IEnumerable<financialstateitemModel> financialstateitemlist { get; set; }

        private readonly UnitOfWork unity = new UnitOfWork();

        public bankreconciliationModel()
        { }

        public bankreconciliationModel Generate(int idBankStatements)
        {
            return new bankreconciliationModel();
        }

        public bankreconciliationModel(long idBankStatements, int idTPV, string TPVName, int idCompany, string CompanyName, int idhotelpartner, string hotelpartnername, int BAccount, string BAccountName, int Currency, string CurrencyName, string bankstatementAplicationDateString, DateTime bankstatementAplicationDate, decimal bankstatementAppliedAmmount, decimal? bankstatementBankFee, decimal? bankstatementAppliedAmmountFinal, int methodConciliation, bool sourcedata, bool financialstateitem)
        {
            // Initializaing
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.docpaymentincomitems = new List<docpaymentincome>();
            this.reservaparentpaymentitems = new List<tblreservationsparentpayment>();
            this.reservapaymentitems = new List<tblreservationspayment>();
            this.purchasepaymentitems = new List<tblpaymentspurchases>();
            this.financialstateitemlist = new List<financialstateitemModel>();
            this.tblincomemovementitems = new List<tblincomemovement>();

            this.idBankStatements = idBankStatements;
            this.idTPV = idTPV;
            this.tpvname = TPVName;
            this.idCompany = idCompany;
            this.companyname = CompanyName;
            this.idhotelpartner = idhotelpartner;
            this.hotelpartnername = hotelpartnername;
            this.baccount = BAccount;
            this.baccountname = BAccountName;
            this.currency = Currency;
            this.currencyname = CurrencyName;
            this.bankstatementaplicationdatestring = bankstatementAplicationDateString;
            this.bankstatementAplicationDate = bankstatementAplicationDate;

            this.bankstatementAppliedAmmount = bankstatementAppliedAmmount;
            this.bankstatementappliedammountstring = MoneyUtils.ParseDecimalToString(bankstatementAppliedAmmount);

            //this.bankstatementAdjust = bankstatementAdjust == null ? 0m : bankstatementAdjust;
            //this.bankstatementadjuststring = MoneyUtils.ParseDecimalToString((decimal)this.bankstatementAdjust);

            //this.bankstatementInterchangesCharges = bankstatementInterchangesCharges == null ? 0m : bankstatementInterchangesCharges;
            //this.bankstatementinterchangeschargesstring = MoneyUtils.ParseDecimalToString((decimal)this.bankstatementInterchangesCharges);

            this.bankstatementBankFee = bankstatementBankFee == null ? 0m : bankstatementBankFee;
            this.bankstatementbankfeestring = MoneyUtils.ParseDecimalToString((decimal)this.bankstatementBankFee);


            //this.bankstatementChargesPayments = bankstatementChargesPayments == null ? 0m : bankstatementChargesPayments;
            //this.bankstatementchargespaymentsstring = MoneyUtils.ParseDecimalToString((decimal)bankstatementChargesPayments);

            //this.bankstatementRefund = bankstatementRefund == null ? 0m : bankstatementRefund;
            //this.bankstatementrefundstring = MoneyUtils.ParseDecimalToString((decimal)bankstatementRefund);

            this.bankstatementAppliedAmmountFinal = bankstatementAppliedAmmountFinal;
            this.bankstatementappliedammountfinalstring = MoneyUtils.ParseDecimalToString((decimal)bankstatementAppliedAmmountFinal); ;

            this.reservaparentpaymentitems = reservaparentpaymentitems;
            this.purchasepaymentitems = purchasepaymentitems;
            this.reservapaymentitems = reservapaymentitems;
            this.tblincomemovementitems = tblincomemovementitems;

            this.statusconciliation = (int)BankAccountReconcilitionStatus.Sin_conciliar;
            this.statusconciliationname = BankAccountReconcilitionStatus.Sin_conciliar.ToString();
            this.methodconciliation = methodConciliation;
            this.idBankStatementMethod = methodConciliation;

            //pagos de reservaciones parent            
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.BankstateParentReservRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.tblreservationsparentpayment).ToList()));

            //pagos de reservaciones
            var rsvpayment = this.unity.BankstateReservRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.tblreservationspayment).ToList();
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(rsvpayment));

            //reservaciones Web
            //var rsvweb = this.unity.ReservationsRepository.Get(x => x.idSubcategory == (int)ReservaWeb.Reserva_Web && x.memberCharge == this.idTPV && x.reservationSellPrice == this.bankstatementAppliedAmmount, null, "").ToList();
            //var rsvpaymentweb = this.unity.BankstateReservWebRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.tblreservations).ToList();
            //this.addSource(GeneralModelHelper.ConvertTbltoHelper(rsvpaymentweb));

            //pagos de purchase
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.BankstatePurchaseRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.tblpaymentspurchases).ToList()));

            //Incomes
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.BankstateIncomeRepository.Get(i => i.idBankStatements == this.idBankStatements).Select(m => m.tblincomemovement).ToList(), string.Empty));


            this.addReportItem(this.docpaymentpurchaseitems.ToList());
            this.addReportItem(this.docpaymentreservitems.ToList());
            this.addReportItem(this.docpaymentparentreservitems.ToList());
            this.addReportItem(this.docpaymentincomitems.ToList());

            if (docpaymentreservitems != null && this.docpaymentreservitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentreservitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentreservitems.First().PaymentMethodName;
            }
            else if (docpaymentpurchaseitems != null && this.docpaymentpurchaseitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentpurchaseitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentpurchaseitems.First().PaymentMethodName;
            }
            else if (docpaymentparentreservitems != null && this.docpaymentparentreservitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentparentreservitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentparentreservitems.First().PaymentMethodName;
            }
            else if ((this.docpaymentincomitems != null) && (this.docpaymentincomitems.Count() != 0))
            {
                this.paymentmethod = this.docpaymentincomitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentincomitems.First().PaymentMethodName;
            }
            else
            {
                this.paymentmethod = 0;
                this.paymentmethodname = "";
            }

            this.validateStatusStament(null);
            this.applyRowIndexFInancialStateItem();

            this.reservaparentpaymentitems = new List<tblreservationsparentpayment>();
            this.purchasepaymentitems = new List<tblpaymentspurchases>();
            this.reservapaymentitems = new List<tblreservationspayment>();
            this.tblincomemovementitems = new List<tblincomemovement>();

            if (sourcedata == false)
            {
                this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
                this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
                this.docpaymentreservitems = new List<docpaymentreserv>();
                this.docpaymentincomitems = new List<docpaymentincome>();
                this.financialstateitemlist = new List<financialstateitemModel>();
            }
        }

        public void GenerateConciliation(long idBankStatements, bool containGeneralData)
        {
            this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.docpaymentincomitems = new List<docpaymentincome>();
            this.financialstateitemlist = new List<financialstateitemModel>();

            var model = unity.BankstatementsRepository.Get(x => x.idBankStatements == idBankStatements, null, "tbltpv,tblcompanies,tblbankaccount.tblcurrencies").FirstOrDefault();

            if (!containGeneralData)
            {
                if (model != null)
                {
                    this.idTPV = model.idTPV;
                    this.idBankStatements = model.idBankStatements;
                    this.idBAccount = model.idBAccount;
                    this.tpvname = model.tbltpv.tpvidlocation;
                    this.idCompany = idCompany;
                    this.companyname = model.tblbankaccount.tblcompanies.companyshortname;
                    this.currency = model.tblbankaccount.tblcurrencies.idCurrency;
                    this.currencyname = model.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
                    this.baccount = model.tblbankaccount.idbaccount;
                    this.baccountname = model.tblbankaccount.baccountshortname;
                    this.bankstatementaplicationdatestring = DateTimeUtils.ParseDatetoString(model.bankstatementAplicationDate);
                    this.bankstatementAplicationDate = model.bankstatementAplicationDate;
                    this.bankstatementAppliedAmmount = model.bankstatementAppliedAmmount;
                    this.bankstatementappliedammountstring = MoneyUtils.ParseDecimalToString(model.bankstatementAppliedAmmount);
                    //this.bankstatementAdjust = model.bankstatementAdjust;
                    //this.bankstatementInterchangesCharges = model.bankstatementInterchangesCharges;
                    this.bankstatementBankFee = model.bankstatementBankFee;
                    //this.bankstatementChargesPayments = model.bankstatementChargesPayments;
                    //this.bankstatementRefund = model.bankstatementRefund;
                    this.statusconciliation = (int)BankAccountReconcilitionStatus.Sin_conciliar;
                    this.bankStatementsAuthCode = model.bankStatementsAuthCode;
                    this.bankStatementsTC = model.bankStatementsTC;

                    var rsvpayment = model.tblbankstatereserv.Select(y => y.tblreservationspayment).ToList();
                    // se agrega reserva
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(rsvpayment));
                    // se agrega reserva web   
                    //if (rsvpayment.Count() == 0)
                    //{
                    //    this.addSource(GeneralModelHelper.ConvertTbltoHelper(unity.ReservationsRepository.Get(y => y.idSubcategory == (int)ReservaWeb.Reserva_Web && y.idReservationStatus == (int)ReservaStatus.Active && y.memberCharge == this.idTPV && y.reservationSellPrice == this.bankstatementAppliedAmmount, null, "").ToList()));
                    //}
                    // se agrega purchases
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstatepurchase.Select(y => y.tblpaymentspurchases).ToList()));
                    // se agrega reservas directivos, awards/courtesy y i love worldpass
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstateparentreserv.Select(y => y.tblreservationsparentpayment).ToList()));
                    // se agregas ingresos
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstateincome.Select(c => c.tblincomemovement).ToList(), string.Empty));


                    this.addReportItem(this.docpaymentreservitems.ToList());
                    this.addReportItem(this.docpaymentpurchaseitems.ToList());
                    this.addReportItem(this.docpaymentparentreservitems.ToList());
                    this.addReportItem(this.docpaymentincomitems.ToList());
                }
                else throw new Exception("No se encuentran conciliaciones con el ID especificado.");
            }
            this.validateStatusStament(null);
            this.BodyCommonConciliation();
            this.validateStatusStament(null);
        }

        public void GenerateConciliationByExcelUpload()
        {
            this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.docpaymentincomitems = new List<docpaymentincome>();
            this.financialstateitemlist = new List<financialstateitemModel>();

            this.BodyCommonConciliation();
            this.validateStatusStament(null);
            this.validateMethodConciliationbyExcelUpload();
        }

        #region Common Action
        private void BodyCommonConciliation()
        {
            List<financialstateitemModel> _tmp = new List<financialstateitemModel>();
            int counterDaysBefore = 1;
            bool FinishSearch = false;

            int counterSearchStatement = 0;

            while ((this.financialstateitemlist.Sum(x => x.appliedAmmount) + _tmp.Sum(y => y.appliedAmmount)) < this.bankstatementAppliedAmmount && FinishSearch == false)
            {
                List<financialstateitemModel> _tmpwhile = new List<financialstateitemModel>();
                #region Getting New Data 
                DateTime dayBeforeStart = Convert.ToDateTime(this.bankstatementAplicationDate).AddDays(counterDaysBefore * -1);
                DateTime dayBeforeEnd = Convert.ToDateTime(this.bankstatementAplicationDate).AddDays(counterDaysBefore * -1);

                List<tblreservationspayment> tempDownPayReserva = new List<tblreservationspayment>();
                //List<tblreservations> tempDownPayReservaWeb = new List<tblreservations>();
                List<tblreservationsparentpayment> tempDownPayParentReserva = new List<tblreservationsparentpayment>();
                List<tblpaymentspurchases> tempDownPayPurchase = new List<tblpaymentspurchases>();
                List<tblincomemovement> tempIncomes = new List<tblincomemovement>();

                if (idhotelpartner != 0)
                {
                    tempDownPayParentReserva = (List<tblreservationsparentpayment>)this.getAnualPaymentsParentRsvTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idhotelpartner, this.idTPV);
                    tempDownPayReserva = (List<tblreservationspayment>)this.getAnualPaymentsRsvTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idhotelpartner, this.idTPV);
                    //tempDownPayReservaWeb = (List<tblreservations>)this.getAnualPaymentsRsvTPV_WEB(dayBeforeStart, dayBeforeEnd, this.currency, this.idhotelpartner, this.idTPV);
                    tempDownPayPurchase = (List<tblpaymentspurchases>)this.getAnualPaymentsPurchaseTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idhotelpartner, this.idTPV);

                }

                if (idhotelpartner == 0)
                {
                    tempDownPayParentReserva = (List<tblreservationsparentpayment>)this.getAnualPaymentsParentRsvTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idTPV);
                    tempDownPayPurchase = (List<tblpaymentspurchases>)this.getAnualPaymentsPurchaseTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idTPV);
                    tempDownPayReserva = (List<tblreservationspayment>)this.getAnualPaymentsRsvTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idTPV);
                    //tempDownPayReservaWeb = (List<tblreservations>)this.getAnualPaymentsRsvTPV_WEB(dayBeforeStart, dayBeforeEnd, this.currency, this.idTPV);

                }

                tempIncomes = (List<tblincomemovement>)this.getAnualPaymentsINCOMETPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idTPV);

                IEnumerable<docpaymentreserv> _tmpWhileDoReserv = new List<docpaymentreserv>();
                IEnumerable<docpaymentparentreserv> _tmpWhileDoParentReserv = new List<docpaymentparentreserv>();
                IEnumerable<docpaymentpurchase> _tmpWhileDoPurchase = new List<docpaymentpurchase>();
                IEnumerable<docpaymentincome> _tmpWhileDocIncome = new List<docpaymentincome>();

                _tmpWhileDoReserv = IEnumerableUtils.AddList(_tmpWhileDoReserv, GeneralModelHelper.ConvertTbltoHelper(tempDownPayReserva.Where(c => c.tblbankstatereserv.Count() == 0).ToList()).ToList());
                //_tmpWhileDoReserv = IEnumerableUtils.AddList(_tmpWhileDoReserv, GeneralModelHelper.ConvertTbltoHelper(tempDownPayReservaWeb).ToList());
                _tmpWhileDoParentReserv = IEnumerableUtils.AddList(_tmpWhileDoParentReserv, GeneralModelHelper.ConvertTbltoHelper(tempDownPayParentReserva.Where(c => c.tblbankstateparentreserv.Count() == 0).ToList()).ToList());
                _tmpWhileDoPurchase = IEnumerableUtils.AddList(_tmpWhileDoPurchase, GeneralModelHelper.ConvertTbltoHelper(tempDownPayPurchase.Where(c => c.tblbankstatepurchase.Count() == 0).ToList()).ToList());
                _tmpWhileDocIncome = IEnumerableUtils.AddList(_tmpWhileDocIncome, GeneralModelHelper.ConvertTbltoHelper(tempIncomes.Where(a => a.tblbankstateincome.Count() == 0).ToList(), string.Empty).ToList());

                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDoReserv.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDoParentReserv.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDoPurchase.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReporItem(_tmpWhileDocIncome.ToList()));
                #endregion

                #region Eval Data
                foreach (financialstateitemModel item in _tmpwhile)
                {
                    bool add = this.ReporItemISposibleIn(item);
                    if (add)
                    {
                        var allCost = this.financialstateitemlist.Sum(x => x.appliedAmmount) + _tmp.Sum(x => x.appliedAmmount) + item.appliedAmmount;
                        if (allCost < this.bankstatementAppliedAmmount)
                        {
                            _tmp.Add(item);
                        }
                        else if (allCost > this.bankstatementAppliedAmmount)
                        {
                            if (this.passDiference(allCost, this.bankstatementAppliedAmmount))
                            { // Con una diferencia mayor de 0.01 a 0.99 se puede agregar
                                _tmp.Add(item);
                                FinishSearch = true;
                                break;
                            }
                            else
                            {
                                FinishSearch = true;
                                break;
                            }
                        }
                        else if (allCost == this.bankstatementAppliedAmmount)
                        {
                            _tmp.Add(item);
                            FinishSearch = true;
                            break;
                        }
                    }
                }

                var interval = Convert.ToDateTime(this.bankstatementAplicationDate).Subtract(dayBeforeStart);
                if (interval.TotalDays > 10)
                {
                    break;
                }
                #endregion

                counterDaysBefore = counterDaysBefore + 1;
                counterSearchStatement = counterSearchStatement + 1;
            }
            this.validateStatusStament(null);
            this.financialstateitemlist = IEnumerableUtils.AddList(this.financialstateitemlist, _tmp);
            this.applyRowIndexFInancialStateItem();
        }

        private void validateMethodConciliationbyExcelUpload()
        {
            var allCost = this.financialstateitemlist.Sum(x => x.appliedAmmount);
            if (allCost < this.bankstatementAppliedAmmount)
            {
                // this.methodConciliation = (int)BankAccountReconciliationMethod.SinConciliar;

                if (this.passDiference(allCost, this.bankstatementAppliedAmmount) == true)
                { this.methodconciliation = (int)BankAccountReconciliationMethod.Sistema; }
                else { this.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar; }

            }
            else if (allCost > this.bankstatementAppliedAmmount)
            {
                // this.methodConciliation = (int)BankAccountReconciliationMethod.SinConciliar;
                if (this.passDiference(allCost, this.bankstatementAppliedAmmount) == true)
                { this.methodconciliation = (int)BankAccountReconciliationMethod.Sistema; }
                else { this.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar; }
            }
            else if (allCost == this.bankstatementAppliedAmmount && allCost != 0)
            {
                this.methodconciliation = (int)BankAccountReconciliationMethod.Sistema;
            }
            else
            {
                this.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar;
            }
        }

        private void validateStatusStament(decimal? toeval)
        {
            if (this.financialstateitemlist != null && this.financialstateitemlist.Count() != 0)
            {
                decimal? sum = toeval != null ? (decimal)toeval : financialstateitemlist.Sum(x => x.appliedAmmount);
                if (sum == this.bankstatementAppliedAmmount)
                {
                    this.statusconciliation = (int)BankAccountReconcilitionStatus.Completo; this.statusconciliationname = BankAccountReconcilitionStatus.Completo.ToString();
                    /*****************************************************/
                    var list = this.financialstateitemlist.ToList();
                    list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconcilitionStatus.Completo);
                    this.financialstateitemlist = list;
                    /******************************************************/
                }
                else if (sum < this.bankstatementAppliedAmmount)
                {
                    if (this.passDiference(sum, this.bankstatementAppliedAmmount) == true)
                    {
                        this.statusconciliation = (int)BankAccountReconcilitionStatus.Completo; this.statusconciliationname = BankAccountReconcilitionStatus.Completo.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconcilitionStatus.Completo);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }
                    else
                    {
                        this.statusconciliation = (int)BankAccountReconcilitionStatus.Parcial; this.statusconciliationname = BankAccountReconcilitionStatus.Parcial.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconcilitionStatus.Parcial);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }
                }
                else if (sum > this.bankstatementAppliedAmmount)
                {
                    if (this.passDiference(sum, this.bankstatementAppliedAmmount) == true)
                    {
                        this.statusconciliation = (int)BankAccountReconcilitionStatus.Completo; this.statusconciliationname = BankAccountReconcilitionStatus.Completo.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconcilitionStatus.Completo);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }

                    else
                    {
                        this.statusconciliation = (int)BankAccountReconcilitionStatus.Error; this.statusconciliationname = BankAccountReconcilitionStatus.Error.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconcilitionStatus.Error);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }
                }
                else
                {
                    this.statusconciliation = (int)BankAccountReconcilitionStatus.Sin_conciliar; this.statusconciliationname = BankAccountReconcilitionStatus.Sin_conciliar.ToString().Replace('_', ' ');
                    /*****************************************************/
                    var list = this.financialstateitemlist.ToList();
                    list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconcilitionStatus.Sin_conciliar);
                    this.financialstateitemlist = list;
                    /******************************************************/
                    ;
                }
            }
        }

        private bool passDiference(decimal? current, decimal? total)
        {
            var absolute = unity.BankstatementsGapRepository.Get(x => x.bankstatementsgapDate <= this.bankstatementAplicationDate).Select(c => c.bankstatementsgapValue).FirstOrDefault();

            if (absolute != 0)
            {
                decimal abs = Math.Abs((decimal)total - (decimal)current);
                if (abs >= 0.01m && abs <= absolute)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private bool passDiferenceFrom0(decimal current, decimal total)
        {
            var absolute = unity.BankstatementsGapRepository.Get(x => x.bankstatementsgapDate <= this.bankstatementAplicationDate).Select(c => c.bankstatementsgapValue).FirstOrDefault();
            if (absolute != 0)
            {
                decimal abs = Math.Abs(total - current);

                if (abs >= 0.00m && abs <= absolute)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void addReporItem(financialstateitemModel financialstateitem)
        {
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(financialstateitem);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, financialstateitem); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }

        }

        private void applyRowIndexFInancialStateItem()
        {
            int counterIndex = 0;
            int counterNumber = 1;

            foreach (financialstateitemModel model in this.financialstateitemlist)
            {
                model.rowIndex = counterIndex;
                counterIndex = counterIndex + 1;

                model.rowNumber = counterNumber;
                counterNumber = counterNumber + 1;
            }
        }

        public bool ReporItemISposibleIn(financialstateitemModel financialstateitem)
        {
            // Getting Current Object List
            List<financialstateitemModel> _tmp = new List<financialstateitemModel>();
            _tmp = this.financialstateitemlist.ToList();

            var _resultSearch = _tmp.Where(x => x.SourceData == financialstateitem.SourceData && x.ReferenceItem == financialstateitem.ReferenceItem).FirstOrDefault();


            if (_resultSearch == null)
            {
                return true;
                // this.addReporItem(financialstateitem);
            }
            else return false;

        }

        #endregion

        #region Getting Data
        private IEnumerable<tblreservationspayment> getAnualPaymentsRsvTPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<tblreservationspayment> paymentsrsv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && t.terminal == idTpv, null, "tblreservations,tblreservations.tblmemberships,tblreservations.tblpartners,tblreservations.tblpartners.tblhotelchains").ToList();
            return paymentsrsv;
        }

        private IEnumerable<tblreservationspayment> getAnualPaymentsRsvTPV(DateTime start, DateTime end, int idCurrency, int idHotelPartner, int idTpv)
        {
            List<tblreservationspayment> paymentsrsv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && t.tblreservations.idPartner == idHotelPartner && t.terminal == idTpv, null, "tblreservations,tblreservations.tblmemberships,tblreservations.tblpartners,tblreservations.tblpartners.tblhotelchains").ToList();
            return paymentsrsv;
        }

        private IEnumerable<tblreservations> getAnualPaymentsRsvTPV_WEB(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<tblreservations> paymentsrsv = new List<tblreservations>();
            if (idCurrency == (int)Currencies.US_Dollar)
            {
                paymentsrsv = unity.ReservationsRepository.Get(t => t.purchaseDate >= start && t.purchaseDate <= end && t.memberCharge == idTpv, null, "tblreservations.tblmemberships,tblreservations.tblpartners,tblreservations.tblpartners.tblhotelchains").ToList();
            }
            return paymentsrsv;
        }

        private IEnumerable<tblreservations> getAnualPaymentsRsvTPV_WEB(DateTime start, DateTime end, int idCurrency, int idHotelPartner, int idTpv)
        {
            List<tblreservations> paymentsrsv = new List<tblreservations>();
            if (idCurrency == (int)Currencies.US_Dollar)
            {
                paymentsrsv = unity.ReservationsRepository.Get(t => t.purchaseDate >= start && t.purchaseDate <= end && t.idPartner == idHotelPartner && t.memberCharge == idTpv, null, "tblreservations,tblreservations.tblmemberships,tblreservations.tblpartners,tblreservations.tblpartners.tblhotelchains").ToList();
            }
            return paymentsrsv;
        }

        private IEnumerable<tblreservationsparentpayment> getAnualPaymentsParentRsvTPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<tblreservationsparentpayment> paymentsparentrsv = unity.ReservationsParentPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && t.terminal == idTpv, null, "tblreservationsparent").ToList();
            return paymentsparentrsv;
        }

        private IEnumerable<tblreservationsparentpayment> getAnualPaymentsParentRsvTPV(DateTime start, DateTime end, int idCurrency, int idHotelPartner, int idTpv)
        {
            List<tblreservationsparentpayment> paymentsparentrsv = unity.ReservationsParentPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && t.tblreservationsparent.idPartner == idHotelPartner && t.terminal == idTpv, null, "tblreservationsparent").ToList();
            return paymentsparentrsv;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchaseTPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<tblpaymentspurchases> paymentspurchase = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate >= start && t.paymentDate <= end && t.idCurrency == idCurrency && t.terminal == idTpv, null, "tblpurchases.tblmemberships,tblpurchases.tblpartners,tblpurchases.tblpartners.tblhotelchains").ToList();
            return paymentspurchase;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchaseTPV(DateTime start, DateTime end, int idCurrency, int idHotelPartner, int idTpv)
        {
            List<tblpaymentspurchases> paymentspurchase = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate >= start && t.paymentDate <= end && t.idCurrency == idCurrency && t.tblpurchases.idPartner == idHotelPartner && t.terminal == idTpv, null, "tblpurchases.tblmemberships,tblpurchases.tblpartners,tblpurchases.tblpartners.tblhotelchains").ToList();
            return paymentspurchase;
        }

        private IEnumerable<tblincomemovement> getAnualPaymentsINCOMETPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            return unity.IncomeMovementsRepository.Get(n => n.incomemovapplicationdate >= start && n.incomemovapplicationdate <= end && n.tblincome.idcurrency == idCurrency && n.idtpv == idTpv).ToList();
        }

        private IEnumerable<tblincomemovement> getAnualPaymentsINCOMETPV(DateTime start, DateTime end, int idCurrency, int idCompany, int idTpv)
        {
            return unity.IncomeMovementsRepository.Get(n => n.incomemovapplicationdate >= start && n.incomemovapplicationdate <= end && n.tblincome.idcurrency == idCurrency && n.tblincome.idcompany == idCompany && n.idtpv == idTpv).ToList();
        }

        #endregion

        #region  Procesing Data PaymentReserva and Payment Purchase

        public void addSource(docpaymentreserv helper)
        {
            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0)
            {
                this.docpaymentreservitems = IEnumerableUtils.AddSingle<docpaymentreserv>(helper);
            }
            else { this.docpaymentreservitems = IEnumerableUtils.AddSingle<docpaymentreserv>(docpaymentreservitems, helper); }
        }

        public void addSource(List<docpaymentreserv> listhelper)
        {
            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0)
            {
                this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(listhelper);
            }
            else { this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(docpaymentreservitems, listhelper); }
        }

        public void addSource(docpaymentparentreserv helper)
        {
            if (this.docpaymentparentreservitems == null || this.docpaymentparentreservitems.Count() == 0)
            {
                this.docpaymentparentreservitems = IEnumerableUtils.AddSingle<docpaymentparentreserv>(helper);
            }
            else { this.docpaymentparentreservitems = IEnumerableUtils.AddSingle<docpaymentparentreserv>(docpaymentparentreservitems, helper); }
        }

        public void addSource(List<docpaymentparentreserv> listhelper)
        {
            if (this.docpaymentparentreservitems == null || this.docpaymentparentreservitems.Count() == 0)
            {
                this.docpaymentparentreservitems = IEnumerableUtils.AddList<docpaymentparentreserv>(listhelper);
            }
            else { this.docpaymentparentreservitems = IEnumerableUtils.AddList<docpaymentparentreserv>(docpaymentparentreservitems, listhelper); }
        }

        public void addSource(docpaymentpurchase helper)
        {
            if (this.docpaymentpurchaseitems == null || this.docpaymentpurchaseitems.Count() == 0)
            {
                this.docpaymentpurchaseitems = IEnumerableUtils.AddSingle<docpaymentpurchase>(helper);
            }
            else { this.docpaymentpurchaseitems = IEnumerableUtils.AddSingle<docpaymentpurchase>(docpaymentpurchaseitems, helper); }
        }

        public void addSource(List<docpaymentpurchase> listhelper)
        {
            if (this.docpaymentpurchaseitems == null || this.docpaymentpurchaseitems.Count() == 0)
            {
                this.docpaymentpurchaseitems = IEnumerableUtils.AddList<docpaymentpurchase>(listhelper);
            }
            else { this.docpaymentpurchaseitems = IEnumerableUtils.AddList<docpaymentpurchase>(docpaymentpurchaseitems, listhelper); }
        }

        public void addSource(docpaymentincome helper)
        {
            if (this.docpaymentincomitems == null || this.docpaymentincomitems.Count() == 0) { this.docpaymentincomitems = IEnumerableUtils.AddSingle<docpaymentincome>(helper); }
            else { IEnumerableUtils.AddSingle<docpaymentincome>(this.docpaymentincomitems, helper); }
        }

        public void addSource(List<docpaymentincome> listhelper)
        {
            if (this.docpaymentincomitems == null || this.docpaymentincomitems.Count() == 0) { this.docpaymentincomitems = IEnumerableUtils.AddList<docpaymentincome>(listhelper); }
            else { IEnumerableUtils.AddList<docpaymentincome>(this.docpaymentincomitems, listhelper); }
        }

        #endregion

        #region  Generatin ReportItem
        #region Reservationpayment
        private void addReportItem(docpaymentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, "", 7, "RESERVA", helper.Reservation, helper.ReservationPayment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, "- Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString(), " - pago ", helper.ReservationPayment.ToString()),true,1);
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentreserv> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private financialstateitemModel GenerateReportItem(docpaymentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, "", 7, "RESERVA", helper.Reservation, helper.ReservationPayment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, " - Reserva : ", helper.Reservation == 0 ? "" : helper.Reservation.ToString(), " - pago ", helper.ReservationPayment.ToString()), false,1);

            _object.generateString();
            return _object;
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpaymentreserv> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();
            foreach (docpaymentreserv _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }
            return lst;
        }
        #endregion
        #region Reservationparentpayment
        private void addReportItem(docpaymentparentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, "", 7, "RESERVA", helper.Reservation, helper.ReservationPayment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, "- Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString(), " - pago ", helper.ReservationPayment.ToString()), true,2);
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentparentreserv> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }


        private financialstateitemModel GenerateReportItem(docpaymentparentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, "", 7, "RESERVA", helper.Reservation, helper.ReservationPayment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, " - Reserva : ", helper.Reservation == 0 ? "" : helper.Reservation.ToString(), " - pago ", helper.ReservationPayment.ToString()), false,2);

            _object.generateString();
            return _object;
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpaymentparentreserv> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();
            foreach (docpaymentparentreserv _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }
            return lst;
        }
        #endregion
        #region Purchase payment
        private void addReportItem(docpaymentpurchase helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.paymentDate, helper.paymentAmount, 0m, helper.PaymentMethodName, "", 8, "PURCHASE", helper.Purchase, helper.Payment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, "- Purchase : ", helper.Purchase == 0 ? " " : helper.Purchase.ToString(), " - pago ", helper.Payment.ToString()), true,0);
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentpurchase> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private financialstateitemModel GenerateReportItem(docpaymentpurchase helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.paymentDate, helper.paymentAmount, 0m, helper.PaymentMethodName, "", 8, "PURCHASE", helper.Purchase, helper.Payment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, " - Purchase : ", helper.Purchase == 0 ? "" : helper.Purchase.ToString(), " - pago ", helper.Payment.ToString()), false,0);

            _object.generateString();
            return _object;
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpaymentpurchase> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();
            foreach (docpaymentpurchase _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }
            return lst;
        }
        #endregion
        #region Income
        private void addReportItem(docpaymentincome helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.incomemovapplicationDate, helper.incomemovchargedAmount, 0m, helper.PaymentMethodName, helper.CompanyName, 2, "Income movement", helper.Income, helper.IncomeMovement, string.Concat("Movimiento de ingresos: " + helper.identifier), true,0);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }

            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentincome> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private financialstateitemModel GenerateReporItem(docpaymentincome helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.incomemovapplicationDate, helper.incomemovchargedAmount, 0m, helper.PaymentMethodName, helper.CompanyName, 2, "Income movement", helper.Income, helper.IncomeMovement, string.Concat("Movimiento de ingresos: " + helper.identifier), false,0);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            return _object;
        }

        private List<financialstateitemModel> GenerateReporItem(List<docpaymentincome> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();

            foreach (docpaymentincome _helper in helper)
            {
                lst.Add(GenerateReporItem(_helper));
            }

            return lst;
        }
        #endregion
        #endregion
    }

    public class bankstatements : tblbankstatements2
    {
        public long idbankstatements2 { get; set; }
        public int idbaccount { get; set; }
        public string baccountName { get; set; }
        public int idcompany { get; set; }
        public string companyname { get; set; }
        public int idbankstatementmethod { get; set; }
        public string bankstatementMethodName { get; set; }
        public int idmovementtype { get; set; }
        public string MovementTypeName { get; set; }
        public DateTime bankstatementsAplicationDate { get; set; }
        public string bankstatementsAplicationDateString { get; set; }
        public string bankstatementsConcept { get; set; }
        public decimal? bankstatementsCargo { get; set; }
        public string bankstatementsCargoString { get; set; }
        public decimal? bankstatementsAbono { get; set; }
        public string bankstatementsAbonoString { get; set; }
        public string bankstatements2ComisionString { get; set; }
        public decimal bankstatementsamountfinal { get; set; }
        public string bankstatementsamountfinalstring { get; set; }
        public string bankstatementsdescriptionstring { get; set; }
        public int bankstatementsCreateBy { get; set; }
        public int bankstatementsUpdateBy { get; set; }
        public int currency { get; set; }
        public string currencyName { get; set; }
        public int paymentmethod { get; set; }
        public string paymentmethodname { get; set; }
        public int statusconciliation { get; set; }
        public string statusconciliationname { get; set; }
        public int methodconciliation { get; set; }
        public int rowIndex { get; set; }
        public int rowNumber { get; set; }
        public bool ispositive { get; set; }

        private IEnumerable<tblreservationsparentpayment> reservaparentpaymentitems { get; set; }
        private IEnumerable<tblreservationspayment> reservapaymentitems { get; set; }
        private IEnumerable<tblpaymentspurchases> purchasepaymentitems { get; set; }
        private IEnumerable<tblfondos> fondositems { get; set; }
        private IEnumerable<tblpayment> paymentitems { get; set; }
        private IEnumerable<tblincomemovement> incomemovementsitems { get; set; }


        public IEnumerable<docpaymentparentreserv> docpaymentparentreservitems { get; set; }
        public IEnumerable<docpaymentreserv> docpaymentreservitems { get; set; }
        public IEnumerable<docpaymentpurchase> docpaymentpurchaseitems { get; set; }
        public IEnumerable<docfondos> docfondositems { get; set; }
        public IEnumerable<docpayments> docpaymentitems { get; set; }
        public IEnumerable<docpaymentincome> docpaymentincomitems { get; set; }

        public virtual IEnumerable<financialstateitemModel> financialstateitemlist { get; set; }

        private readonly UnitOfWork unity = new UnitOfWork();

        public bankstatements()
        { }

        public bankstatements(long idBankStatements2, int BAccount, string BAccountName, int Currency, string CurrencyName, int idMovementType, string MovementTypeName, string bankstatementAplicationDateString, DateTime bankstatementAplicationDate, decimal? bankstatementCargo, decimal? bankstatementAbono, bool sourcedata, bool financialstateitem)
        {
            this.idBankStatements2 = idBankStatements2;
            this.idBAccount = BAccount;
            this.baccountName = BAccountName;
            this.idMovementType = idMovementType;
            this.currency = Currency;
            this.currencyName = CurrencyName;
            this.MovementTypeName = MovementTypeName;
            this.bankstatementsAplicationDateString = bankstatementAplicationDateString;
            this.bankstatementsAplicationDate = bankstatementAplicationDate;
            this.bankstatementsCargo = bankstatementCargo;
            this.bankstatementsAbono = bankstatementAbono;
            this.bankstatementsCargoString = MoneyUtils.ParseDecimalToString((decimal)bankstatementCargo);
            this.bankstatementsAbonoString = MoneyUtils.ParseDecimalToString((decimal)bankstatementsAbono);
        }
        public bankstatements(long idBankStatements2, /*int idTPV, string TPVName,*/ int idCompany, string CompanyName, int BAccount, string BAccountName, int Currency, string CurrencyName, int idMovementType, string MovementTypeName, string bankstatementAplicationDateString, DateTime bankstatementAplicationDate, decimal? bankstatementCargo, decimal? bankstatementAbono, string Concept, int methodConciliation, decimal? bankfee, bool sourcedata, bool financialstateitem)
        {
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.docpaymentincomitems = new List<docpaymentincome>();
            this.docfondositems = new List<docfondos>();
            this.docpaymentitems = new List<docpayments>();
            this.financialstateitemlist = new List<financialstateitemModel>();
            this.purchasepaymentitems = new List<tblpaymentspurchases>();
            this.reservapaymentitems = new List<tblreservationspayment>();
            this.reservaparentpaymentitems = new List<tblreservationsparentpayment>();
            this.incomemovementsitems = new List<tblincomemovement>();
            this.fondositems = new List<tblfondos>();
            this.paymentitems = new List<tblpayment>();

            this.idBankStatements2 = idBankStatements2;
            //this.idTPV = idTPV;
            //this.TPVName = TPVName;
            this.idBAccount = BAccount;
            this.baccountName = BAccountName;
            this.idcompany = idCompany;
            this.companyname = CompanyName;
            this.idMovementType = idMovementType;
            this.currency = Currency;
            this.currencyName = CurrencyName;
            this.MovementTypeName = MovementTypeName;
            this.bankstatementsAplicationDateString = bankstatementAplicationDateString;
            this.bankstatements2AplicationDate = bankstatementAplicationDate;

            this.bankstatementsCargo = bankstatementCargo;
            this.bankstatementsAbono = bankstatementAbono;
            this.bankstatementsCargoString = MoneyUtils.ParseDecimalToString((decimal)bankstatementsCargo);
            this.bankstatementsAbonoString = MoneyUtils.ParseDecimalToString((decimal)bankstatementsAbono);
            this.bankstatementsamountfinal = bankstatementsCargo == 0m ? (decimal)bankstatementsAbono : (decimal)bankstatementsCargo;
            this.bankstatementsamountfinalstring = bankstatementsCargo == 0m ? MoneyUtils.ParseDecimalToString(((decimal)bankstatementsAbono + (decimal)bankfee)) : MoneyUtils.ParseDecimalToString((decimal)bankstatementsCargo);
            this.bankstatementsdescriptionstring = Concept;
            this.bankstatements2BankFee = bankfee;
            this.bankstatements2ComisionString = MoneyUtils.ParseDecimalToString((decimal)bankstatements2BankFee);

            this.reservaparentpaymentitems = reservaparentpaymentitems;
            this.purchasepaymentitems = purchasepaymentitems;
            this.reservapaymentitems = reservapaymentitems;
            this.incomemovementsitems = incomemovementsitems;
            this.fondositems = fondositems;
            this.paymentitems = paymentitems;
            this.ispositive = bankstatementsAbono == 0m ? false : true;

            this.statusconciliation = (int)BankAccountReconciliationStatus.Sin_conciliar;
            this.statusconciliationname = BankAccountReconciliationStatus.Sin_conciliar.ToString();
            this.methodconciliation = methodConciliation;
            this.idBankStatementMethod = methodConciliation;

            // Adding PURCHASES PAYMENTS
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.Statements2PURCHASESRepository.Get(x => x.idBankStatements2 == this.idBankStatements2).Select(c => c.tblpaymentspurchases).ToList()));

            // Adding RESERVATIONS
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.Statements2RESERVRepository.Get(x => x.idBankStatements2 == this.idBankStatements2).Select(c => c.tblreservationspayment).ToList()));

            // Adding RESERVATIONS WITHOUT MEMBERSHIP
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.Statements2PARENTRESERVRepository.Get(x => x.idBankStatements2 == this.idBankStatements2).Select(c => c.tblreservationsparentpayment).ToList()));

            // Adding INCOMES
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.Statements2INCOMERepository.Get(i => i.idBankStatements2 == this.idBankStatements2).Select(m => m.tblincomemovement).ToList(), string.Empty));

            // Adding FONDO
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.Statements2FONDORepository.Get(i => i.idBankStatements2In == this.idBankStatements2).Select(m => m.tblfondos).ToList(), ispositive));
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.Statements2FONDORepository.Get(i => i.idBankStatements2Out == this.idBankStatements2).Select(m => m.tblfondos).ToList(), ispositive));

            // Adding PAYMENT INVOICE
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.Statements2INVOICERepository.Get(i => i.idBankStatements2 == this.idBankStatements2).Select(m => m.tblpayment).ToList(), string.Empty));


            this.addReportItem(this.docpaymentreservitems.ToList());//RESERVATIONS
            this.addReportItem(this.docpaymentparentreservitems.ToList());//RESERVATION WITHOUT MEMBERSHIP
            this.addReportItem(this.docpaymentpurchaseitems.ToList());//PURCHASES
            this.addReportItem(this.docfondositems.ToList()); //FONDOS
            this.addReportItem(this.docpaymentincomitems.ToList());// INCOMES
            this.addReportItem(this.docpaymentitems.ToList()); // PAYMENT INVOICE

            if (this.docpaymentreservitems != null && this.docpaymentreservitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentreservitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentreservitems.First().PaymentMethodName;
            }
            else if (this.docpaymentparentreservitems != null && this.docpaymentparentreservitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentparentreservitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentparentreservitems.First().PaymentMethodName;
            }
            else if (this.docpaymentpurchaseitems != null && this.docpaymentpurchaseitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentpurchaseitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentpurchaseitems.First().PaymentMethodName;
            }
            else if (this.docfondositems != null && this.docfondositems.Count() != 0)
            {
                this.paymentmethod = this.docfondositems.First().PaymentMethod;
                this.paymentmethodname = this.docfondositems.First().PaymentMethodName;
            }
            else if (this.docpaymentincomitems != null && this.docpaymentincomitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentincomitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentincomitems.First().PaymentMethodName;
            }
            else if (this.docpaymentitems != null && this.docpaymentitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentitems.First().PaymentMethodName;
            }
            else
            {
                this.paymentmethod = 0;
                this.paymentmethodname = "";
            }

            this.validateStatusStament(null);
            this.applyRowIndexFInancialStateItem();

            this.purchasepaymentitems = new List<tblpaymentspurchases>();
            this.reservapaymentitems = new List<tblreservationspayment>();
            this.reservaparentpaymentitems = new List<tblreservationsparentpayment>();
            this.incomemovementsitems = new List<tblincomemovement>();
            this.fondositems = new List<tblfondos>();
            this.paymentitems = new List<tblpayment>();


            if (sourcedata == false)
            {
                this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
                this.docpaymentreservitems = new List<docpaymentreserv>();
                this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
                this.docpaymentincomitems = new List<docpaymentincome>();
                this.docfondositems = new List<docfondos>();
                this.docpaymentitems = new List<docpayments>();
                this.financialstateitemlist = new List<financialstateitemModel>();
            }
        }
        public void GenerateConciliation(long idBankStatements, bool containGeneralData)
        {
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
            this.docpaymentincomitems = new List<docpaymentincome>();
            this.docfondositems = new List<docfondos>();
            this.docpaymentitems = new List<docpayments>();
            this.financialstateitemlist = new List<financialstateitemModel>();

            var model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatements, null, "tblbankaccount.tblcurrencies").FirstOrDefault();

            if (!containGeneralData)
            {
                if (model != null)
                {
                    this.idBankStatements2 = model.idBankStatements2;
                    this.idBAccount = model.idBAccount;
                    this.idcompany = model.tblbankaccount.idcompany;
                    this.companyname = model.tblbankaccount.tblcompanies.companyshortname;
                    this.currency = model.tblbankaccount.tblcurrencies.idCurrency;
                    this.currencyName = model.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
                    this.idbaccount = model.tblbankaccount.idbaccount;
                    this.baccountName = model.tblbankaccount.baccountshortname;
                    this.bankstatementsAplicationDateString = DateTimeUtils.ParseDatetoString(model.bankstatements2AplicationDate);
                    this.bankstatements2AplicationDate = model.bankstatements2AplicationDate;
                    this.bankstatements2Pay = model.bankstatements2Pay;
                    this.bankstatementsAbonoString = MoneyUtils.ParseDecimalToString((decimal)model.bankstatements2Pay);
                    this.bankstatements2Charge = model.bankstatements2Charge;
                    this.bankstatementsamountfinal = model.bankstatements2Charge == 0m ? (decimal)model.bankstatements2Pay : (decimal)model.bankstatements2Charge * -1;
                    this.statusconciliation = (int)BankAccountReconciliationStatus.Sin_conciliar;
                    this.ispositive = model.bankstatements2Pay == 0m ? false : true;
                    this.bankstatements2BankFee = model.bankstatements2BankFee;
                    this.bankstatements2ComisionString = MoneyUtils.ParseDecimalToString((decimal)bankstatements2BankFee);

                    // Adding PURCHASES
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstat2purchase.Select(y => y.tblpaymentspurchases).ToList()));

                    // Adding RESERVATIONS WITHOUT MEMBERSHIPS
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstat2parentreserv.Select(y => y.tblreservationsparentpayment).ToList()));

                    // Adding RESERVATIONS
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstat2reserv.Select(y => y.tblreservationspayment).ToList()));

                    // Adding INCOMES
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstat2income.Select(c => c.tblincomemovement).ToList(), string.Empty));

                    // Adding FONDOS
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstat2fondo.Select(c => c.tblfondos).ToList(), ispositive));

                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstat2fondo1.Select(c => c.tblfondos).ToList(), ispositive));

                    // Adding PAYMENT INVOICE
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstat2invoice.Select(c => c.tblpayment).ToList(), string.Empty));

                    this.addReportItem(this.docpaymentpurchaseitems.ToList());// PURCHASES
                    this.addReportItem(this.docpaymentparentreservitems.ToList());// RESERVATION WITHOUT MEMBERSHIPS
                    this.addReportItem(this.docpaymentreservitems.ToList()); // RESERVATIONS
                    this.addReportItem(this.docpaymentincomitems.ToList()); // INCOMES
                    this.addReportItem(this.docfondositems.ToList()); // FONDOS
                    this.addReportItem(this.docpaymentitems.ToList()); // PAYMENT INVOICE
                }
                else throw new Exception("No se encuentran conciliaciones con el ID especificado.");
            }
            this.validateStatusStament(null);
            this.BodyCommonConciliation();
            this.validateStatusStament(null);
        }

        public void GenerateConciliationByExcelUpload()
        {
            // Initializaing
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
            this.docpaymentincomitems = new List<docpaymentincome>();
            this.docfondositems = new List<docfondos>();
            this.docpaymentitems = new List<docpayments>();
            this.financialstateitemlist = new List<financialstateitemModel>();

            this.BodyCommonConciliation();
            this.validateStatusStament(null);
            this.validateMethodConciliationbyExcelUpload();
        }

        #region Commom Actions
        private void BodyCommonConciliation()
        {
            /**************************************** Generating Options to Complete BankStatements / Generatins Option if BankStatement is Build by EXCEL *************************************************/
            List<financialstateitemModel> _tmp = new List<financialstateitemModel>();
            int counterDaysBefore = 1;
            bool FinishSearch = false;

            int counterSearchStatement = 0;

            while ((this.financialstateitemlist.Sum(x => x.appliedAmmount) + _tmp.Sum(y => y.appliedAmmount)) < this.bankstatementsamountfinal && FinishSearch == false)
            {
                // List of temp While
                List<financialstateitemModel> _tmpwhile = new List<financialstateitemModel>();

                #region Getting New Data 

                DateTime dayBeforeStart = this.bankstatements2AplicationDate.AddDays(counterDaysBefore * -1);
                DateTime dayBeforeEnd = this.bankstatements2AplicationDate.AddDays(counterDaysBefore * -1);

                // Getting Data
                List<tblreservationspayment> tempReservations = new List<tblreservationspayment>();
                List<tblreservationsparentpayment> tempReservationsParent = new List<tblreservationsparentpayment>();
                List<tblpaymentspurchases> tempPurchases = new List<tblpaymentspurchases>();
                List<tblincomemovement> tempIncomes = new List<tblincomemovement>();
                List<tblfondos> tempFondos = new List<tblfondos>();
                List<tblpayment> tempPayment = new List<tblpayment>();
                // Getting records by hotel
                //if (idhotel != 0)
                //{
                //    tempReservations = (List<tblreservationpayments>)this.getAnualPaymentsRESERVTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idhotel, this.idTPV);
                //    tempUpscales = (List<Payment>)this.getAnualPaymentsUPSCLTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idhotel, this.idTPV);
                //}

                //if (idhotel == 0)
                //{
                tempReservations = (List<tblreservationspayment>)this.getAnualPaymentsRESERV(dayBeforeStart, dayBeforeEnd, this.currency, Constants.methodPayment);
                tempReservationsParent = (List<tblreservationsparentpayment>)this.getAnualPaymentsRESERVPARENT(dayBeforeStart, dayBeforeEnd, this.currency, Constants.methodPayment);
                tempPurchases = (List<tblpaymentspurchases>)this.getAnualPaymentsPURCHASES(dayBeforeStart, dayBeforeEnd, this.currency, Constants.methodPayment);
                //}
                tempIncomes = (List<tblincomemovement>)this.getAnualPaymentsINCOME(dayBeforeStart, dayBeforeEnd, this.idbaccount, this.currency, Constants.methodPaymentVta);
                tempFondos = (List<tblfondos>)this.getAnualPaymentsFONDOSIN(dayBeforeStart, dayBeforeEnd, this.idbaccount, this.currency);
                tempPayment = (List<tblpayment>)this.getAnualPaymentsPAYMENTINVOICE(dayBeforeStart, dayBeforeEnd, this.idbaccount, this.currency);


                // Adding SourceData temp List
                IEnumerable<docpaymentpurchase> _tmpWhileDocPurchase = new List<docpaymentpurchase>();
                IEnumerable<docpaymentreserv> _tmpWhileDocReserv = new List<docpaymentreserv>();
                IEnumerable<docpaymentparentreserv> _tmpWhileDocReservParent = new List<docpaymentparentreserv>();
                IEnumerable<docpaymentincome> _tmpWhileDocIncome = new List<docpaymentincome>();
                IEnumerable<docfondos> _tmpWhileDocFondos = new List<docfondos>();
                IEnumerable<docpayments> _tmpWhileDocPayment = new List<docpayments>();

                // Parsing Data to docpaymentupscl / docpaymentreserv / docpaymentincome
                _tmpWhileDocReserv = IEnumerableUtils.AddList(_tmpWhileDocReserv, GeneralModelHelper.ConvertTbltoHelper(tempReservations.Where(c => c.tblbankstat2reserv.Count() == 0).ToList()).ToList());
                _tmpWhileDocReservParent = IEnumerableUtils.AddList(_tmpWhileDocReservParent, GeneralModelHelper.ConvertTbltoHelper(tempReservationsParent.Where(c => c.tblbankstat2parentreserv.Count() == 0).ToList()).ToList());
                _tmpWhileDocPurchase = IEnumerableUtils.AddList(_tmpWhileDocPurchase, GeneralModelHelper.ConvertTbltoHelper(tempPurchases.Where(c => c.tblbankstat2purchase.Count() == 0).ToList()).ToList());
                _tmpWhileDocIncome = IEnumerableUtils.AddList(_tmpWhileDocIncome, GeneralModelHelper.ConvertTbltoHelper(tempIncomes.Where(a => a.tblbankstat2income.Count() == 0).ToList(), string.Empty).ToList());
                _tmpWhileDocFondos = IEnumerableUtils.AddList(_tmpWhileDocFondos, GeneralModelHelper.ConvertTbltoHelper(tempFondos.Where(a => a.tblbankstat2fondo.Count() == 0).ToList(), ispositive).ToList());
                _tmpWhileDocPayment = IEnumerableUtils.AddList(_tmpWhileDocPayment, GeneralModelHelper.ConvertTbltoHelper(tempPayment.Where(a => a.tblbankstat2invoice.Count() == 0).ToList(), string.Empty).ToList());

                // Adding to Financial State Item Temp
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDocReserv.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDocReservParent.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDocPurchase.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDocIncome.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDocFondos.ToList()));
                _tmpwhile = (List<financialstateitemModel>)IEnumerableUtils.AddList(_tmpwhile, GenerateReportItem(_tmpWhileDocPayment.ToList()));

                #endregion

                #region Eval Data

                foreach (financialstateitemModel item in _tmpwhile)
                {

                    bool add = this.ReporItemISposibleIn(item);
                    if (add)
                    {
                        var allCost = this.financialstateitemlist.Sum(x => x.appliedAmmount) + _tmp.Sum(x => x.appliedAmmount) + item.appliedAmmount;

                        if (allCost < this.bankstatementsamountfinal)
                        {
                            _tmp.Add(item);
                        }
                        else if (allCost > this.bankstatementsamountfinal)
                        {
                            if (this.passDiference(allCost, (decimal)this.bankstatementsamountfinal))
                            { // Con una diferencia mayor de 0.01 a 0.99 se puede agregar
                                _tmp.Add(item);
                                FinishSearch = true;
                                break;
                            }
                            else
                            {
                                FinishSearch = true;
                                break;
                            }
                        }
                        else if (allCost == this.bankstatementsamountfinal)
                        {
                            _tmp.Add(item);
                            FinishSearch = true;
                            break;
                        }
                    }
                }

                // Finishing by Date 
                var interval = this.bankstatements2AplicationDate.Subtract(dayBeforeStart);
                if (interval.TotalDays > 10)
                {
                    break;
                }

                #endregion
                counterDaysBefore = counterDaysBefore + 1;
                counterSearchStatement = counterSearchStatement + 1;
            }

            this.validateStatusStament(null);

            this.financialstateitemlist = IEnumerableUtils.AddList(this.financialstateitemlist, _tmp);

            this.applyRowIndexFInancialStateItem();
        }

        private void validateMethodConciliationbyExcelUpload()
        {
            var allCost = this.financialstateitemlist.Sum(x => x.appliedAmmount);

            if (allCost < this.bankstatementsamountfinal)
            {
                // this.methodConciliation = (int)BankAccountReconciliationMethod.SinConciliar;

                if (this.passDiference(allCost, (decimal)this.bankstatementsamountfinal) == true)
                { this.methodconciliation = (int)BankAccountReconciliationMethod.Sistema; }
                else { this.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar; }

            }
            else if (allCost > this.bankstatementsamountfinal)
            {
                // this.methodConciliation = (int)BankAccountReconciliationMethod.SinConciliar;
                if (this.passDiference(allCost, (decimal)this.bankstatementsamountfinal) == true)
                { this.methodconciliation = (int)BankAccountReconciliationMethod.Sistema; }
                else { this.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar; }
            }
            else if (allCost == this.bankstatementsamountfinal && allCost != 0)
            {
                this.methodconciliation = (int)BankAccountReconciliationMethod.Sistema;
            }
            else
            {
                this.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar;
            }

        }

        public bool ReporItemISposibleIn(financialstateitemModel financialstateitem)
        {
            // Getting Current Object List
            List<financialstateitemModel> _tmp = new List<financialstateitemModel>();
            _tmp = this.financialstateitemlist.ToList();

            var _resultSearch = _tmp.Where(x => x.SourceData == financialstateitem.SourceData && x.ReferenceItem == financialstateitem.ReferenceItem).FirstOrDefault();


            if (_resultSearch == null)
            {
                return true;
                // this.addReporItem(financialstateitem);
            }
            else return false;

        }

        private void validateStatusStament(decimal? toeval)
        {
            if (this.financialstateitemlist != null && this.financialstateitemlist.Count() != 0)
            {
                decimal sum = toeval != null ? (decimal)toeval : financialstateitemlist.Sum(x => x.appliedAmmount);

                if (sum == this.bankstatementsamountfinal)
                {
                    this.statusconciliation = (int)BankAccountReconciliationStatus.Completo; this.statusconciliationname = BankAccountReconciliationStatus.Completo.ToString();
                    /*****************************************************/
                    var list = this.financialstateitemlist.ToList();
                    list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconciliationStatus.Completo);
                    this.financialstateitemlist = list;
                    /******************************************************/
                }

                else if (sum < this.bankstatementsamountfinal)
                {

                    if (this.passDiference(sum, (decimal)this.bankstatementsamountfinal) == true)
                    {
                        this.statusconciliation = (int)BankAccountReconciliationStatus.Completo; this.statusconciliationname = BankAccountReconciliationStatus.Completo.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconciliationStatus.Completo);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }
                    else
                    {
                        this.statusconciliation = (int)BankAccountReconciliationStatus.Parcial; this.statusconciliationname = BankAccountReconciliationStatus.Parcial.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconciliationStatus.Parcial);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }

                }

                else if (sum > this.bankstatementsamountfinal)
                {
                    if (this.passDiference(sum, (decimal)this.bankstatementsamountfinal) == true)
                    {
                        this.statusconciliation = (int)BankAccountReconciliationStatus.Completo; this.statusconciliationname = BankAccountReconciliationStatus.Completo.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconciliationStatus.Completo);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }

                    else
                    {
                        this.statusconciliation = (int)BankAccountReconciliationStatus.Error; this.statusconciliationname = BankAccountReconciliationStatus.Error.ToString();
                        /*****************************************************/
                        var list = this.financialstateitemlist.ToList();
                        list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconciliationStatus.Error);
                        this.financialstateitemlist = list;
                        /******************************************************/
                    }
                }

                else
                {
                    this.statusconciliation = (int)BankAccountReconciliationStatus.Sin_conciliar; this.statusconciliationname = BankAccountReconciliationStatus.Sin_conciliar.ToString().Replace('_', ' ');
                    /*****************************************************/
                    var list = this.financialstateitemlist.ToList();
                    list.ForEach(v => v.bankStatementStatus = (int)BankAccountReconciliationStatus.Sin_conciliar);
                    this.financialstateitemlist = list;
                    /******************************************************/
                    ;
                }
            }
        }

        private bool passDiference(decimal current, decimal total)
        {
            var absolute = unity.BankstatementsGapRepository.Get(x => x.bankstatementsgapDate <= this.bankstatements2AplicationDate).Select(c => c.bankstatementsgapValue).FirstOrDefault();

            if (absolute != 0)
            {
                decimal abs = Math.Abs(total - current);
                if (abs >= 0.01m && abs <= absolute)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private void applyRowIndexFInancialStateItem()
        {
            int counterIndex = 0;
            int counterNumber = 1;

            foreach (financialstateitemModel model in this.financialstateitemlist)
            {
                model.rowIndex = counterIndex;
                counterIndex = counterIndex + 1;
                model.rowNumber = counterNumber;
                counterNumber = counterNumber + 1;
            }
        }
        #endregion

        #region RESERVATION

        #region Procesing Data
        public void addSource(docpaymentreserv helper)
        {
            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0)
            {
                this.docpaymentreservitems = IEnumerableUtils.AddSingle<docpaymentreserv>(helper);
            }
            else { this.docpaymentreservitems = IEnumerableUtils.AddSingle<docpaymentreserv>(docpaymentreservitems, helper); }
        }

        public void addSource(List<docpaymentreserv> listhelper)
        {
            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0)
            {
                this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(listhelper);
            }
            else { this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(docpaymentreservitems, listhelper); }
        }

        public void addSource(docpaymentparentreserv helper)
        {
            if (this.docpaymentparentreservitems == null || this.docpaymentparentreservitems.Count() == 0)
            {
                this.docpaymentparentreservitems = IEnumerableUtils.AddSingle<docpaymentparentreserv>(helper);
            }
            else { this.docpaymentparentreservitems = IEnumerableUtils.AddSingle<docpaymentparentreserv>(docpaymentparentreservitems, helper); }
        }

        public void addSource(List<docpaymentparentreserv> listhelper)
        {
            if (this.docpaymentparentreservitems == null || this.docpaymentparentreservitems.Count() == 0)
            {
                this.docpaymentparentreservitems = IEnumerableUtils.AddList<docpaymentparentreserv>(listhelper);
            }
            else { this.docpaymentparentreservitems = IEnumerableUtils.AddList<docpaymentparentreserv>(docpaymentparentreservitems, listhelper); }
        }
        #endregion

        #region Generating ReportItem

        private void addReportItem(docpaymentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, "", 7, "RESERVA", helper.Reservation, helper.ReservationPayment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, "- Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString(), " - pago ", helper.ReservationPayment.ToString()), true,1);
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentreserv> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private void addReportItem(docpaymentparentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, "", 7, "RESERVA", helper.Reservation, helper.ReservationPayment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, "- Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString(), " - pago ", helper.ReservationPayment.ToString()), true,2);
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentparentreserv> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpaymentreserv> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();

            foreach (docpaymentreserv _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }

            return lst;
        }

        private financialstateitemModel GenerateReportItem(docpaymentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, helper.HotelName, 7, "Reservation", helper.Reservation, helper.ReservationPayment, string.Concat("Reservación pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), false,1);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            return _object;
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpaymentparentreserv> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();

            foreach (docpaymentparentreserv _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }

            return lst;
        }

        private financialstateitemModel GenerateReportItem(docpaymentparentreserv helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, helper.HotelName, 7, "Reservation", helper.Reservation, helper.ReservationPayment, string.Concat("Reservación pago sin membresia " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), false,2);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            return _object;
        }

        #endregion

        #region Getting Data
        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERV(DateTime start, DateTime end, int idCurrency, int[] paymentMethod)
        {
            List<tblreservationspayment> paymentsReserv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && paymentMethod.Contains((int)t.idPaymentType)).ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationsparentpayment> getAnualPaymentsRESERVPARENT(DateTime start, DateTime end, int idCurrency, int[] paymentMethod)
        {
            List<tblreservationsparentpayment> paymentsReserv = unity.ReservationsParentPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && paymentMethod.Contains((int)t.idPaymentType)).ToList();
            return paymentsReserv;
        }
        #endregion

        #endregion

        #region PURCHASES

        #region Procesing Data
        public void addSource(docpaymentpurchase helper)
        {
            if (this.docpaymentpurchaseitems == null || this.docpaymentpurchaseitems.Count() == 0)
            {
                this.docpaymentpurchaseitems = IEnumerableUtils.AddSingle<docpaymentpurchase>(helper);
            }
            else { this.docpaymentpurchaseitems = IEnumerableUtils.AddSingle<docpaymentpurchase>(docpaymentpurchaseitems, helper); }
        }

        public void addSource(List<docpaymentpurchase> listhelper)
        {
            if (this.docpaymentpurchaseitems == null || this.docpaymentpurchaseitems.Count() == 0)
            {
                this.docpaymentpurchaseitems = IEnumerableUtils.AddList<docpaymentpurchase>(listhelper);
            }
            else { this.docpaymentpurchaseitems = IEnumerableUtils.AddList<docpaymentpurchase>(docpaymentpurchaseitems, listhelper); }
        }
        #endregion

        #region Generating ReportItem
        private void addReportItem(docpaymentpurchase helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.paymentDate, helper.paymentAmount, 0m, helper.PaymentMethodName, "", 8, "PURCHASE", helper.Purchase, helper.Payment, string.Concat("Pago " + helper.PaymentMethodName + " , ", helper.authcode, "- Purchase : ", helper.Purchase == 0 ? " " : helper.Purchase.ToString(), " - pago ", helper.Payment.ToString()), true,0);
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentpurchase> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpaymentpurchase> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();

            foreach (docpaymentpurchase _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }

            return lst;
        }

        private financialstateitemModel GenerateReportItem(docpaymentpurchase helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.paymentDate, helper.paymentAmount, 0m, helper.PaymentMethodName, helper.HotelName, 8, "Pagos Membership", helper.Purchase, helper.Payment, string.Concat("Pagos Purchase " + helper.PaymentMethodName + " , ", helper.authRef, " - Purchase : ", helper.Purchase == 0 ? " " : helper.Purchase.ToString()), false,0);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            return _object;
        }
        #endregion

        #region Getting Data
        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPURCHASES(DateTime start, DateTime end, int idCurrency, int[] paymentMethod)
        {
            List<tblpaymentspurchases> paymentsPurchases = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate >= start && t.paymentDate <= end && t.idCurrency == idCurrency && paymentMethod.Contains((int)t.idPaymentType), null, "tblpurchases,tblpurchases.tblpartners").ToList();
            return paymentsPurchases;
        }
        #endregion

        #endregion

        #region INVOICE PAYMENT

        #region Procesing Data
        public void addSource(List<docpayments> listhelper)
        {
            if (this.docpaymentitems == null || this.docpaymentitems.Count() == 0) { this.docpaymentitems = IEnumerableUtils.AddList<docpayments>(listhelper); }
            else { IEnumerableUtils.AddList<docpayments>(this.docpaymentitems, listhelper); }
        }
        #endregion

        #region Generating ReportItem
        private void addReportItem(List<docpayments> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }
        //PAGOS INVOICES
        private void addReportItem(docpayments helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.applicationDate, helper.chargeAmount * -1, 0m, "Pagos", helper.CompanyName, 2, "Pagos", helper.Invoice, helper.Payment, string.Concat("Pago: " + helper.InvoiceIdentifier, " ", helper.authReference), true,0);

            _object.generateString();

            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }

            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpayments> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();

            foreach (docpayments _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }

            return lst;
        }

        private financialstateitemModel GenerateReportItem(docpayments helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.applicationDate, helper.chargeAmount, 0m, "Pagos", helper.CompanyName, 4, "Pagos", helper.Invoice, helper.Payment, string.Concat("Pago: " + helper.Payment), false,0);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            return _object;
        }
        #endregion

        #region Getting Data
        private IEnumerable<tblpayment> getAnualPaymentsPAYMENTINVOICE(DateTime start, DateTime end, int Baccount, int idCurrency)
        {
            return unity.PaymentsVtaRepository.Get(n => n.paymentdate >= start && n.paymentdate <= end && n.idbaccount == Baccount && n.tblbankaccount.idcurrency == idCurrency, null, "tblbankaccount,tblbankaccount.tblbaccounttpv").ToList();
        }
        #endregion

        #endregion

        #region FONDOS

        #region Procesing Data
        public void addSource(List<docfondos> listhelper)
        {
            if (this.docfondositems == null || this.docfondositems.Count() == 0) { this.docfondositems = IEnumerableUtils.AddList<docfondos>(listhelper); }
            else { IEnumerableUtils.AddList<docfondos>(this.docfondositems, listhelper); }
        }
        #endregion

        #region Generating ReporItem

        private void addReportItem(docfondos helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.fechaEntrega, helper.fondoMonto, 0m, "Transferencia", helper.baccntfinancialMethodName + "-" + helper.baccntpaymentMethodName, 1, helper.financeType != null ? "Financiamiento" : "Fondos", helper.fondoInvoice == null ? 0 : (int)helper.fondoInvoice, helper.Fondo, helper.financeType != null ? string.Concat("Movimiento de fondos: origen " + helper.baccntfinancialMethodName + " destino " + helper.baccntpaymentMethodName) : string.Concat("Financiamiento: origen" + helper.baccntfinancialMethodName + " destino " + helper.baccntpaymentMethodName), true,0);

            _object.generateString();

            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }

            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docfondos> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private List<financialstateitemModel> GenerateReportItem(List<docfondos> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();

            foreach (docfondos _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }

            return lst;
        }

        private financialstateitemModel GenerateReportItem(docfondos helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.fechaEntrega, helper.fondoMonto, 0m, helper.PaymentMethodName, helper.baccntfinancialMethodName + " " + helper.baccntpaymentMethodName, 5, helper.financeType != null ? "Financiamiento" : "Fondos", helper.fondoInvoice == null ? 0 : (int)helper.fondoInvoice, helper.Fondo, helper.financeType != null ? string.Concat("Financiamiento: " + helper.Fondo) : string.Concat("Fondo: " + helper.Fondo), false,0);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            return _object;
        }
        #endregion

        #region Getting Data
        private IEnumerable<tblfondos> getAnualPaymentsFONDOSOUT(DateTime start, DateTime end, int Baccount, int idCurrency)
        {
            return unity.FondosRepository.Get(n => n.fondofechaEntrega >= start && n.fondofechaEntrega <= end && n.idFinancialMethod == Baccount && n.tblbankaccount1.idcurrency == idCurrency, null, "tblbankaccount1,tblbankaccount1.tblbaccounttpv").ToList();
        }

        private IEnumerable<tblfondos> getAnualPaymentsFONDOSIN(DateTime start, DateTime end, int Baccount, int idCurrency)
        {
            return unity.FondosRepository.Get(n => n.fondofechaEntrega >= start && n.fondofechaEntrega <= end && n.idPaymentMethod == Baccount && n.tblbankaccount.idcurrency == idCurrency, null, "tblbankaccount,tblbankaccount.tblbaccounttpv").ToList();
        }
        #endregion

        #endregion

        #region INCOMES

        #region Procesing Data
        public void addSource(docpaymentincome helper)
        {
            if (this.docpaymentincomitems == null || this.docpaymentincomitems.Count() == 0) { this.docpaymentincomitems = IEnumerableUtils.AddSingle<docpaymentincome>(helper); }
            else { IEnumerableUtils.AddSingle<docpaymentincome>(this.docpaymentincomitems, helper); }
        }

        public void addSource(List<docpaymentincome> listhelper)
        {
            if (this.docpaymentincomitems == null || this.docpaymentincomitems.Count() == 0) { this.docpaymentincomitems = IEnumerableUtils.AddList<docpaymentincome>(listhelper); }
            else { IEnumerableUtils.AddList<docpaymentincome>(this.docpaymentincomitems, listhelper); }
        }
        #endregion

        #region Generating ReportItem
        private void addReportItem(docpaymentincome helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.incomemovapplicationDate, helper.incomemovchargedAmount, 0m, helper.PaymentMethodName, helper.CompanyName, 2, "Income movement", helper.Income, helper.IncomeMovement, string.Concat("Movimiento de ingresos: " + helper.identifier), true,0);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitemModel>(_object);
            }

            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReportItem(List<docpaymentincome> helper)
        {
            helper.ForEach(y => this.addReportItem(y));
        }

        private List<financialstateitemModel> GenerateReportItem(List<docpaymentincome> helper)
        {
            List<financialstateitemModel> lst = new List<financialstateitemModel>();

            foreach (docpaymentincome _helper in helper)
            {
                lst.Add(GenerateReportItem(_helper));
            }

            return lst;
        }

        private financialstateitemModel GenerateReportItem(docpaymentincome helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.incomemovapplicationDate, helper.incomemovchargedAmount, 0m, helper.PaymentMethodName, helper.CompanyName, 2, "Ingreso movimientos", helper.Income, helper.IncomeMovement, string.Concat("Movimiento de ingresos: " + helper.identifier), false,0);   // son positivos ya que estos pagos son entradas para la empresa

            _object.generateString();

            return _object;
        }
        #endregion

        #region Getting Data
        private IEnumerable<tblincomemovement> getAnualPaymentsINCOME(DateTime start, DateTime end, int Baccount, int idCurrency, int[] paymentMethod)
        {
            return unity.IncomeMovementsRepository.Get(n => n.incomemovapplicationdate >= start && n.incomemovapplicationdate <= end && n.idbaccount == Baccount && n.tblincome.idcurrency == idCurrency && paymentMethod.Contains(n.idbankaccnttype)).ToList();
        }
        #endregion

        #endregion

    }
}