using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.budgets.model;
using System.Diagnostics;
using VTAworldpass.VTACore.Helpers;

namespace VTAworldpass.VTACore.GenericRepository.Repositories
{
    public class BudgetRepository
    {
        private readonly vtclubdbEntities _db = new vtclubdbEntities();

        public BudgetRepository()
        {

        }

        public async Task<IEnumerable<tblfondos>> gettblBudgetSearch(int? Type, int? id, int? idPaymentMethod, int? idCurrency, DateTime? fondofechaEntrega, DateTime? fondoFechaInicio, DateTime? fondoFechaFin, decimal? fondoMontoInicio, decimal? fondoMontoFin, int? idCompany, int[] companies)
        {
            try
            {

                using (var db = new vtclubdbEntities())
                {
                    // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                    var _query = db.tblfondos.Include(t => t.tblbankaccount).Include(t => t.tblbankaccount.tblbank).Include(t => t.tblbankaccount.tblcurrencies).Include(t => t.tblbankaccount.tblcompanies).Include(t => t.tblbankaccount.tblbankaccntclasification).Include(t => t.tblbankaccount1).Include(t => t.tblbankaccount1.tblbank).Include(t => t.tblbankaccount1.tblcurrencies).Include(t => t.tblbankaccount1.tblcompanies).Include(t => t.tblbankaccount1.tblbankaccntclasification).Include(t => t.tblusers.tblprofilesaccounts).Include(p => p.tblfinancetype);

                    // .Include(t => t.tblbankaccount.Currency).Include(t => t.tblbankaccount.tblcompanies).Include(t => t.tblbankaccount1).Include(t => t.tblusers.tblprofilesaccounts).Include(t=>t.tblbankaccount.tblbank).Include(t=>t.tblbankaccount1.tblbank).Include(t => t.tblbankaccount1.tblcompanies).Include(t => t.tblbankaccount1.Currency);
                    _query = _query.AsNoTracking();

                    if (id != null)
                    {
                        if (Type != null)
                        {
                            _query = _query.Where(x => x.idFondos == id && x.idFinanceType == Type);
                        }
                        else
                        {
                            _query = _query.Where(x => x.idFondos == id && x.idFinanceType == Type);
                        }
                    }
                    else
                    {
                        if (Type != null)
                        {
                            _query = _query.Where(x => x.idFinanceType == Type);
                        }
                        else
                        {
                            _query = _query.Where(x => x.idFinanceType == Type);
                        }

                        if (idCompany > 0)
                        { // Only destination Budget
                            _query = _query.Where(x => x.tblbankaccount.tblcompanies.idcompany == idCompany || x.tblbankaccount1.tblcompanies.idcompany == idCompany);
                        }
                        else
                        {

                            if (companies.Count() != 0)
                            {
                                _query = _query.Where(y => companies.Contains(y.tblbankaccount.tblcompanies.idcompany) || companies.Contains(y.tblbankaccount1.tblcompanies.idcompany));
                            }
                            else
                            {
                                throw new Exception("No tiene asignados empresas para realizar búsquedas.");
                            }
                        }

                        if (idPaymentMethod > 0)
                        {
                            _query = _query.Where(x => x.idPaymentMethod == idPaymentMethod || x.idFinancialMethod == idPaymentMethod);
                        }

                        if (fondoFechaInicio != null)
                        {
                            if (fondoFechaFin != null)
                            {
                                _query = _query.Where(x => x.fondoFechaInicio >= fondoFechaInicio || x.fondoFechaFin <= fondoFechaFin);
                            }
                            else
                            {
                                _query = _query.Where(x => x.fondoFechaInicio >= fondoFechaInicio);
                            }
                        }

                        if (fondoFechaFin != null)
                        {
                            if (fondoFechaInicio != null)
                            {
                                _query = _query.Where(x => x.fondoFechaInicio >= fondoFechaInicio || x.fondoFechaFin <= fondoFechaFin);
                            }
                            else
                            {
                                _query = _query.Where(x => x.fondoFechaFin <= fondoFechaFin);
                            }
                        }

                        if (fondoMontoInicio != null)
                        {
                            _query = _query.Where(x => x.fondoMonto >= fondoMontoInicio);
                        }

                        if (fondoMontoFin != null)
                        {
                            _query = _query.Where(x => x.fondoMonto <= fondoMontoFin);
                        }
                    }

                    return await _query.OrderByDescending(x => x.fondoFechaInicio).Take(Globals.EntityMax150PredefinedResult).ToListAsync();

                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
                throw new Exception("no se puede realizar la cosulta.");
            }

        }


        /// <summary>
        /// Consultando las facturas a partir de un intervalo predeterminado (fecha hoy) o seleccionado desde frontEnd
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>

        public async Task<IEnumerable<tblpayment>> getTblPaymentsSearch(int? idPaymentMethod, DateTime? fondoFechaInicio, DateTime? fondoFechaFin, int? idCompany, int[] companies)
        {
            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var _query = db.tblpayment.Include(t => t.tblbankaccount).Include(t => t.tblbankaccount.tblcompanies).Include(t => t.tblbankaccount.tblcurrencies).Include(t => t.tblinvoice.tblcompanies).Include(t => t.tblinvoice.tblinvoiceditem).Include(t => t.tblinvoice.tblinvoiceditem).Include(t => t.tblinvoice.tblusers.tblprofilesaccounts).Include(t => t.tblinvoice.tblinvoiceditem);
                _query = _query.AsNoTracking();

                if (idPaymentMethod > 0)
                {
                    _query = _query.Where(x => x.tblbankaccount.idbaccount == idPaymentMethod);
                }

                if (idCompany > 0)
                {
                    _query = _query.Where(x => x.tblbankaccount.tblcompanies.idcompany == idCompany);
                }
                //
                if (fondoFechaInicio != null)
                {
                    _query = _query.Where(x => x.tblinvoice.invoicedate >= fondoFechaInicio);
                }
                //
                if (fondoFechaFin != null)
                {
                    _query = _query.Where(x => x.tblinvoice.invoicedate <= fondoFechaFin);
                }
                //
                if (companies.Count() != 0)
                {
                    _query = _query.Where(y => companies.Contains(y.tblbankaccount.tblcompanies.idcompany));
                }
                else
                {
                    throw new Exception("No tiene asignados empresas para realizar búsquedas.");
                }

                return await _query.OrderByDescending(x => x.paymentdate).Take(Globals.EntityMax150PredefinedResult).ToListAsync();

            }
        }



