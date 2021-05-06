using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.bankreconciliation;
using VTAworldpass.VTAServices.Services.bankreconciliation.implements;
using VTAworldpass.VTAServices.Services.bankreconciliation.model;
using VTAworldpass.VTAServices.Services.budgets.helpers;
using VTAworldpass.VTAServices.Services.budgets.model;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTAServices.Services.Models.commons;
using static VTAworldpass.VTACore.Collections.CollectionsUtils;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;
using System.Data.Entity.Infrastructure;
using VTAworldpass.VTACore.Cores.Globales;

namespace VTAworldpass.VTAServices.Services.Models
{
    public class financialstateModel : budgetHelper
    {
        public virtual int row { get; set; }
        public int idBAccount { get; set; }
        public int idBank { get; set; }
        public int BankName { get; set; }
        public string baccountName { get; set; }
        public string baccountShortName { get; set; }
        public string CompanyBaccountName { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string PaymentMethodName { get; set; }
        public virtual int Currency { get; set; }
        public virtual string CurrencyName { get; set; }
        public virtual decimal MontoCargo { get; set; }
        public virtual decimal MontoAbonos { get; set; }
        public virtual string MontoCargoString { get; set; }
        public virtual string MontoAbonosString { get; set; }
        public virtual decimal balance { get; set; }
        public virtual string balanceString { get; set; }
        public virtual decimal balanceBefore { get; set; }
        public virtual string balanceBeforeString { get; set; }
        public virtual decimal maxBalance { get; set; }
        public virtual string maxBalanceString { get; set; }
        public virtual bool hasMaxLimit { get; set; }
        public virtual string FechaInicioString { get; set; }
        public virtual string FechaFinString { get; set; }
        public virtual int BankAccntType { get; set; }
        public virtual int BankAccntTypeName { get; set; }
        public virtual bool? allowsNegatives { get; set; }

        public virtual IList<financialstateitemModel> financialstateitemlist { get; set; }

        // private sourcesdata
        private IEnumerable<tblbankstatements> bankstatements { get; set; }
        private IEnumerable<int> HotelPartner { get; set; }
        private IEnumerable<tbltpv> tbltpv { get; set; }
        private IEnumerable<bankaccountsourcedataModel> sourcedataitems { get; set; }


        private IEnumerable<invoiceitems> docitemsitems { get; set; }
        private IEnumerable<invoicepayment> docpaymtitems { get; set; }
        private IEnumerable<fondoModel> fondoitems { get; set; }
        private IEnumerable<docpaymentpurchase> docpaymentpurchaseitems { get; set; }
        private IEnumerable<docpaymentreserv> docpaymentreservitems { get; set; }
        private IEnumerable<docpaymentparentreserv> docpaymentparentreservitems { get; set; }
        private IList<incomepayment> incomemovements { get; set; }
        //private IEnumerable<tblbaccountsalesrooms> baccountsalesrooms { get; set; }
        private List<bankreconciliationModel> bankreconciliationitems { get; set; }
        private List<bankstatements> bankstatementsitems { get; set; }

        /**********************************************************************/
        private DateTime start;
        private DateTime end;
        private int idBanAccount;
        private FinancialStateReport type;
        private bool keepData;
        private int tpv;
        private int idBankAccntType;
        private int typeSourceData;

        private readonly UnitOfWork unity = new UnitOfWork();
        private readonly IBankReconciliationServices bankReconciliationServices = new BankReconciliationServices();



        public financialstateModel()
        { }

        public financialstateModel(DateTime startdate, DateTime enddate, int idBanAccount, FinancialStateReport type, bool keepData)
        {

            this.start = startdate;
            this.end = enddate;
            this.idBanAccount = idBanAccount;
            this.type = type;
            this.keepData = keepData;


            this.BodyCalculatingandData();

            #region KeepData
            if (!keepData) { this.ClearSourceData(); }
            #endregion

            #region applyRowIndex
            this.applyRowIndex();
            #endregion
        }



        public financialstateModel(DateTime startdate, DateTime enddate, int idBanAccount, int tpv, decimal ammountStart, decimal ammountEnd, int idHotel, FinancialStateReport type, bool keepData)
        {
            this.start = startdate;
            this.end = enddate;
            this.idBanAccount = idBanAccount;
            this.type = type;
            this.keepData = keepData;
            this.tpv = tpv;

            this.BodyCalculatingandData(tpv, ammountStart, ammountEnd, idHotel);


            #region KeepData
            if (!keepData) { this.ClearSourceData(); }
            #endregion

            #region applyRowIndex
            this.applyRowIndex();
            #endregion
        }

        public financialstateModel(DateTime startdate, DateTime enddate, int idBanAccount, decimal ammountStart, decimal ammountEnd, FinancialStateReport type, bool keepData, int typeSorceData)
        {
            this.start = startdate;
            this.end = enddate;
            this.idBanAccount = idBanAccount;
            this.type = type;
            this.keepData = keepData;
            this.typeSourceData = typeSorceData;

            this.BodyCalculatingandData(ammountStart, ammountEnd);


            #region KeepData
            if (!keepData) { this.ClearSourceData(); }
            #endregion

            #region applyRowIndex
            this.applyRowIndex();
            #endregion
        }

        public financialstateModel(DateTime startdate, DateTime enddate, int idBanAccount, FinancialStateReport type, bool keepData, int idBankAccntType)
        {
            this.start = startdate;
            this.end = enddate;
            this.idBanAccount = idBanAccount;
            this.type = type;
            this.keepData = keepData;
            this.idBankAccntType = idBankAccntType;

            this.BodyCalculatingandData();
            var _BankAccount_ = unity.BankAccountRepository.Get(t => t.idbaccount == idBanAccount, null, "tblcompanies,tblbank,tblcurrencies").FirstOrDefault();

            #region KeepData
            if (!keepData) { this.ClearSourceData(); }
            #endregion

            #region Allow Negatives
            // Allow Negatives
            var _typePrudct = _BankAccount_.tblbaccounprodttype.Where(x => x.idbaccountprodtype == idBankAccntType).FirstOrDefault();

            if (_typePrudct != null) { this.allowsNegatives = _typePrudct.baccountprodtypeallowneg; this.BankAccntType = _typePrudct.idbankprodttype; } else { this.allowsNegatives = false; this.BankAccntType = idBankAccntType; }

            #endregion

            #region applyRowIndex
            this.applyRowIndex();
            #endregion
        }


        #region Actions Common

        private void BodyCalculatingandData()
        {

            #region Calculating and Data

            this.FechaInicioString = DateTimeUtils.ParseDatetoString(start);
            this.FechaFinString = DateTimeUtils.ParseDatetoString(end);

            // Initializing List
            this.docitemsitems = new List<invoiceitems>();
            this.docpaymtitems = new List<invoicepayment>();
            this.fondoitems = new List<fondoModel>();
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.bankreconciliationitems = new List<bankreconciliationModel>();
            this.financialstateitemlist = new List<financialstateitemModel>();
            this.sourcedataitems = new List<bankaccountsourcedataModel>();
            this.HotelPartner = new List<int>();
            this.tbltpv = new List<tbltpv>();
            //this.baccounthotels = new List<tblbaccounthotels>();
            this.incomemovements = new List<incomepayment>();

            // Getting only General onformation
            var _BankAccount = unity.BankAccountRepository.Get(t => t.idbaccount == idBanAccount, null, "tblcompanies,tblcompanies.tblcompanygroupdevelopment,tblbank,tblcurrencies").FirstOrDefault();
            this.baccountName = _BankAccount.baccountname;
            this.baccountShortName = _BankAccount.baccountshortname;
            this.idBAccount = _BankAccount.idbaccount;

            // Getting the Bank Account - Hotel 
            //var _companygroupdevelopment = unity.CompanyGroupDevelopmentRepository.Get(de => de.idCompanyParent == _BankAccount.tblcompanies.idcompany).ToList();
            var grouplist = _BankAccount.tblcompanies.tblcompanygroupdevelopment.Select(c => c.idCompanyChild).ToList();
            var _companydevelopment = this.unity.CompanyDevelopmentRepository.Get(de => grouplist.Contains(de.idCompany)).ToList();
            var lst = _companydevelopment.Select(ch => ch.idHotelChain).ToList();
            this.HotelPartner = IEnumerableUtils.AddList(this.HotelPartner, lst/*_company.tblcompanydevelopment.Select(d => d.idHotelChain).ToList()*/);

            // Gettting Bank Account TPV's
            this.tbltpv = IEnumerableUtils.AddList(this.tbltpv, _BankAccount.tblbaccounttpv.Select(t => new tbltpv { idtpv = t.tbltpv.idtpv, tpvname = t.tbltpv.tpvname, tpvidlocation = t.tbltpv.tpvidlocation }).ToList());
            // Getting the Bank Account Sources data
            this.sourcedataitems = IEnumerableUtils.AddList(this.sourcedataitems, _BankAccount.tblbankaccountsourcedata.Select(y => new bankaccountsourcedataModel { SourceData = y.idsourcedata, sourcedataDateStart = y.sourcedatadatestart, Types = y.tblbankaccountsourcedatatypes.Select(t => t.idtype).ToList() }).ToList());
            // Getting id hotel relationships VTH
            //this.HotelPartner = IEnumerableUtils.AddList(this.HotelPartner, _BankAccount.tblcompanies.tblcompanyhotel.Select(y => y.idHotel).ToList());
            // Getting the Bank Account - Hotel 
            //this.baccounthotels = IEnumerableUtils.AddList(this.baccounthotels, _BankAccount.tblbaccounthotels.Where(t => t.baccounthotelActive == Constantes.activeRecord).Select(t => new tblbaccounthotels { idBAccount = t.idBAccount, idHotel = t.idHotel }).ToList());

            DateTime _startFondos = new DateTime();
            DateTime _startPayments = new DateTime();
            // DateTime _startDocitems = new DateTime();
            DateTime _startPurchase = new DateTime();
            DateTime _startReservatios = new DateTime();
            DateTime _startIncomesMovements = new DateTime();
            DateTime _startReconciliations = new DateTime();
            DateTime _startBankStatement = new DateTime();

            var bankAccountShortName = _BankAccount.baccountshortname;
            var bankName = _BankAccount.tblbank.bankshortname;
            var currencyName = _BankAccount.tblcurrencies.currencyAlphabeticCode;
            var companyName = _BankAccount.tblcompanies.companyshortname;

            var baccountName = string.Concat(bankAccountShortName + " " + bankName + " " + currencyName + " " + companyName);

            this.CompanyBaccountName = baccountName;

            foreach (bankaccountsourcedataModel model in sourcedataitems)
            {
                this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName); this.PaymentMethodName = _BankAccount.baccountname;

                switch (model.SourceData)
                {
                    case 1: // Ingresos
                        {

                        }
                        break;
                    case 2: // Movimientos ingreso
                        {
                            this.addSource(convertTbltoHelper(this.getAnualIncomeMovements((DateTime)model.sourcedataDateStart, end, idBanAccount).ToList()));
                            _startIncomesMovements = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 3: // Facturas
                        {
                            // this.addSource(this.convertTbltoHelper(this.getAnualDocitems(model.sourcedataDateStart, end, _BankAccount.idCompany).ToList()));
                            // _startDocitems = model.sourcedataDateStart;
                        }
                        break;
                    case 4: // Pagos
                        {
                            this.addSource(convertTbltoHelper(this.getAnualPayments((DateTime)model.sourcedataDateStart, end, idBanAccount).ToList()));
                            _startPayments = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 5: // Fondos
                        {
                            this.addSource(convertTbltoHelper(this.getAnualBudgetsTo((DateTime)model.sourcedataDateStart, end, idBanAccount).ToList()));
                            this.addSource(convertTbltoHelper(this.getAnualBudgetsFrom((DateTime)model.sourcedataDateStart, end, idBanAccount).ToList()));
                            _startFondos = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 6:// Conciliaciones
                        {
                            this.addSource(bankReconciliationServices.getBakReconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true));
                            _startReconciliations = (DateTime)model.sourcedataDateStart;

                            this.addSource(bankReconciliationServices.getBankStatements2(model.sourcedataDateStart, end, this.idBAccount, true, true));
                            _startBankStatement = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 7:// Reservas
                        {
                            //Reservas Member
                            this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray()).AsEnumerable()).ToList());
                            // Reservas Member WEB
                            //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVAS_WEB((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray()).AsEnumerable()).ToList());
                            // Reservas Parent Payment
                            this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERV_PARENT((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray()).AsEnumerable()).ToList());

                            // Si la cuenta tiene terminales se buscan los registros de ReservationsPayments que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVTPV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, this.tbltpv.Select(y => y.idtpv).ToArray()).ToList()).ToList());

                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVTPVPARENT((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, this.tbltpv.Select(y => y.idtpv).ToArray()).ToList()).ToList());
                            }
                            // Si la cuenta tiene registros de Cuentas-Hoteles (BankAccount-Hotels), lo cual significa que esos hoteles aportan a esta cuenta en las modalidades que se encuentren segun el tipo de pago.
                            //if (this.baccounthotels.Count() != 0)
                            //{
                            //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVBAccountHotels((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray()/*, this.baccounthotels.Select(v => v.idHotel).ToArray()*/).ToList()).ToList());
                            //}
                            _startReservatios = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 8: // Membership o Purchases
                        {
                            // Adding Purchase Renewed
                            this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchase((DateTime)model.sourcedataDateStart, end, _BankAccount.tblcurrencies.idCurrency, model.Types.ToArray(), this.HotelPartner.ToArray()).ToList()).ToList());
                            // Adding Payment Post-Batch Detail
                            this.addSource(this.convertTbltoHelper(this.getAnualPaymentPurchaseBatchDetail((DateTime)model.sourcedataDateStart, end, model.Types.ToArray(), idBanAccount).ToList()).ToList());
                            // Adding Payment Pre-Batch Detail Pre
                            this.addSource(this.convertTbltoHelper(this.getAnualPaymentPurchaseBatchDetailPre((DateTime)model.sourcedataDateStart, end, model.Types.ToArray(), idBanAccount).ToList()).ToList());

                            // Purchase New
                            //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchaseNew((DateTime)model.sourcedataDateStart, end, _BankAccount.tblcurrencies.idCurrency, model.Types.ToArray(), this.HotelPartner.ToArray()).ToList()).ToList());
                            // Purchase Upgrade
                            //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchaseUpgrade((DateTime)model.sourcedataDateStart, end, _BankAccount.tblcurrencies.idCurrency, model.Types.ToArray(), this.HotelPartner.ToArray()).ToList()).ToList());

