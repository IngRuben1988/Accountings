using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.vtaccountingVtclub.DataBase;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.invoices.model;

namespace VTAworldpass.VTAServices.Services.bank.model
{
    public class fondo
    {
        public int      idfondo { get; set; }
        public int      paymentmethod { get; set; }
        public string   paymentmethodname { get; set; }
        public int      idcompany { get; set; }
        public int      companyorder { get; set; }
        public string   companyname { get; set; }
        public int      idcurrency { get; set; }
        public string   currencyname { get; set; }
        public DateTime fechacaptura { get; set; }
        public string   fechacapturastring { get; set; }
        public DateTime fechaentrega { get; set; }
        public string   fechaentregastring { get; set; }
        public DateTime fechainicio { get; set; }
        public string   fechainiciostring { get; set; }
        public DateTime fechafin { get; set; }
        public string   fechafinstring { get; set; }
        public decimal  montocargo { get; set; }
        public decimal  montoabonos { get; set; }
        public string   montocargostring { get; set; }
        public string   montoabonosstring { get; set; }
        public decimal  saldo { get; set; }
        public string   saldostring { get; set; }
        public decimal  fondofee { get; set; }
        public string   fondofeeString { get; set; }
        public int?     idinvoice { get; set; }
        public string   comments { get; set; }
        public bool     editable { get; set; }
        public string   description { get; set; }

        // -----------------------------//

        public int      financialMethod { get; set; }
        public string   financialMethodName { get; set; }
        public int      companyFinancial { get; set; }
        public int      companyFinancialOrder { get; set; }
        public string   companyFinancialName { get; set; }
        public int      currencyFinancial { get; set; }
        public string   currencyFinancialName { get; set; }

        // ------------------------------//
        public IEnumerable<invoiceitems> invoiceitem { get; set; }
        public IEnumerable<invoicepayment> invoicepay { get; set; }


        public void calculateMontoCargos()
        {
            this.montocargo = (decimal)this.invoiceitem.Sum(y => y.ammount);
        }
        public void calculateMontoAbonos()
        {
            this.montoabonos = this.invoicepay.Sum(y => y.chargedAmount);
        }
        public void calculateSaldo()
        {
            this.saldo = montocargo - montoabonos;
        }
        public void parseAmmountsToString()
        {
            this.montocargostring  = MoneyUtils.ParseDecimalToString(this.montocargo);
            this.montoabonosstring = MoneyUtils.ParseDecimalToString(this.montoabonos);
            this.saldostring       = MoneyUtils.ParseDecimalToString(this.saldo);
        }
        public void parseAmmountEnumerablesToString()
        {
            this.montoabonosstring = this.invoicepay == null ? "" : MoneyUtils.ParseDecimalToString((decimal)this.invoicepay.Sum(y => y.chargedAmount));
            this.saldostring = MoneyUtils.ParseDecimalToString(this.saldo);
        }

        public string generateAccountName(tblbankaccount tblbankaccount)
        {
            return tblbankaccount != null ? string.Concat(tblbankaccount.baccountshortname, " ", tblbankaccount.tblbank.bankshortname, " ", tblbankaccount.tblcurrencies.currencyAlphabeticCode, " ", tblbankaccount.tblcompanies.companyshortname) : " ";
        }
    }
}