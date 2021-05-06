using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.vtaccountingVtclub.DataBase;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTAServices.Services.Models.commons;
using static VTAworldpass.VTACore.Collections.CollectionsUtils;

namespace VTAworldpass.VTAServices.Services.bank.model
{
    public class financialstate
    {
        public virtual int row { get; set; }
        public int    idBAccount { get; set; }
        public int    idBank { get; set; }
        public int    BankName              { get; set; }
        public string baccountName          { get; set; }
        public string baccountShortName     { get; set; }
        public virtual string  CompanyName  { get; set; }
        public virtual string  PaymentMethodName { get; set; }
        public virtual int     Currency { get; set; }
        public virtual string  CurrencyName { get; set; }
        public virtual decimal MontoCargo { get; set; }
        public virtual decimal MontoAbonos { get; set; }
        public virtual string  MontoCargoString { get; set; }
        public virtual string  MontoAbonosString { get; set; }
        public virtual decimal balance { get; set; }
        public virtual string  balanceString { get; set; }
        public virtual decimal balanceBefore { get; set; }
        public virtual string  balanceBeforeString { get; set; }
        public virtual decimal maxBalance { get; set; }
        public virtual string  maxBalanceString { get; set; }
        public virtual bool    hasMaxLimit { get; set; }
        public virtual string  FechaInicioString { get; set; }
        public virtual string  FechaFinString { get; set; }
        public virtual int     BankAccntType { get; set; }
        public virtual int     BankAccntTypeName { get; set; }
        public virtual bool    allowsNegatives { get; set; }

        public virtual IList<financialstateitem> financialstateitemlist { get; set; }

        // private sourcesdata
        private List<bankreconciliation> bankreconciliationitems { get; set; }
        private IEnumerable<int> HotelVTH { get; set; }

        private IEnumerable<bankaccountsourcedata> sourcedataitems { get; set; }
        private IEnumerable<invoiceitems> docitemsitems { get; set; }
        private IEnumerable<invoicepayment> docpaymtitems { get; set; }
        private IEnumerable<fondo> fondoitems { get; set; }
        private IEnumerable<docpaymentupscl> docpaymentupsclitems { get; set; }
        private IEnumerable<docpaymentreserv> docpaymentreservitems { get; set; }
        private IList<incomepayment> incomemovements { get; set; }
        private IEnumerable<tbltpv>  tbltpv { get; set; }
        private IEnumerable<tblbankstatements> bankstatements { get; set; }
        private IEnumerable<tblbaccounthotels> baccounthotels { get; set; }




        /**********************************************************************/
        private DateTime start;
        private DateTime end;
        private int idBanAccount;
        private FinancialStateReport type;
        private bool keepData;
        private int tpv;
        private int idBankAccntType;
        // private decimal ammountStart;
        // private decimal ammountEnd;
        /**********************************************************************/

        //public virtual IEnumerable<fondo> fondoitems { get; set; }
        // To generate Historial


        private readonly UnitOfWork unity = new UnitOfWork();
        private readonly IBankReconciliationServices bankReconciliationServices = new BankReconciliationServices();


        public financialstate()
        { }


        public financialstate(DateTime startdate, DateTime enddate, int idBanAccount, FinancialStateReport type, bool keepData)
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



        public financialstate(DateTime startdate, DateTime enddate, int idBanAccount, int tpv, decimal ammountStart, decimal ammountEnd, int idHotel, FinancialStateReport type, bool keepData)
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

