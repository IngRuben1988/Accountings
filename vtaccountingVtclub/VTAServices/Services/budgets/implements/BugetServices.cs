using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VTAworldpass.Business.Services.Implementations;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.budgets.helpers;
using VTAworldpass.VTAServices.Services.invoices;
using VTAworldpass.VTAServices.Services.Logger;
using VTAworldpass.VTAServices.Services.Models;
using VTAworldpass.VTAServices.Services.utilsapp;
using VTAworldpass.VTAServices.Services.budgets.model;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTACore.Logger;
using System.Data.Entity.Core;
using VTAworldpass.VTAServices.Services.invoices.implements;
using VTAworldpass.VTACore.Cores.Globales;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTAServices.Services.budgets.implements
{
    public class BugetServices : budgetHelper, IBudgetServices
    {
        UnitOfWork unity = new UnitOfWork();
        private BudgetRepository budgetRepository;
        private readonly IAccountServices accountServices;
        private readonly ILogsServices logsServices;
        private readonly IUtilsappServices utilsappServices;
        private readonly IInvoiceServices invoiceServices;

        public BugetServices(IAccountServices _accountServices, ILogsServices _logsServices, IInvoiceServices _invoiceServices, IUtilsappServices _utilsappServices)
        {
            budgetRepository = new BudgetRepository();
            accountServices = _accountServices;
            logsServices = _logsServices;
            invoiceServices = _invoiceServices;
            utilsappServices = _utilsappServices;
        }

        public tblfondos getBudget(int idHotel, string date, int idpaymentMethod)
        {

            List<tblfondos> listmodel = this.getAnualBusgets(idHotel, date, idpaymentMethod);
            DateTime find = (DateTime)DateTimeUtils.ParseStringToDate(date);

            if (listmodel.Count() != 0)
            {
                return this.selectBudget(listmodel, find);
            }
            else { return null; }

        }

        public financialstateModel getBudgetAvailable(int Company, DateTime date, int idBankAccount)
        {
            // GEtting date today nd calculate next Sunday to add the last --
            // Gerting Balance to ApplicationDate
            // DateTime endDate = utilsappServices.getSundayCurrentWeek(date);
            return getTotalFinanceState(idBankAccount, null, date);
        }

        public financialstateModel getBudgetAvailable(int Company, DateTime date, int idBankAccount, int BankAccntType)
        {
            // GEtting date today nd calculate next Sunday to add the last --
            // Gerting Balance to ApplicationDate
            // DateTime endDate = utilsappServices.getSundayCurrentWeek(date);
            return getTotalFinanceState(idBankAccount, null, date, BankAccntType);
        }

        public async Task<List<fondoModel>> getWeeklyBudgets(int? Type, int? id, int? idPaymentMethod, int? idCurrency, DateTime? fondofechaEntrega, DateTime? fondoFechaInicio, DateTime? fondoFechaFin, decimal? fondoMontoInicio, decimal? fondoMontoFin, int? idCompany)
        {

            List<fondoModel> lstFondos = new List<fondoModel>();
            List<fondoModel> listFondosSend = new List<fondoModel>();
            // List<fondoModel> listDinamic = new List<fondoModel>();
            List<invoicepayment> listDocpayments = new List<invoicepayment>();

            /** Si no hay fechas de inicio y fin, se predeterminan en base a la fecha actual**/
            fondoFechaInicio = fondoFechaInicio == null ? utilsappServices.getMondayCurrentWeek(DateTime.Now.AddMonths(Globals.MonthstoSearchTblFondos * -1)) : fondoFechaInicio;
            fondoFechaFin = fondoFechaFin == null ? utilsappServices.getSundayCurrentWeek(DateTime.Now) : fondoFechaFin;

            /** obteniendo las empresas para mostrar solo las empresas **/
            int[] listHotles = new int[accountServices.AccountCompanies().Count];
            listHotles = accountServices.AccountCompanies().ToArray();

            lstFondos = this.convertTbltoHelper(await this.budgetRepository.gettblBudgetSearch(Type, id, idPaymentMethod, idCurrency, fondofechaEntrega, fondoFechaInicio, fondoFechaFin, fondoMontoInicio, fondoMontoFin, idCompany, listHotles)).ToList();


            foreach (fondoModel helper in lstFondos)
            {
                helper.parseAmmountsToString();
                helper.parseAmmountEnumerablesToString();
                listFondosSend.Add(helper);
            }

            return listFondosSend.OrderByDescending(y => y.FechaInicio).ThenBy(t => t.CompanyOrder).ToList();
        }

        public fondoModel GetFondo(int idFondo)
        {
            fondoModel helper = new fondoModel();

            var _result = this.unity.FondosRepository.Get(x => x.idFondos == idFondo, null, "tblbankaccount.Currency,tblbankaccount.tblbank,tblbankaccount.tblcompanies,tblbankaccount1.Currency,tblbankaccount1.tblbank,tblbankaccount1.tblcompanies").FirstOrDefault();

            if (_result != null) { convertTbltoHelper(_result, helper); }

            return helper;
        }

        public async Task<fondoModel> AddFondo(fondoModel helper)
        {
            bool _savedDocument = false;
            invoice dochelper = new invoice();

            // Si hay diferente de cero en FondoFee, se genera un movimiento para el E-R
            if (helper.FondoFee != 0)
            {
                // Obteniendo los datos del movimiento para agregar al ER, 
                // var _fondo = this.GetFondo(helper.id);
                var _bankAccocuntSourceData = this.unity.BankAccountRepository.Get(x => x.idbaccount == helper.FinancialMethod, null, "tblcurrencies,tblbank,tblcompanies").FirstOrDefault();
                var _parameterAccl4Value = this.unity.ParametersVTARepository.Get(x => x.parameterShortName.Contains("accl4TaxbyBudget"), null, "").FirstOrDefault();
                if (_parameterAccl4Value == null) Log.Error("No se puede obtener el valor accl4TaxbyBudget. "); else Log.Info("La cuenta para movimiento Fondos E-R : " + _parameterAccl4Value.parameterValue);

                if (_parameterAccl4Value.parameterValue != null)
                {
                    // Generando el registro para añadir al estado de resultados (Invoice)
                    dochelper = makeInvoicebyFondo(_bankAccocuntSourceData, helper);

                    // Generando registro para añadir al esta de Resultados (Invoiceditem)
                    invoiceitems docitemHelper = new invoiceitems();

                    docitemHelper = makeInvoicedItembyFondo(helper, Convert.ToInt32(_parameterAccl4Value.parameterValue));
                    docitemHelper.description = string.Concat(docitemHelper.description, ",", _bankAccocuntSourceData.baccountshortname, "-", _bankAccocuntSourceData.tblcurrencies.currencyAlphabeticCode);

                    // Generando registro para añadir al estado de Resultados (Payments)
                    invoicepayment docpaymentHelper = new invoicepayment();
                    docpaymentHelper = makePaymentbyFondo(helper);
                    docpaymentHelper.authRef = string.Concat(docpaymentHelper.authRef, ",", _bankAccocuntSourceData.baccountshortname, "-", _bankAccocuntSourceData.tblcurrencies.currencyAlphabeticCode);

                    try
                    {
                        dochelper = await this.invoiceServices.SaveInvoiceByBudget(dochelper, docitemHelper, docpaymentHelper);
                        _savedDocument = true;
                        Log.Info("No es posible guardar el documento para el E-R mediante Fondo");
                    }

                    catch (Exception e)
                    {
                        Log.Error("No es posible guardar el documento para el E-R mediante Fondo", e);
                        _savedDocument = false;
                    }
                }

                else
                {
                    Log.Error("Se puede obtener el parámetro accl4TaxbyBudget, pero no tiene valor para generar el registro para el E-R");
                }

                // Guardando el fondo despues de generar el Registro de Estado de Resultados (Invoice-InvoicedItem,Payment)
                try
                {
                    // preparando para guradar
                    tblfondos model = new tblfondos();

                    if (_savedDocument == true && helper.FondoFee != 0) { helper.Invoice = dochelper.id; }

                    this.PrepareToSave(helper, model);
                    model.fondoCreatedby = this.accountServices.AccountIdentity();

                    //Saving principal
                    this.unity.FondosRepository.Insert(model);
                    this.unity.Commit();

                    //Saving log
                    this.logsServices.addTblLog(model, model.idFinanceType != null ? "Alta de registro tblFondo tipo Financiamiento" : "Alta de registro tblFondo");
                    helper.id = model.idFondos;
                }

                catch (Exception e)
                {
                    // Si se genera un error al guardar el fondo, se elimina de igual forma el movimiento del E-R
                    var _result = await this.invoiceServices.DeleteInvoice(dochelper.id);
                    throw new Exception(e.Message);
                }
            }

            else
            {
                // Guardando el fondo despues de generar el Registro de Estado de Resultados
                try
                {
                    // preparando para guradar
                    tblfondos model = new tblfondos();

                    this.PrepareToSave(helper, model);
                    model.fondoCreatedby = this.accountServices.AccountIdentity();

                    //Saving principal
                    this.unity.FondosRepository.Insert(model);
                    this.unity.Commit();

                    //Saving log
                    this.logsServices.addTblLog(model, model.idFinanceType != null ? "Alta de registro tblFondo tipo Financiamiento" : "Alta de registro tblFondo");
                    helper.id = model.idFondos;
                }

                catch (Exception e)
                {
                    this.unity.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return helper;
        }

        public async Task<fondoModel> UpdateFondoAsync(fondoModel helper)
        {
            bool _savedInvoice = false;
            tblfondos model = unity.FondosRepository.Get(t => t.idFondos == helper.id).FirstOrDefault();

            // Si hay diferente de cero en FondoFee, se genera un movimiento para el E-R
            if (helper.FondoFee != 0)
            {
                var _bankAccocuntSource = this.unity.BankAccountRepository.Get(x => x.idbaccount == helper.FinancialMethod, null, "tblcurrencies,tblbank,tblcompanies").FirstOrDefault();
                var _parameterValue = this.unity.ParametersVTARepository.Get(x => x.parameterShortName.Contains("accl4TaxbyBudget"), null, "").FirstOrDefault();

                if (_parameterValue == null) Log.Error("No se puede obtener el valor accl4TaxbyBudget."); else Log.Info("La cuenta para movimiento Fondos E-R: " + _parameterValue.parameterValue);

                if (_parameterValue.parameterValue != null)
                {
                    if ((model.fondoInvoice != 0) && (unity.InvoiceRepository.Get(i => i.idinvoice == model.fondoInvoice).Count() > 0))
                    {
                        tblinvoice prev_inv = unity.InvoiceRepository.Get(i => i.idinvoice == model.fondoInvoice).FirstOrDefault();
                        tblinvoiceditem prev_invitem = unity.InvoiceItemRepository.Get(i => i.idinvoice == model.fondoInvoice).FirstOrDefault() ?? new tblinvoiceditem();
                        tblpayment prev_invpay = unity.PaymentsVtaRepository.Get(i => i.idinvoice == model.fondoInvoice).FirstOrDefault() ?? new tblpayment();

                        prev_inv.idcompany = _bankAccocuntSource.idcompany;
                        prev_inv.invoicedate = (DateTime)DateTimeUtils.ParseStringToDate(helper.fechaEntregaString);
                        prev_inv.invoiceupdatedby = accountServices.AccountIdentity();
                        prev_inv.invoiceupdateon = DateTime.Now;
                        prev_inv.idcurrency = _bankAccocuntSource.idcurrency;
                        unity.InvoiceRepository.Update(prev_inv);
                        unity.Commit();

                        prev_invitem.idinvoice = prev_inv.idinvoice;
                        prev_invitem.idaccountl4 = int.Parse(_parameterValue.parameterValue);
                        prev_invitem.idinvoiceitemstatus = (new InvoiceServices()).CalculateDociItemStatusToSave(prev_inv, prev_invitem, utilsappServices);
                        prev_invitem.iduser = accountServices.AccountIdentity();
                        prev_invitem.itemsubtotal = helper.FondoFee;
                        prev_invitem.itemdescription = helper.comments;
                        prev_invitem.ditemistax = false;
                        prev_invitem.itemtax = 0;
                        prev_invitem.itemidentifier = helper.comments;
                        prev_invitem.idsupplier = 1;
                        prev_invitem.itemsupplierother = null;
                        prev_invitem.itemothertax = 0;
                        prev_invitem.itemsinglepayment = true;
                        prev_invitem.idbudgettype = 1;
                        unity.InvoiceItemRepository.Update(prev_invitem);
                        unity.Commit();

                        prev_invpay.idinvoice = prev_inv.idinvoice;
                        prev_invpay.idbaccount = helper.FinancialMethod;
                        prev_invpay.idbankprodttype = (int)BankAccounType.Transferencias;
                        prev_invpay.paymentdate = (DateTime)DateTimeUtils.ParseStringToDate(helper.fechaEntregaString);
                        prev_invpay.paymentamount = (decimal)helper.FondoFee;
                        prev_invpay.paymentauthref = helper.comments;
                        prev_invpay.paymentcreatedby = accountServices.AccountIdentity();
                        prev_invpay.paymentdate = prev_inv.invoicedate;
                        prev_invpay.paymentupdatedby = accountServices.AccountIdentity();
                        prev_invpay.paymentupdatedon = DateTime.Now;
                        unity.PaymentsVtaRepository.Update(prev_invpay);
                        unity.Commit();

                        model.fondoInvoice = (int)prev_inv.idinvoice;
                        helper.Invoice = (int)model.fondoInvoice;
                    }

                    else
                    {
                        tblinvoice new_inv = new tblinvoice();
                        tblinvoiceditem new_invitem = new tblinvoiceditem();
                        tblpayment new_invpay = new tblpayment();

                        var new_num = unity.InvoiceRepository.Get(x => x.idcompany == _bankAccocuntSource.idcompany, null, "");
                        int new_number = new_num.Count() == 0 ? Variables.OneInt : new_num.Max(y => y.invoicenumber) + Variables.OneInt;

                        new_inv.idcompany = _bankAccocuntSource.idcompany;
                        new_inv.invoicedate = (DateTime)DateTimeUtils.ParseStringToDate(helper.fechaEntregaString);
                        new_inv.invoicecreateon = DateTime.Now;
                        new_inv.invoicecreatedby = accountServices.AccountIdentity();
                        new_inv.invoiceupdatedby = accountServices.AccountIdentity();
                        new_inv.invoiceupdateon = DateTime.Now;
                        new_inv.idcurrency = _bankAccocuntSource.idcurrency;
                        new_inv.invoicenumber = new_number;
                        unity.InvoiceRepository.Insert(new_inv);
                        unity.Commit();

                        new_invitem.idinvoice = new_inv.idinvoice;
                        new_invitem.idaccountl4 = int.Parse(_parameterValue.parameterValue);
                        new_invitem.idinvoiceitemstatus = (new InvoiceServices()).CalculateDociItemStatusToSave(new_inv, new_invitem, utilsappServices);
                        new_invitem.iduser = accountServices.AccountIdentity();
                        new_invitem.itemsubtotal = helper.FondoFee;
                        new_invitem.itemdescription = helper.comments;
                        new_invitem.ditemistax = false;
                        new_invitem.itemtax = 0;
                        new_invitem.itemidentifier = helper.comments;
                        new_invitem.idsupplier = 1;
                        new_invitem.itemsupplierother = null;
                        new_invitem.itemothertax = 0;
                        new_invitem.itemsinglepayment = true;
                        new_invitem.idbudgettype = 1;
                        unity.InvoiceItemRepository.Insert(new_invitem);
                        unity.Commit();

                        new_invpay.idinvoice = new_inv.idinvoice;
                        new_invpay.idbaccount = helper.FinancialMethod;
                        new_invpay.idbankprodttype = (int)BankAccounType.Transferencias;
                        new_invpay.paymentdate = (DateTime)DateTimeUtils.ParseStringToDate(helper.fechaEntregaString);
                        new_invpay.paymentamount = (decimal)helper.FondoFee;
                        new_invpay.paymentauthref = helper.comments;
                        new_invpay.paymentcreatedby = accountServices.AccountIdentity();
                        new_invpay.paymentcreateon = new_inv.invoicecreateon;
                        new_invpay.paymentupdatedby = accountServices.AccountIdentity();
                        new_invpay.paymentupdatedon = DateTime.Now;
                        
                        unity.PaymentsVtaRepository.Insert(new_invpay);
                        unity.Commit();

                        model.fondoInvoice = (int)new_inv.idinvoice;
                        helper.Invoice = (int)model.fondoInvoice;
                    }
                }

                else
                {
                    Log.Error("Se puede obtener el parámetro accl4TaxbyBudget, pero no tiene valor para generar el registro para el E-R.");
                }
            }

            else
            {
                if (model.fondoInvoice != 0)
                {
                    try
                    {
                        if (unity.InvoiceRepository.Get(i => i.idinvoice == model.fondoInvoice).Count() > 0) await invoiceServices.DeleteInvoice((int)model.fondoInvoice);
                    }

                    catch (Exception e)
                    {
                        Log.Error("No se puede eliminar el fondo, dependencia con Invoice " + model.fondoInvoice, e);
                        throw new Exception("No se puede borrar el fondo, dependencia con Invoice " + model.fondoInvoice, e);
                    }

                    model.fondoInvoice = null;
                    helper.Invoice = null;
                }
            }

            PrepareToSave(helper, model);

            //Updating Fondo
            unity.FondosRepository.Update(model);
            unity.Commit();

            //Saving Log
            logsServices.addTblLog(model, model.idFinanceType != null ? "Actualización de registro tblFondo tipo Financiamiento" : "Actualización de registro tblFondo");

            return helper;
        }

        public async Task<fondoModel> DeleteFondo(fondoModel helper)
        {
            tblfondos model = new tblfondos();
            model = unity.FondosRepository.Get(t => t.idFondos == helper.id).FirstOrDefault();

            if (model.fondoInvoice != null)
            {
                try
                {
                    if (unity.InvoiceRepository.Get(i => i.idinvoice == model.fondoInvoice).Count() > 0) await invoiceServices.DeleteInvoice((int)model.fondoInvoice);
                }

                catch (Exception e)
                {
                    Log.Error("No se puede eliminar el fondo, dependencia con Invoice " + model.fondoInvoice, e);
                    throw new Exception("No se puede borrar el fondo, dependencia con Invoice " + model.fondoInvoice, e);
                }
            }

            // Deleting fondo
            unity.FondosRepository.Delete(model);
            unity.Commit();

            //Saving Log
            logsServices.addTblLog(model, "Borrado de registro tblFondo");

            return helper;
        }

        // Devuelve el monto de todos los pagos aplicados en un un rago de fechas, hotel, moneda y método de pago.
        public decimal getInvoicesAmmountAppliedbyDate(int idcompany, DateTime start, DateTime end, int idpaymentMethod)
        {
            List<tblpayment> listmodel = unity.PaymentsVtaRepository.Get(t => t.tblinvoice.tblcompanies.idcompany == idcompany
            && t.tblbankaccount.idbaccount == idpaymentMethod && t.tblinvoice.invoicedate >= start && t.tblinvoice.invoicedate <= end, null, "").ToList();

            if (listmodel != null)
            {
                return listmodel.Sum(t => t.paymentamount);
            }

            else
            {
                return Variables.ZeroDecimal;
            }
        }

        public decimal getInvoicesAmmountAppliedAnual(int idcompany, DateTime end, int idpaymentMethod)
        {
            List<tblpayment> listmodel = unity.PaymentsVtaRepository.Get(t => t.tblbankaccount.idbaccount == idpaymentMethod && t.tblinvoice.invoicedate.Year >= end.Year && t.tblinvoice.invoicedate <= end, null, "").ToList();

            if (listmodel != null)
            {
                return listmodel.Sum(t => t.paymentamount);
            }

            else
            {
                return Variables.ZeroDecimal;
            }
        }

        public List<tblpayment> getInvoicesAmmountAppliedAnual(DateTime end, int idpaymentMethod)
        {
            List<tblpayment> listmodel = unity.PaymentsVtaRepository.Get(t => t.tblbankaccount.idbaccount == idpaymentMethod && t.tblinvoice.invoicedate.Year >= end.Year && t.tblinvoice.invoicedate <= end, null, "").ToList();
            return listmodel;
        }

        public List<tblpayment> getPaymentsbyBudget(int idcompany, int currency, DateTime start, DateTime end, int idpaymentMethod)
        {
            List<tblpayment> listmodel = unity.PaymentsVtaRepository.Get(t => t.tblinvoice.tblcompanies.idcompany == idcompany /* && t.tblinvoice.Currency.idCurrency == currency */
            && t.tblbankaccount.idbaccount == idpaymentMethod && t.tblinvoice.invoicedate >= start && t.tblinvoice.invoicedate <= end, null, "").ToList();

            if (listmodel != null)
            {
                return listmodel;
            }

            else
            {
                return null;
            }
        }

        /********************************* INICIO ***************************************************************/
        // permite determinar de una lista el objeto tbl fondo a tomar en base a la fecha de aplicación
        protected bool hasChargedBudgetBool(List<fondoModel> fondos, DateTime datetofind, int idPaymentMethod, int idCompany)
        {

            for (int i = 0; i <= fondos.Count() - 1; i++)
            {
                if ((datetofind.CompareTo(fondos[i].FechaInicio) >= 0 && datetofind.CompareTo(fondos[i].FechaFin) <= 0) && fondos[i].PaymentMethod == idPaymentMethod && fondos[i].Company == idCompany)
                {
                    return true;
                }
            }
            return false;
        }

        protected fondoModel hasChargedBudget(List<fondoModel> fondos, DateTime datetofind, int idPaymentMethod, int idCompany)
        {

            for (int i = 0; i <= fondos.Count() - 1; i++)
            {
                if ((datetofind.CompareTo(fondos[i].FechaInicio) >= 0 && datetofind.CompareTo(fondos[i].FechaFin) <= 0) && fondos[i].PaymentMethod == idPaymentMethod && fondos[i].Company == idCompany)
                {
                    return fondos[i];
                }
            }
            return null;
        }

        /********************************* FIN ***************************************************************/
        public fondoModel CalculateBudgetFinalDate(int? PaymentMethod, int? Hotel, int? idCurrency, string fechaSelecion)
        {

            fondoModel helper = new fondoModel();

            DateTime fecha = new DateTime();
            fecha = (DateTime)DateTimeUtils.ParseStringToDate(fechaSelecion);

            int monthIni = 0;
            int monthFin = 0;

            switch (Globals.tipoCalculoFechaFinal)
            {
                // Semana solo de L-D
                case 1:
                    {
                        // Calculo de fecha de inicio  y fin de la fecha seleccionada

                        if (DateTimeUtils.dayOfWeek(fecha) == 1)
                        {
                            helper.FechaInicio = fecha;
                            helper.FechaFin = this.calculateDateEnd(fecha);
                            helper.FechaInicioString = DateTimeUtils.ParseDatetoString(fecha);
                            helper.FechaFinString = DateTimeUtils.ParseDatetoString(this.calculateDateEnd(fecha));
                            monthIni = DateTimeUtils.MonthOfDate((DateTime)helper.FechaInicio);
                            monthFin = DateTimeUtils.MonthOfDate((DateTime)helper.FechaFin);
                        }
                        else
                        {
                            helper.FechaInicio = this.calculateDateIni(fecha); ;
                            helper.FechaFin = this.calculateDateEnd(fecha);
                            helper.FechaInicioString = DateTimeUtils.ParseDatetoString(this.calculateDateIni(fecha));
                            helper.FechaFinString = DateTimeUtils.ParseDatetoString(this.calculateDateEnd(fecha));
                            monthIni = DateTimeUtils.MonthOfDate((DateTime)helper.FechaInicio);
                            monthFin = DateTimeUtils.MonthOfDate((DateTime)helper.FechaFin);
                        }

                        break;
                    }
                case 2:
                    { // Caso por numero de dias 

                        break;///
                    }
                case 3:
                    { //Caso por fechas abiertas 
                        break;
                    }

            }

            return helper;
        }

        public financialstateModel getTotalFinanceState(int idBankAccount, DateTime? startDate, DateTime endDate)
        {
            financialstateModel financialstate = new financialstateModel(DateTime.Now, endDate, idBankAccount, FinancialStateReport.MaxBalance, false);

            return financialstate;
        }

        public financialstateModel getTotalFinanceState(int idBankAccount, DateTime? startDate, DateTime endDate, int idBankAccntType)
        {

            financialstateModel financialstate = new financialstateModel(DateTime.Now, endDate, idBankAccount, FinancialStateReport.MaxBalanceConciliationIn, false, idBankAccntType);
            return financialstate;
        }

        // permite determinar de una lista el objeto tbl fondo a tomar en base a la fecha de aplicación
        private tblfondos selectBudget(List<tblfondos> listmodel, DateTime datetofind)
        {
            for (int i = 0; i <= listmodel.Count - 1; i++)
            {
                if (datetofind.CompareTo(listmodel[i].fondoFechaInicio) >= 0 && datetofind.CompareTo(listmodel[i].fondoFechaFin) <= 0)
                {
                    Debug.WriteLine(" ------->>>>> Esta dentro del rango. " + listmodel[i].fondoFechaInicio.ToShortDateString() + "  <<<<< ------- " + listmodel[i].fondoMonto);
                    return listmodel[i];
                }
            }
            return null;
        }


        private List<tblfondos> selectBudgetS(List<tblfondos> listmodel, DateTime fechaInicio, DateTime fechaFin)
        {
            List<tblfondos> lst = new List<tblfondos>();

            if (listmodel != null)
            {
                for (int i = 0; i <= listmodel.Count - 1; i++)
                {

                    if (fechaInicio.CompareTo(listmodel[i].fondoFechaInicio) >= 0 && fechaFin.CompareTo(listmodel[i].fondoFechaFin) <= 0)
                    {
                        lst.Add(listmodel[i]);
                    }
                }

                return lst;
            }
            else return lst;
        }

        private List<tblfondos> getAnualBusgets(int idHotel, string fecha, int idpaymentMethod)
        {
            DateTime date = (DateTime)DateTimeUtils.ParseStringToDate(fecha);
            List<tblfondos> fondos = unity.FondosRepository.Get(t => t.tblbankaccount.idbaccount == idpaymentMethod && t.fondoFechaInicio.Year >= date.Year && t.fondoFechaFin <= date, null, "tblbankaccount.Currency,tblbankaccount.tblcompanies,tblbankaccount,tblbankaccount1.Currency,tblbankaccount1.tblcompanies,tblbankaccount1").ToList();
            return fondos;
        }

        private List<tblfondos> getAnualBusgetsFrom(int idHotel, string fecha, int idpaymentMethod)
        {
            DateTime date = (DateTime)DateTimeUtils.ParseStringToDate(fecha);
            List<tblfondos> fondos = unity.FondosRepository.Get(t => t.tblbankaccount1.idbaccount == idpaymentMethod && t.fondoFechaInicio.Year >= date.Year && t.fondoFechaFin <= date, null, "tblbankaccount.Currency,tblbankaccount.tblcompanies,tblbankaccount,tblbankaccount1.Currency,tblbankaccount1.tblcompanies,tblbankaccount1").ToList();
            foreach (tblfondos model in fondos)
            { model.fondoMonto = model.fondoMonto * -1; }
            return fondos;
        }

        private IEnumerable<tblfondos> getFondos(int idCompany, int idPaymentMethod, DateTime startDate, DateTime endDate)
        {
            var fondos = unity.FondosRepository.Get(y => y.tblbankaccount.tblcompanies.idcompany == idCompany && y.idPaymentMethod == idPaymentMethod && y.fondoFechaInicio >= startDate && y.fondoFechaFin <= endDate).AsEnumerable();
            return fondos;
        }

        private IEnumerable<tblfondos> getFondosFrom(int idCompany, int idPaymentMethod, DateTime startDate, DateTime endDate)
        {
            var fondos = unity.FondosRepository.Get(y => y.tblbankaccount.tblcompanies.idcompany == idCompany && y.idPaymentMethod == idPaymentMethod && y.fondoFechaInicio >= startDate && y.fondoFechaFin <= endDate).AsEnumerable();
            foreach (tblfondos model in fondos)
            { model.fondoMonto = model.fondoMonto * -1; }
            return fondos;
        }

        /********************* BUDGET LIMITS*************************************/

        public IEnumerable<fondosmaxammountModel> searchBudgetLimiter(fondosmaxammountModel helper)
        {
            return this.convertTbltoHelper(this.budgetRepository.getbudgetLimitSearch(helper));
        }

        public void saveBudgetLimiter(fondosmaxammountModel helper)
        {
            try
            {
                var result = unity.FondosMaxLimitRepository.Get(x => x.idBAccount == helper.PaymentMethod).FirstOrDefault();

                if (result != null)
                    throw new Exception("Ya se encuentra un registro con estas caracteristicas");

                tblfondosmaxammount model = new tblfondosmaxammount();
                this.PrepareToSave(model, helper);
                model.fondosmaxCreatedby = this.accountServices.AccountIdentity();
                this.unity.FondosMaxLimitRepository.Insert(model);
                this.unity.Commit();
                helper.FondosMax = model.idFondosMax;
                this.logsServices.addTblLog(model, "Agregando limite a fondo.");
            }
            catch (EntityException EntityEx)
            {
                this.unity.Rollback();
                throw new Exception(string.Concat("No se puede guardar la los datos.", "error en: ", EntityEx.Message, ",  [Stack Trace ----->>>]", EntityEx.StackTrace));
            }
            catch (Exception e)
            {
                this.unity.Rollback();
                throw new Exception(e.Message);
            }
        }

        public void updateBudgetLimiter(fondosmaxammountModel helper)
        {
            tblfondosmaxammount model = unity.FondosMaxLimitRepository.GetById(helper.FondosMax);
            this.PrepareToUpdate(model, helper);
            this.unity.FondosMaxLimitRepository.Update(model);
            this.unity.Commit();
            this.logsServices.addTblLog(model, "Editando lìmite de fondo.");
        }

        public void deleteBudgetLimiter(int id)
        {
            tblfondosmaxammount model = unity.FondosMaxLimitRepository.Get(x => x.idFondosMax == id, null, "").First();
            this.unity.FondosMaxLimitRepository.Delete(model);
            this.unity.Commit();
            this.logsServices.addTblLog(model, "Eliminando lìmite de fondo.");
        }

        public fondosmaxammountModel fondosmaxammount(int id)
        {
            return convertTbltoHelper(this.unity.FondosMaxLimitRepository.Get(t => t.idFondosMax == id).FirstOrDefault());
        }

        public fondosmaxammountModel getfondosmaxammount(int hotel, int idPaymentMethod)
        {
            var result = this.unity.FondosMaxLimitRepository.Get(t => t.tblbankaccount.idbaccount == idPaymentMethod, null, "tblbankaccount").FirstOrDefault();

            if (result != null)
                return convertTbltoHelper(result);
            else return new fondosmaxammountModel();

        }

        /***********************************************************************/

        public List<fondoModel> convertToModelHelper(List<tblfondos> list)
        {
            return convertTbltoHelper(list);
        }

        public List<invoicepayment> convertToModelHelper(List<tblpayment> list)
        {
            return convertTbltoHelper(list);
        }
    }
}