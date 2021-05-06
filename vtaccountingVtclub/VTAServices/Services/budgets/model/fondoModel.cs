using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.invoices.model;

namespace VTAworldpass.VTAServices.Services.budgets.model
{
    public class fondoModel
    {
        public int id { get; set; }
        public int? FinanceType { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public int Company { get; set; }
        public int CompanyOrder { get; set; }
        public string CompanyName { get; set; }
        public int Currency { get; set; }
        public string CurrencyName { get; set; }
        public DateTime? fechaCaptura { get; set; }
        public string fechaCapturaString { get; set; }
        public DateTime? fechaEntrega { get; set; }
        public string fechaEntregaString { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string FechaInicioString { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FechaFinString { get; set; }
        public decimal MontoCargo { get; set; }
        public decimal MontoAbonos { get; set; }
        public string MontoCargoString { get; set; }
        public string MontoAbonosString { get; set; }
        public decimal Saldo { get; set; }
        public string SaldoString { get; set; }
        public decimal? FondoFee { get; set; }
        public string FondoFeeString { get; set; }
        public long? Invoice { get; set; }
        public string comments { get; set; }
        public bool editable { get; set; }
        public string description { get; set; }

        // -----------------------------//

        public int FinancialMethod { get; set; }
        public string FinancialMethodName { get; set; }
        public int CompanyFinancial { get; set; }
        public int CompanyFinancialOrder { get; set; }
        public string CompanyFinancialName { get; set; }
        public int CurrencyFinancial { get; set; }
        public string CurrencyFinancialName { get; set; }

        // ------------------------------//
        public IEnumerable<invoiceitems> docitems { get; set; }
        public IEnumerable<invoicepayment> docpaymt { get; set; }

        /*
        public tblfondos hasChargedBudget(List<tblfondos> tblfondos, DateTime datetofind)
        {

            for (int i = 0; i <= tblfondos.Count() - 1; i++)
            {

                if (datetofind.CompareTo(tblfondos[i].fondoFechaInicio) >= 0 && datetofind.CompareTo(tblfondos[i].fondoFechaFin) <= 0)
                {
                    Debug.WriteLine(" ------->>>>> Esta dentro del rango. " + tblfondos[i].fondoFechaInicio.ToShortDateString() + "  <<<<< ------- " + tblfondos[i].fondoMonto);
                    return tblfondos[i];
                }

            }
            return null;
        }

        public bool hasChargedBudgetBool(List<tblfondos> tblfondos, DateTime datetofind)
        {

            for (int i = 0; i <= tblfondos.Count() - 1; i++)
            {

                if (datetofind.CompareTo(tblfondos[i].fondoFechaInicio) >= 0 && datetofind.CompareTo(tblfondos[i].fondoFechaFin) <= 0)
                {
                    Debug.WriteLine(" ------->>>>> Esta dentro del rango. " + tblfondos[i].fondoFechaInicio.ToShortDateString() + "  <<<<< ------- " + tblfondos[i].fondoMonto);
                    return true;
                }

            }
            return false;
        }
        */

        public void calculateMontoCargos()
        {
            this.MontoCargo = (decimal)this.docitems.Sum(y => y.ammount);
        }
        public void calculateMontoAbonos()
        {
            this.MontoAbonos = this.docpaymt.Sum(y => y.chargedAmount);
        }
        public void calculateSaldo()
        {
            this.Saldo = MontoCargo - MontoAbonos;
        }
        public void parseAmmountsToString()
        {
            this.MontoCargoString = MoneyUtils.ParseDecimalToString(this.MontoCargo);
            this.MontoAbonosString = MoneyUtils.ParseDecimalToString(this.MontoAbonos);
            this.SaldoString = MoneyUtils.ParseDecimalToString(this.Saldo);
        }
        public void parseAmmountEnumerablesToString()
        {
            // this.MontoCargoString = this.docitems == null ? "" : MoneyUtils.ParseDecimalToString((decimal) this.docitems.Sum(y=>y.Amount));
            this.MontoAbonosString = this.docpaymt == null ? "" : MoneyUtils.ParseDecimalToString((decimal)this.docpaymt.Sum(y => y.chargedAmount));
            this.SaldoString = MoneyUtils.ParseDecimalToString(this.Saldo);
        }

        public string generateAccountName(tblbankaccount tblbankaccount)
        {
            return tblbankaccount != null ? string.Concat(tblbankaccount.baccountshortname, " ", tblbankaccount.tblbank.bankshortname, " ", tblbankaccount.tblcurrencies.currencyAlphabeticCode, " ", tblbankaccount.tblcompanies.companyshortname) : " ";
        }
    }
}