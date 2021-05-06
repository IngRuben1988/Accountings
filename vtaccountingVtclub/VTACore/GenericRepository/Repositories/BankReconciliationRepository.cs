using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTACore.GenericRepository.Repositories
{
    public class BankReconciliationRepository
    {
        public static List<tblbankstatements> getbankstatements(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, decimal ammountStart, decimal ammountEnd)
        {
            List<tblbankstatements> list = new List<tblbankstatements>();

            try
            {
                using (var db = new vtclubdbEntities())
                {
                    var _query = db.tblbankstatements.Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.tblcurrencies).Include(y => y.tblbankstatementmethod).Include(y => y.tblbankaccount.tblbank).Include(y => y.tblbankaccount.tblcompanies).Include(y => y.tblbankaccount).Include(y => y.tblcompanies).Include(y => y.tbltpv).Include(y => y.tblbankstatereserv).Include(x => x.tblbankstatereserv.Select(y => y.tblreservationspayment)).Include(y => y.tblbankstateparentreserv).Include(x => x.tblbankstateparentreserv.Select(y => y.tblreservationsparentpayment)).Include(y => y.tblbankstatepurchase).Include(x => x.tblbankstatepurchase.Select(y => y.tblpaymentspurchases)).Include(y => y.tblbankstateincome).Include(x => x.tblbankstateincome.Select(y => y.tblincomemovement))/*.Include(y => y.tblcompanies.tblcompanyhotel.Select(c => c.tblcompanies))*/;
                    _query = _query.AsNoTracking();

                    if (dateReportStart != null)
                    {
                        _query = _query.Where(y => y.bankstatementAplicationDate >= dateReportStart);
                    }

                    if (dateReportEnd != null)
                    {
                        _query = _query.Where(y => y.bankstatementAplicationDate <= dateReportEnd);
                    }

                    if (PaymentMethod != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.idbaccount == PaymentMethod);
                    }

                    if (Tpv != 0)
                    {
                        _query = _query.Where(y => y.tbltpv.idtpv == Tpv);
                    }

                    if (company != 0)
                    {
                        _query = _query.Where(y => y.tblcompanies.idcompany == company);
                    }

                    if (ammountStart != 0)
                    {
                        _query = _query.Where(y => y.bankstatementAppliedAmmount >= ammountStart);
                        _query = _query.Where(y => y.bankstatementAppliedAmmountFinal >= ammountStart);
                    }

                    if (ammountEnd != 0)
                    {
                        _query = _query.Where(y => y.bankstatementAppliedAmmount <= ammountEnd);
                        _query = _query.Where(y => y.bankstatementAppliedAmmountFinal <= ammountEnd);
                    }

                    list = _query.ToList();
                }

                return list;

            }
            catch (Exception e)
            {
                Log.Error("Error el realizar consulta de conciliaciones", e);
                throw new Exception("Error el realizar consulta de conciliaciones", e);
            }
        }

        public static List<tblbankstatements> getbankstatements(int month, int year, int Tpv, int currency, int externalgroup, int BankAccount)
        {
            List<tblbankstatements> list = new List<tblbankstatements>();

            using (var db = new vtclubdbEntities())
            {
                var _query = db.tblbankstatements.Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.tblcurrencies).Include(y => y.tblbankstatementmethod).Include(y => y.tblbankaccount.tblbank).Include(y => y.tblbankaccount.tblcompanies).Include(y => y.tblbankaccount).Include(y => y.tbltpv).Include(y => y.tblbankstatereserv).Include(x => x.tblbankstatereserv.Select(y => y.tblreservationspayment))/*.Include(x => x.tblbankstateupscl.Select(y => y.Payment)).Include(y => y.tblbankstateupscl).Include(y => y.tblcompanies.tblcompanyhotel.Select(c => c.tblhotels))*/;
                _query = _query.AsNoTracking();

                if (month != 0)
                {
                    _query = _query.Where(y => y.bankstatementAplicationDate.Month == month);
                }

                if (year != 0)
                {
                    _query = _query.Where(y => y.bankstatementAplicationDate.Year == year);
                }

                if (BankAccount != 0)
                {
                    _query = _query.Where(y => y.tblbankaccount.idbaccount == BankAccount);
                }

                if (currency != 0)
                {
                    _query = _query.Where(y => y.tblbankaccount.tblcurrencies.idCurrency == currency);
                }

                if (Tpv != 0)
                {
                    _query = _query.Where(y => y.tbltpv.idtpv == Tpv);
                }

                list = _query.ToList();
            }

            return list;
        }

        /*public static List<tblscotiabankstatements> getScotiaDetails(string authcode)
        {
            List<tblscotiabankstatements> lst = new List<tblscotiabankstatements>();
            try
            {
                using (var db = new vtclubdbEntities())
                {
                    var query = db.tblscotiabankstatements.Include(c => c.Currency).Include(c => c.Currency1).Include(c => c.tbltpv.tblbaccounttpv);
                    if (!string.IsNullOrEmpty(authcode) && !string.IsNullOrWhiteSpace(authcode))
                    {
                        query = query.Where(v => v.scotiastatementauthorizationcode.Equals(authcode));
                    }
                    lst = query.ToList();
                }
            }
            catch (Exception e)
            {
                Log.Error("No es posible realizar la consulta  - public static List<tblscotiabankstatements> getScotiaDetails -.", e);
            }
            return lst;
        }*/

        public static List<tblbankstatements2> getBankStatements(DateTime? dateReportStart, DateTime? dateReportEnd, int PaymentMethod, decimal ammountStart, decimal ammountEnd)
        {
            List<tblbankstatements2> list = new List<tblbankstatements2>();

            try
            {
                using (var db = new vtclubdbEntities())
                {
                    // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                    var _query = db.tblbankstatements2.Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.tblcurrencies).Include(y => y.tblbankaccount.tblbank).Include(y => y.tblbankaccount.tblcompanies).Include(y => y.tblbankaccount).Include(y => y.tblusers).Include(y => y.tblusers1).Include(y => y.tblmovementtype);
                    _query = _query.AsNoTracking();

                    if (dateReportStart != null)
                    {
                        _query = _query.Where(y => y.bankstatements2AplicationDate >= dateReportStart);
                    }

                    if (dateReportEnd != null)
                    {
                        _query = _query.Where(y => y.bankstatements2AplicationDate <= dateReportEnd);
                    }

                    if (PaymentMethod != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.idbaccount == PaymentMethod);
                    }

                    if (ammountStart != 0)
                    {
                        _query = _query.Where(y => y.bankstatements2Charge >= ammountStart);
                        _query = _query.Where(y => y.bankstatements2Pay >= ammountStart);
                    }

                    if (ammountEnd != 0)
                    {
                        _query = _query.Where(y => y.bankstatements2Charge <= ammountEnd);
                        _query = _query.Where(y => y.bankstatements2Pay <= ammountEnd);
                    }

                    list = _query.ToList();
                }

                return list;

            }
            catch (Exception e)
            {
                Log.Error("Error el realizar consulta de conciliaciones", e);
                throw new Exception("Error el realizar consulta de conciliaciones", e);
            }
        }

        public static List<tblbankstatements2> getBankStatements(DateTime? dateReportStart, DateTime? dateReportEnd, decimal ammountStart, decimal ammountEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int type, string descripcion)
        {
            List<tblbankstatements2> list = new List<tblbankstatements2>();
            var bandera = false;
            try
            {
                using (var db = new vtclubdbEntities())
                {

                    var _query = db.tblbankstatements2.Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.tblcurrencies).Include(y => y.tblbankaccount.tblbank).Include(y => y.tblbankaccount.tblbaccounttpv.Select(x => x.tbltpv)).Include(y => y.tblbankaccount.tblcompanies).Include(y => y.tblbankaccount).Include(y => y.tblusers).Include(y => y.tblusers1).Include(y => y.tblmovementtype);
                    _query = _query.AsNoTracking();

                    if (dateReportStart != null)
                    {
                        _query = _query.Where(y => y.bankstatements2AplicationDate >= dateReportStart);
                    }

                    if (dateReportEnd != null)
                    {
                        _query = _query.Where(y => y.bankstatements2AplicationDate <= dateReportEnd);
                    }

                    if (ammountStart != 0 && ammountEnd != 0)
                    {
                        _query = _query.Where(y => y.bankstatements2Charge >= ammountStart && y.bankstatements2Charge <= ammountEnd || y.bankstatements2Pay >= ammountStart && y.bankstatements2Pay <= ammountEnd);
                        //_query = _query.Where(y => y.bankstatements2Pay >= ammountStart && y.bankstatements2Pay <= ammountEnd);
                        bandera = true;
                    }

                    if (ammountStart != 0 && !bandera)
                    {
                        _query = _query.Where(y => y.bankstatements2Charge >= ammountStart || y.bankstatements2Pay >= ammountStart);
                        //_query = _query.Where(y => y.bankstatements2Pay >= ammountStart);
                    }

                    if (ammountEnd != 0 && !bandera)
                    {
                        _query = _query.Where(y => y.bankstatements2Charge <= ammountEnd);
                        _query = _query.Where(y => y.bankstatements2Pay <= ammountEnd);
                    }

                    if (Tpv != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.tblbaccounttpv.FirstOrDefault().tbltpv.idtpv == Tpv);
                    }

                    if (company != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.tblcompanies.idcompany == company);
                    }

                    if (PaymentMethod != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.idbaccount == PaymentMethod);
                    }

                    if (type != 0)
                    {
                        _query = _query.Where(y => y.tblmovementtype.idOperationType == type);
                    }

                    if (descripcion != "")
                    {
                        _query = _query.Where(y => y.bankstatements2Concept.Contains(descripcion));
                    }

                    list = _query.ToList();
                }

                return list;

            }
            catch (Exception e)
            {
                Log.Error("Error el realizar consulta de conciliaciones", e);
                throw new Exception("Error el realizar consulta de conciliaciones", e);
            }
        }

        public static List<tblbankstatements2> getBankstatements(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, decimal ammountStart, decimal ammountEnd)
        {
            List<tblbankstatements2> list = new List<tblbankstatements2>();

            try
            {
                using (var db = new vtclubdbEntities())
                {
                    var _query = db.tblbankstatements2.Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.tblcurrencies).Include(y => y.tblbankstatementmethod).Include(y => y.tblbankaccount.tblbank).Include(y => y.tblbankaccount.tblcompanies).Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.tblbaccounttpv.Select(x => x.tbltpv)).Include(y => y.tblbankstat2reserv).Include(y => y.tblbankstat2purchase).Include(x => x.tblbankstat2purchase.Select(y => y.tblpaymentspurchases)).Include(x => x.tblbankstat2reserv.Select(y => y.tblreservationspayment)).Include(x => x.tblbankstat2fondo).Include(x => x.tblbankstat2fondo.Select(y => y.tblfondos)).Include(x => x.tblbankstat2invoice).Include(x => x.tblbankstat2invoice.Select(y => y.tblpayment)).Include(x => x.tblbankstat2income).Include(x => x.tblbankstat2income.Select(y => y.tblincomemovement)).Include(x => x.tblmovementtype);
                    _query = _query.AsNoTracking();

                    if (dateReportStart != null)
                    {
                        _query = _query.Where(y => y.bankstatements2AplicationDate >= dateReportStart);
                    }

                    if (dateReportEnd != null)
                    {
                        _query = _query.Where(y => y.bankstatements2AplicationDate <= dateReportEnd);
                    }

                    if (PaymentMethod != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.idbaccount == PaymentMethod);
                    }

                    if (Tpv != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.tblbaccounttpv.FirstOrDefault().idtpv == Tpv);
                    }

                    if (company != 0)
                    {
                        _query = _query.Where(y => y.tblbankaccount.tblcompanies.idcompany == company);
                    }

                    if (ammountStart != 0)
                    {
                        _query = _query.Where(y => y.bankstatements2Pay >= ammountStart);
                        //_query = _query.Where(y => y.bankstatementAppliedAmmountFinal >= ammountStart);
                    }

                    if (ammountEnd != 0)
                    {
                        _query = _query.Where(y => y.bankstatements2Pay <= ammountEnd);
                        //_query = _query.Where(y => y.bankstatementAppliedAmmountFinal <= ammountEnd);
                    }

                    list = _query.ToList();
                }

                return list;

            }
            catch (Exception e)
            {
                Log.Error("Error el realizar consulta de conciliaciones", e);
                throw new Exception("Error el realizar consulta de conciliaciones", e);
            }
        }

    }
}