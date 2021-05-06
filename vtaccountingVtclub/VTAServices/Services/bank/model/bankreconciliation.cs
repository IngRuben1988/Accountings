using System;
using System.Collections.Generic;
using System.Linq;
using vtaccountingVtclub.Business.Core.Helper;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTAServices.Services.Models.commons;
using static VTAworldpass.VTACore.Collections.CollectionsUtils;

namespace VTAworldpass.VTAServices.Services.bank.model
{
    public class bankreconciliation : tblbankstatements
    {
        public int    idcurrency { get; set; }
        public string currencyname { get; set; }
        public int    baccount { get; set; }
        public string baccountname { get; set; }
        public string bankstatementappliedammountstring { get; set; }
        public string tpvname { get; set; }
        public string companyname { get; set; }
        public int    idhotel { get; set; }
        public string hotelname { get; set; }
        public string bankstatementaplicationdatestring { get; set; }
        public string bankstatementappliedammountfinalstring { get; set; }
        public string bankstatementadjuststring { get; set; }
        public string bankstatementinterchangeschargesstring { get; set; }
        public string bankstatementbankfeestring { get; set; }
        public string bankstatementchargespaymentsstring { get; set; }
        public string bankstatementrefundstring { get; set; }
        public int    paymentmethod { get; set; }
        public string paymentmethodname { get; set; }
        public int    statusconciliation { get; set; }
        public string statusconciliationname { get; set; }
        public int    methodconciliation { get; set; }
        public int    rowIndex { get; set; }
        public int    rowNumber { get; set; }
        public bool   xlscorrectlyformed { get; set; }

        private IEnumerable<invoicepayment> paymentitems { get; set; }
        private IEnumerable<tblreservationpayments> tblreservationpaymentsitems { get; set; }
        public  IEnumerable<docpaymentupscl> docpaymentupsclitems { get; set; }
        public  IEnumerable<docpaymentreserv> docpaymentreservitems { get; set; }
        public  virtual IEnumerable<financialstateitem> financialstateitemlist { get; set; }
        private readonly UnitOfWork unity = new UnitOfWork();

        public bankreconciliation()
        { }

        public bankreconciliation Generate(int idBankStatements)
        {
            return new bankreconciliation();
        }