        public financialstate(DateTime startdate, DateTime enddate, int idBanAccount, FinancialStateReport type, bool keepData, int idBankAccntType)
        {
            this.start = startdate;
            this.end = enddate;
            this.idBanAccount = idBanAccount;
            this.type = type;
            this.keepData = keepData;
            this.idBankAccntType = idBankAccntType;

            this.BodyCalculatingandData();
            var _BankAccount_ = unity.BankAccountRepository.Get(t => t.idBAccount == idBanAccount, null, "tblcompanies,tblcompanies.tblcompanyhotel,tblbank,Currency").FirstOrDefault();

            #region KeepData
            if (!keepData) { this.ClearSourceData(); }
            #endregion

            #region Allow Negatives
            // Allow Negatives
            var _typePrudct = _BankAccount_.tblbaccounttype.Where(x => x.idBankAccntType == idBankAccntType).FirstOrDefault();

            if (_typePrudct != null) { this.allowsNegatives = _typePrudct.baccounttypeAllowNeg; this.BankAccntType = _typePrudct.idBankAccntType; } else { this.allowsNegatives = false; this.BankAccntType = idBankAccntType; }

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
            this.docitemsitems = new List<invoicepayment>();
            this.fondoitems = new List<fondo>();
            this.docpaymentupsclitems = new List<docpaymentupscl>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.bankreconciliationitems = new List<bankreconciliation>();
            this.financialstateitemlist = new List<financialstateitem>();
            this.sourcedataitems = new List<bankaccountsourcedata>();
            this.HotelVTH = new List<int>();
            this.tbltpv = new List<tbltpv>();
            this.baccounthotels = new List<tblbaccounthotels>();
            this.incomemovements = new List<incomepayment>();

            // Getting only General onformation
            var _BankAccount = unity.BankAccountRepository.Get(t => t.idBAccount == idBanAccount, null, "tblcompanies,tblcompanies.tblcompanyhotel,tblbank,Currency").FirstOrDefault();
            this.baccountName = _BankAccount.baccountName;
            this.baccountShortName = _BankAccount.baccountShortName;
            this.idBAccount = _BankAccount.idBAccount;

            // Gettting Bank Account TPV's
            this.tbltpv = IEnumerableUtils.AddList(this.tbltpv, _BankAccount.tblbaccounttpv.Select(t => new tbltpv { idtpv = t.tbltpv.idTPV, tpvname = t.tbltpv.tpvName, tpvIdLocation = t.tbltpv.tpvIdLocation }).ToList());
            // Getting the Bank Account Sources data
            this.sourcedataitems = IEnumerableUtils.AddList(this.sourcedataitems, _BankAccount.tblbankaccountsourcedata.Select(y => new bankaccountsourcedata { SourceData = y.idSourceData, sourcedataDateStart = y.sourcedataDateStart, Types = y.tblbankaccountsourcedatatypes.Select(t => t.idType).ToList() }).ToList());
            // Getting id hotel relationships VTH
            this.HotelVTH = IEnumerableUtils.AddList(this.HotelVTH, _BankAccount.tblcompanies.tblcompanyhotel.Select(y => y.idHotel).ToList());
            // Getting the Bank Account - Hotel 
            this.baccounthotels = IEnumerableUtils.AddList(this.baccounthotels, _BankAccount.tblbaccounthotels.Where(t => t.baccounthotelActive == Constantes.activeRecord).Select(t => new tblbaccounthotels { idBAccount = t.idBAccount, idHotel = t.idHotel }).ToList());

            DateTime _startFondos   = new DateTime();
            DateTime _startPayments = new DateTime();
            // DateTime _startDocitems = new DateTime();
            DateTime _startUpscale  = new DateTime();
            DateTime _startReservatios = new DateTime();
            DateTime _startIncomesMovements = new DateTime();
            DateTime _startReconciliations = new DateTime();


            foreach (bankaccountsourcedata model in sourcedataitems)
            {
                this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName); this.PaymentMethodName = _BankAccount.baccountName;

                switch (model.SourceData)
                {
                    case 1: // Fondos
                        {
                            this.addSource(ConvertTbltoHelper(this.getAnualBudgetsTo(model.sourcedataDateStart, end, idBanAccount).ToList()));
                            this.addSource(ConvertTbltoHelper(this.getAnualBudgetsFrom(model.sourcedataDateStart, end, idBanAccount).ToList()));
                            _startFondos = model.sourcedataDateStart;
                        }
                        break;
                    case 2: // Pagos
                        {
                            this.addSource(ConvertTbltoHelper(this.getAnualPayments(model.sourcedataDateStart, end, idBanAccount).ToList()));
                            _startPayments = model.sourcedataDateStart;
                        }
                        break;
                    case 3: // Facturas
                        {
                            // this.addSource(this.ConvertTbltoHelper(this.getAnualDocitems(model.sourcedataDateStart, end, _BankAccount.idCompany).ToList()));
                            // _startDocitems = model.sourcedataDateStart;
                        }
                        break;
                    case 4: // Upscales
                        {
                            // Adding 
                            this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsUPSCL(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.HotelVTH.ToArray()).ToList()).ToList());
                            // Si la cuenta tiene terminales se buscan los registros de upscl.PAyments-PaymentsInstruments que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {
                                foreach (tbltpv _model in tbltpv)
                                {
                                    this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsUPSCLTPV(model.sourcedataDateStart, end, _BankAccount.idCurrency, _model.idTPV).ToList()).ToList());
                                }
                            }
                            // Si la cuenta tiene registros de Cuentas-Hoteles (BankAccount-Hotels), lo cual significa que esos hoteles aportan a esta cuenta en las modalidades que que se encuentren segun el tipo de pago.
                            if (this.baccounthotels.Count() != 0)
                            {
                                this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsUPSCLBAccountHotels(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.baccounthotels.Select(v => v.idHotel).ToArray()).ToList()).ToList());
                            }
                            _startUpscale = model.sourcedataDateStart;
                        }
                        break;
                    case 5:
                        {
                            // Reservaciones
                            this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsRESERV(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.HotelVTH.ToArray()).AsEnumerable()).ToList());
                            // Si la cuenta tiene terminales se buscan los registros de ReservationsPayments que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {
                                this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsRESERVTPV(model.sourcedataDateStart, end, _BankAccount.idCurrency, this.tbltpv.Select(y => y.idTPV).ToArray()).ToList()).ToList());
                            }
                            // Si la cuenta tiene registros de Cuentas-Hoteles (BankAccount-Hotels), lo cual significa que esos hoteles aportan a esta cuenta en las modalidades que que se encuentren segun el tipo de pago.
                            if (this.baccounthotels.Count() != 0)
                            {
                                this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsRESERVBAccountHotels(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.baccounthotels.Select(v => v.idHotel).ToArray()).ToList()).ToList());
                            }
                            _startReservatios = model.sourcedataDateStart;
                        }
                        break;
                    case 6:
                        {
                            this.addSource(bankReconciliationServices.getBakReconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconcilitionStatus.Completo, true, true));
                            _startReconciliations = model.sourcedataDateStart;
                        }
                        break;
                    case 7: // Ingresos
                        {

                        }
                        break;
                    case 8: // Movimientos ingreso
                        {
                            this.addSource(ConvertTbltoHelper(this.getAnualIncomeMovements(model.sourcedataDateStart, end, idBanAccount).ToList()));
                            _startIncomesMovements = model.sourcedataDateStart;
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

                        this.financialstateitemlist = new List<financialstateitem>();
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        var _fondoitemsTemp = this.fondoitems;
                        var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentupsclitemsTemp = this.docpaymentupsclitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _incomeMovementTemp = this.incomemovements;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Balance Before //////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstate financialStateBefore = new financialstate();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, "-", _BankAccount.Currency.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountName;


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
                        // UPSCLS - Pagos (Se toman comom ingresos)
                        if (_startUpscale.Year == start.Year && _startUpscale.Month == start.Month && _startUpscale.Day != start.Day)
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1) < _startUpscale ? _startUpscale : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
                        }
                        else
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
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

                        financialStateBefore.bankreconciliationitems = new List<bankreconciliation>(); // in this reports not include Reconciliations

                        financialStateBefore.calculateMontoCargos();
                        financialStateBefore.calculateMontoAbonos();
                        financialStateBefore.calculateBalance();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;


                        this.balanceBefore = financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountName;

                        //*******************************/
                        this.docitemsitems = new List<docitems>();
                        this.docpaymtitems = new List<docpaymt>();
                        this.fondoitems = new List<fondo>();
                        this.docpaymentupsclitems = new List<docpaymentupscl>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.incomemovements = new List<incomepayment>();

                        /*******************************/
                        // Getting budgets and Payments in interval
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        //this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= startAfter && y.aplicationdate <= endAfter).ToList());

                        // this.bankreconciliationitems = new List<bankreconciliation>(); // in this reports not include Reconciliations

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.calculateBalanceWithBalceBefore();
                        this.generateTimeLineBalance(6);
                        this.parseAmmountsToString();
                        this.hightLightBankreconciliations();
                        this.bankreconciliationitems = new List<bankreconciliation>(); // in this reports not include Reconciliations


                    }
                    break;

                case FinancialStateReport.MaxBalance: // Max Balance  // NO se le han implementado la eliminación de las conciliaciones
                    {

                        fondosmaxammount max = this.getfondosmaxammount(idBanAccount);

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();

                        if (max.FondosMax != 0)
                        {
                            this.calculateMaxBalanceWithLimit(max.fondosmaxAmmount); hasMaxLimit = true;
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

                        this.financialstateitemlist = new List<financialstateitem>();
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        var _fondoitemsTemp = this.fondoitems;
                        // var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentupsclitemsTemp = this.docpaymentupsclitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _incomeMovementTemp = this.incomemovements;
                        var _bankreconciliationitemsTemp = this.bankreconciliationitems;


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GWTTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        List<docpaymentupscl> toEvalUpsc = new List<docpaymentupscl>();
                        toEvalUpsc = _docpaymentupsclitemsTemp.ToList();
                        // toEvalUpsc = _docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList();

                        List<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        // toEvalReserv = _docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList();
                        toEvalReserv = _docpaymentreservitemsTemp.ToList();


                        // Bank reconciliations 
                        // ******************************* Getting Data and Deleting Upscls and Reservation that are in BankReconciliations
                        bankreconciliation[] reconciliationsTmpWork = new bankreconciliation[] { };
                        // reconciliationsTmpWork = bankReconciliationServices.getBakReconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconcilitionStatus.Completo, true, true).ToArray();
                        reconciliationsTmpWork = _bankreconciliationitemsTemp.ToArray();

                        for (int i = 0; i <= reconciliationsTmpWork.Count() - 1; i++)
                        {
                            if (reconciliationsTmpWork[i].docpaymentupsclitems != null || reconciliationsTmpWork[i].docpaymentupsclitems.Count() != 0)
                            {
                                foreach (docpaymentupscl model in reconciliationsTmpWork[i].docpaymentupsclitems)
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
                        _docpaymentupsclitemsTemp = toEvalUpsc;
                        _docpaymentreservitemsTemp = toEvalReserv;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// CALCULATING BEFORE FINANCIAL STATE  /////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstate financialStateBefore = new financialstate();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, "-", _BankAccount.Currency.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountName;


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
                        // UPSCLS - Pagos (Se toman comom ingresos)
                        if (_startUpscale.Year == start.Year && _startUpscale.Month == start.Month && _startUpscale.Day != start.Day)
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1) < _startUpscale ? _startUpscale : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
                        }
                        else
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
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


                        this.balanceBefore = financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountName;

                        //******************************* Initializaing sourcedata
                        this.fondoitems = new List<fondo>();
                        //this.docitemsitems = new List<docitems>();
                        this.docpaymtitems = new List<docpaymt>();
                        this.docpaymentupsclitems = new List<docpaymentupscl>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.incomemovements = new List<incomepayment>();
                        this.bankreconciliationitems = new List<bankreconciliation>();


                        //******************************* Getting interval data
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        // this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
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
                        this.financialstateitemlist = new List<financialstateitem>();
                        var _fondoitemsTemp = this.fondoitems;
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        // var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentupsclitemsTemp = this.docpaymentupsclitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _incomeMovementTemp = this.incomemovements;
                        var _bankreconciliationitemsTemp = this.bankreconciliationitems;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GETTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        IList<docpaymentupscl> toEvalUpsc = new List<docpaymentupscl>();
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, _docpaymentupsclitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, _docpaymentupsclitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        IList<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        toEvalReserv = IListUtils.AddList(toEvalReserv, _docpaymentreservitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReserv = IListUtils.AddList(toEvalReserv, _docpaymentreservitemsTemp.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        // Setting values before get Transfer and deposits
                        _docpaymentupsclitemsTemp = toEvalUpsc;
                        _docpaymentreservitemsTemp = toEvalReserv;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// CALCULATING BEFORE FINANCIAL STATE  /////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstate financialStateBefore = new financialstate();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, "-", _BankAccount.Currency.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountName;


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
                        // UPSCLS - Pagos (Se toman comom ingresos)
                        if (_startUpscale.Year == start.Year && _startUpscale.Month == start.Month && _startUpscale.Day != start.Day)
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1) < _startUpscale ? _startUpscale : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
                        }
                        else
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
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


                        this.balanceBefore = financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountName;

                        //******************************* Initializaing sourcedata
                        this.docitemsitems = new List<docitems>();
                        this.docpaymtitems = new List<docpaymt>();
                        this.fondoitems = new List<fondo>();
                        this.docpaymentupsclitems = new List<docpaymentupscl>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.bankreconciliationitems = new List<bankreconciliation>();

                        //******************************* Getting interval data
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        //this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
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

                        fondosmaxammount max = this.getfondosmaxammount(idBanAccount);


                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// GETTING DEPOSIT AND TRANSFER ////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        IList<docpaymentupscl> toEvalUpsc = new List<docpaymentupscl>();
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentupsclitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentupsclitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        IList<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        // Setting values before get Transfer and deposits
                        this.docpaymentupsclitems = toEvalUpsc;
                        this.docpaymentreservitems = toEvalReserv;
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();

                        if (max.FondosMax != 0)
                        {
                            this.calculateMaxBalanceWithLimit(max.fondosmaxAmmount); hasMaxLimit = true;
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
                        IList<docpaymentupscl> toEvalUpsc = new List<docpaymentupscl>();
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentupsclitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalUpsc = IListUtils.AddList(toEvalUpsc, this.docpaymentupsclitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        IList<docpaymentreserv> toEvalReserv = new List<docpaymentreserv>();
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Transfer).ToList());
                        toEvalReserv = IListUtils.AddList(toEvalReserv, this.docpaymentreservitems.Where(y => y.PaymentMethod == (int)PaymentMethods_Bank_Report.Deposit).ToList());


                        // Setting values before get Transfer and deposits
                        this.docpaymentupsclitems = toEvalUpsc;
                        this.docpaymentreservitems = toEvalReserv;
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //******************************* Getting interval data *******************************/
                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.parseAmmountsToString();

                    }
                    break;

                default:
                    {
                        start = new DateTime(2018, 1, 1);
                        // Appli Date from tblParámeters


                        // financialstate financialState = new financialstate();
                        this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountName;


                        this.addSource(ConvertTbltoHelper(this.getAnualBudgetsTo(start, end, idBanAccount).ToList()));
                        this.addSource(ConvertTbltoHelper(this.getAnualBudgetsFrom(start, end, idBanAccount).ToList()));
                        this.addSource(ConvertTbltoHelper(this.getAnualPayments(start, end, idBanAccount).ToList()));
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
            this.fondoitems = new List<fondo>();
            this.docpaymentupsclitems = new List<docpaymentupscl>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.bankreconciliationitems = new List<bankreconciliation>();
            this.financialstateitemlist = new List<financialstateitem>();
            this.sourcedataitems = new List<bankaccountsourcedata>();
            this.HotelVTH = new List<int>();
            this.tbltpv = new List<tbltpv>();
            this.baccounthotels = new List<tblbaccounthotels>();
            this.incomemovements = new List<incomepayment>();

            // Getting only General onformation
            var _BankAccount = unity.BankAccountRepository.Get(t => t.idBAccount == idBanAccount, null, "tblcompanies,tblcompanies.tblcompanyhotel,tblbank,Currency").FirstOrDefault();
            this.baccountName = _BankAccount.baccountName;
            this.baccountShortName = _BankAccount.baccountShortName;
            this.idBAccount = _BankAccount.idBAccount;

            // Gettting Bank Account TPV's // Se asigna solo el hotel solicitado
            // 
            if (tpv != 0)
            {
                this.tbltpv = IEnumerableUtils.AddList(this.tbltpv, _BankAccount.tblbaccounttpv.Where(b => b.idTPV == this.tpv).Select(t => new tbltpv { idTPV = t.tbltpv.idTPV, tpvName = t.tbltpv.tpvName, tpvIdLocation = t.tbltpv.tpvIdLocation }).ToList());
            }
            else
            {
                this.tbltpv = IEnumerableUtils.AddList(this.tbltpv, _BankAccount.tblbaccounttpv.Select(t => new tbltpv { idTPV = t.tbltpv.idTPV, tpvName = t.tbltpv.tpvName, tpvIdLocation = t.tbltpv.tpvIdLocation }).ToList());
            }
            // Getting the Bank Account Sources data
            this.sourcedataitems = IEnumerableUtils.AddList(this.sourcedataitems, _BankAccount.tblbankaccountsourcedata.Select(y => new bankaccountsourcedata { SourceData = y.idSourceData, sourcedataDateStart = y.sourcedataDateStart, Types = y.tblbankaccountsourcedatatypes.Select(t => t.idType).ToList() }).ToList());
            // Getting id hotel relationships VTH
            this.HotelVTH = IEnumerableUtils.AddList(this.HotelVTH, _BankAccount.tblcompanies.tblcompanyhotel.Select(y => y.idHotel).ToList());
            // Getting the Bank Account - Hotel 
            if (idHotel != 0) // Si el hotel es diferente de cero, se ha solicitado un banco en especifico
            {
                this.baccounthotels = IEnumerableUtils.AddList(this.baccounthotels, _BankAccount.tblbaccounthotels.Where(t => t.idHotel == idHotel && t.baccounthotelActive == Constantes.activeRecord).Select(t => new tblbaccounthotels { idBAccount = t.idBAccount, idHotel = t.idHotel }).ToList());
                this.HotelVTH = IEnumerableUtils.AddList(this.HotelVTH, _BankAccount.tblcompanies.tblcompanyhotel.Select(y => y.idHotel).ToList());
            }
            else
            {
                this.baccounthotels = IEnumerableUtils.AddList(this.baccounthotels, _BankAccount.tblbaccounthotels.Where(t => t.baccounthotelActive == Constantes.activeRecord).Select(t => new tblbaccounthotels { idBAccount = t.idBAccount, idHotel = t.idHotel }).ToList());
            }



            DateTime _startFondos = new DateTime();
            DateTime _startPayments = new DateTime();
            // DateTime _startDocitems = new DateTime();
            DateTime _startUpscale = new DateTime();
            DateTime _startReservatios = new DateTime();
            DateTime _startIncomesMovements = new DateTime();
            DateTime _startReconciliations = new DateTime();


            foreach (bankaccountsourcedata model in sourcedataitems)
            {
                this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName); this.PaymentMethodName = _BankAccount.baccountName;

                switch (model.SourceData)
                {
                    case 1: // Fondos
                        {
                            this.addSource(ConvertTbltoHelper(this.getAnualBudgetsTo(model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            this.addSource(ConvertTbltoHelper(this.getAnualBudgetsFrom(model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            _startFondos = model.sourcedataDateStart;
                        }
                        break;
                    case 2: // Pagos
                        {
                            this.addSource(ConvertTbltoHelper(this.getAnualPayments(model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            _startPayments = model.sourcedataDateStart;
                        }
                        break;
                    case 3: // Facturas
                        {
                            // this.addSource(this.ConvertTbltoHelper(this.getAnualDocitems(model.sourcedataDateStart, end, _BankAccount.idCompany).ToList()));
                            // _startDocitems = model.sourcedataDateStart;
                        }
                        break;
                    case 4: // Upscales
                        {
                            // Adding 
                            this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsUPSCL(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.HotelVTH.ToArray(), ammountStart, ammountEnd).ToList()).ToList());
                            // Si la cuenta tiene terminales se buscan los registros de upscl.PAyments-PaymentsInstruments que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {
                                foreach (tbltpv _model in tbltpv)
                                {
                                    this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsUPSCLTPV(model.sourcedataDateStart, end, _BankAccount.idCurrency, _model.idTPV, ammountStart, ammountEnd, baccounthotels.Select(c => c.idHotel).ToArray()).ToList()).ToList());
                                }
                            }
                            // Si la cuenta tiene registros de Cuentas-Hoteles (BankAccount-Hotels), lo cual significa que esos hoteles aportan a esta cuenta en las modalidades que que se encuentren segun el tipo de pago.
                            if (this.baccounthotels.Count() != 0)
                            {
                                this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsUPSCLBAccountHotels(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.baccounthotels.Select(v => v.idHotel).ToArray(), ammountStart, ammountEnd).ToList()).ToList());
                            }
                            _startUpscale = model.sourcedataDateStart;
                        }
                        break;
                    case 5:
                        {
                            // Reservaciones
                            this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsRESERV(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.HotelVTH.ToArray(), ammountStart, ammountEnd).AsEnumerable()).ToList());
                            // Si la cuenta tiene terminales se buscan los registros de ReservationsPayments que esten con esa terminal
                            if (this.tbltpv.Count() != 0)
                            {
                                this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsRESERVTPV(model.sourcedataDateStart, end, _BankAccount.idCurrency, this.tbltpv.Select(y => y.idTPV).ToArray(), ammountStart, ammountEnd, baccounthotels.Select(c => c.idHotel).ToArray()).ToList()).ToList());
                            }
                            // Si la cuenta tiene registros de Cuentas-Hoteles (BankAccount-Hotels), lo cual significa que esos hoteles aportan a esta cuenta en las modalidades que que se encuentren segun el tipo de pago.
                            if (this.baccounthotels.Count() != 0)
                            {
                                this.addSource(this.ConvertTbltoHelper(this.getAnualPaymentsRESERVBAccountHotels(model.sourcedataDateStart, end, _BankAccount.idCurrency, model.Types.ToArray(), this.baccounthotels.Select(v => v.idHotel).ToArray(), ammountStart, ammountEnd).ToList()).ToList());
                            }
                            _startReservatios = model.sourcedataDateStart;
                        }
                        break;
                    case 6:
                        {
                            this.addSource(bankReconciliationServices.getBakReconcilitions(model.sourcedataDateStart, end, 0, 0, 0, this.idBAccount, 0, BankAccountReconcilitionStatus.Completo, true, true));
                            _startReconciliations = model.sourcedataDateStart;
                        }
                        break;
                    case 7: // Ingresos
                        {

                        }
                        break;
                    case 8: // Movimientos ingreso
                        {
                            this.addSource(ConvertTbltoHelper(this.getAnualIncomeMovements(model.sourcedataDateStart, end, idBanAccount, ammountStart, ammountEnd).ToList()));
                            _startIncomesMovements = model.sourcedataDateStart;
                        }
                        break;
                    default:
                        {
                            break;
                        }
                }
            }


            switch (type)
            {/*
                case FinancialStateReport.Balance: //balance // NO se le han implementado la eliminación de las conciliaciones
                    {
                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.parseAmmountsToString();
                    }
                    break;
                    */
                case FinancialStateReport.AccountHistory: // AccountHistory
                    {
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Initializing ////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        this.financialstateitemlist = new List<financialstateitem>();
                        var _docpaymtitemsTemp = this.docpaymtitems;
                        var _fondoitemsTemp = this.fondoitems;
                        var _docitemsitemsTemp = this.docitemsitems;
                        var _docpaymentupsclitemsTemp = this.docpaymentupsclitems;
                        var _docpaymentreservitemsTemp = this.docpaymentreservitems;
                        var _incomeMovementTemp = this.incomemovements;

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Balance Before //////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        financialstate financialStateBefore = new financialstate();
                        financialStateBefore.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, "-", _BankAccount.Currency.currencyName);
                        financialStateBefore.PaymentMethodName = _BankAccount.baccountName;


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
                        // UPSCLS - Pagos (Se toman comom ingresos)
                        if (_startUpscale.Year == start.Year && _startUpscale.Month == start.Month && _startUpscale.Day != start.Day)
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1) < _startUpscale ? _startUpscale : start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
                        }
                        else
                        {
                            DateTime endBeforeUpscales = start.AddDays(-1);
                            financialStateBefore.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= _startUpscale && y.aplicationDate <= endBeforeUpscales).ToList());
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

                        financialStateBefore.bankreconciliationitems = new List<bankreconciliation>(); // in this reports not include Reconciliations

                        financialStateBefore.calculateMontoCargos();
                        financialStateBefore.calculateMontoAbonos();
                        financialStateBefore.calculateBalance();

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////// Lapse Time Selected /////////////////////////////////////////////////////////////////////////////////////////
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        DateTime startAfter = start;
                        DateTime endAfter = end;


                        this.balanceBefore = financialStateBefore.balance;
                        this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountName;

                        //*******************************/
                        this.docitemsitems = new List<docitems>();
                        this.docpaymtitems = new List<docpaymt>();
                        this.fondoitems = new List<fondo>();
                        this.docpaymentupsclitems = new List<docpaymentupscl>();
                        this.docpaymentreservitems = new List<docpaymentreserv>();
                        this.incomemovements = new List<incomepayment>();

                        /*******************************/
                        // Getting budgets and Payments in interval
                        this.addSource(_fondoitemsTemp.Where(y => y.fechaEntrega >= startAfter && y.fechaEntrega <= endAfter).ToList());
                        this.addSource(_docpaymtitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        //this.addSource(_docitemsitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentupsclitemsTemp.Where(y => y.aplicationDate >= startAfter && y.aplicationDate <= endAfter).ToList());
                        this.addSource(_docpaymentreservitemsTemp.Where(y => y.reservationPaymentDate >= startAfter && y.reservationPaymentDate <= endAfter).ToList());
                        this.addSource(_incomeMovementTemp.Where(y => y.aplicationdate >= startAfter && y.aplicationdate <= endAfter).ToList());

                        // this.bankreconciliationitems = new List<bankreconciliation>(); // in this reports not include Reconciliations

                        this.calculateMontoCargos();
                        this.calculateMontoAbonos();
                        this.calculateBalance();
                        this.calculateBalanceWithBalceBefore();
                        this.generateTimeLineBalance(6);
                        this.parseAmmountsToString();
                        this.hightLightBankreconciliations();
                        this.bankreconciliationitems = new List<bankreconciliation>(); // in this reports not include Reconciliations


                    }
                    break;
                default:
                    {
                        start = new DateTime(2018, 1, 1);
                        // Appli Date from tblParámeters


                        // financialstate financialState = new financialstate();
                        this.CurrencyName = string.Concat(_BankAccount.Currency.currencyAlphabeticCode, " - ", _BankAccount.Currency.currencyName);
                        this.PaymentMethodName = _BankAccount.baccountName;


                        this.addSource(ConvertTbltoHelper(this.getAnualBudgetsTo(start, end, idBanAccount).ToList()));
                        this.addSource(ConvertTbltoHelper(this.getAnualBudgetsFrom(start, end, idBanAccount).ToList()));
                        this.addSource(ConvertTbltoHelper(this.getAnualPayments(start, end, idBanAccount).ToList()));
                        this.parseAmmountsToString();
                    }
                    break;
            }
            #endregion
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

        private fondosmaxammount getfondosmaxammount(int idPaymentMethod)
        {
            var result = this.unity.FondosMaxLimitRepository.Get(t => t.tblbankaccount.idBAccount == idPaymentMethod, null, "tblbankaccount").FirstOrDefault();

            if (result != null)
                return ConvertTbltoHelper(result);
            else return new Models.fondosmaxammount();

        }

        public void deleteFinancialStateItemLinked()
        {
            this.financialstateitemlist = this.financialstateitemlist.Where(f => f.bankStatementLinked == false).ToList();
            this.applyRowIndex();
        }

        public void hightLightBankreconciliations()
        {
            // Current FinancialStateitemlist of Financial State
            financialstateitem[] currentfinancialstateitem = new financialstateitem[] { };
            currentfinancialstateitem = this.financialstateitemlist.ToArray();
            this.financialstateitemlist = new List<financialstateitem>();

            // Current Bank reconciliations 
            bankreconciliation[] reconciliationsTmpWork = new bankreconciliation[] { };
            List<bankreconciliation> allBR = new List<bankreconciliation>();
            List<bankreconciliation> br1 = bankReconciliationServices.getBakReconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconcilitionStatus.Completo, true, true).ToList();
            List<bankreconciliation> br2 = bankReconciliationServices.getBakReconcilitions(this.start, this.end, 0, 0, 0, this.idBAccount, 0, BankAccountReconcilitionStatus.Parcial, true, true).ToList();

            br1.ForEach(v => allBR.Add(v));
            br2.ForEach(v => allBR.Add(v));
            reconciliationsTmpWork = allBR.ToArray();

            List<financialstateitem> tmpFinancialStateItem = new List<financialstateitem>();

            // Recorriendo las conciliaciones actuales y tomando todos los financialstateitem
            for (int i = 0; i <= reconciliationsTmpWork.Count() - 1; i++)
            {
                if (reconciliationsTmpWork[i].financialstateitemlist != null || reconciliationsTmpWork[i].financialstateitemlist.Count() != 0)
                {
                    foreach (financialstateitem model in reconciliationsTmpWork[i].financialstateitemlist)
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
        public void addSource(fondo helper)
        {
            if (this.fondoitems == null || this.fondoitems.Count() == 0)
            {
                this.fondoitems = IEnumerableUtils.AddSingle<fondo>(helper);
            }
            else
            {
                IEnumerableUtils.AddSingle<fondo>(this.fondoitems, helper);
            }

        }
        public void addSource(List<fondo> listhelper)
        {
            if (this.fondoitems == null || this.fondoitems.Count() == 0)
            {
                this.fondoitems = IEnumerableUtils.AddList<fondo>(listhelper);
            }
            else
            {
                this.fondoitems = IEnumerableUtils.AddList<fondo>(this.fondoitems, listhelper);
            }


        }
        /*****************************/
        public void addSource(docpaymt helper)
        {
            if (this.docpaymtitems == null || this.docpaymtitems.Count() == 0)
            { this.docpaymtitems = IEnumerableUtils.AddSingle<docpaymt>(helper); }
            else { IEnumerableUtils.AddSingle<docpaymt>(this.docpaymtitems, helper); }

        }
        public void addSource(List<docpaymt> listhelper)
        {
            if (this.docpaymtitems == null || this.docpaymtitems.Count() == 0)
            { this.docpaymtitems = IEnumerableUtils.AddList<docpaymt>(listhelper); }
            else { this.docpaymtitems = IEnumerableUtils.AddList<docpaymt>(this.docpaymtitems, listhelper); }

        }

        /*****************************/

        public void addSource(docitems helper)
        {
            if (this.docitemsitems == null || this.docitemsitems.Count() == 0) { this.docitemsitems = IEnumerableUtils.AddSingle<docitems>(helper); }
            else { IEnumerableUtils.AddSingle<docitems>(this.docitemsitems, helper); }
        }
        public void addSource(List<docitems> listhelper)
        {
            if (this.docitemsitems == null || this.docitemsitems.Count() == 0) { this.docitemsitems = IEnumerableUtils.AddList<docitems>(listhelper); }
            else { this.docitemsitems = IEnumerableUtils.AddList<docitems>(this.docitemsitems, listhelper); }
        }

        /*****************************/
        public void addSource(docpaymentupscl helper)
        {
            if (this.docpaymentupsclitems == null || this.docpaymentupsclitems.Count() == 0) { this.docpaymentupsclitems = IEnumerableUtils.AddSingle<docpaymentupscl>(helper); }
            else { IEnumerableUtils.AddSingle<docpaymentupscl>(this.docpaymentupsclitems, helper); }
        }
        public void addSource(List<docpaymentupscl> listhelper)
        {
            if (this.docpaymentupsclitems == null || this.docpaymentupsclitems.Count() == 0) { this.docpaymentupsclitems = IEnumerableUtils.AddList<docpaymentupscl>(listhelper); }
            else { this.docpaymentupsclitems = IEnumerableUtils.AddList<docpaymentupscl>(this.docpaymentupsclitems, listhelper); }
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

        /******************************************************************************************************/
        public void addSource(bankreconciliation helper)
        {
            if (this.bankreconciliationitems == null || this.bankreconciliationitems.Count() == 0) { this.bankreconciliationitems = (List<bankreconciliation>)IListUtils.AddSingle<bankreconciliation>(helper); }
            else { IListUtils.AddSingle<bankreconciliation>(this.bankreconciliationitems, helper); }
        }

        public void addSource(List<bankreconciliation> listhelper)
        {
            if (this.bankreconciliationitems == null || this.bankreconciliationitems.Count() == 0) { this.bankreconciliationitems = (List<bankreconciliation>)IListUtils.AddList<bankreconciliation>(listhelper); }
            else { this.bankreconciliationitems = (List<bankreconciliation>)IListUtils.AddList<bankreconciliation>(this.bankreconciliationitems, listhelper); }
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
            financialstateitem _object = new financialstateitem(2, helper.aplicationDate, helper.chargedAmount * -1, 0, "", "", 2, "Pagos", helper.Invoice, helper.Payment, string.Concat(helper.InvoiceIdentifier, " ", helper.description));
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<invoicepayment> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(fondo helper)
        {
            financialstateitem _object = null;

            if (helper.MontoCargo < 0)
            {
                // _object = new financialstateitem("*** Movimiento de salida, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***", helper.fechaEntrega, helper.MontoCargo);
                _object = new financialstateitem(2, helper.fechaEntrega, helper.MontoCargo, 0m, "Transferencia", helper.CompanyFinancialName + "-" + helper.FinancialMethodName, 1, "Fondos", helper.Invoice == null ? 0 : (int)helper.Invoice, helper.id, "*** Movimiento de salida, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***");
                _object.generateString();
            }
            else
            {
                // _object = new financialstateitem("*** Movimiento de entrada, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***", helper.fechaEntrega, helper.MontoCargo);
                _object = new financialstateitem(1, helper.fechaEntrega, helper.MontoCargo, 0m, "Transferencia", helper.CompanyFinancialName + "-" + helper.FinancialMethodName, 1, "Fondos", helper.Invoice == null ? 0 : (int)helper.Invoice, helper.id, "*** Movimiento de salida, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***");
                _object.generateString();
            }

            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }

        }

        private void addReporItem(List<fondo> helper)
        {

            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(docpaymentupscl helper)
        {
            // financialstateitem _object = new financialstateitem(string.Concat(" UPSCL pago " + helper.PaymentMethodName + " , ", helper.authRef , " - Reserva : ", helper.Reserva == 0 ? " " : helper.Reserva.ToString(), " - Upscale ", helper.Invoice.ToString() ), helper.aplicationDate, helper.chargedAmount); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitem _object = new financialstateitem(1, helper.aplicationDate, helper.chargedAmount, 0m, helper.PaymentMethodName, helper.HotelName, 4, "UPSCL", helper.Invoice, helper.Payment, string.Concat(" UPSCL pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reserva == 0 ? " " : helper.Reserva.ToString(), " - Upscale ", helper.Invoice.ToString(), helper.Hotel != 0 ? "- Hotel " + helper.HotelName : ""), helper.bankStatementLinked);    // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<docpaymentupscl> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(docpaymentreserv helper)
        {
            //financialstateitem _object = new financialstateitem(string.Concat("Reservación pago "+ helper.PaymentMethodName +" , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.reservationPaymentDate, helper.reservationPaymentQuantity); // son positivos ya que estos pagos son entradas para la empresa
            financialstateitem _object = new financialstateitem(1, helper.reservationPaymentDate, helper.reservationPaymentQuantity, 0m, helper.PaymentMethodName, helper.HotelName, 5, "Reservation", helper.Reservation, helper.ReservationPayment, string.Concat("Reservación pago " + helper.PaymentMethodName + " , ", helper.authRef, " - Reserva : ", helper.Reservation == 0 ? " " : helper.Reservation.ToString()), helper.bankStatementLinked);   // son positivos ya que estos pagos son entradas para la empresa
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<docpaymentreserv> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }

        private void addReporItem(incomepayment helper)
        {
            financialstateitem _object = new financialstateitem(helper.aplicationdate, helper.ammounttotal, 0, helper.bankaccnttypename, "", 8, "Movimiento Igreso", helper.parent, helper.item, string.Concat(helper.identifier, " ", helper.description));
            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<incomepayment> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
        }
        /****************************************** Converting BankReconciliation To FinancialStateItem ***************************************************/
        private void addReporItem(bankreconciliation helper)
        {
            financialstateitem _object = new financialstateitem(1, helper.bankstatementAplicationDate, (decimal)helper.bankstatementAppliedAmmountFinal, 0m, "Conciliación", helper.hotelname, "Conciliaciones", helper.idBankStatements, string.Format("Conciliación bancaria ScotiaPos, fecha aplicación {0}, origen: {1}", helper.bankstatementaplicationdatestring, helper.hotelname));

            _object.generateString();
            if (this.financialstateitemlist == null || this.financialstateitemlist.Count() == 0)
            {
                this.financialstateitemlist = IListUtils.AddSingle<financialstateitem>(_object);
            }
            else
            {
                this.financialstateitemlist = IListUtils.AddToList<financialstateitem>(this.financialstateitemlist, _object); this.financialstateitemlist = this.financialstateitemlist.OrderBy(y => y.aplicationDate).ToList();
            }
        }

        private void addReporItem(List<bankreconciliation> helper)
        {
            helper.ForEach(y => this.addReporItem(y));
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
                this.addReporItem(this.docpaymentupsclitems.ToList());
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
                financialstateitem[] _itemsTemp = this.financialstateitemlist.ToArray();

                if (_itemsTemp.Count() != 0)
                {
                    for (int i = 0; i <= _itemsTemp.Count() - 1; i++)
                    {
                        _itemsTemp[i].balance = _balancetemp + _itemsTemp[i].appliedAmmount;
                        _itemsTemp[i].balanceString = MoneyUtils.ParseDecimalToString(_itemsTemp[i].balance);
                        _balancetemp = _itemsTemp[i].balance;
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
                    case 1:
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
                    case 2:
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
                    case 3:
                        {

                        }
                        break;
                    case 4:
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymentupsclitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los upscales", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 5:
                        {
                            var result = exceptionSourcedata.Contains(_sourcedataObject[i]);
                            if (result != true)
                            {
                                try
                                {
                                    this.addReporItem(this.docpaymentreservitems.ToList());
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("No se pueden agregar a la lista FinancialStateItem los reservations", e.InnerException);
                                }
                            }
                        }
                        break;
                    case 6:
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
                    case 7:
                        {

                        }
                        break;
                    case 8:
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
                }
            }

            decimal _balancetemp = this.balanceBefore;

            if (this.financialstateitemlist != null || this.financialstateitemlist.Count() != 0)
            {
                financialstateitem[] _itemsTemp = this.financialstateitemlist.ToArray();

                if (_itemsTemp.Count() != 0)
                {
                    for (int i = 0; i <= _itemsTemp.Count() - 1; i++)
                    {
                        _itemsTemp[i].balance = _balancetemp + _itemsTemp[i].appliedAmmount;
                        _itemsTemp[i].balanceString = MoneyUtils.ParseDecimalToString(_itemsTemp[i].balance);
                        _balancetemp = _itemsTemp[i].balance;
                    }
                }
                this.financialstateitemlist = _itemsTemp;
            }
        }

        private void calculateMontoCargos()
        {
            var _docpayments = this.docpaymtitems != null ? this.docpaymtitems.Sum(y => y.chargedamount) : 0;
            var _totalFondos = this.fondoitems != null ? this.fondoitems.Where(x => x.MontoAbonos < 0).Sum(y => y.MontoAbonos) : 0;

            this.MontoCargo = _docpayments + _totalFondos;


        }

        private void calculateMontoAbonos()
        {
            var _totalFondos = this.fondoitems != null ? this.fondoitems.Where(x => x.MontoCargo > 0).Sum(y => y.MontoCargo) : 0;
            var _totalPaymentsUpscl = this.docpaymentupsclitems != null ? this.docpaymentupsclitems.Sum(y => y.chargedAmount) : 0;
            var _totalPaymentsReserv = this.docpaymentreservitems != null ? this.docpaymentreservitems.Sum(y => y.reservationPaymentQuantity) : 0;
            var _totalBankReconciliations = this.bankreconciliationitems != null ? this.bankreconciliationitems.Sum(y => y.bankstatementAppliedAmmountFinal) : 0;
            var _totalIncomeMovements = this.incomemovements != null ? this.incomemovements.Sum(y => y.ammounttotal) : 0;
            // this.MontoCargo = this.fondoitems != null  ? this.fondoitems.Sum(y => y.MontoCargo) : 0
            // this.MontoCargo = this.fondoitems != null  ? this.fondoitems.Sum(y => y.MontoCargo) : 0
            this.MontoAbonos = _totalFondos + _totalPaymentsUpscl + _totalPaymentsReserv + (decimal)_totalBankReconciliations + _totalIncomeMovements;
            //this.MontoCargo = this.financialstateitemlist.Where(v => v.accounttype == 1).Sum(c=> c.appliedAmmount);


        }

        private void calculateBalance()
        {
            this.balance = this.MontoAbonos - this.MontoCargo;
        }

        public void calculateMaxBalanceWithLimit(decimal limitBudget)
        {
            var _temp = (this.MontoCargo - this.MontoAbonos);
            this.maxBalance = _temp <= 0 ? limitBudget : limitBudget - _temp;
        }

        public void calculateBalanceWithBalceBefore()
        {
            this.balance = (this.MontoCargo - this.MontoAbonos) + this.balanceBefore;
        }

        private void parseAmmountsToString()
        {
            this.MontoCargoString = MoneyUtils.ParseDecimalToString(this.MontoCargo);
            this.MontoAbonosString = MoneyUtils.ParseDecimalToString(this.MontoAbonos);
            this.balanceString = MoneyUtils.ParseDecimalToString(this.balance);
            this.maxBalanceString = MoneyUtils.ParseDecimalToString(this.maxBalance);
            this.balanceBeforeString = MoneyUtils.ParseDecimalToString(this.balanceBefore);
        }
        #endregion

        #region Keep Data

        private void ClearSourceData()
        {

            // Initializing List
            this.docitemsitems = new List<docitems>();
            this.docpaymtitems = new List<docpaymt>();
            this.fondoitems = new List<fondo>();
            this.docpaymentupsclitems = new List<docpaymentupscl>();
            this.docpaymentreservitems = new List<docpaymentreserv>();
            this.sourcedataitems = new List<bankaccountsourcedata>();
            this.financialstateitemlist = new List<financialstateitem>();
            this.HotelVTH = new List<int>();
        }

        private void ClearFinancialHistory()
        {

        }

        /*******************************
        public void generateReporItem()
        {
            financialstateitem[] items = new financialstateitem[this.fondoitems.Count() + this.docpaymtitems.Count()];

            int count = 0;
                foreach (docpaymt helper in this.docpaymtitems)
                {
                this.addReporItem(helper);

                /* financialstateitem _object = new financialstateitem(helper.description, helper.aplicationDate, helper.chargedAmount * -1);
                items[count] = _object; 
                count++;
                }
                foreach (fondo helper in fondo)
                {
                if (helper.MontoCargo < 0)
                {
                    financialstateitem _object = new financialstateitem("*** Movimiento de salida, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName  + " ***", helper.fechaEntrega, helper.MontoCargo);
                    items[count] = _object;
                }
                else
                {
                    financialstateitem _object = new financialstateitem("*** Movimiento de entrada, cuenta origen " + helper.FinancialMethodName + " a cuenta destino " + helper.PaymentMethodName + " ***", helper.fechaEntrega, helper.MontoCargo);
                    items[count] = _object;
                }
                
                count++;
                }

            foreach (fondo helper in this.fondoitems)
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
            List<tblfondos> fondos = unity.FondosRepository.Get(t => t.tblbankaccount.idBAccount == idpaymentMethod && t.fondofechaEntrega >= start && t.fondofechaEntrega <= end, null, "tblbankaccount.Currency,tblbankaccount.tblcompanies,tblbankaccount").ToList();
            return fondos;
        }

        private IEnumerable<tblfondos> getAnualBudgetsTo(DateTime start, DateTime end, int idpaymentMethod, decimal ammountStart, decimal ammountEnd)
        {
            List<tblfondos> fondos = new List<tblfondos>();

            using (var db = new vtaccountingVtclubContext())
            {
                var query = db.tblfondos.Include(v => v.tblbankaccount.Currency).Include(v => v.tblbankaccount.tblcompanies).Include(v => v.tblbankaccount).Include(v => v.tblbankaccount.tblbank).Include(v => v.tblbankaccount1.Currency).Include(v => v.tblbankaccount1.tblcompanies).Include(v => v.tblbankaccount1).Include(v => v.tblbankaccount1.tblbank);

                query = query.Where(c => c.tblbankaccount.idBAccount == idpaymentMethod).Where(c => c.fondofechaEntrega >= start).Where(c => c.fondofechaEntrega <= end);

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
            List<tblfondos> fondos = unity.FondosRepository.Get(t => t.tblbankaccount1.idBAccount == idpaymentMethod && t.fondofechaEntrega >= start && t.fondofechaEntrega <= end, null, "tblbankaccount.Currency,tblbankaccount.tblcompanies,tblbankaccount").ToList();
            foreach (tblfondos model in fondos)
            {
                model.fondoMonto = model.fondoMonto * -1;
            }

            return fondos;
        }

        private IEnumerable<tblfondos> getAnualBudgetsFrom(DateTime start, DateTime end, int idpaymentMethod, decimal ammountStart, decimal ammountEnd)
        {
            List<tblfondos> fondos = new List<tblfondos>();

            using (var db = new vtaccountingVtclubContext())
            {
                var query = db.tblfondos.Include(v => v.tblbankaccount1.Currency).Include(v => v.tblbankaccount1.tblcompanies).Include(v => v.tblbankaccount1).Include(v => v.tblbankaccount1.tblbank).Include(v => v.tblbankaccount.Currency).Include(v => v.tblbankaccount.tblcompanies).Include(v => v.tblbankaccount).Include(v => v.tblbankaccount.tblbank);

                query = query.Where(c => c.tblbankaccount1.idBAccount == idpaymentMethod).Where(c => c.fondofechaEntrega >= start).Where(c => c.fondofechaEntrega <= end);

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
            List<tblpayment> payments = unity.PaymentsVtaRepository.Get(t => t.tblbankaccount.idbaccount == idBankAccount && t.tblinvoice.invoiceApplicationDate >= start && t.tblinvoice.invoiceApplicationDate <= end, null, "tblbankaccount.Currency,tblbankaccount.tblcompanies,tblbankaccount").ToList();
            return payments;
        }

        private IEnumerable<tblpayment> getAnualPayments(DateTime start, DateTime end, int idBankAccount, decimal ammountStart, decimal ammountEnd)
        {
            List<tblpayment> payments = new List<tblpayment>();

            using (var db = new vtaccountingVtclubContext())
            {
                var query = db.tblpayment.Include(c => c.tblbankaccount.Currency).Include(c => c.tblbankaccount.tblcompanies).Include(c => c.tblbankaccount).Include(c => c.tblinvoice.tblinvoiceditem).Include(c => c.tblinvoice.tblcompanies).Include(c => c.tblinvoice.tblusers.tblprofilesaccounts);

                query = query.Where(c => c.tblbankaccount.idBAccount == idBankAccount).Where(c => c.applicationDate >= start).Where(c => c.applicationDate <= end);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.chargedAmount >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.chargedAmount <= ammountEnd);
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
            List<tblinvoiceditem> invoicesitems = unity.PaymentsVtaRepository.Get(t => t.tblinvoice.idcompany == idcompany && t.tblinvoice.invoiceApplicationDate >= start && t.tblinvoice.invoiceApplicationDate <= end, null, "tblbankaccount.Currency,tblbankaccount.tblcompanies,tblbankaccount,tblinvoice.tblinvoiceditem").SelectMany(y => y.tblinvoice.tblinvoiceditem).ToList();
            return invoicesitems;
        }
        #endregion

        #region  Upscales

        /************ Upscales */
        private IEnumerable<invoicepayment> getAnualPaymentsUPSCL(DateTime start, DateTime end, int idCurrency, int idPaymentMethodUpscl, int[] idHotelVTH)
        {
            List<invoicepayment> paymentsUpscl = unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.idCurrency == idCurrency && t.idPaymentMethod == idPaymentMethodUpscl && idHotelVTH.Contains(t.Invoice1.idHotel), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();
            return paymentsUpscl;
        }

        private IEnumerable<invoicepayment> getAnualPaymentsUPSCL(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodUpscl, int[] idHotelVTH)
        {
            List<invoicepayment> paymentsUpscl = unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.idCurrency == idCurrency && idPaymentMethodUpscl.Contains(t.idPaymentMethod) && idHotelVTH.Contains(t.Invoice1.idHotel), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();
            return paymentsUpscl;
        }

        private IEnumerable<invoicepayment> getAnualPaymentsUPSCL(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodUpscl, int[] idHotelVTH, decimal ammountStart, decimal ammountEnd)
        {
            List<invoicepayment> paymentsUpscl = new List<invoicepayment>();

            using (var db = new vtaccountingVtclubContext())
            {
                // unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.idCurrency == idCurrency && idPaymentMethodUpscl.Contains(t.idPaymentMethod) && idHotelVTH.Contains(t.Invoice1.idHotel), null, 
                //     "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();

                var query = db.Payment.Include(c => c.Invoice1).Include(c => c.Invoice1.Currency).Include(c => c.Invoice1.tblhotels).Include(v => v.Invoice1).Include(c => c.tblpaymentmethods);

                query = query.Where(c => c.applicationDate >= start && c.applicationDate <= end && c.Invoice1.Currency.idCurrency == idCurrency && idPaymentMethodUpscl.Contains(c.idPaymentMethod) && idHotelVTH.Contains(c.Invoice1.idHotel));

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.chargedAmount >= (double)ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.chargedAmount <= (double)ammountEnd);
                }

                paymentsUpscl = query.ToList();
            }
            return paymentsUpscl;
        }


        // Getting by Terminals
        private IEnumerable<invoicepayment> getAnualPaymentsUPSCLTPV(DateTime start, DateTime end, int idCurrency, int idTpv)
        {
            List<invoicepayment> paymentsUpscl = unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.idCurrency == idCurrency && (t.PaymentInstrument.Select(v => v.idTerminal).ToList()).Contains(idTpv), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();
            return paymentsUpscl;
        }

        private IEnumerable<invoicepayment> getAnualPaymentsUPSCLTPV(DateTime start, DateTime end, int idCurrency, int idTpv, decimal ammountStart, decimal ammountEnd, int[] baccounthotels)
        {
            List<invoicepayment> paymentsUpscl = new List<invoicepayment>();

            //    unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.idCurrency == idCurrency && (t.PaymentInstrument.Select(v => v.idTerminal).ToList()).Contains(idTpv), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();

            using (var db = new vtaccountingVtclubContext())
            {

                var query = db.Payment.Include(c => c.Invoice1).Include(c => c.Invoice1.Currency).Include(c => c.Invoice1.tblhotels).Include(c => c.tblpaymentmethods);

                query = query.Where(c => c.applicationDate >= start && c.applicationDate <= end && c.Invoice1.Currency.idCurrency == idCurrency && (c.PaymentInstrument.Select(v => v.idTerminal).ToList()).Contains(idTpv));

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.chargedAmount >= (double)ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.chargedAmount <= (double)ammountEnd);
                }

                if (baccounthotels.Count() != 0)
                {
                    query = query.Where(c => baccounthotels.Contains(c.Invoice1.idHotel));
                }

                paymentsUpscl = query.ToList();
            }


            return paymentsUpscl;
        }

        // Getting by BankAccount-Hotels
        private IEnumerable<invoicepayment> getAnualPaymentsUPSCLBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodUpscl, int[] baccounthotels)
        {
            List<invoiceitems> paymentsUpscl = unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.Currency.idCurrency == idCurrency && idPaymentMethodUpscl.Contains(t.idPaymentMethod) && baccounthotels.Contains(t.Invoice1.idHotel), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();
            return paymentsUpscl;
        }

        private IEnumerable<invoiceitems> getAnualPaymentsUPSCLBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodUpscl, int[] baccounthotels, decimal ammountStart, decimal ammountEnd)
        {

            List<invoiceitems> paymentsUpscl = new List<invoiceitems>();
            //unity.PaymentsUPSCLRepository.Get(t => t.applicationDate >= start && t.applicationDate <= end && t.Invoice1.Currency.idCurrency == idCurrency && idPaymentMethodUpscl.Contains(t.idPaymentMethod) && baccounthotels.Contains(t.Invoice1.idHotel), null, "Invoice1,tblpaymentmethods,Invoice1.Currency,Invoice1.tblhotels").ToList();

            using (var db = new vtaccountingVtclubContext())
            {
                var query = db.Payment.Include(c => c.Invoice1).Include(c => c.Invoice1.Currency).Include(c => c.Invoice1.tblhotels).Include(c => c.tblpaymentmethods);

                query = query.Where(c => c.applicationDate >= start && c.applicationDate <= end && c.Invoice1.Currency.idCurrency == idCurrency && idPaymentMethodUpscl.Contains(c.idPaymentMethod) && baccounthotels.Contains(c.Invoice1.idHotel));

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.chargedAmount >= (double)ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.chargedAmount <= (double)ammountEnd);
                }

                paymentsUpscl = query.ToList();
            }
            return paymentsUpscl;
        }
        #endregion

        #region  Reservations
        /************ Reservations */
        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERV(DateTime start, DateTime end, int idCurrency, int idPaymentMethodReserv, int[] idHotelVTH)
        {
            List<tblreservationpayments> paymentsReserv = unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && t.idPaymentMethod == idPaymentMethodReserv && idHotelVTH.Contains(t.tblreservations.idHotel), null, "tblpaymentmethods,tblreservations,tblreservations.Currency,tblreservations.tblhotels").ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERV(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelVTH)
        {
            List<tblreservationpayments> paymentsReserv = unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idPaymentMethodReserv.Contains(t.idPaymentMethod) && idHotelVTH.Contains(t.tblreservations.idHotel), null, "tblpaymentmethods,tblreservations,tblreservations.Currency,tblreservations.tblhotels").ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERV(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] idHotelVTH, decimal ammountStart, decimal ammountEnd)
        {
            List<tblreservationpayments> paymentsReserv = new List<tblreservationpayments>();
            // unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idPaymentMethodReserv.Contains(t.idPaymentMethod) && idHotelVTH.Contains(t.tblreservations.idHotel), null, 
            //     "tblpaymentmethods,tblreservations,tblreservations.Currency,tblreservations.tblhotels").ToList();

            using (var db = new vtaccountingVtclubContext())
            {
                var query = db.tblreservationpayments.Include(c => c.tblpaymentmethods).Include(c => c.tblreservations).Include(c => c.tblreservations.Currency).Include(c => c.tblreservations.tblhotels);

                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idPaymentMethodReserv.Contains(t.idPaymentMethod) && idHotelVTH.Contains(t.tblreservations.idHotel));

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentQuantity >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentQuantity <= ammountEnd);
                }

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        // Getting by Terminals
        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERVTPV(DateTime start, DateTime end, int idCurrency, int[] idTpv)
        {
            List<tblreservationpayments> paymentsReserv = unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idTpv.Contains((int)t.idTerminal)).ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERVTPV(DateTime start, DateTime end, int idCurrency, int[] idTpv, decimal ammountStart, decimal ammountEnd, int[] baccounthotels)
        {
            List<tblreservationpayments> paymentsReserv = new List<tblreservationpayments>();
            // unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idTpv.Contains((int)t.idTerminal)).ToList();

            using (var db = new vtaccountingVtclubContext())
            {
                db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var query = db.tblreservationpayments.Include(c => c.tblpaymentmethods).Include(c => c.tblreservations).Include(c => c.tblreservations.Currency).Include(c => c.tblreservations.tblhotels);
                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idTpv.Contains((int)t.idTerminal));



                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentQuantity >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentQuantity <= ammountEnd);
                }

                if (baccounthotels.Count() != 0)
                {
                    query = query.Where(c => baccounthotels.Contains(c.tblreservations.idHotel));
                }

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        // Getting by BankAccount-Hotels
        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERVBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] baccounthotels)
        {
            List<tblreservationpayments> paymentsReserv = unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idPaymentMethodReserv.Contains(t.idPaymentMethod) && baccounthotels.Contains(t.tblreservations.idHotel), null, "tblpaymentmethods,tblreservations,tblreservations.Currency,tblreservations.tblhotels").ToList();
            return paymentsReserv;
        }

        private IEnumerable<tblreservationpayments> getAnualPaymentsRESERVBAccountHotels(DateTime start, DateTime end, int idCurrency, int[] idPaymentMethodReserv, int[] baccounthotels, decimal ammountStart, decimal ammountEnd)
        {
            List<tblreservationpayments> paymentsReserv = new List<tblreservationpayments>();

            //unity.PaymentsRESERVRepository.Get(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idPaymentMethodReserv.Contains(t.idPaymentMethod) && baccounthotels.Contains(t.tblreservations.idHotel), null, 
            // "tblpaymentmethods,tblreservations,tblreservations.Currency,tblreservations.tblhotels").ToList();

            using (var db = new vtaccountingVtclubContext())
            {
                var query = db.tblreservationpayments.Include(c => c.tblpaymentmethods).Include(c => c.tblreservations).Include(c => c.tblreservations.Currency).Include(c => c.tblreservations.tblhotels);

                query = query.Where(t => t.reservationPaymentDate >= start && t.reservationPaymentDate <= end && t.tblreservations.Currency.idCurrency == idCurrency && idPaymentMethodReserv.Contains(t.idPaymentMethod) && baccounthotels.Contains(t.tblreservations.idHotel));

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.reservationPaymentQuantity >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.reservationPaymentQuantity <= ammountEnd);
                }

                paymentsReserv = query.ToList();
            }

            return paymentsReserv;
        }

        #endregion

        #region Income Movements
        private IEnumerable<tblincomemovement> getAnualIncomeMovements(DateTime start, DateTime end, int idbankaccount)
        {
            List<tblincomemovement> incomemovements = unity.IncomeMovementsRepository.Get(t => t.tblbankaccount.idBAccount == idbankaccount && t.tblincome.incomeApplicationDate >= start && t.tblincome.incomeApplicationDate <= end, null, "tblincome.tblcompanies.tblsegment, tblincome.tblusers.tblprofilesaccounts, tblbankaccnttype, tblbankaccount, tbltpv, tblbankaccount.tblbank, tblbankaccount.Currency, tblbankaccount.tblcompanies").ToList();
            return incomemovements;
        }

        private IEnumerable<tblincomemovement> getAnualIncomeMovements(DateTime start, DateTime end, int idbankaccount, decimal ammountStart, decimal ammountEnd)
        {
            List<tblincomemovement> incomemovements = new List<tblincomemovement>();

            unity.IncomeMovementsRepository.Get(t => t.tblbankaccount.idBAccount == idbankaccount && t.tblincome.incomeApplicationDate >= start && t.tblincome.incomeApplicationDate <= end, null,

                "tblincome.tblcompanies.tblsegment, tblincome.tblusers.tblprofilesaccounts, tblbankaccnttype, tblbankaccount, tbltpv, tblbankaccount.tblbank, tblbankaccount.Currency, tblbankaccount.tblcompanies").ToList();

            using (var db = new vtaccountingVtclubContext())
            {
                var query = db.tblincomemovement.Include(c => c.tblincome.tblcompanies.tblsegment).Include(c => c.tblincome.tblusers.tblprofilesaccounts).Include(c => c.tblbankaccnttype).Include(c => c.tblbankaccount).Include(c => c.tblbankaccount.tblbank).Include(c => c.tblbankaccount.Currency).Include(c => c.tblbankaccount.Currency);

                query = query.Where(t => t.tblbankaccount.idBAccount == idbankaccount && t.tblincome.incomeApplicationDate >= start && t.tblincome.incomeApplicationDate <= end);

                if (ammountStart != 0)
                {
                    query = query.Where(c => c.incomemovchargedAmount >= ammountStart);
                }

                if (ammountEnd != 0)
                {
                    query = query.Where(c => c.incomemovchargedAmount <= ammountEnd);
                }

                incomemovements = query.ToList();
            }
            return incomemovements;
        }

        #endregion						
        #endregion

    }
}