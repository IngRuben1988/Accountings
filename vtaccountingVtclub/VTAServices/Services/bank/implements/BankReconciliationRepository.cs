using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.vtaccountingVtclub.DataBase;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTAServices.Services.bank.implements
{
    public class BankReconciliationRepository
    {
        public static List<tblbankstatements> getbankstatements(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, decimal ammountStart, decimal ammountEnd)
        {
            List<tblbankstatements> list = new List<tblbankstatements>();

            try
            {
                using (var db = new vtaccountingVtclubContext())
                {
                    var _query = db.tblbankstatements.Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.Currency).Include(y => y.tblbankstatementmethod).Include(y => y.tblbankaccount.tblbank).Include(y => y.tblbankaccount.tblcompanies).Include(y => y.tblbankaccount).Include(y => y.tblcompanies.tblcompanyhotel.Select(c => c.tblhotels)).Include(y => y.tbltpv).Include(y => y.tblbankstatereserv).Include(y => y.tblbankstateupscl).Include(x => x.tblbankstateupscl.Select(y => y.Payment)).Include(x => x.tblbankstatereserv.Select(y => y.tblreservationpayments));
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
                        _query = _query.Where(y => y.tblbankaccount.idBAccount == PaymentMethod);
                    }

                    if (Tpv != 0)
                    {
                        _query = _query.Where(y => y.tbltpv.idTPV == Tpv);
                    }

                    if (company != 0)
                    {
                        _query = _query.Where(y => y.tblcompanies.idCompany == company);
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

            using (var db = new vtaccountingVtclubContext())
            {
                var _query = db.tblbankstatements.Include(y => y.tblbankaccount).Include(y => y.tblbankaccount.Currency).Include(y => y.tblbankstatementmethod).Include(y => y.tblbankaccount.tblbank).Include(y => y.tblbankaccount.tblcompanies).Include(y => y.tblbankaccount).Include(y => y.tblcompanies.tblcompanyhotel.Select(c => c.tblhotels)).Include(y => y.tbltpv).Include(y => y.tblbankstatereserv).Include(y => y.tblbankstateupscl).Include(x => x.tblbankstateupscl.Select(y => y.Payment)).Include(x => x.tblbankstatereserv.Select(y => y.tblreservationpayments));
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
                    _query = _query.Where(y => y.tblbankaccount.idBAccount == BankAccount);
                }

                if (currency != 0)
                {
                    _query = _query.Where(y => y.tblbankaccount.Currency.idCurrency == currency);
                }

                if (Tpv != 0)
                {
                    _query = _query.Where(y => y.tbltpv.idTPV == Tpv);
                }

                list = _query.ToList();
            }

            return list;
        }

        public static List<tblscotiabankstatements> getScotiaDetails(string authcode)
        {
            List<tblscotiabankstatements> lst = new List<tblscotiabankstatements>();
            try
            {
                using (var db = new vtaccountingVtclubContext())
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
        }
    }
}