        public bankreconciliation(long idBankStatements, int idTPV, string TPVName, int idCompany, string CompanyName, int hotel, string hotelname, int BAccount, string BAccountName, int Currency, string CurrencyName, string bankstatementAplicationDateString, DateTime bankstatementAplicationDate, decimal bankstatementAppliedAmmount, decimal? bankstatementAdjust, decimal? bankstatementInterchangesCharges, decimal? bankstatementBankFee, decimal? bankstatementChargesPayments, decimal? bankstatementRefund, decimal? bankstatementAppliedAmmountFinal, int methodConciliation, bool sourcedata, bool financialstateitem)
        {
            // Initializaing
            this.docpaymentupsclitems = new List<docpaymentupscl>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.financialstateitemlist = new List<financialstateitem>();
            this.paymentitems = new List<invoicepayment>();
            this.tblreservationpaymentsitems = new List<tblreservationpayments>();

            this.idBankStatements = idBankStatements;
            this.idTPV = idTPV;
            this.tpvname = TPVName;
            this.idCompany = idCompany;
            this.companyname = CompanyName;
            this.idhotel = hotel;
            this.hotelname = hotelname;
            this.baccount = BAccount;
            this.baccountname = BAccountName;
            this.idcurrency = Currency;
            this.currencyname = CurrencyName;
            this.bankstatementaplicationdatestring = bankstatementAplicationDateString;
            this.bankstatementAplicationDate = bankstatementAplicationDate;

            this.bankstatementAppliedAmmount = bankstatementAppliedAmmount;
            this.bankstatementappliedammountstring = MoneyUtils.ParseDecimalToString(bankstatementAppliedAmmount);


            this.bankstatementAdjust = bankstatementAdjust == null ? 0m : bankstatementAdjust;
            this.bankstatementadjuststring = MoneyUtils.ParseDecimalToString((decimal)this.bankstatementAdjust);

            this.bankstatementInterchangesCharges = bankstatementInterchangesCharges == null ? 0m : bankstatementInterchangesCharges;
            this.bankstatementinterchangeschargesstring = MoneyUtils.ParseDecimalToString((decimal)this.bankstatementInterchangesCharges);

            this.bankstatementBankFee = bankstatementBankFee == null ? 0m : bankstatementBankFee;
            this.bankstatementbankfeestring = MoneyUtils.ParseDecimalToString((decimal)this.bankstatementBankFee);


            this.bankstatementChargesPayments = bankstatementChargesPayments == null ? 0m : bankstatementChargesPayments;
            this.bankstatementchargespaymentsstring = MoneyUtils.ParseDecimalToString((decimal)bankstatementChargesPayments);

            this.bankstatementRefund = bankstatementRefund == null ? 0m : bankstatementRefund;
            this.bankstatementrefundstring = MoneyUtils.ParseDecimalToString((decimal)bankstatementRefund);


            this.bankstatementAppliedAmmountFinal = bankstatementAppliedAmmountFinal;
            this.bankstatementappliedammountfinalstring = MoneyUtils.ParseDecimalToString((decimal)bankstatementAppliedAmmountFinal); ;


            this.paymentitems = paymentitems;
            this.tblreservationpaymentsitems = tblreservationpaymentsitems;

            this.statusconciliation = (int)BankAccountReconcilitionStatus.Sin_conciliar;
            this.statusconciliationname = BankAccountReconcilitionStatus.Sin_conciliar.ToString();
            this.methodconciliation = methodConciliation;
            this.idBankStatementMethod = methodConciliation;


            // this.Payment =  IEnumerableUtils.AddList(this.Payment, this.unity.StatementsUPSCLRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.Payment).ToList());
            // this.tblreservationpayments = IEnumerableUtils.AddList(this.tblreservationpayments, this.unity.StatementsRESERVRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.tblreservationpayments).ToList());


            // Adding UPSCALES
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.StatementsUPSCLRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.Payment).ToList()));

            // Adding RESERVATIONS
            this.addSource(GeneralModelHelper.ConvertTbltoHelper(this.unity.StatementsRESERVRepository.Get(x => x.idBankStatements == this.idBankStatements).Select(c => c.tblreservationpayments).ToList()));

            // Generating FinancialStateItem
            this.addReporItem(this.docpaymentupsclitems.ToList()); // UPSCALES
            this.addReporItem(this.docpaymentreservitems.ToList()); // RESERVATIONS

            // Adding MethdPay

            if (this.docpaymentupsclitems != null && this.docpaymentupsclitems.Count() != 0)
            {
                this.paymentmethod = this.docpaymentupsclitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentupsclitems.First().PaymentMethodName;
            }
            else if (this.docpaymentreservitems != null && this.docpaymentreservitems.Count() != 0)
            {

                this.paymentmethod = this.docpaymentreservitems.First().PaymentMethod;
                this.paymentmethodname = this.docpaymentreservitems.First().PaymentMethodName;

            }
            else
            {
                this.paymentmethod = 0;
                this.paymentmethodname = "";
            }



            /*********************************************************************************************************/
            this.validateStatusStament(null);
            this.applyRowIndexFInancialStateItem();

            /*********************************************************************************************************/


            this.paymentitems = new List<invoicepayment>();
            this.tblreservationpaymentsitems = new List<tblreservationpayments>();

            if (sourcedata == false)
            {
                this.docpaymentupsclitems = new List<docpaymentupscl>();
                this.docpaymentreservitems = new List<docpaymentreserv>();
            }

            if (sourcedata == false)
            {
                this.financialstateitemlist = new List<financialstateitem>();
            }

        }

        public void GenerateConciliation(long idBankStatements, bool containGeneralData)
        {
            // Initializaing
            this.docpaymentupsclitems = new List<docpaymentupscl>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.financialstateitemlist = new List<financialstateitem>();

            var model = unity.StatementsRepository.Get(x => x.idBankStatements == idBankStatements, null, "tbltpv,tblcompanies,tblbankaccount.Currency").FirstOrDefault();

            if (!containGeneralData)
            {
                if (model != null)
                {
                    this.idTPV = model.idTPV;
                    this.idBankStatements = model.idBankStatements;
                    this.idBAccount = model.idBAccount;
                    this.tpvname = model.tbltpv.tpvIdLocation;
                    this.idCompany = idCompany;
                    this.companyname = model.tblbankaccount.tblcompanies.companyShortName;
                    this.idcurrency = model.tblbankaccount.Currency.idCurrency;
                    this.currencyname = model.tblbankaccount.Currency.currencyAlphabeticCode;
                    this.baccount = model.tblbankaccount.idBAccount;
                    this.baccountname = model.tblbankaccount.baccountShortName;
                    this.bankstatementaplicationdatestring = DateTimeUtils.ParseDatetoString(model.bankstatementAplicationDate);
                    this.bankstatementAplicationDate = model.bankstatementAplicationDate;
                    this.bankstatementAppliedAmmount = model.bankstatementAppliedAmmount;
                    this.bankstatementappliedammountstring = MoneyUtils.ParseDecimalToString(model.bankstatementAppliedAmmount);
                    this.bankstatementAdjust = model.bankstatementAdjust;
                    this.bankstatementInterchangesCharges = model.bankstatementInterchangesCharges;
                    this.bankstatementBankFee = model.bankstatementBankFee;
                    this.bankstatementChargesPayments = model.bankstatementChargesPayments;
                    this.bankstatementRefund = model.bankstatementRefund;
                    this.statusconciliation = (int)BankAccountReconcilitionStatus.Sin_conciliar;

                    // Adding UPSCALES Previusly Saved
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstateupscl.Select(y => y.Payment).ToList()));
                    // Adding RESERVATIONS Previusly Saved
                    this.addSource(GeneralModelHelper.ConvertTbltoHelper(model.tblbankstatereserv.Select(y => y.tblreservationpayments).ToList()));

                    // Generating Financial State Item 
                    this.addReporItem(this.docpaymentupsclitems.ToList()); // UPSCALES
                    this.addReporItem(this.docpaymentreservitems.ToList()); // RESERVATIONS

                }

                else throw new Exception("No se encuentra el id SCOTIA POS");
            }

            this.validateStatusStament(null);
            this.BodyCommonConciliation();
            this.validateStatusStament(null);
        }

        public void GenerateConciliationByExcelUpload()
        {
            // Initializaing
            this.docpaymentupsclitems = new List<docpaymentupscl>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.financialstateitemlist = new List<financialstateitem>();


            this.BodyCommonConciliation();
            this.validateStatusStament(null);
            this.validateMethodConciliationbyExcelUpload();

        }

        #region Commom Actions

        private void BodyCommonConciliation()
        {
            /**************************************** Generating Options to Complete BankStatements / Generatins Option if BankStatement is Build by EXCEL *************************************************/


            List<financialstateitem> _tmp = new List<financialstateitem>();
            int counterDaysBefore = 1;
            bool FinishSearch = false;

            int counterSearchStatement = 0;

            while ((this.financialstateitemlist.Sum(x => x.appliedAmmount) + _tmp.Sum(y => y.appliedAmmount)) < this.bankstatementAppliedAmmount && FinishSearch == false)
            {

                // if (counterSearchStatement == 100)
                // { break; }

                // List of temp While
                List<financialstateitem> _tmpwhile = new List<financialstateitem>();

                #region Getting New Data 

                DateTime dayBeforeStart = this.bankstatementAplicationDate.AddDays(counterDaysBefore * -1);
                DateTime dayBeforeEnd = this.bankstatementAplicationDate.AddDays(counterDaysBefore * -1);

                // Getting Data
                List<tblreservationpayments> tempReservations = new List<tblreservationpayments>();
                List<invoicepayment> tempUpscales = new List<invoicepayment>();

                // Getting records by hotel
                if (idhotel != 0)
                {
                    tempReservations = (List<tblreservationpayments>)this.getAnualPaymentsRESERVTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idhotel, this.idTPV);
                    tempUpscales = (List<invoicepayment>)this.getAnualPaymentsUPSCLTPV(dayBeforeStart, dayBeforeEnd, this.idcurrency, this.idhotel, this.idTPV);
                }

                if (idhotel == 0)
                {
                    tempReservations = (List<tblreservationpayments>)this.getAnualPaymentsRESERVTPV(dayBeforeStart, dayBeforeEnd, this.currency, this.idTPV);
                    tempUpscales = (List<invoicepayment>)this.getAnualPaymentsUPSCLTPV(dayBeforeStart, dayBeforeEnd, this.idcurrency, this.idTPV);
                }

                // Adding SourceData temp List
                IEnumerable<docpaymentupscl> _tmpWhileDocUpscl = new List<docpaymentupscl>();
                IEnumerable<docpaymentreserv> _tmpWhileDocReserv = new List<docpaymentreserv>();
                // Parsing Data to docpaymentupscl / docpaymentreserv
                _tmpWhileDocReserv = IEnumerableUtils.AddList(_tmpWhileDocReserv, GeneralModelHelper.ConvertTbltoHelper(tempReservations.Where(c => c.tblbankstatereserv.Count() == 0).ToList()).ToList());
                _tmpWhileDocUpscl = IEnumerableUtils.AddList(_tmpWhileDocUpscl, GeneralModelHelper.ConvertTbltoHelper(tempUpscales.Where(c => c.tblbankstateupscl.Count() == 0).ToList()).ToList());
                // Adding to Financial State Item Temp
                _tmpwhile = (List<financialstateitem>)IEnumerableUtils.AddList(_tmpwhile, GenerateReporItem(_tmpWhileDocReserv.ToList()));
                _tmpwhile = (List<financialstateitem>)IEnumerableUtils.AddList(_tmpwhile, GenerateReporItem(_tmpWhileDocUpscl.ToList()));

                #endregion


                #region Eval Data

                foreach (financialstateitem item in _tmpwhile)
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

                // Finishing by Date 
                var interval = this.bankstatementAplicationDate.Subtract(dayBeforeStart);
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
                decimal sum = toeval != null ? (decimal)toeval : financialstateitemlist.Sum(x => x.appliedAmmount);

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
        /*
        private void validateStatusStament(decimal toeval, decimal total)
        {
                if (toeval == total)
                {
                    this.statusconciliation = (int)BankAccountReconcilitionStatus.Completo; this.statusconciliationname = BankAccountReconcilitionStatus.Completo.ToString();
            }
                else if (toeval < total)
                {
                    if (this.passDiference(toeval, total) == true)
                    { this.statusconciliation = (int)BankAccountReconcilitionStatus.Completo; this.statusconciliationname = BankAccountReconcilitionStatus.Completo.ToString(); }
                    else { this.statusconciliation = (int)BankAccountReconcilitionStatus.Parcial; this.statusconciliationname = BankAccountReconcilitionStatus.Parcial.ToString(); }
                }
                else if (toeval > total)
                {
                    if (this.passDiference(toeval, total))
                    { this.statusconciliation = (int)BankAccountReconcilitionStatus.Completo; this.statusconciliationname = BankAccountReconcilitionStatus.Completo.ToString(); }
                    else { this.statusconciliation = (int)BankAccountReconcilitionStatus.Error; this.statusconciliationname = BankAccountReconcilitionStatus.Error.ToString().Replace('_', ' '); ; }
                }
                else
                {
                    this.statusconciliation = (int)BankAccountReconcilitionStatus.Sin_conciliar; this.statusconciliationname = BankAccountReconcilitionStatus.Sin_conciliar.ToString();
            }   
        }
        */



        private bool passDiference(decimal current, decimal total)
        {
            var absolute = unity.StatementsgapRepository.Get(x => x.bankstatementsgapDate <= this.bankstatementAplicationDate).Select(c => c.bankstatementsgapValue).FirstOrDefault();

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

        private bool passDiferenceFrom0(decimal current, decimal total)
        {
            var absolute = unity.StatementsgapRepository.Get(x => x.bankstatementsgapDate <= this.bankstatementAplicationDate).Select(c => c.bankstatementsgapValue).FirstOrDefault();

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

        public void addReporItem(financialstateitem financialstateitem)
        {
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitem>(financialstateitem);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitem>(this.financialstateitemlist, financialstateitem); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }

        }

        public bool ReporItemISposibleIn(financialstateitem financialstateitem)
        {
            // Getting Current Object List
            List<financialstateitem> _tmp = new List<financialstateitem>();
            _tmp = this.financialstateitemlist.ToList();

            var _resultSearch = _tmp.Where(x => x.SourceData == financialstateitem.SourceData && x.ReferenceItem == financialstateitem.ReferenceItem).FirstOrDefault();


            if (_resultSearch == null)
            {
                return true;
                // this.addReporItem(financialstateitem);
            }
            else return false;

        }

        private void applyRowIndexFInancialStateItem()
        {
            int counterIndex = 0;
            int counterNumber = 1;

            foreach (financialstateitem model in this.financialstateitemlist)
            {
                model.rowIndex = counterIndex;
                counterIndex = counterIndex + 1;

                model.rowNumber = counterNumber;
                counterNumber = counterNumber + 1;
            }
        }



        #endregion

        #region Getting Data

        #region UPSCALES
        private IEnumerable<invoicepayment> getAnualPaymentsUPSCLTPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<invoicepayment> paymentsUpscl = unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.idCurrency == idCurrency && (t.PaymentInstrument.Select(v => v.idTerminal).ToList()).Contains(idTpv), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();
            return paymentsUpscl;
        }

        private IEnumerable<invoicepayment> getAnualPaymentsUPSCLTPV(DateTime start, DateTime end, int idCurrency, int idHotel, int idTpv)
        {
            List<invoicepayment> paymentsUpscl = unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.idCurrency == idCurrency && t.Invoice1.tblhotels.idHotel == idHotel && (t.PaymentInstrument.Select(v => v.idTerminal).ToList()).Contains(idTpv), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();
            return paymentsUpscl;
        }
        #endregion

        #region Reservations

        // Getting by Terminals

        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERVTPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<tblreservationpayments> paymentsReserv = unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && t.idTerminal == idTpv).ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERVTPV(DateTime start, DateTime end, int idCurrency, int idHotel, int idTpv)
        {
            List<tblreservationpayments> paymentsReserv = unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && t.tblreservations.tblhotels.idHotel == idHotel && t.idTerminal == idTpv).ToList();
            return paymentsReserv;
        }
        #endregion

        #endregion

        #region Procesing Data

        #region UPSCL
        /*****************************/
        public void addSource(docpaymentupscl helper)
        {

            if (this.docpaymentupsclitems == null || this.docpaymentupsclitems.Count() == 0)
            {
                this.docpaymentupsclitems = IEnumerableUtils.AddSingle<docpaymentupscl>(helper);
            }
            else {
                IEnumerableUtils.AddSingle<docpaymentupscl>(this.docpaymentupsclitems, helper);
            };
        }

        public void addSource(List<docpaymentupscl> listhelper)
        {

            if (this.docpaymentupsclitems == null || this.docpaymentupsclitems.Count() == 0) { this.docpaymentupsclitems = IEnumerableUtils.AddList<docpaymentupscl>(listhelper); }
            else { this.docpaymentupsclitems = IEnumerableUtils.AddList<docpaymentupscl>(this.docpaymentupsclitems, listhelper); }

            //listhelper.ForEach(x => this.addReporItem(x));
            // calculateMontoAbonos(); calculateBalance(); parseAmmountsToString();
        }
        #endregion

        #region Reservations
        /*****************************/
        public void addSource(docpaymentreserv helper)
        {

            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0)
            {
                this.docpaymentreservitems = IEnumerableUtils.AddSingle<docpaymentreserv>(helper);
            }
            else {
                IEnumerableUtils.AddSingle<docpaymentreserv>(this.docpaymentreservitems, helper);
            }
        }
        public void addSource(List<docpaymentreserv> listhelper)
        {

            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0)
            {
                this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(listhelper);
            }
            else {
                this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(this.docpaymentreservitems, listhelper);
            }

        }
        #endregion

        #endregion

        #region Generatin ReportItem

        #region UPSCALE
        private void addReporItem(docpaymentupscl helper)
        {
            // financialstateitem _object = new financialstateitem(string.Concat(" UPSCL pago " + helper.PaymentMethodName + " , ", helper.authRef , " - Reserva : ", helper.Reserva == 0 ? " " : helper.Reserva.ToString(), " - Upscale ", helper.Invoice.ToString() ), helper.aplicationDate, helper.chargedAmount); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitem _object = new financialstateitem(helper.aplicationDate, helper.chargedAmount, 0m, helper.PaymentMethodName, helper.HotelName, 4, "UPSCL", helper.Invoice, helper.Payment, string.Concat(" UPSCL pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reserva == 0 ? " " : helper.Reserva.ToString(), " - Upscale ", helper.Invoice.ToString()), true);    // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReporItem(List<docpaymentupscl> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private financialstateitem GenerateReporItem(docpaymentupscl helper)
        {
            // financialstateitem _object = new financialstateitem(string.Concat(" UPSCL pago " + helper.PaymentMethodName + " , ", helper.authRef , " - Reserva : ", helper.Reserva == 0 ? " " : helper.Reserva.ToString(), " - Upscale ", helper.Invoice.ToString() ), helper.aplicationDate, helper.chargedAmount); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitem _object = new financialstateitem(helper.aplicationDate, helper.chargedAmount, 0m, helper.PaymentMethodName, helper.HotelName, 4, "UPSCL", helper.Invoice, helper.Payment, string.Concat(" UPSCL pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reserva == 0 ? " " : helper.Reserva.ToString(), " - Upscale ", helper.Invoice.ToString()), false);    // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            return _object;
        }

        private List<financialstateitem> GenerateReporItem(List<docpaymentupscl> helper)
        {
            List<financialstateitem> lst = new List<financialstateitem>();
            foreach (docpaymentupscl _helper in helper)
            {
                lst.Add(GenerateReporItem(_helper));
            }
            return lst;
        }


        #endregion

        #region RESERVATION

        private void addReporItem(docpaymentreserv helper)
        {
            //financialstateitem _object = new financialstateitem(string.Concat("Reservación pago "+ helper.PaymentMethodName +" , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.reservationPaymentDate, helper.reservationPaymentQuantity); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitem _object = new financialstateitem(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, helper.HotelName, 5, "Reservation", helper.Reservation, helper.ReservationPayment, string.Concat("Reservación pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), true);   // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IEnumerableUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IEnumerableUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).AsEnumerable();
            }
        }

        private void addReporItem(List<docpaymentreserv> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private financialstateitem GenerateReporItem(docpaymentreserv helper)
        {
            //financialstateitem _object = new financialstateitem(string.Concat("Reservación pago "+ helper.PaymentMethodName +" , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.reservationPaymentDate, helper.reservationPaymentQuantity); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitem _object = new financialstateitem(helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, helper.HotelName, 5, "Reservation", helper.Reservation, helper.ReservationPayment, string.Concat("Reservación pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), false);   // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            return _object;
        }

        private List<financialstateitem> GenerateReporItem(List<docpaymentreserv> helper)
        {
            List<financialstateitem> lst = new List<financialstateitem>();
            foreach (docpaymentreserv _helper in helper)
            {
                lst.Add(GenerateReporItem(_helper));
            }
            return lst;
        }

        #endregion

        #endregion

    }
}