                            // Si la cuenta tiene terminales se buscan los registros que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {
                                foreach (tbltpv _model in tbltpv)
                                {
                                    this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchaseTPV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, _model.idtpv).ToList()).ToList());
                                }
                            }
                            // Si la cuenta tiene registros de Cuentas-Hoteles (BankAccount-Hotels), lo cual significa que esos hoteles aportan a esta cuenta en las modalidades que que se encuentren segun el tipo de pago.
                            //if (this.baccounthotels.Count() != 0)
                            //{
                            //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVBAccountHotels((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray()/*, this.baccounthotels.Select(v => v.idHotel).ToArray()*/).ToList()).ToList());
                            //}
                            _startPurchase = (DateTime)model.sourcedataDateStart;
                        }
                        break;

                    default:
                        {
                            break;
                        }
                }
            }


            switch (type)
            {
                case FinancialStateReport.Balance: //balance // NO se le han implementado la eliminación de las conciliaciones
                    {
                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.parseAmmountsToString();
                    }
                    break;
                case FinancialStateReport.AccountHistory: // AccountHistory
                    {
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Initializing ////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        this.financialstateitemlist = new List<financialstateitemModel>();
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        var _fondoitemsTemp = this.fondoitems;
                        var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentpurchaseitemsTemp = this.docpaymentpurchaseitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _docpaymentparentreservitemsTemp = this.docpaymentparentreservitems;
                        var _incomeMovementTemp = this.incomemovements;
                        //var _conciliationComision = this.bankreconciliationitems;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Balance Before //////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstateModel financialStateBefore = new financialstateModel();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, "-", _BankAccount.tblcurrencies.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountname;


                        // Fondos
                        if (_startFondos.Year == start.Year && _startFondos.Month == start.Month && _startFondos.Day != start.Day)
                        {
                            DateTime endBeforeFondos = start.AddDays(-1) < _startFondos ? _startFondos : start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        else
                        {
                            DateTime endBeforeFondos = start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        // Egresos - Documentos 
                        /*
                        if (_startDocitems.Year == start.Year && _startDocitems.Month == start.Month && _startDocitems.Day != start.Day)
                        {
                            DateTime endBeforeDocitems = start.AddDays(-1) < _startDocitems ? _startDocitems : start.AddDays(-1);
                            financialStateBefore.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= _startDocitems && y.aplicationDate <= endBeforeDocitems).ToList());
                        }
                        else
                        {
                            DateTime endBeforeDocitems = start.AddDays(-1);
                            financialStateBefore.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= _startDocitems && y.aplicationDate <= endBeforeDocitems).ToList());
                        } */
                        // Egresos - Pagos
                        if (_startPayments.Year == start.Year && _startPayments.Month == start.Month && _startPayments.Day != start.Day)
                        {
                            DateTime endBeforePayments = start.AddDays(-1) < _startPayments ? _startPayments : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        else
                        {
                            DateTime endBeforePayments = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        // Purchase - Pagos (Se toman como ingresos)
                        if (_startPurchase.Year == start.Year && _startPurchase.Month == start.Month && _startPurchase.Day != start.Day)
                        {
                            DateTime endBeforePurchase = start.AddDays(-1) < _startPurchase ? _startPurchase : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforePurchase).ToList());
                        }
                        else
                        {
                            DateTime endBeforePurchase = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforePurchase).ToList());
                        }
                        // Reservations - Pagos (Se toman comom ingresos
                        if (_startReservatios.Year == start.Year && _startReservatios.Month == start.Month && _startReservatios.Day != start.Day)
                        {
                            DateTime endBeforeReservations = start.AddDays(-1) < _startReservatios ? _startReservatios : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());
                            financialStateBefore.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());
                        }
                        else
                        {
                            DateTime endBeforeReservations = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());
                            financialStateBefore.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                        }
                        // Ingresos 
                        if (_startIncomesMovements.Year == start.Year && _startIncomesMovements.Month == start.Month && _startIncomesMovements.Day != start.Day)
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1) < _startIncomesMovements ? _startIncomesMovements : start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }
                        else
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }

                        //Conciliation comision
                        //if (_startReconciliations.Year == start.Year && _startReconciliations.Month == start.Month && _startReconciliations.Day != start.Day)
                        //{
                        //    DateTime endBeforeReconciliations = start.AddDays(-1) < _startReconciliations ? _startReconciliations : start.AddDays(-1);
                        //    financialStateBefore.addSource(_conciliationComision.Where(y => y.bankstatementAplicationDate >= _startReconciliations && y.bankstatementAplicationDate <= endBeforeReconciliations).ToList());
                        //}
                        //else
                        //{
                        //    DateTime endBeforeReconciliations = start.AddDays(-1);
                        //    financialStateBefore.addSource(_conciliationComision.Where(y => y.bankstatementAplicationDate >= _startIncomesMovements && y.bankstatementAplicationDate <= endBeforeReconciliations).ToList());
                        //}

                        //financialStateBefore.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations

                        financialStateBefore.calculateMontoCargosCajas();
                        financialStateBefore.calculateMontoAbonosCajas();
                        financialStateBefore.calculateBalance();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;


                        this.balanceBefore = (decimal)financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;

                        //*******************************/
                        this.docitemsitems = new List<invoiceitems>();
                        this.docpaymtitems = new List<invoicepayment>();
                        this.fondoitems = new List<fondoModel>();
                        this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
                        this.incomemovements = new List<incomepayment>();
                        this.bankreconciliationitems = new List<bankreconciliationModel>();

                        /*******************************/
                        // Getting budgets and Payments in interval
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        //this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= startAfter && y.paymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= startAfter && y.aplicationdate <= endAfter).ToList());
                        //this.addSource(_conciliationComision.Where(y => y.bankstatementAplicationDate >= startAfter && y.bankstatementAplicationDate <= endAfter).ToList());
                        // this.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations

                        this.calculateMontoCargosCajas();
                        this.calculateMontoAbonosCajas();
                        this.calculateBalance();
                        this.calculateBalanceWithBalceBeforeBancos();
                        this.generateTimeLineBalanceBancos(6);
                        this.parseAmmountsToString();
                        this.hightLightBankreconciliations();
                        //this.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations
                    }
                    break;

                case FinancialStateReport.MaxBalance: // Max Balance  // NO se le han implementado la eliminación de las conciliaciones
                    {

                        fondosmaxammountModel max = this.getfondosmaxammount(idBanAccount);

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();

                        if (max.FondosMax != 0)
                        {
                            this.calculateMaxBalanceWithLimit((decimal)max.fondosmaxAmmount); hasMaxLimit = true;
                        }
                        else
                        {
                            hasMaxLimit = false;
                        }
                        this.parseAmmountsToString();
                    }
                    break;

                case FinancialStateReport.AccountHistoryConciliationsIn: // AccountHistory With BankStatements (Conciliaciones)
                    {

                        this.financialstateitemlist = new List<financialstateitemModel>();
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        var _fondoitemsTemp = this.fondoitems;
                        // var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentpurchaseitemsTemp = this.docpaymentpurchaseitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _incomeMovementTemp = this.incomemovements;
                        var _bankreconciliationitemsTemp = this.bankreconciliationitems;


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GWTTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        List<docpaymentpurchase> toEvalUpsc = new List<docpaymentpurchase>();
                        toEvalUpsc = _docpaymentpurchaseitemsTemp.ToList();
                        // toEvalUpsc = _docpaymentpurchaseitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList();

                        List<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        // toEvalReserv = _docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList();
                        toEvalReserv = _docpaymentreservitemsTemp.ToList();


                        // Bank reconciliations 
                        // ******************************* Getting Data and Deleting Upscls and Reservation that are in BankReconciliations
                        bankreconciliationModel[] reconciliationsTmpWork = new bankreconciliationModel[] { };
                        // reconciliationsTmpWork = bankReconciliationServices.getBakReconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true).ToArray();
                        reconciliationsTmpWork = _bankreconciliationitemsTemp.ToArray();

                        for (int i = 0; i <= reconciliationsTmpWork.Count() - 1; i++)
                        {
                            if (reconciliationsTmpWork[i].docpaymentpurchaseitems != null || reconciliationsTmpWork[i].docpaymentpurchaseitems.Count() != 0)
                            {
                                foreach (docpaymentpurchase model in reconciliationsTmpWork[i].docpaymentpurchaseitems)
                                {   // upscales.Add(model.Payment);
                                    toEvalUpsc.RemoveAll(c => c.Payment == model.Payment);
                                    // toEvalUpsc.Remove(model);
                                }
                            }

                            if (reconciliationsTmpWork[i].docpaymentreservitems != null || reconciliationsTmpWork[i].docpaymentreservitems.Count() != 0)
                            {
                                foreach (docpaymentreserv model in reconciliationsTmpWork[i].docpaymentreservitems)
                                {  // reservations.Add(model.ReservationPayment);
                                    toEvalReserv.RemoveAll(v => v.ReservationPayment == model.ReservationPayment);
                                    // toEvalReserv.Remove(model);
                                }
                            }
                        }
                        // Setting values before deleting conciliations
                        _docpaymentpurchaseitemsTemp = toEvalUpsc;
                        _docpaymentreservitemsTemp = toEvalReserv;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// CALCULATING BEFORE FINANCIAL STATE  /////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstateModel financialStateBefore = new financialstateModel();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, "-", _BankAccount.tblcurrencies.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountname;


                        // Fondos
                        if (_startFondos.Year == start.Year && _startFondos.Month == start.Month && _startFondos.Day != start.Day)
                        {
                            DateTime endBeforeFondos = start.AddDays(-1) < _startFondos ? _startFondos : start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        else
                        {
                            DateTime endBeforeFondos = start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        // Egresos - Documentos
                        /* if (_startDocitems.Year == start.Year && _startDocitems.Month == start.Month && _startDocitems.Day != start.Day)
                        {
                            DateTime endBeforeDocitems = start.AddDays(-1) < _startDocitems ? _startDocitems : start.AddDays(-1);
                            financialStateBefore.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= _startDocitems && y.aplicationDate <= endBeforeDocitems).ToList());
                        }
                        else
                        {
                            DateTime endBeforeDocitems = start.AddDays(-1);
                            financialStateBefore.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= _startDocitems && y.aplicationDate <= endBeforeDocitems).ToList());
                        } */
                        // Egresos - Pagos
                        if (_startPayments.Year == start.Year && _startPayments.Month == start.Month && _startPayments.Day != start.Day)
                        {
                            DateTime endBeforePayments = start.AddDays(-1) < _startPayments ? _startPayments : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        else
                        {
                            DateTime endBeforePayments = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        // Purchase - Pagos (Se toman comom ingresos)
                        if (_startPurchase.Year == start.Year && _startPurchase.Month == start.Month && _startPurchase.Day != start.Day)
                        {
                            DateTime endBeforePurchase = start.AddDays(-1) < _startPurchase ? _startPurchase : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforePurchase).ToList());
                        }
                        else
                        {
                            DateTime endBeforePurchase = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforePurchase).ToList());
                        }
                        // Reservations - Pagos (Se toman comom ingresos
                        if (_startReservatios.Year == start.Year && _startReservatios.Month == start.Month && _startReservatios.Day != start.Day)
                        {
                            DateTime endBeforeReservations = start.AddDays(-1) < _startReservatios ? _startReservatios : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());
                        }
                        else
                        {
                            DateTime endBeforeReservations = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());
                        }
                        // Ingresos 
                        if (_startIncomesMovements.Year == start.Year && _startIncomesMovements.Month == start.Month && _startIncomesMovements.Day != start.Day)
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1) < _startIncomesMovements ? _startIncomesMovements : start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }
                        else
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }
                        // Conciliaciones
                        if (_startReconciliations.Year == start.Year && _startReconciliations.Month == start.Month && _startReconciliations.Day != start.Day)
                        {
                            DateTime endBeforeBanConciliations = start.AddDays(-1) < _startReconciliations ? _startReconciliations : start.AddDays(-1);
                            financialStateBefore.addSource(_bankreconciliationitemsTemp.Where(y => y.bankstatementAplicationDate >= _startReconciliations && y.bankstatementAplicationDate <= endBeforeBanConciliations).ToList());
                        }
                        else
                        {
                            DateTime endBeforeBanConciliations = start.AddDays(-1);
                            financialStateBefore.addSource(_bankreconciliationitemsTemp.Where(y => y.bankstatementAplicationDate >= _startReconciliations && y.bankstatementAplicationDate <= endBeforeBanConciliations).ToList());
                        }

                        financialStateBefore.calculateMontoCargos();
                        financialStateBefore.calculateMontoAbonos();
                        financialStateBefore.calculateBalance();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;


                        this.balanceBefore = (decimal)financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;

                        //******************************* Initializaing sourcedata
                        this.fondoitems = new List<fondoModel>();
                        //this.docitemsitems = new List<invoiceitems>();
                        this.docpaymtitems = new List<invoicepayment>();
                        this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.incomemovements = new List<incomepayment>();
                        this.bankreconciliationitems = new List<bankreconciliationModel>();


                        //******************************* Getting interval data
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        // this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= startAfter && y.paymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= startAfter && y.aplicationdate <= endAfter).ToList());
                        this.addSource(_bankreconciliationitemsTemp.Where(y => y.bankstatementAplicationDate >= startAfter && y.bankstatementAplicationDate <= endAfter).ToList());


                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.calculateBalanceWithBalceBefore();
                        this.generateTimeLineBalance();
                        this.parseAmmountsToString();


                    }
                    break;
                case FinancialStateReport.AccountHistoryOnlyConciliationsIn:
                    {
                        this.financialstateitemlist = new List<financialstateitemModel>();
                        var _fondoitemsTemp = this.fondoitems;
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        // var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentpurchaseitemsTemp = this.docpaymentpurchaseitems.GroupBy(i => i.Payment).Select(g => g.First());
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems.GroupBy(i => i.ReservationPayment).Select(g => g.First());
                        var _incomeMovementTemp = this.incomemovements;
                        var _bankreconciliationitemsTemp = this.bankreconciliationitems;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GETTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        IList<docpaymentpurchase> toEvalUpsc = new List<docpaymentpurchase>();
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, _docpaymentpurchaseitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, _docpaymentpurchaseitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        IList<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        toEvalReserv = IListUtils.AddList(toEvalReserv, _docpaymentreservitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReserv = IListUtils.AddList(toEvalReserv, _docpaymentreservitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        // Setting values before get Transfer and deposits
                        _docpaymentpurchaseitemsTemp = toEvalUpsc;
                        _docpaymentreservitemsTemp = toEvalReserv;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// CALCULATING BEFORE FINANCIAL STATE  /////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstateModel financialStateBefore = new financialstateModel();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, "-", _BankAccount.tblcurrencies.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountname;


                        // Fondos
                        if (_startFondos.Year == start.Year && _startFondos.Month == start.Month && _startFondos.Day != start.Day)
                        {
                            DateTime endBeforeFondos = start.AddDays(-1) < _startFondos ? _startFondos : start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        else
                        {
                            DateTime endBeforeFondos = start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        // Egresos - DOcumentos
                        /*if (_startDocitems.Year == start.Year && _startDocitems.Month == start.Month && _startDocitems.Day != start.Day)
                        {
                            DateTime endBeforeDocitems = start.AddDays(-1) < _startDocitems ? _startDocitems : start.AddDays(-1);
                            financialStateBefore.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= _startDocitems && y.aplicationDate <= endBeforeDocitems).ToList());
                        }
                        else
                        {
                            DateTime endBeforeDocitems = start.AddDays(-1);
                            financialStateBefore.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= _startDocitems && y.aplicationDate <= endBeforeDocitems).ToList());
                        } */
                        //  Egresos - Pagos
                        if (_startPayments.Year == start.Year && _startPayments.Month == start.Month && _startPayments.Day != start.Day)
                        {
                            DateTime endBeforePayments = start.AddDays(-1) < _startPayments ? _startPayments : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        else
                        {
                            DateTime endBeforePayments = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        // Purchase - Pagos (Se toman comom ingresos)
                        if (_startPurchase.Year == start.Year && _startPurchase.Month == start.Month && _startPurchase.Day != start.Day)
                        {
                            DateTime endBeforePurchase = start.AddDays(-1) < _startPurchase ? _startPurchase : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforePurchase).ToList());
                        }
                        else
                        {
                            DateTime endBeforePurchase = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforePurchase).ToList());
                        }
                        // Reservations - Pagos (Se toman comom ingresos
                        if (_startReservatios.Year == start.Year && _startReservatios.Month == start.Month && _startReservatios.Day != start.Day)
                        {
                            DateTime endBeforeReservations = start.AddDays(-1) < _startReservatios ? _startReservatios : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());
                        }
                        else
                        {
                            DateTime endBeforeReservations = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());
                        }
                        // Ingresos 
                        if (_startIncomesMovements.Year == start.Year && _startIncomesMovements.Month == start.Month && _startIncomesMovements.Day != start.Day)
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1) < _startIncomesMovements ? _startIncomesMovements : start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }
                        else
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }
                        // Conciliaciones
                        if (_startReconciliations.Year == start.Year && _startReconciliations.Month == start.Month && _startReconciliations.Day != start.Day)
                        {
                            DateTime endBeforeBanConciliations = start.AddDays(-1) < _startReconciliations ? _startReconciliations : start.AddDays(-1);
                            financialStateBefore.addSource(_bankreconciliationitemsTemp.Where(y => y.bankstatementAplicationDate >= _startReconciliations && y.bankstatementAplicationDate <= endBeforeBanConciliations).ToList());
                        }
                        else
                        {
                            DateTime endBeforeBanConciliations = start.AddDays(-1);
                            financialStateBefore.addSource(_bankreconciliationitemsTemp.Where(y => y.bankstatementAplicationDate >= _startReconciliations && y.bankstatementAplicationDate <= endBeforeBanConciliations).ToList());
                        }

                        financialStateBefore.calculateMontoCargos();
                        financialStateBefore.calculateMontoAbonos();
                        financialStateBefore.calculateBalance();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;


                        this.balanceBefore = (decimal)financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;

                        //******************************* Initializaing sourcedata
                        this.docitemsitems = new List<invoiceitems>();
                        this.docpaymtitems = new List<invoicepayment>();
                        this.fondoitems = new List<fondoModel>();
                        this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.bankreconciliationitems = new List<bankreconciliationModel>();

                        //******************************* Getting interval data
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        //this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= startAfter && y.paymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= startAfter && y.aplicationdate <= endAfter).ToList());
                        this.addSource(_bankreconciliationitemsTemp.Where(y => y.bankstatementAplicationDate >= startAfter && y.bankstatementAplicationDate <= endAfter).ToList());



                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.calculateBalanceWithBalceBefore();
                        this.generateTimeLineBalance();
                        this.parseAmmountsToString();

                    }
                    break;
                case FinancialStateReport.MaxBalanceConciliationIn:
                    {

                        fondosmaxammountModel max = this.getfondosmaxammount(idBanAccount);


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GETTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        IList<docpaymentpurchase> toEvalUpsc = new List<docpaymentpurchase>();
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentpurchaseitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentpurchaseitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        IList<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());

                        IList<docpaymentparentreserv> toEvalReservParent = new List<docpaymentparentreserv>();
                        toEvalReservParent = IListUtils.AddList(toEvalReservParent, this.docpaymentparentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReservParent = IListUtils.AddList(toEvalReservParent, this.docpaymentparentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        // Setting values before get Transfer and deposits
                        //this.docpaymentpurchaseitems = toEvalUpsc;
                        //this.docpaymentreservitems = toEvalReserv;

                        IList<invoiceitems> toEvalDocitems = new List<invoiceitems>();
                        IList<invoicepayment> toEvalDocpaymt = new List<invoicepayment>();
                        IList<fondoModel> toEvalFondo = new List<fondoModel>();
                        IList<incomepayment> toEvalIncomemovement = new List<incomepayment>();
                        IList<bankreconciliationModel> toEvalbankreconciliation = new List<bankreconciliationModel>();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();

                        if (max.FondosMax != 0)
                        {
                            this.calculateMaxBalanceWithLimit((decimal)max.fondosmaxAmmount); hasMaxLimit = true;
                        }
                        else
                        {
                            hasMaxLimit = false;
                        }
                        this.parseAmmountsToString();
                    }
                    break;
                case FinancialStateReport.BalanceConciliationIn:
                    {

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GETTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        IList<docpaymentpurchase> toEvalUpsc = new List<docpaymentpurchase>();
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentpurchaseitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentpurchaseitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());

                        IList<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());

                        IList<docpaymentparentreserv> toEvalReservParent = new List<docpaymentparentreserv>();
                        toEvalReservParent = IListUtils.AddList(toEvalReservParent, this.docpaymentparentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReservParent = IListUtils.AddList(toEvalReservParent, this.docpaymentparentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());

                        // Setting values before get Transfer and deposits
                        this.docpaymentpurchaseitems = toEvalUpsc;
                        this.docpaymentreservitems = toEvalReserv;
                        this.docpaymentparentreservitems = toEvalReservParent;
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //******************************* Getting interval data *******************************/
                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.parseAmmountsToString();

                    }
                    break;
                case FinancialStateReport.BAccountCash: // Efectivo en cuenta bancaria
                    {
                        var _conciliationComision = this.bankreconciliationitems;
                        var _bankstatements = this.bankstatementsitems;
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Balance Before //////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstateModel financialStateBefore = new financialstateModel();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, "-", _BankAccount.tblcurrencies.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountname;

                        //Conciliation comision
                        if (_startReconciliations.Year == start.Year && _startReconciliations.Month == start.Month && _startReconciliations.Day != start.Day)
                        {
                            DateTime endBeforeReconciliations = start.AddDays(-1) < _startReconciliations ? _startReconciliations : start.AddDays(-1);
                            financialStateBefore.addSource(_conciliationComision.Where(y => y.bankstatementAplicationDate >= _startReconciliations && y.bankstatementAplicationDate <= endBeforeReconciliations).ToList());
                        }
                        else
                        {
                            DateTime endBeforeReconciliations = start.AddDays(-1);
                            financialStateBefore.addSource(_conciliationComision.Where(y => y.bankstatementAplicationDate >= _startReconciliations && y.bankstatementAplicationDate <= endBeforeReconciliations).ToList());
                        }
                        //Movimientos de cuenta
                        if (_startBankStatement.Year == start.Year && _startBankStatement.Month == start.Month && _startBankStatement.Day != start.Day)
                        {
                            DateTime endBeforeBankStatement = start.AddDays(-1) < _startBankStatement ? _startBankStatement : start.AddDays(-1);
                            financialStateBefore.addSource(_bankstatements.Where(y => y.bankstatementsAplicationDate >= _startBankStatement && y.bankstatementsAplicationDate <= endBeforeBankStatement).ToList());
                        }
                        else
                        {
                            DateTime endBeforeBankStatement = start.AddDays(-1);
                            financialStateBefore.addSource(_bankstatements.Where(y => y.bankstatementsAplicationDate >= _startBankStatement && y.bankstatementsAplicationDate <= endBeforeBankStatement).ToList());
                        }

                        financialStateBefore.calculateMontoCargosSaldoBancos();
                        financialStateBefore.calculateMontoAbonosSaldoBancos();
                        financialStateBefore.calculateBalanceBancos();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;

                        this.balanceBefore = (decimal)financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;

                        this.bankreconciliationitems = new List<bankreconciliationModel>();
                        this.bankstatementsitems = new List<bankstatements>();

                        this.addSource(_conciliationComision.Where(y => y.bankstatementAplicationDate >= startAfter && y.bankstatementAplicationDate <= endAfter).ToList());
                        this.addSource(_bankstatements.Where(y => y.bankstatementsAplicationDate >= startAfter && y.bankstatementsAplicationDate <= endAfter).ToList());

                        this.calculateMontoCargosSaldoBancos();
                        this.calculateMontoAbonosSaldoBancos();
                        this.calculateBalanceBancos();
                        this.calculateBalanceWithBalceBeforeBancos();
                        this.generateTimeLineBalanceBancos();
                        this.parseAmmountsToString();
                        this.hightLightBankreconciliations();
                    }
                    break;
                default:
                    {
                        start = new DateTime(2018, 1, 1);
                        // Appli Date from tblParámeters


                        // financialstate financialState = new financialstate();
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;


                        this.addSource(convertTbltoHelper(this.getAnualBudgetsTo(start, end, idBanAccount).ToList()));
                        this.addSource(convertTbltoHelper(this.getAnualBudgetsFrom(start, end, idBanAccount).ToList()));
                        this.addSource(convertTbltoHelper(this.getAnualPayments(start, end, idBanAccount).ToList()));
                        this.parseAmmountsToString();
                        //financialState.calculateMaxBalanceWithLimit(max.fondosmaxAmmount);

                        // return financialState;

                    }
                    break;

            }

            #endregion

        }

        private void BodyCalculatingandData(int tpv, decimal ammountStart, decimal ammountEnd, int idHotel)
        {

            #region Calculating and Data

            this.FechaInicioString = DateTimeUtils.ParseDatetoString(start);
            this.FechaFinString = DateTimeUtils.ParseDatetoString(end);

            // Initializing List
            this.docitemsitems = new List<invoiceitems>();
            this.docpaymtitems = new List<invoicepayment>();
            this.fondoitems = new List<fondoModel>();
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.bankreconciliationitems = new List<bankreconciliationModel>();
            this.financialstateitemlist = new List<financialstateitemModel>();
            this.sourcedataitems = new List<bankaccountsourcedataModel>();
            this.HotelPartner = new List<int>();
            this.tbltpv = new List<tbltpv>();
            //this.baccounthotels = new List<tblbaccounthotels>();
            this.incomemovements = new List<incomepayment>();

            // Getting only General onformation
            var _BankAccount = unity.BankAccountRepository.Get(t => t.idbaccount == idBanAccount, null, "tblcompanies,tblbank,tblcurrencies").FirstOrDefault();/*se quita tblcompanies.tblcompanyhotel*/
            this.baccountName = _BankAccount.baccountname;
            this.baccountShortName = _BankAccount.baccountshortname;
            this.idBAccount = _BankAccount.idbaccount;

            // Gettting Bank Account TPV's // Se asigna solo el hotel solicitado
            // 
            if (tpv != 0)
            {
                this.tbltpv = IEnumerableUtils.AddList(this.tbltpv, _BankAccount.tblbaccounttpv.Where(b => b.idtpv == this.tpv).Select(t => new tbltpv { idtpv = t.tbltpv.idtpv, tpvname = t.tbltpv.tpvname, tpvidlocation = t.tbltpv.tpvidlocation }).ToList());
            }

            else
            {
                this.tbltpv = IEnumerableUtils.AddList(this.tbltpv, _BankAccount.tblbaccounttpv.Select(t => new tbltpv { idtpv = t.tbltpv.idtpv, tpvname = t.tbltpv.tpvname, tpvidlocation = t.tbltpv.tpvidlocation }).ToList());
            }

            // Getting the Bank Account Sources data
            this.sourcedataitems = IEnumerableUtils.AddList(this.sourcedataitems, _BankAccount.tblbankaccountsourcedata.Select(y => new bankaccountsourcedataModel { SourceData = y.idsourcedata, sourcedataDateStart = y.sourcedatadatestart, Types = y.tblbankaccountsourcedatatypes.Select(t => t.idtype).ToList() }).ToList());

            // Getting id hotel relationships VTH
            //this.HotelVTH = IEnumerableUtils.AddList(this.HotelVTH, _BankAccount.tblcompanies.tblcompanyhotel.Select(y => y.idHotel).ToList());

            // Getting the Bank Account - Hotel 
            if (idHotel != 0) // Si el hotel es diferente de cero, se ha solicitado un banco en especifico
            {
                //this.baccounthotels = IEnumerableUtils.AddList(this.baccounthotels, _BankAccount.tblbaccounthotels.Where(t => t.idHotel == idHotel && t.baccounthotelActive == Constantes.activeRecord).Select(t => new tblbaccounthotels { idBAccount = t.idBAccount, idHotel = t.idHotel }).ToList());
                //this.HotelVTH = IEnumerableUtils.AddList(this.HotelVTH, _BankAccount.tblcompanies.tblcompanyhotel.Select(y => y.idHotel).ToList());
            }

            else
            {
                //this.baccounthotels = IEnumerableUtils.AddList(this.baccounthotels, _BankAccount.tblbaccounthotels.Where(t => t.baccounthotelActive == Constantes.activeRecord).Select(t => new tblbaccounthotels { idBAccount = t.idBAccount, idHotel = t.idHotel }).ToList());
            }

            DateTime _startFondos = new DateTime();
            DateTime _startPayments = new DateTime();
            DateTime _startPurchase = new DateTime();
            DateTime _startReservatios = new DateTime();
            DateTime _startIncomesMovements = new DateTime();
            DateTime _startReconciliations = new DateTime();

            foreach (bankaccountsourcedataModel model in sourcedataitems)
            {
                this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName); this.PaymentMethodName = _BankAccount.baccountname;

                switch (model.SourceData)
                {
                    case 1: // Ingresos
                        {

                        }
                        break;
                    case 2: // Movimientos ingreso
                        {
                            this.addSource(convertTbltoHelper(this.getAnualIncomeMovements((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            _startIncomesMovements = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 3: // Facturas
                        {
                            // this.addSource(this.convertTbltoHelper(this.getAnualDocitems(model.sourcedataDateStart, end, _BankAccount.idCompany).ToList()));
                            // _startDocitems = model.sourcedataDateStart;
                        }
                        break;
                    case 4: // Pagos
                        {
                            this.addSource(convertTbltoHelper(this.getAnualPayments((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            _startPayments = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 5: // Fondos
                        {
                            this.addSource(convertTbltoHelper(this.getAnualBudgetsTo((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            this.addSource(convertTbltoHelper(this.getAnualBudgetsFrom((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            _startFondos = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 6:
                        {
                            this.addSource(bankReconciliationServices.getBakReconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true));
                            _startReconciliations = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 7:
                        {

                            // Si la cuenta tiene terminales se buscan los registros de ReservationsPayments que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {   // Reservas Members
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVTPV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, this.tbltpv.Select(y => y.idtpv).ToArray(), ammountStart, ammountEnd/*, baccounthotels.Select(c => c.idHotel).ToArray()*/).ToList()).ToList());
                                //Reservas Directivos, Award/Courtesy, I love worldpass
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVTPVPARENT((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, this.tbltpv.Select(y => y.idtpv).ToArray(), ammountStart, ammountEnd/*, baccounthotels.Select(c => c.idHotel).ToArray()*/).ToList()).ToList());

                            }
                            else
                            {
                                // Reservaciones Member Payments
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                                // Reservacines WEB PAYPAL
                                //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVAS_WEB((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                                // Reservaciones Directivos, Award/Courtesy, I love worldpass
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVPARENT((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                            }
                            _startReservatios = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    case 8: // Purchase
                        {

                            // Si la cuenta tiene terminales se buscan los registros de upscl.PAyments-PaymentsInstruments que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {
                                foreach (tbltpv _model in tbltpv)
                                {
                                    this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchaseTPV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, _model.idtpv, ammountStart, ammountEnd/*, baccounthotels.Select(c => c.idHotel).ToArray()*/).ToList()).ToList());
                                }
                            }
                            else
                            {
                                // Adding Purchase Renewals 
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchase((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), /*this.HotelPartner.ToArray(),*/ ammountStart, ammountEnd).ToList()).ToList());
                            }
                            _startPurchase = (DateTime)model.sourcedataDateStart;
                        }
                        break;
                    default:
                        {
                            break;
                        }
                }
            }


            switch (type)
            {

                case FinancialStateReport.AccountHistory: // AccountHistory
                    {
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Initializing ////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        this.financialstateitemlist = new List<financialstateitemModel>();
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        var _fondoitemsTemp = this.fondoitems;
                        var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentpurchaseitemsTemp = this.docpaymentpurchaseitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _docpaymentparentreservitemsTemp = this.docpaymentparentreservitems;
                        var _incomeMovementTemp = this.incomemovements;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Balance Before //////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstateModel financialStateBefore = new financialstateModel();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, "-", _BankAccount.tblcurrencies.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountname;


                        // Fondos
                        if (_startFondos.Year == start.Year && _startFondos.Month == start.Month && _startFondos.Day != start.Day)
                        {
                            DateTime endBeforeFondos = start.AddDays(-1) < _startFondos ? _startFondos : start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        else
                        {
                            DateTime endBeforeFondos = start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        // Egresos - Pagos
                        if (_startPayments.Year == start.Year && _startPayments.Month == start.Month && _startPayments.Day != start.Day)
                        {
                            DateTime endBeforePayments = start.AddDays(-1) < _startPayments ? _startPayments : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        else
                        {
                            DateTime endBeforePayments = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        // PURCHASE - Pagos (Se toman comom ingresos)
                        if (_startPurchase.Year == start.Year && _startPurchase.Month == start.Month && _startPurchase.Day != start.Day)
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1) < _startPurchase ? _startPurchase : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforeUpscales).ToList());
                        }
                        else
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforeUpscales).ToList());
                        }
                        // Reservations - Pagos (Se toman como ingresos)
                        if (_startReservatios.Year == start.Year && _startReservatios.Month == start.Month && _startReservatios.Day != start.Day)
                        {
                            DateTime endBeforeReservations = start.AddDays(-1) < _startReservatios ? _startReservatios : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                            financialStateBefore.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                        }
                        else
                        {
                            DateTime endBeforeReservations = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                            financialStateBefore.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                        }
                        // Ingresos 
                        if (_startIncomesMovements.Year == start.Year && _startIncomesMovements.Month == start.Month && _startIncomesMovements.Day != start.Day)
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1) < _startIncomesMovements ? _startIncomesMovements : start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }
                        else
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }

                        financialStateBefore.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations

                        financialStateBefore.calculateMontoCargos();
                        financialStateBefore.calculateMontoAbonos();
                        financialStateBefore.calculateBalance();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;


                        this.balanceBefore = (decimal)financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;

                        //*******************************/
                        this.docitemsitems = new List<invoiceitems>();
                        this.docpaymtitems = new List<invoicepayment>();
                        this.fondoitems = new List<fondoModel>();
                        this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
                        this.incomemovements = new List<incomepayment>();

                        /*******************************/
                        // Getting budgets and Payments in interval
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        //this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= startAfter && y.paymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= startAfter && y.aplicationdate <= endAfter).ToList());

                        // this.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.calculateBalanceWithBalceBefore();
                        this.generateTimeLineBalance(6);
                        this.parseAmmountsToString();
                        this.hightLightBankreconciliations();
                        this.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations


                    }
                    break;
               
                default:
                    {
                        start = new DateTime(2018, 1, 1);
                        // Appli Date from tblParámeters


                        // financialstate financialState = new financialstate();
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;


                        this.addSource(convertTbltoHelper(this.getAnualBudgetsTo(start, end, idBanAccount).ToList()));
                        this.addSource(convertTbltoHelper(this.getAnualBudgetsFrom(start, end, idBanAccount).ToList()));
                        this.addSource(convertTbltoHelper(this.getAnualPayments(start, end, idBanAccount).ToList()));
                        this.parseAmmountsToString();
                        //financialState.calculateMaxBalanceWithLimit(max.fondosmaxAmmount);

                        // return financialState;

                    }
                    break;

            }

            #endregion

        }

        private void BodyCalculatingandData(decimal ammountStart, decimal ammountEnd)
        {
            this.FechaInicioString = DateTimeUtils.ParseDatetoString(start);
            this.FechaFinString = DateTimeUtils.ParseDatetoString(end);

            // Initializing List
            this.docitemsitems = new List<invoiceitems>();
            this.docpaymtitems = new List<invoicepayment>();
            this.fondoitems = new List<fondoModel>();
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
            this.bankstatementsitems = new List<bankstatements>();
            this.financialstateitemlist = new List<financialstateitemModel>();
            this.sourcedataitems = new List<bankaccountsourcedataModel>();
            this.HotelPartner = new List<int>();
            this.tbltpv = new List<tbltpv>();
            //this.baccounthotels = new List<tblbaccounthotels>();
            this.incomemovements = new List<incomepayment>();

            // Getting only General onformation
            var _BankAccount = unity.BankAccountRepository.Get(t => t.idbaccount == idBanAccount, null, "tblcompanies,tblbank,tblcurrencies").FirstOrDefault();/*se quita tblcompanies.tblcompanyhotel*/
            this.baccountName = _BankAccount.baccountname;
            this.baccountShortName = _BankAccount.baccountshortname;
            this.idBAccount = _BankAccount.idbaccount;

            // Gettting Bank Account TPV's // Se asigna solo el hotel solicitado
            this.tbltpv = IEnumerableUtils.AddList(this.tbltpv, _BankAccount.tblbaccounttpv.Select(t => new tbltpv { idtpv = t.tbltpv.idtpv, tpvname = t.tbltpv.tpvname, tpvidlocation = t.tbltpv.tpvidlocation }).ToList());
            //}

            // Getting the Bank Account Sources data
            this.sourcedataitems = IEnumerableUtils.AddList(this.sourcedataitems, _BankAccount.tblbankaccountsourcedata.Select(y => new bankaccountsourcedataModel { SourceData = y.idsourcedata, sourcedataDateStart = y.sourcedatadatestart, Types = y.tblbankaccountsourcedatatypes.Select(t => t.idtype).ToList() }).ToList());


            DateTime _startFondos = new DateTime();
            DateTime _startPayments = new DateTime();
            DateTime _startPurchase = new DateTime();
            DateTime _startReservatios = new DateTime();
            DateTime _startIncomesMovements = new DateTime();
            DateTime _startReconciliations = new DateTime();

            foreach (bankaccountsourcedataModel model in sourcedataitems)
            {
                this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName); this.PaymentMethodName = _BankAccount.baccountname;

                if (this.typeSourceData == 0) // Todos
                {
                    switch (model.SourceData)
                    {
                        case 1: // Ingresos
                            {

                            }
                            break;
                        case 2: // Movimientos ingreso
                            {
                                this.addSource(convertTbltoHelper(this.getAnualIncomeMovements((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                _startIncomesMovements = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 3: // Facturas
                            {
                                // this.addSource(this.convertTbltoHelper(this.getAnualDocitems(model.sourcedataDateStart, end, _BankAccount.idCompany).ToList()));
                                // _startDocitems = model.sourcedataDateStart;
                            }
                            break;
                        case 4: // Pagos
                            {
                                this.addSource(convertTbltoHelper(this.getAnualPayments((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                _startPayments = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 5: // Fondos
                            {
                                this.addSource(convertTbltoHelper(this.getAnualBudgetsTo((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                this.addSource(convertTbltoHelper(this.getAnualBudgetsFrom((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                _startFondos = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 6: // Conciliaciones
                            {
                                this.addSource(bankReconciliationServices.getBankStat2Reconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true));
                                _startReconciliations = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 7:
                            {
                                // Reservaciones Member Payments
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, Constants.methodPayment, this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                                // Reservacines WEB PAYPAL
                                //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVAS_WEB((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                                // Reservaciones Directivos, Award/Courtesy, I love worldpass
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVPARENT((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, Constants.methodPayment, this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());

                                _startReservatios = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 8: // Purchase
                            {
                                // Adding Purchase Renewals 
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchase((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, Constants.methodPayment, /*this.HotelPartner.ToArray(),*/ ammountStart, ammountEnd).ToList()).ToList());

                                _startPurchase = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        default:
                            {
                                break;
                            }
                    }
                }
                if (this.typeSourceData == (int)BankSourceData.Fondos) // Entrada y Salida
                {
                    switch (model.SourceData)
                    {
                        case 5: // Fondos
                            {
                                this.addSource(convertTbltoHelper(this.getAnualBudgetsTo((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                this.addSource(convertTbltoHelper(this.getAnualBudgetsFrom((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                _startFondos = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 6: // Conciliaciones 2
                            {
                                this.addSource(bankReconciliationServices.getBankStat2Reconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true));
                                _startReconciliations = (DateTime)model.sourcedataDateStart;
                            }
                            break;

                        default:
                            {
                                break;
                            }
                    }
                }
                if (this.typeSourceData == (int)BankSourceData.Pagos) // Salidas
                {
                    switch (model.SourceData)
                    {
                        case 4: // Pagos
                            {
                                this.addSource(convertTbltoHelper(this.getAnualPayments((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                _startPayments = (DateTime)model.sourcedataDateStart;
                            }
                            break;;
                        case 6: // Conciliaciones 2
                            {
                                this.addSource(bankReconciliationServices.getBankStat2Reconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true));
                                _startReconciliations = (DateTime)model.sourcedataDateStart;
                            }
                            break;      
                        default:
                            {
                                break;
                            }
                    }
                }
                if (this.typeSourceData == (int)BankSourceData.Ingresos) // Entrada
                {
                    switch (model.SourceData)
                    {
                        case 1: // Ingresos
                            {

                            }
                            break;
                        case 2: // Movimientos ingreso
                            {
                                this.addSource(convertTbltoHelper(this.getAnualIncomeMovements((DateTime)model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                                _startIncomesMovements = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 6: // Conciliaciones
                            {
                                this.addSource(bankReconciliationServices.getBankStat2Reconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true));
                                _startReconciliations = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 7: // Reservas
                            {
                                // Reservaciones Member Payments
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERV((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, Constants.methodPayment, this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                                // Reservacines WEB PAYPAL
                                //this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVAS_WEB((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, model.Types.ToArray(), this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                                // Reservaciones Directivos, Award/Courtesy, I love worldpass
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsRESERVPARENT((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, Constants.methodPayment, this.HotelPartner.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());

                                _startReservatios = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        case 8: // Purchase
                            {
                                // Adding Purchase Renewals 
                                this.addSource(this.convertTbltoHelper(this.getAnualPaymentsPurchase((DateTime)model.sourcedataDateStart, end, _BankAccount.idcurrency, Constants.methodPayment, /*this.HotelPartner.ToArray(),*/ ammountStart, ammountEnd).ToList()).ToList());
                                // Adding Purchase New and Upgrade


                                _startPurchase = (DateTime)model.sourcedataDateStart;
                            }
                            break;
                        default:
                            {
                                break;
                            }
                    }
                }
            }

            switch (type)
            {
                case FinancialStateReport.AccountHistory: // AccountHistory
                    {
                        this.financialstateitemlist = new List<financialstateitemModel>();
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        var _fondoitemsTemp = this.fondoitems;
                        //var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentpurchaseitemsTemp = this.docpaymentpurchaseitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _docpaymentparentreservitemsTemp = this.docpaymentparentreservitems;
                        var _incomeMovementTemp = this.incomemovements;


                        financialstateModel financialStateBefore = new financialstateModel();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, "-", _BankAccount.tblcurrencies.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountname;

                        // Fondos
                        if (_startFondos.Year == start.Year && _startFondos.Month == start.Month && _startFondos.Day != start.Day)
                        {
                            DateTime endBeforeFondos = start.AddDays(-1) < _startFondos ? _startFondos : start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        else
                        {
                            DateTime endBeforeFondos = start.AddDays(-1);
                            financialStateBefore.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= _startFondos && y.fechaEntrega <= endBeforeFondos).ToList());
                        }
                        // Egresos - Pagos
                        if (_startPayments.Year == start.Year && _startPayments.Month == start.Month && _startPayments.Day != start.Day)
                        {
                            DateTime endBeforePayments = start.AddDays(-1) < _startPayments ? _startPayments : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        else
                        {
                            DateTime endBeforePayments = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= _startPayments && y.aplicationDate <= endBeforePayments).ToList());
                        }
                        // Purchases
                        if (_startPurchase.Year == start.Year && _startPurchase.Month == start.Month && _startPurchase.Day != start.Day)
                        {
                            DateTime endBeforeReservations = start.AddDays(-1) < _startPurchase ? _startPurchase : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforeReservations).ToList());
                        }
                        else
                        {
                            DateTime endBeforePurchase = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= _startPurchase && y.paymentDate <= endBeforePurchase).ToList());
                        }
                        // Reservations - Pagos (Se toman comom ingresos
                        if (_startReservatios.Year == start.Year && _startReservatios.Month == start.Month && _startReservatios.Day != start.Day)
                        {
                            DateTime endBeforeReservations = start.AddDays(-1) < _startReservatios ? _startReservatios : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                            financialStateBefore.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                        }
                        else
                        {
                            DateTime endBeforeReservations = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                            financialStateBefore.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= _startReservatios && y.reservationPaymentDate <= endBeforeReservations).ToList());

                        }
                        // Ingresos 
                        if (_startIncomesMovements.Year == start.Year && _startIncomesMovements.Month == start.Month && _startIncomesMovements.Day != start.Day)
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1) < _startIncomesMovements ? _startIncomesMovements : start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }
                        else
                        {
                            DateTime endBeforeIncomeMovements = start.AddDays(-1);
                            financialStateBefore.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= _startIncomesMovements && y.aplicationdate <= endBeforeIncomeMovements).ToList());
                        }

                        financialStateBefore.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations

                        financialStateBefore.calculateMontoCargos();
                        financialStateBefore.calculateMontoAbonos();
                        financialStateBefore.calculateBalance();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;


                        this.balanceBefore = (decimal)financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;

                        //*******************************/
                        this.docitemsitems = new List<invoiceitems>();
                        this.docpaymtitems = new List<invoicepayment>();
                        this.fondoitems = new List<fondoModel>();
                        this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.docpaymentparentreservitems = new List<docpaymentparentreserv>();
                        this.incomemovements = new List<incomepayment>();

                        /*******************************/
                        // Getting budgets and Payments in interval
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        //this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentpurchaseitemsTemp.Where(y => y.paymentDate >= startAfter && y.paymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_docpaymentparentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= startAfter && y.aplicationdate <= endAfter).ToList());

                        // this.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.calculateBalanceWithBalceBefore();
                        this.generateTimeLineBalance(6);
                        this.parseAmmountsToString();
                        this.hightLightBankreconciliations2();
                        this.bankreconciliationitems = new List<bankreconciliationModel>(); // in this reports not include Reconciliations

                    }
                    break;
                default:
                    {
                        start = new DateTime(2018, 1, 1);
                        // Appli Date from tblParámeters


                        // financialstate financialState = new financialstate();
                        this.CurrencyName = string.Concat(_BankAccount.tblcurrencies.currencyAlphabeticCode, " - ", _BankAccount.tblcurrencies.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountname;


                        this.addSource(convertTbltoHelper(this.getAnualBudgetsTo(start, end, idBanAccount).ToList()));
                        this.addSource(convertTbltoHelper(this.getAnualBudgetsFrom(start, end, idBanAccount).ToList()));
                        this.addSource(convertTbltoHelper(this.getAnualPayments(start, end, idBanAccount).ToList()));
                    }
                    break;
            }


        }

        private void applyRowIndex()
        {
            int counterIndex = 0, counterNumber = 1;

            for (int i = 0; i <= financialstateitemlist.Count() - 1; i++)
            {
                financialstateitemlist.ElementAt(i).rowIndex = counterIndex;
                financialstateitemlist.ElementAt(i).rowNumber = counterNumber;
                counterIndex++;
                counterNumber++;
            }
        }

        private fondosmaxammountModel getfondosmaxammount(int idPaymentMethod)
        {
            var result = this.unity.FondosMaxLimitRepository.Get(t => t.tblbankaccount.idbaccount == idPaymentMethod, null, "tblbankaccount").FirstOrDefault();

            if (result != null)
                return convertTbltoHelper(result);
            else return new fondosmaxammountModel();

        }

        public void deleteFinancialStateItemLinked()
        {
            this.financialstateitemlist = this.financialstateitemlist.Where(f => f.bankStatementLinked == false).ToList();
            this.applyRowIndex();
        }

        public void hightLightBankreconciliations()
        {
            // Current FinancialStateitemlist of Financial State
            financialstateitemModel[] currentfinancialstateitem = new financialstateitemModel[] { };
            //var financialstateitem = RemoveDuplicate(this.financialstateitemlist.ToList());
            currentfinancialstateitem = this.financialstateitemlist.ToArray();
            this.financialstateitemlist = new List<financialstateitemModel>();

            // Current Bank reconciliations 
            bankreconciliationModel[] reconciliationsTmpWork = new bankreconciliationModel[] { };


            List<bankreconciliationModel> allBR = new List<bankreconciliationModel>();

            List<bankreconciliationModel> br1 = bankReconciliationServices.getBakReconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true).ToList();
            List<bankreconciliationModel> br2 = bankReconciliationServices.getBakReconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Parcial, true, true).ToList();

            br1.ForEach(v => allBR.Add(v));
            br2.ForEach(v => allBR.Add(v));
            reconciliationsTmpWork = allBR.ToArray();

            List<financialstateitemModel> tmpFinancialStateItem = new List<financialstateitemModel>();

            // Recorriendo las conciliaciones actuales y tomando todos los financialstateitemModel
            for (int i = 0; i <= reconciliationsTmpWork.Count() - 1; i++)
            {
                if (reconciliationsTmpWork[i].financialstateitemlist != null || reconciliationsTmpWork[i].financialstateitemlist.Count() != 0)
                {
                    foreach (financialstateitemModel model in reconciliationsTmpWork[i].financialstateitemlist)
                    {
                        // model.bankStatementStatus = reconciliationsTmpWork[i].statusconciliation;
                        tmpFinancialStateItem.Add(model);
                    }
                }
            }

            var tmpFinancialStateItemArray = tmpFinancialStateItem.ToArray(); // Parsig to array the list obtained of bankreconciliaitons
            for (int k = 0; k <= tmpFinancialStateItem.Count() - 1; k++)
            {
                var index = Array.FindIndex(currentfinancialstateitem, m => m.SourceData == tmpFinancialStateItemArray[k].SourceData && m.Reference == tmpFinancialStateItemArray[k].Reference && m.ReferenceItem == tmpFinancialStateItemArray[k].ReferenceItem);
                if (index != -1)
                {
                    currentfinancialstateitem[index].bankStatementLinked = true;
                    currentfinancialstateitem[index].bankStatementStatus = tmpFinancialStateItemArray[k].bankStatementStatus;
                }
            }

            this.financialstateitemlist = currentfinancialstateitem.ToList(); // Adding the new List of finanical state item adding linked true in each case
        }

        public void hightLightBankreconciliations2()
        {
            // Current FinancialStateitemlist of Financial State
            financialstateitemModel[] currentfinancialstateitem = new financialstateitemModel[] { };
            //var financialstateitem = RemoveDuplicate(this.financialstateitemlist.ToList());
            currentfinancialstateitem = this.financialstateitemlist.ToArray();
            this.financialstateitemlist = new List<financialstateitemModel>();

            // Current Bank reconciliations 
            bankstatements[] reconciliationsTmpWork = new bankstatements[] { };


            List<bankstatements> allBR = new List<bankstatements>();

            List<bankstatements> br1 = bankReconciliationServices.getBankStat2Reconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Completo, true, true).ToList();
            List<bankstatements> br2 = bankReconciliationServices.getBankStat2Reconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconciliationStatus.Parcial, true, true).ToList();

            br1.ForEach(v => allBR.Add(v));
            br2.ForEach(v => allBR.Add(v));
            reconciliationsTmpWork = allBR.ToArray();

            List<financialstateitemModel> tmpFinancialStateItem = new List<financialstateitemModel>();

            // Recorriendo las conciliaciones actuales y tomando todos los financialstateitemModel
            for (int i = 0; i <= reconciliationsTmpWork.Count() - 1; i++)
            {
                if (reconciliationsTmpWork[i].financialstateitemlist != null || reconciliationsTmpWork[i].financialstateitemlist.Count() != 0)
                {
                    foreach (financialstateitemModel model in reconciliationsTmpWork[i].financialstateitemlist)
                    {
                        // model.bankStatementStatus = reconciliationsTmpWork[i].statusconciliation;
                        tmpFinancialStateItem.Add(model);
                    }
                }
            }

            var tmpFinancialStateItemArray = tmpFinancialStateItem.ToArray(); // Parsig to array the list obtained of bankreconciliaitons
            for (int k = 0; k <= tmpFinancialStateItem.Count() - 1; k++)
            {
                var index = Array.FindIndex(currentfinancialstateitem, m => m.SourceData == tmpFinancialStateItemArray[k].SourceData && m.Reference == tmpFinancialStateItemArray[k].Reference && m.ReferenceItem == tmpFinancialStateItemArray[k].ReferenceItem);
                if (index != -1)
                {
                    currentfinancialstateitem[index].bankStatementLinked = true;
                    currentfinancialstateitem[index].bankStatementStatus = tmpFinancialStateItemArray[k].bankStatementStatus;
                }
            }

            this.financialstateitemlist = currentfinancialstateitem.ToList(); // Adding the new List of finanical state item adding linked true in each case
        }

        #endregion

        #region AddSource
        /*******************************/
        public void addSource(fondoModel helper)
        {
            if (this.fondoitems == null || this.fondoitems.Count() == 0)
            {
                this.fondoitems = IEnumerableUtils.AddSingle<fondoModel>(helper);
            }
            else
            {
                IEnumerableUtils.AddSingle<fondoModel>(this.fondoitems, helper);
            }

        }
        public void addSource(List<fondoModel> listhelper)
        {
            if (this.fondoitems == null || this.fondoitems.Count() == 0)
            {
                this.fondoitems = IEnumerableUtils.AddList<fondoModel>(listhelper);
            }
            else
            {
                this.fondoitems = IEnumerableUtils.AddList<fondoModel>(this.fondoitems, listhelper);
            }


        }
        /*****************************/
        public void addSource(invoicepayment helper)
        {
            if (this.docpaymtitems == null || this.docpaymtitems.Count() == 0)
            { this.docpaymtitems = IEnumerableUtils.AddSingle<invoicepayment>(helper); }
            else { IEnumerableUtils.AddSingle<invoicepayment>(this.docpaymtitems, helper); }

        }
        public void addSource(List<invoicepayment> listhelper)
        {
            if (this.docpaymtitems == null || this.docpaymtitems.Count() == 0)
            { this.docpaymtitems = IEnumerableUtils.AddList<invoicepayment>(listhelper); }
            else { this.docpaymtitems = IEnumerableUtils.AddList<invoicepayment>(this.docpaymtitems, listhelper); }

        }

        /*****************************/

        public void addSource(invoiceitems helper)
        {
            if (this.docitemsitems == null || this.docitemsitems.Count() == 0) { this.docitemsitems = IEnumerableUtils.AddSingle<invoiceitems>(helper); }
            else { IEnumerableUtils.AddSingle<invoiceitems>(this.docitemsitems, helper); }
        }
        public void addSource(List<invoiceitems> listhelper)
        {
            if (this.docitemsitems == null || this.docitemsitems.Count() == 0) { this.docitemsitems = IEnumerableUtils.AddList<invoiceitems>(listhelper); }
            else { this.docitemsitems = IEnumerableUtils.AddList<invoiceitems>(this.docitemsitems, listhelper); }
        }

        /*****************************/
        public void addSource(docpaymentpurchase helper)
        {
            if (this.docpaymentpurchaseitems == null || this.docpaymentpurchaseitems.Count() == 0) { this.docpaymentpurchaseitems = IEnumerableUtils.AddSingle<docpaymentpurchase>(helper); }
            else { IEnumerableUtils.AddSingle<docpaymentpurchase>(this.docpaymentpurchaseitems, helper); }
        }
        public void addSource(List<docpaymentpurchase> listhelper)
        {
            if (this.docpaymentpurchaseitems == null || this.docpaymentpurchaseitems.Count() == 0) { this.docpaymentpurchaseitems = IEnumerableUtils.AddList<docpaymentpurchase>(listhelper); }
            else { this.docpaymentpurchaseitems = IEnumerableUtils.AddList<docpaymentpurchase>(this.docpaymentpurchaseitems, listhelper); }
        }

        /*****************************/
        public void addSource(docpaymentreserv helper)
        {
            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0) { this.docpaymentreservitems = IEnumerableUtils.AddSingle<docpaymentreserv>(helper); }
            else { IEnumerableUtils.AddSingle<docpaymentreserv>(this.docpaymentreservitems, helper); }
        }
        public void addSource(List<docpaymentreserv> listhelper)
        {

            if (this.docpaymentreservitems == null || this.docpaymentreservitems.Count() == 0) { this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(listhelper); }
            else { this.docpaymentreservitems = IEnumerableUtils.AddList<docpaymentreserv>(this.docpaymentreservitems, listhelper); }

        }

        public void addSource(List<docpaymentparentreserv> listhelper)
        {

            if (this.docpaymentparentreservitems == null || this.docpaymentparentreservitems.Count() == 0) { this.docpaymentparentreservitems = IEnumerableUtils.AddList<docpaymentparentreserv>(listhelper); }
            else { this.docpaymentparentreservitems = IEnumerableUtils.AddList<docpaymentparentreserv>(this.docpaymentparentreservitems, listhelper); }

        }

        /******************************************************************************************************/
        public void addSource(bankreconciliationModel helper)
        {
            if (this.bankreconciliationitems == null || this.bankreconciliationitems.Count() == 0) { this.bankreconciliationitems = (List<bankreconciliationModel>)IListUtils.AddSingle<bankreconciliationModel>(helper); }
            else { IListUtils.AddSingle<bankreconciliationModel>(this.bankreconciliationitems, helper); }
        }

        public void addSource(List<bankreconciliationModel> listhelper)
        {
            if (this.bankreconciliationitems == null || this.bankreconciliationitems.Count() == 0) { this.bankreconciliationitems = (List<bankreconciliationModel>)IListUtils.AddList<bankreconciliationModel>(listhelper); }
            else { this.bankreconciliationitems = (List<bankreconciliationModel>)IListUtils.AddList<bankreconciliationModel>(this.bankreconciliationitems, listhelper); }
        }

        public void addSource(bankstatements helper)
        {
            if (this.bankstatementsitems == null || this.bankstatementsitems.Count() == 0) { this.bankstatementsitems = (List<bankstatements>)IListUtils.AddSingle<bankstatements>(helper); }
            else { IListUtils.AddSingle<bankstatements>(this.bankstatementsitems, helper); }
        }

        public void addSource(List<bankstatements> listhelper)
        {
            if (this.bankstatementsitems == null || this.bankstatementsitems.Count() == 0) { this.bankstatementsitems = (List<bankstatements>)IListUtils.AddList<bankstatements>(listhelper); }
            else { this.bankstatementsitems = (List<bankstatements>)IListUtils.AddList<bankstatements>(this.bankstatementsitems, listhelper); }
        }
        /******************************************************************************************************/
        public void addSource(incomepayment helper)
        {
            if (this.incomemovements == null || this.incomemovements.Count() == 0) { this.incomemovements = (List<incomepayment>)IListUtils.AddSingle<incomepayment>(helper); }
            else { IListUtils.AddSingle<incomepayment>(this.incomemovements, helper); }
        }

        public void addSource(List<incomepayment> listhelper)
        {
            if (this.incomemovements == null || this.incomemovements.Count() == 0) { this.incomemovements = (List<incomepayment>)IListUtils.AddList<incomepayment>(listhelper); }
            else { this.incomemovements = (List<incomepayment>)IListUtils.AddList<incomepayment>(this.incomemovements, listhelper); }
        }

        #endregion

        #region addReporItem
        private void addReporItem(invoicepayment helper)
        {
            financialstateitemModel _object = new financialstateitemModel(2, helper.aplicationDate, helper.chargedAmount * -1, 0, "", "", 4, "Pagos", helper.Invoice, helper.Payment, string.Concat(helper.InvoiceIdentifier, " ", helper.description));
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<invoicepayment> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(fondoModel helper)
        {
            financialstateitemModel _object = null;

            if (helper.MontoCargo < 0)
            {
                // _object = new financialstateitemModel("*** Movimiento de salida, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***", helper.fechaEntrega, helper.MontoCargo);
                _object = new financialstateitemModel(2, helper.fechaEntrega, helper.MontoCargo, 0m, "Transferencia", helper.CompanyFinancialName + "-" + helper.FinancialMethodName, 5, helper.FinanceType != null ? "Financiamiento" : "Fondos", helper.Invoice == null ? 0 : (int)helper.Invoice, helper.id, helper.FinanceType != null ? "*** Financiamiento de cuenta origen" + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***" : "*** Movimiento de salida, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***");
                _object.generateString();
            }
            else
            {
                // _object = new financialstateitemModel("*** Movimiento de entrada, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***", helper.fechaEntrega, helper.MontoCargo);
                _object = new financialstateitemModel(1, helper.fechaEntrega, helper.MontoCargo, 0m, "Transferencia", helper.CompanyFinancialName + "-" + helper.FinancialMethodName, 5, helper.FinanceType != null ? "Financiamiento" : "Fondos", helper.Invoice == null ? 0 : (int)helper.Invoice, helper.id, helper.FinanceType != null ? "*** Financiamiento de cuenta origen" + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***" : "*** Movimiento de entrada, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***");
                _object.generateString();
            }

            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }

        }

        private void addReporItem(List<fondoModel> helper)
        {

            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(docpaymentpurchase helper)
        {
            // financialstateitemModel _object = new financialstateitemModel(string.Concat(" UPSCL pago " + helper.PaymentMethodName + " , ", helper.authRef , " - Reserva : ", helper.Reserva == 0 ? " " : helper.Reserva.ToString(), " - Upscale ", helper.Invoice.ToString() ), helper.aplicationDate, helper.chargedAmount); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitemModel _object = new financialstateitemModel(1, helper.paymentDate, helper.paymentAmount, 0m, helper.PaymentMethodName, helper.HotelName, 8, "Purchase", helper.Purchase, helper.Payment, string.Concat(" Purchase pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Membresia : ", helper.Memberships == 0 ? " " : helper.Memberships.ToString(), " - Purchase ", helper.Purchase.ToString(), helper.Hotel != 0 ? "- Partner " + helper.HotelName : ""), helper.bankStatementLinked, 0);    // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<docpaymentpurchase> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(docpaymentreserv helper)
        {
            //financialstateitemModel _object = new financialstateitemModel(string.Concat("Reservación pago "+ helper.PaymentMethodName +" , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.reservationPaymentDate, helper.reservationPaymentQuantity); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitemModel _object = new financialstateitemModel(1, helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, helper.HotelName, 7, "Reservation", helper.Reservation, helper.ReservationPayment, string.Concat("Reservación pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.bankStatementLinked, (int)BankstateRsvType.RsvMember);   // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(docpaymentparentreserv helper)
        {
            //financialstateitemModel _object = new financialstateitemModel(string.Concat("Reservación pago "+ helper.PaymentMethodName +" , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.reservationPaymentDate, helper.reservationPaymentQuantity); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitemModel _object = new financialstateitemModel(1, helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, helper.HotelName, 7, "Reservation without membership", helper.Reservation, helper.ReservationPayment, string.Concat("Reservación pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.bankStatementLinked, (int)BankstateRsvType.RsvParent);   // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<docpaymentreserv> helper)
        {
            helper = helper.GroupBy(i => i.ReservationPayment).Select(g => g.First()).ToList();
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(List<docpaymentparentreserv> helper)
        {
            helper = helper.GroupBy(i => i.ReservationPayment).Select(g => g.First()).ToList();
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(incomepayment helper)
        {
            financialstateitemModel _object = new financialstateitemModel(helper.aplicationdate, helper.ammounttotal, 0, helper.bankaccnttypename, "", 2, "Movimiento Igreso", helper.parent, helper.item, string.Concat(helper.identifier, " ", helper.description));
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<incomepayment> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }
        /****************************************** Converting BankReconciliation To FinancialStateItem ***************************************************/
        private void addReporItem(bankreconciliationModel helper)
        {
            financialstateitemModel _object = new financialstateitemModel(1, helper.bankstatementAplicationDate, (decimal)helper.bankstatementAppliedAmmountFinal, 0m, "Conciliación", helper.companyname, "Conciliaciones", helper.idBankStatements, string.Format("Conciliación bancaria ScotiaPos, fecha aplicación {0}, origen: {1}", helper.bankstatementaplicationdatestring, helper.companyname));

            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<bankreconciliationModel> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItemBancosComision(List<bankreconciliationModel> helper)
        {
            bankreconciliationModel model = new bankreconciliationModel();

            var fechaConciliation = helper.Select(x => x.bankstatementAplicationDate).LastOrDefault();
            var totalComisiones = helper.Sum(x => Math.Abs((decimal)x.bankstatementBankFee));

            model.bankstatementaplicationdatestring = DateTimeUtils.ParseDatetoString(fechaConciliation);
            model.bankstatementAplicationDate = fechaConciliation;
            model.bankstatementAppliedAmmount = totalComisiones * -1;
            model.bankstatementAppliedAmmountFinal = totalComisiones * -1;
            model.companyname = CompanyBaccountName;
            addReporItemBancosComision(model);
        }

        private void addReporItemBancosComision(bankreconciliationModel helper)
        {
            financialstateitemModel _object = new financialstateitemModel(2, helper.bankstatementAplicationDate, (decimal)helper.bankstatementAppliedAmmountFinal, 0m, "Conciliación", helper.companyname, "Conciliaciones", helper.idBankStatements, string.Format("Comisiones bancaria CTA origen: {0} , fecha aplicación {1}", helper.companyname, helper.bankstatementaplicationdatestring));

            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItemBancos(bankreconciliationModel helper)
        {
            financialstateitemModel _object = new financialstateitemModel(2, helper.bankstatementAplicationDate, (decimal)helper.bankstatementAppliedAmmountFinal, 0m, "Conciliación", helper.companyname, "Conciliaciones", helper.idBankStatements, string.Format("Deposito bancario CTA origen: {0} , fecha aplicación {1}", CompanyBaccountName, helper.bankstatementaplicationdatestring));

            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItemBancos(List<bankreconciliationModel> helper)
        {
            helper.ForEach(y => this.addReporItemBancos(y));
        }

        private void addReporItemBancos(List<bankstatements> helper)
        {
            helper.ForEach(y => this.addReporItemBancos(y));
        }

        private void addReporItemBancos(bankstatements helper)
        {
            financialstateitemModel _object = null;
            if (helper.bankstatementsAbono > 0)
            {//Entrada
                _object = new financialstateitemModel(1, helper.bankstatementsAplicationDate, (decimal)helper.bankstatementsAbono, 0m, "Movimientos banco", "Movimientos banco", helper.idBankStatements2, string.Format("Movimiento entrada banco CTA origen: {0} , por {1}", CompanyBaccountName, helper.bankstatementsConcept));
                _object.generateString();
            }
            else
            {//Salida
                _object = new financialstateitemModel(2, helper.bankstatementsAplicationDate, (decimal)helper.bankstatementsCargo, 0m, "Movimientos banco", "Movimientos banco", helper.idBankStatements2, string.Format("Movimiento salida banco  CTA origen: {0} , por {1}", CompanyBaccountName, helper.bankstatementsConcept));
                _object.generateString();
            }

            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitemModel>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitemModel>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        /***************************************************************************************************************************************************/

        #endregion

        #region Generating & Calculating Balance
        /************ CALCULATING *****************/
        public void generateTimeLineBalance()
        {
            try
            {
                this.addReporItem(this.docpaymtitems.ToList());
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem los pagos -----> ", e.InnerException);
            }

            try
            {
                this.addReporItem(this.fondoitems.ToList());
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem los fondos", e.InnerException);
            }

            try
            {
                this.addReporItem(this.docpaymentpurchaseitems.ToList());
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem los upscales", e.InnerException);
            }

            try
            {
                this.addReporItem(this.docpaymentreservitems.ToList());
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem los reservations", e.InnerException);
            }
            /**************************************************************************************************************/
            try
            {
                this.addReporItem(this.bankreconciliationitems);
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem las conciliaciones", e.InnerException);
            }
            /**************************************************************************************************************/
            try
            {
                this.addReporItem(this.incomemovements.ToList());
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem los incomemovements", e.InnerException);
            }

            decimal _balancetemp = this.balanceBefore;

            if (this.financialstateitemlist != null || this.financialstateitemlist.Count() != 0)
            {
                var financialstateitem = RemoveDuplicate(this.financialstateitemlist.ToList());
                financialstateitemModel[] _itemsTemp = financialstateitem.ToArray();

                if (_itemsTemp.Count() != 0)
                {
                    for (int i = 0; i <= _itemsTemp.Count() - 1; i++)
                    {
                        _itemsTemp[i].balance = _balancetemp + _itemsTemp[i].appliedAmmount;
                        _itemsTemp[i].balanceString = MoneyUtils.ParseDecimalToString((decimal)_itemsTemp[i].balance);
                        _balancetemp = (decimal)_itemsTemp[i].balance;
                    }
                }
                this.financialstateitemlist = _itemsTemp;
            }
        }

        public void generateTimeLineBalance(params int[] exceptionSourcedata)
        {

            var _sourcedataObject = this.sourcedataitems.Select(c => c.SourceData).ToArray();

            for (int i = 0; i <= _sourcedataObject.Count() - 1; i++)
            {

                switch (_sourcedataObject[i])
                {
                    case 1: //Ingresos
                        {

                        }
                        break;
                    case 2: //Ingresos Movimientos
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.incomemovements.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los incomemovements", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 3: //Facturas
                        {

                        }
                        break;
                    case 4: //Pagos
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymtitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los pagos -----> ", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 5: //Fondos
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.fondoitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los fondos", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 6: //Conciliaciones
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.bankreconciliationitems);
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem las conciliaciones", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 7://Reservas
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymentreservitems.ToList());

                                    this.addReporItem(this.docpaymentparentreservitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los reservations", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 8://Pagos Membership - Purchases
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymentpurchaseitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los upscales", e.InnerException);
                                }
                            }
                        }
                        break;
                }
            }

            decimal _balancetemp = this.balanceBefore;

            if (this.financialstateitemlist != null || this.financialstateitemlist.Count() != 0)
            {
                var financialstateitem = RemoveDuplicate(this.financialstateitemlist.ToList());
                //this.financialstateitemlist = financialstateitem;
                financialstateitemModel[] _itemsTemp = this.financialstateitemlist.ToArray();

                if (_itemsTemp.Count() != 0)
                {
                    for (int i = 0; i <= _itemsTemp.Count() - 1; i++)
                    {
                        _itemsTemp[i].balance = _balancetemp + _itemsTemp[i].appliedAmmount;
                        _itemsTemp[i].balanceString = MoneyUtils.ParseDecimalToString((decimal)_itemsTemp[i].balance);
                        _balancetemp = (decimal)_itemsTemp[i].balance;
                    }
                }
                this.financialstateitemlist = _itemsTemp;
            }
        }

        public void generateTimeLineBalanceBancos(params int[] exceptionSourcedata)
        {

            var _sourcedataObject = this.sourcedataitems.Select(c => c.SourceData).ToArray();

            for (int i = 0; i <= _sourcedataObject.Count() - 1; i++)
            {

                switch (_sourcedataObject[i])
                {
                    case 1: //Ingresos
                        {

                        }
                        break;
                    case 2: //Ingresos Movimientos
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.incomemovements.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los incomemovements", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 3: //Facturas
                        {

                        }
                        break;
                    case 4: //Pagos
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymtitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los pagos -----> ", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 5: //Fondos
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.fondoitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los fondos", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 6: //Conciliaciones
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.bankreconciliationitems);
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem las conciliaciones", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 7://Reservas
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymentreservitems.ToList());

                                    this.addReporItem(this.docpaymentparentreservitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los reservations", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 8://Pagos Membership - Purchases
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymentpurchaseitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los upscales", e.InnerException);
                                }
                            }
                        }
                        break;
                }
            }

            decimal _balancetemp = this.balanceBefore;

            if (this.financialstateitemlist != null || this.financialstateitemlist.Count() != 0)
            {
                var financialstateitem = RemoveDuplicate(this.financialstateitemlist.ToList());
                //this.financialstateitemlist = financialstateitem;
                financialstateitemModel[] _itemsTemp = this.financialstateitemlist.ToArray();

                if (_itemsTemp.Count() != 0)
                {
                    for (int i = 0; i <= _itemsTemp.Count() - 1; i++)
                    {
                        _itemsTemp[i].balance = _balancetemp + _itemsTemp[i].appliedAmmount;
                        _itemsTemp[i].balanceString = MoneyUtils.ParseDecimalToString((decimal)_itemsTemp[i].balance);
                        _balancetemp = (decimal)_itemsTemp[i].balance;
                    }
                }
                this.financialstateitemlist = _itemsTemp;
            }
        }

        public void generateTimeLineBalanceBancos()
        {
            try
            {
                this.addReporItemBancos(this.bankreconciliationitems);
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem las conciliaciones", e.InnerException);
            }

            try
            {
                this.addReporItemBancos(this.bankstatementsitems);
            }
            catch (Exception e)
            {
                throw new Exception("No se pueden agregar a la lista FinancialStateItem las conciliaciones", e.InnerException);
            }

            //try
            //{
            //    this.addReporItemBancosComision(this.bankreconciliationitems);
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("No se pueden agregar a la lista FinancialStateItem las conciliaciones", e.InnerException);
            //}

            decimal _balancetemp = this.balanceBefore;

            if (this.financialstateitemlist != null || this.financialstateitemlist.Count() != 0)
            {
                var financialstateitem = RemoveDuplicate(this.financialstateitemlist.ToList());
                //this.financialstateitemlist = financialstateitem;
                financialstateitemModel[] _itemsTemp = this.financialstateitemlist.ToArray();

                if (_itemsTemp.Count() != 0)
                {
                    for (int i = 0; i <= _itemsTemp.Count() - 1; i++)
                    {
                        _itemsTemp[i].balance = _balancetemp + _itemsTemp[i].appliedAmmount;
                        _itemsTemp[i].balanceString = MoneyUtils.ParseDecimalToString((decimal)_itemsTemp[i].balance);
                        _balancetemp = (decimal)_itemsTemp[i].balance;
                    }
                }
                this.financialstateitemlist = _itemsTemp;
            }
        }

        private void calculateMontoCargosCajas()
        {
            var _docpayments = this.docpaymtitems != null ? this.docpaymtitems.Sum(y => y.chargedAmount) : 0;
            var _totalFondos = this.fondoitems != null ? this.fondoitems.Where(x => x.MontoCargo < 0).Sum(y => y.MontoCargo) : 0;
            //var _bankstatementBankFee = this.bankreconciliationitems != null ? this.bankreconciliationitems.Sum(y => Math.Abs((decimal)y.bankstatementBankFee)) : 0;

            this.MontoCargo = _docpayments + Math.Abs((decimal)_totalFondos) /*+ _bankstatementBankFee */;
        }


        private void calculateMontoAbonosCajas()
        {
            var _totalFondos = this.fondoitems != null ? this.fondoitems.Where(x => x.MontoCargo > 0).Sum(y => y.MontoCargo) : 0;
            var _totalPaymentsPurchase = this.docpaymentpurchaseitems != null ? this.docpaymentpurchaseitems.Sum(y => y.paymentAmount) : 0;
            var _totalPaymentsReserv = this.docpaymentreservitems != null ? this.docpaymentreservitems.Sum(y => y.reservationPaymentQuantity) : 0;
            //var _totalBankReconciliations = this.bankreconciliationitems != null ? this.bankreconciliationitems.Sum(y => y.bankstatementAppliedAmmountFinal) : 0;
            var _totalIncomeMovements = this.incomemovements != null ? this.incomemovements.Sum(y => y.ammounttotal) : 0;
            // this.MontoCargo = this.fondoitems != null  ? this.fondoitems.Sum(y => y.MontoCargo) : 0
            // this.MontoCargo = this.fondoitems != null  ? this.fondoitems.Sum(y => y.MontoCargo) : 0
            this.MontoAbonos = _totalFondos + _totalPaymentsPurchase + _totalPaymentsReserv + _totalIncomeMovements;
            //this.MontoCargo = this.financialstateitemlist.Where(v => v.accounttype == 1).Sum(c=> c.appliedAmmount);
        }

        private void calculateMontoCargos()
        {
            var _docpayments = this.docpaymtitems != null ? this.docpaymtitems.Sum(y => y.chargedAmount) : 0;
            var _totalFondos = this.fondoitems != null ? this.fondoitems.Where(x => x.MontoCargo < 0).Sum(y => y.MontoCargo) : 0;

            this.MontoCargo = _docpayments + Math.Abs((decimal)_totalFondos);
        }


        private void calculateMontoAbonos()
        {
            var _totalFondos = this.fondoitems != null ? this.fondoitems.Where(x => x.MontoCargo > 0).Sum(y => y.MontoCargo) : 0;
            var _totalPaymentsPurchase = this.docpaymentpurchaseitems != null ? this.docpaymentpurchaseitems.Sum(y => y.paymentAmount) : 0;
            var _totalPaymentsReserv = this.docpaymentreservitems != null ? this.docpaymentreservitems.Sum(y => y.reservationPaymentQuantity) : 0;
            var _totalPaymentsReservParent = this.docpaymentparentreservitems != null ? this.docpaymentparentreservitems.Sum(y => y.reservationPaymentQuantity) : 0;
            //var _totalBankReconciliations = this.bankreconciliationitems != null ? this.bankreconciliationitems.Sum(y => y.bankstatementAppliedAmmountFinal) : 0;
            var _totalIncomeMovements = this.incomemovements != null ? this.incomemovements.Sum(y => y.ammounttotal) : 0;
            // this.MontoCargo = this.fondoitems != null  ? this.fondoitems.Sum(y => y.MontoCargo) : 0
            // this.MontoCargo = this.fondoitems != null  ? this.fondoitems.Sum(y => y.MontoCargo) : 0
            this.MontoAbonos = _totalFondos + _totalPaymentsPurchase + _totalPaymentsReserv + _totalPaymentsReservParent/*+ (decimal)_totalBankReconciliations*/ + _totalIncomeMovements;
            //this.MontoCargo = this.financialstateitemlist.Where(v => v.accounttype == 1).Sum(c=> c.appliedAmmount);
        }

        private void calculateBalance()
        {
            this.balance = this.MontoAbonos - this.MontoCargo;
        }

        private void calculateMontoCargosSaldoBancos()
        {
            var _totalBankFee = this.bankreconciliationitems != null ? this.bankreconciliationitems.Sum(y => y.bankstatementBankFee) : 0;
            var _totalBankStatements = this.bankstatementsitems != null ? this.bankstatementsitems.Sum(y => y.bankstatementsCargo) : 0;

            this.MontoCargo = Math.Abs((decimal)_totalBankFee) + Math.Abs((decimal)_totalBankStatements);
        }

        private void calculateMontoAbonosSaldoBancos()
        {
            var _totalBankReconciliations = this.bankreconciliationitems != null ? this.bankreconciliationitems.Sum(y => y.bankstatementAppliedAmmount) : 0;
            var _totalBankStatements = this.bankstatementsitems != null ? this.bankstatementsitems.Sum(y => y.bankstatementsAbono) : 0;

            this.MontoAbonos = _totalBankReconciliations + (decimal)_totalBankStatements;
        }

        private void calculateBalanceBancos()
        {
            this.balance = this.MontoAbonos - this.MontoCargo;
        }

        public void calculateMaxBalanceWithLimit(decimal limitBudget)
        {
            var _temp = (this.MontoCargo - this.MontoAbonos);
            this.maxBalance = (decimal)_temp <= 0 ? limitBudget : limitBudget - (decimal)_temp;
        }

        public void calculateBalanceWithBalceBeforeBancos()
        {
            this.balance = (this.MontoAbonos - this.MontoCargo) + this.balanceBefore;
        }

        public void calculateBalanceWithBalceBefore()
        {
            this.balance = (this.MontoAbonos - this.MontoCargo) + this.balanceBefore;
        }

        private void parseAmmountsToString()
        {
            this.MontoCargoString = MoneyUtils.ParseDecimalToString((decimal)this.MontoCargo);
            this.MontoAbonosString = MoneyUtils.ParseDecimalToString((decimal)this.MontoAbonos);
            this.balanceString = MoneyUtils.ParseDecimalToString((decimal)this.balance);
            this.maxBalanceString = MoneyUtils.ParseDecimalToString((decimal)this.maxBalance);
            this.balanceBeforeString = MoneyUtils.ParseDecimalToString((decimal)this.balanceBefore);
        }
        #endregion

        #region Keep Data

        private void ClearSourceData()
        {

            // Initializing List
            this.docitemsitems = new List<invoiceitems>();
            this.docpaymtitems = new List<invoicepayment>();
            this.fondoitems = new List<fondoModel>();
            this.docpaymentpurchaseitems = new List<docpaymentpurchase>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.sourcedataitems = new List<bankaccountsourcedataModel>();
            this.financialstateitemlist = new List<financialstateitemModel>();
            this.HotelPartner = new List<int>();
        }

        private void ClearFinancialHistory()
        {

        }

        private List<financialstateitemModel> RemoveDuplicate(List<financialstateitemModel> listitem)
        {
            List<financialstateitemModel> financialitem = new List<financialstateitemModel>();

            //financialitem = listitem.GroupBy(i => i.Reference).Select(g => g.First()).ToList();
            // ciclo para descartar los duplicados
            for (int i = 0; i < listitem.Count; i++)
            {
                bool duplicate = false;
                for (int z = 0; z < i; z++)
                {
                    if (listitem[z].Reference == listitem[i].Reference && listitem[z].ReferenceItem == listitem[i].ReferenceItem && listitem[z].SourceData == listitem[i].SourceData)
                    {
                        // si es duplicado se rompe el ciclo for.
                        duplicate = true;
                        break;
                    }
                }// si no se encuantra duplicado se agrega a la lista
                if (!duplicate)
                {
                    financialitem.Add(listitem[i]);
                }
            }

            return financialitem;
        }

        /*******************************
        public void generateReporItem()
        {
            financialstateitemModel[] items = new financialstateitemModel[this.fondoitems.Count() + this.docpaymtitems.Count()];

            int count = 0;
                foreach (invoicepayment helper in this.docpaymtitems)
                {
                this.addReporItem(helper);

                /* financialstateitemModel _object = new financialstateitemModel(helper.description, helper.aplicationDate, helper.chargedAmount * -1);
                items[count] = _object; 
                count++;
                }
                foreach (fondoModel helper in fondoModel)
                {
                if (helper.MontoCargo < 0)
                {
                    financialstateitemModel _object = new financialstateitemModel("*** Movimiento de salida, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName  + " ***", helper.fechaEntrega, helper.MontoCargo);
                    items[count] = _object;
                }
                else
                {
                    financialstateitemModel _object = new financialstateitemModel("*** Movimiento de entrada, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***", helper.fechaEntrega, helper.MontoCargo);
                    items[count] = _object;
                }
                
                count++;
                }

            foreach (fondoModel helper in this.fondoitems)
            {
                this.addReporItem(helper);
                count++;
            }



                // } 

                this.financialstateitemlist = items.OrderBy(y=>y.aplicationDate).AsEnumerable();
        }
        */
        #endregion

        #region Getting Data
        #region Fondos

        // Money IN
        private IEnumerable<tblfondos> getAnualBudgetsTo(DateTime start, DateTime end, int idpaymentMethod)
        {
            List<tblfondos> fondos = unity.FondosRepository.Get(t => t.tblbankaccount.idbaccount == idpaymentMethod && t.fondofechaEntrega >= start && t.fondofechaEntrega <= end, null, "tblbankaccount.tblcurrencies,tblbankaccount.tblcompanies,tblbankaccount").ToList();
            return fondos;
        }

        private IEnumerable<tblfondos> getAnualBudgetsTo(DateTime start, DateTime end, int idpaymentMethod, decimal ammountStart, decimal ammountEnd)
        {
            List<tblfondos> fondos = new List<tblfondos>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblfondos.Include(v => v.tblbankaccount.tblcurrencies).Include(v => v.tblbankaccount.tblcompanies).Include(v => v.tblbankaccount).Include(v => v.tblbankaccount.tblbank).Include(v => v.tblbankaccount1.tblcurrencies).Include(v => v.tblbankaccount1.tblcompanies).Include(v => v.tblbankaccount1).Include(v => v.tblbankaccount1.tblbank);

                query = query.Where(c => c.tblbankaccount.idbaccount == idpaymentMethod).Where(c => c.fondofechaEntrega >= start).Where(c => c.fondofechaEntrega <= end);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.fondoMonto >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.fondoMonto <= ammountEnd);
                }

                fondos = query.ToList();
            }

            return fondos;
        }

        // Money OUT
        private IEnumerable<tblfondos> getAnualBudgetsFrom(DateTime start, DateTime end, int idpaymentMethod)
        {
            List<tblfondos> fondos = unity.FondosRepository.Get(t => t.tblbankaccount1.idbaccount == idpaymentMethod && t.fondofechaEntrega >= start && t.fondofechaEntrega <= end, null, "tblbankaccount.tblcurrencies,tblbankaccount.tblcompanies,tblbankaccount").ToList();
            foreach (tblfondos model in fondos)
            {
                model.fondoMonto = model.fondoMonto * -1;
            }

            return fondos;
        }

        private IEnumerable<tblfondos> getAnualBudgetsFrom(DateTime start, DateTime end, int idpaymentMethod, decimal ammountStart, decimal ammountEnd)
        {
            List<tblfondos> fondos = new List<tblfondos>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblfondos.Include(v => v.tblbankaccount1.tblcurrencies).Include(v => v.tblbankaccount1.tblcompanies).Include(v => v.tblbankaccount1).Include(v => v.tblbankaccount1.tblbank).Include(v => v.tblbankaccount.tblcurrencies).Include(v => v.tblbankaccount.tblcompanies).Include(v => v.tblbankaccount).Include(v => v.tblbankaccount.tblbank);

                query = query.Where(c => c.tblbankaccount1.idbaccount == idpaymentMethod).Where(c => c.fondofechaEntrega >= start).Where(c => c.fondofechaEntrega <= end);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.fondoMonto >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.fondoMonto <= ammountEnd);
                }

                fondos = query.ToList();

                foreach (tblfondos model in fondos)
                {
                    model.fondoMonto = model.fondoMonto * -1;
                }
            }

            return fondos;
        }
        #endregion

        #region Pagos
        /*****/
        private IEnumerable<tblpayment> getAnualPayments(DateTime start, DateTime end, int idBankAccount)
        {
            List<tblpayment> payments = unity.PaymentsVtaRepository.Get(t => t.tblbankaccount.idbaccount == idBankAccount && t.tblinvoice.invoicedate >= start && t.tblinvoice.invoicedate <= end, null, "tblbankaccount.tblcurrencies,tblbankaccount.tblcompanies,tblbankaccount").ToList();
            return payments;
        }

        private IEnumerable<tblpayment> getAnualPayments(DateTime start, DateTime end, int idBankAccount, decimal ammountStart, decimal ammountEnd)
        {
            List<tblpayment> payments = new List<tblpayment>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblpayment.Include(c => c.tblbankaccount.tblcurrencies).Include(c => c.tblbankaccount.tblcompanies).Include(c => c.tblbankaccount).Include(c => c.tblinvoice.tblinvoiceditem).Include(c => c.tblinvoice.tblcompanies).Include(c => c.tblinvoice.tblusers.tblprofilesaccounts);

                query = query.Where(c => c.tblbankaccount.idbaccount == idBankAccount).Where(c => c.paymentdate >= start).Where(c => c.paymentdate <= end);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.paymentamount >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.paymentamount <= ammountEnd);
                }

                payments = query.ToList();
            }
            return payments;
        }

        #endregion

        #region  Facturas  - Not Useful

        #endregion

        #region  Facturas

        private IEnumerable<tblinvoiceditem> getAnualDocitems(DateTime start, DateTime end, int idcompany)
        {
            List<tblinvoiceditem> invoicesitems = unity.PaymentsVtaRepository.Get(t => t.tblinvoice.idcompany == idcompany && t.tblinvoice.invoicedate >= start && t.tblinvoice.invoicedate <= end, null, "tblbankaccount.tblcurrencies,tblbankaccount.tblcompanies,tblbankaccount,tblinvoice.tblinvoiceditem").SelectMany(y => y.tblinvoice.tblinvoiceditem).ToList();
            return invoicesitems;
        }
        #endregion

        #region  Upscales

        /************ Purchase */
        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchase(DateTime start, DateTime end, int idCurrency, int idPaymentMethod, int[] HotelPartner)
        {
            List<tblpaymentspurchases> paymentsPurchase = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate >= start && t.paymentDate <= end && t.idCurrency == idCurrency && t.idPaymentType == idPaymentMethod /*&& idHotelVTH.Contains(t.tblpurchases.idPartner)*/, null, "tblcurrencies,tblpurchases,tblpurchases.tblpartners,tblpurchases.tblmemberships,tblpurchases.tblmemberships.tblmembers").ToList();
            return paymentsPurchase;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchase(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethod, int[] HotelPartner)
        {
            List<tblpaymentspurchases> paymentsPurchase = new List<tblpaymentspurchases>();
            using (var db = new vtclubdbEntities())
            {  //var query = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate >= start && t.paymentDate <= end && t.idCurrency == idCurrency && idPaymentMethod.Contains((int)t.idPaymentType) /*&& HotelPartner.Contains(t.tblpurchases.idPartner)*/, null, "tblcurrencies,tblpurchases,tblpurchases.tblpartners,tblpurchases.tblmemberships,tblpurchases.tblmemberships.tblmembers").ToList();
                //((IObjectContextAdapter)db.Configuration).ObjectContext.CommandTimeout = 180;
                var query = db.tblpaymentspurchases.Include(c => c.tblpurchases).Include(c => c.tblcurrencies).Include(c => c.tblpurchases.tblpartners).Include(v => v.tblpurchases.tblmemberships);

                if (start != null)
                {
                    query = query.Where(c => c.paymentDate >= start);
                }
                if (end != null)
                {
                    query = query.Where(c => c.paymentDate <= end);
                }
                if (idCurrency != 0)
                {
                    query = query.Where(c => c.idCurrency == idCurrency);
                }
                if (idPaymentMethod.Count() != 0)
                {
                    query = query.Where(c => idPaymentMethod.Contains((int)c.idPaymentType));
                }

                paymentsPurchase = query.ToList();
            }
            return paymentsPurchase;
        }

        private IEnumerable<tblpurchases> getAnualPaymentsPurchaseNew(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethod, int[] HotelPartner)
        {
            List<tblpurchases> Purchase = new List<tblpurchases>();
            if (idCurrency == (int)Currencies.US_Dollar && idPaymentMethod.Contains((int)PaymentMethods_Bank_Report.Transfer) && idBanAccount == (int)BAccountDefault.BAccount_USD_BOA)
            {
                Purchase = unity.PurchasesRepository.Get(t => t.purchaseDate >= start && t.purchaseDate <= end && t.saleType == (int)PurchaseSaleType.New /*&& HotelPartner.Contains(t.tblpurchases.idPartner)*/, null, "tblpartners,tblmemberships").ToList();
            }
            return Purchase;
        }

        private IEnumerable<tblpurchases> getAnualPaymentsPurchaseUpgrade(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethod, int[] HotelPartner)
        {
            List<tblpurchases> Purchase = new List<tblpurchases>();
            if (idCurrency == (int)Currencies.US_Dollar && idPaymentMethod.Contains((int)PaymentMethods_Bank_Report.Transfer) && idBanAccount == (int)BAccountDefault.BAccount_USD_BOA)
            {
                Purchase = unity.PurchasesRepository.Get(t => t.purchaseDate >= start && t.purchaseDate <= end && t.saleType == (int)PurchaseSaleType.Upgrade /*&& HotelPartner.Contains(t.tblpurchases.idPartner)*/, null, "tblpartners,tblmemberships").ToList();
            }
            return Purchase;
        }

        private IEnumerable<tblbatchdetail> getAnualPaymentPurchaseBatchDetail(DateTime start, DateTime end, int[] idPaymentMethod, int baccount)
        {
            List<tblbatchdetail> purchaseBatch = new List<tblbatchdetail>();

            purchaseBatch = unity.BatchDetailRepository.Get(b => b.tblbatch.datePay >= start && b.tblbatch.datePay <= end && idPaymentMethod.Contains((int)b.tblbatch.idPaymentForm) && b.tblbatch.idbaccount == baccount, null, "tblbatch,tblpurchases,tblpurchases.tblmemberships,tblpurchases.tblpartners").ToList();

            return purchaseBatch;
        }

        private IEnumerable<tblbatchdetailpre> getAnualPaymentPurchaseBatchDetailPre(DateTime start, DateTime end, int[] idPaymentMethod, int baccount)
        {
            List<tblbatchdetailpre> purchaseBatch = new List<tblbatchdetailpre>();

            purchaseBatch = unity.BatchDetailPreRepository.Get(b => b.tblbatch.datePay >= start && b.tblbatch.datePay <= end && idPaymentMethod.Contains((int)b.tblbatch.idPaymentForm) && b.tblbatch.idbaccount == baccount, null, "tblbatch,tblpartners,tblprefixes").ToList();

            return purchaseBatch;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchase(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethod, /*int[] idHotelVTH,*/ decimal ammountStart, decimal ammountEnd)
        {
            List<tblpaymentspurchases> paymentsPurchase = new List<tblpaymentspurchases>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblpaymentspurchases.Include(c => c.tblpurchases).Include(c => c.tblcurrencies).Include(c => c.tblpurchases.tblpartners).Include(v => v.tblpurchases.tblmemberships);

                query = query.Where(c => c.paymentDate >= start && c.paymentDate <= end && c.idCurrency == idCurrency && idPaymentMethod.Contains((int)c.idPaymentType) /*&& idHotelVTH.Contains(c.tblpurchases.idPartner)*/);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.paymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.paymentCost <= ammountEnd);
                }

                paymentsPurchase = query.ToList();
            }
            return paymentsPurchase;
        }


        // Getting by Terminals
        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchaseTPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<tblpaymentspurchases> paymentsPurchase = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate >= start && t.paymentDate <= end && t.idCurrency == idCurrency && t.terminal == idTpv, null, "tblcurrencies,tblpurchases,tblpurchases.tblpartners").ToList();
            return paymentsPurchase;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchaseTPV(DateTime start, DateTime end, int idCurrency, int idTpv, decimal? ammountStart, decimal ammountEnd/*, int[] baccounthotels*/)
        {
            List<tblpaymentspurchases> paymentsPurchase = new List<tblpaymentspurchases>();

            using (var db = new vtclubdbEntities())
            {

                var query = db.tblpaymentspurchases.Include(c => c.tblpurchases).Include(c => c.tblcurrencies).Include(c => c.tblpurchases.tblpartners);

                query = query.Where(c => c.paymentDate >= start && c.paymentDate <= end && c.idCurrency == idCurrency && c.terminal == idTpv);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.paymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.paymentCost <= ammountEnd);
                }

                /*if (baccounthotels.Count() != 0)
                {
                    query = query.Where(c => baccounthotels.Contains(c.tblpurchases.idPartner));
                }*/

                paymentsPurchase = query.ToList();
            }


            return paymentsPurchase;
        }

        // Getting by BankAccount-Hotels
        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchaseBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethod/*, int[] baccounthotels*/)
        {
            List<tblpaymentspurchases> paymentsPurchase = unity.PaymentsPurchasesRepository.Get(t => t.paymentDate >= start && t.paymentDate <= end && t.idCurrency == idCurrency && idPaymentMethod.Contains((int)t.idPaymentType) /*&& baccounthotels.Contains(t.tblpurchases.idPartner)*/, null, "tblcurrencies,tblpurchases").ToList();
            return paymentsPurchase;
        }

        private IEnumerable<tblpaymentspurchases> getAnualPaymentsPurchaseBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodUpscl, /*int[] baccounthotelspartner,*/ decimal ammountStart, decimal ammountEnd)
        {

            List<tblpaymentspurchases> paymentsPurchase = new List<tblpaymentspurchases>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblpaymentspurchases.Include(c => c.tblpurchases).Include(c => c.tblcurrencies).Include(c => c.tblpurchases.tblpartners);

                query = query.Where(c => c.paymentDate >= start && c.paymentDate <= end && c.idCurrency == idCurrency && idPaymentMethodUpscl.Contains((int)c.idPaymentType) /*&& baccounthotelspartner.Contains(c.tblpurchases.idPartner)*/);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.paymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.paymentCost <= ammountEnd);
                }

                paymentsPurchase = query.ToList();
            }

            return paymentsPurchase;
        }

        #endregion

        #region  Reservations

        public IEnumerable<tblreservations> getAnualPaymentsRESERVAS_WEB(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelPartner)
        {
            List<tblreservations> Reserv = new List<tblreservations>();
            if (idCurrency == (int)Currencies.US_Dollar && idPaymentMethodReserv.Contains((int)PaymentMethods_Bank_Report.PayPal))
            {
                Reserv = unity.ReservationsRepository.Get(t => t.purchaseDate.Value >= start && t.purchaseDate.Value <= end && (t.idSubcategory == (int)ReservaWeb.Reserva_Web) /*&& idHotelPartner.Contains(t.tblpartners.tblhotelchains.idHotelChain)*/, null, "tblmemberships").ToList();
            }
            return Reserv;
        }

        public IEnumerable<tblreservations> getAnualPaymentsRESERVAS_WEB(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelPartner, decimal ammountStart, decimal ammountEnd)
        {
            List<tblreservations> Reserv = new List<tblreservations>();
            if (idCurrency == (int)Currencies.US_Dollar && idPaymentMethodReserv.Contains((int)ReservaWeb.PaymentType))
            {
                using (var db = new vtclubdbEntities())
                {
                    var query = db.tblreservations.Include(c => c.tblreservationspayment).Include(c => c.tblreservationcancellation).Include(c => c.tblpartners);

                    query = query.Where(t => t.purchaseDate.Value >= start && t.purchaseDate.Value <= end && (t.idSubcategory == (int)ReservaWeb.Reserva_Web) /*&& idHotelPartner.Contains((int)t.tblreservations.idPartner)*/);

                    if (ammountStart != 0)
                    {
                        query = query.Where(c => c.reservationSellPrice >= ammountStart);
                    }

                    if (ammountEnd != 0)
                    {
                        query = query.Where(c => c.reservationSellPrice <= ammountEnd);
                    }

                    Reserv = query.ToList();
                }
            }


            return Reserv;
        }

        /************ Reservations */
        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERV(DateTime start, DateTime end, int idCurrency, int idPaymentMethodReserv, int[] idHotelPartner)
        {
            List<tblreservationspayment> paymentsReserv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && t.idPaymentType == idPaymentMethodReserv /*&& idHotelPartner.Contains((int)t.tblreservations.idPartner)*/, null, "tblreservations").ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERV(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelPartner)
        {
            List<tblreservationspayment> paymentsReserv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& idHotelPartner.Contains((int)t.tblreservations.idPartner)*/, null, "tblreservations").ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationsparentpayment> getAnualPaymentsRESERV_PARENT(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelPartner)
        {
            List<tblreservationsparentpayment> paymentsReserv = unity.ReservationsParentPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& idHotelPartner.Contains((int)t.tblreservations.idPartner)*/, null, "tblreservationsparent").ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERV(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelPartner, decimal ammountStart, decimal ammountEnd)
        {
            List<tblreservationspayment> paymentsReserv = new List<tblreservationspayment>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblreservationspayment.Include(c => c.tblreservations);

                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& idHotelPartner.Contains((int)t.tblreservations.idPartner)*/);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost <= ammountEnd);
                }

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        private IEnumerable<tblreservationsparentpayment> getAnualPaymentsRESERVPARENT(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelPartner, decimal ammountStart, decimal ammountEnd)
        {
            List<tblreservationsparentpayment> paymentsReserv = new List<tblreservationsparentpayment>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblreservationsparentpayment.Include(c => c.tblreservationsparent);

                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& idHotelPartner.Contains((int)t.tblreservations.idPartner)*/);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost <= ammountEnd);
                }

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        // Getting by Terminals
        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERVTPV(DateTime start, DateTime end, int idCurrency, int[] idTpv)
        {
            List<tblreservationspayment> paymentsReserv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idTpv.Contains((int)t.terminal)).ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationsparentpayment> getAnualPaymentsRESERVTPVPARENT(DateTime start, DateTime end, int idCurrency, int[] idTpv)
        {
            List<tblreservationsparentpayment> paymentsReserv = unity.ReservationsParentPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idTpv.Contains((int)t.terminal)).ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERVTPV(DateTime start, DateTime end, int idCurrency, int[] idTpv, decimal ammountStart, decimal ammountEnd/*, int[] baccounthotels*/)
        {
            List<tblreservationspayment> paymentsReserv = new List<tblreservationspayment>();

            using (var db = new vtclubdbEntities())
            {
                db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var query = db.tblreservationspayment.Include(c => c.tblreservations);
                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idTpv.Contains((int)t.terminal));



                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost <= ammountEnd);
                }

                /*if (baccounthotels.Count() != 0)
                {
                    query = query.Where(c => baccounthotels.Contains((int)c.tblreservations.idPartner));
                }*/

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        private IEnumerable<tblreservationsparentpayment> getAnualPaymentsRESERVTPVPARENT(DateTime start, DateTime end, int idCurrency, int[] idTpv, decimal ammountStart, decimal ammountEnd/*, int[] baccounthotels*/)
        {
            List<tblreservationsparentpayment> paymentsReserv = new List<tblreservationsparentpayment>();

            using (var db = new vtclubdbEntities())
            {
                db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var query = db.tblreservationsparentpayment.Include(c => c.tblreservationsparent);
                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idTpv.Contains((int)t.terminal));



                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost <= ammountEnd);
                }

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        // Getting by BankAccount-Hotels
        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERVBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv/*, int[] baccounthotels*/)
        {
            List<tblreservationspayment> paymentsReserv = unity.ReservationsPaymentsRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& baccounthotels.Contains((int)t.tblreservations.idPartner)*/, null, "tblreservations").ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationspayment> getAnualPaymentsRESERVBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, /*int[] baccounthotels,*/ decimal ammountStart, decimal ammountEnd)
        {
            List<tblreservationspayment> paymentsReserv = new List<tblreservationspayment>();

            using (var db = new vtclubdbEntities())
            {
                var query = db.tblreservationspayment.Include(c => c.tblreservations);

                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.idCurrency == idCurrency && idPaymentMethodReserv.Contains((int)t.idPaymentType) /*&& baccounthotels.Contains((int)t.tblreservations.idPartner)*/);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentCost <= ammountEnd);
                }

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        #endregion

        #region Income Movements

        private IEnumerable<tblincomemovement> getAnualIncomeMovements(DateTime start, DateTime end, int idbankaccount)
        {
            List<tblincomemovement> incomemovements = unity.IncomeMovementsRepository.Get(t => t.tblbankaccount.idbaccount == idbankaccount && t.tblincome.incomeapplicationdate >= start && t.tblincome.incomeapplicationdate <= end, null, "tblincome.tblcompanies.tblsegment, tblincome.tblusers.tblprofilesaccounts, tblbankprodttype, tblbankaccount, tbltpv, tblbankaccount.tblbank, tblbankaccount.tblcurrencies, tblbankaccount.tblcompanies").ToList();
            return incomemovements;
        }

        private IEnumerable<tblincomemovement> getAnualIncomeMovements(DateTime start, DateTime end, int idbankaccount, decimal ammountStart, decimal ammountEnd)
        {
            //List<tblincomemovement> incomemovements = unity.IncomeMovementsRepository.Get(t => t.tblbankaccount.idbaccount == idbankaccount && t.tblincome.incomeapplicationdate >= start && t.tblincome.incomeapplicationdate <= end, null, "tblincome.tblcompanies.tblsegment, tblincome.tblusers.tblprofilesaccounts, tblbankprodttype, tblbankaccount, tbltpv, tblbankaccount.tblbank, tblbankaccount.tblcurrencies, tblbankaccount.tblcompanies").ToList();
            List<tblincomemovement> incomemovements = new List<tblincomemovement>();
            using (var db = new vtclubdbEntities())
            {
                var query = db.tblincomemovement.Include(c => c.tblincome.tblcompanies.tblsegment).Include(c => c.tblincome.tblusers.tblprofilesaccounts).Include(c => c.tblbankprodttype).Include(c => c.tblbankaccount).Include(c => c.tblbankaccount.tblbank).Include(c => c.tblbankaccount.tblcurrencies).Include(c => c.tblbankaccount.tblcurrencies).Include(c => c.tblbankaccount.tblcompanies).Include(c => c.tbltpv);

                query = query.Where(t => t.tblbankaccount.idbaccount == idbankaccount && t.tblincome.incomeapplicationdate >= start && t.tblincome.incomeapplicationdate <= end);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.incomemovchargedamount >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.incomemovchargedamount <= ammountEnd);
                }

                incomemovements = query.ToList();
            }

            return incomemovements;
        }

        #endregion

        #endregion
    }
}