        /* public decimal getBuget(int idCompany, int idPaymentMethod, DateTime startDate, DateTime endDate)
        {


            using (var db = new vtAccount())
            {
                var _query = db.tblfondos.Include(t => t.tblcompanies).Include(t => t.tblbankaccount).Include(t => t.tblbankaccount1).Include(t => t.tblbankaccount.Currency).Include(t => t.tblbankaccount1);




                if (idPaymentMethod > 0)
                {
                    _query = _query.Where(x => x.idPaymentMethod == idPaymentMethod);
                }

                //
                if (startDate != null)
                {
                    _query = _query.Where(x => x.fondoFechaInicio >= startDate);
                }
                //
                if (endDate != null)
                {
                    _query = _query.Where(x => x.fondoFechaFin <= endDate);
                }

                return _query.Sum(t=>t.fondoMonto);

            }
        }
        */
        public IEnumerable<tblfondosmaxammount> getbudgetLimitSearch(fondosmaxammountModel helper)
        {
            using (var db = new vtclubdbEntities())
            {
                var _query = db.tblfondosmaxammount.Include(t => t.tblbankaccount).Include(t => t.tblbankaccount.tblcurrencies).Include(t => t.tblbankaccount.tblcompanies).Include(t => t.tblbankaccount.tblbank);
                _query = _query.AsNoTracking();

                if (helper.FondosMax != 0)
                {
                    _query = _query.Where(t => t.idFondosMax == helper.FondosMax);
                }
                else
                {
                    if (helper.PaymentMethod != 0)
                    {
                        _query = _query.Where(x => x.idBAccount == helper.PaymentMethod);
                    }

                    if (helper.Company != 0)
                    {
                        _query = _query.Where(x => x.tblbankaccount.tblcompanies.idcompany == helper.Company);
                    }
                }

                return _query.OrderByDescending(x => x.tblbankaccount.tblcompanies.companyname).Take(150).ToList();

            }
        }
    }
}