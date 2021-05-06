using System;
using System.Collections.Generic;
using System.Linq;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.invoices.model
{
    public class invoiceitems 
    {
        public long     item { get; set; }
        public string   identifier { get; set; }
        public long     id { get; set; }
        public int      accountl1 { get; set; }
        public int      accountl1order { get; set; }
        public string   accountl1name { get; set; }
        public int      accountl2 { get; set; }
        public string   accountl2name { get; set; }
        public int      accountl2order { get; set; }
        public int      accountl3 { get; set; }
        public string   accountl3name { get; set; }
        public int      accountl3order { get; set; }
        public int      accountl4 { get; set; }
        public string   accountl4name { get; set; }
        public int      accountl4order { get; set; }
        public int      category { get; set; }
        public string   categoryName { get; set; }
        public int      type { get; set; }
        public string   typename { get; set; }
        public int      segment { get; set; }
        public string   segmentname { get; set; }
        public int      company { get; set; }
        public string   companyname { get; set; }
        public int      user { get; set; }
        public decimal? ammount { get; set; }
        public string   ammountstring { get; set; }
        public decimal  taxesammount { get; set; }
        public string   taxesammountstring { get; set; }
        public decimal  ammounttotal { get; set; }
        public string   ammounttotalstring { get; set; }
        public decimal  othertaxesammount { get; set; }
        public string   othertaxesammountstring { get; set; }
        public bool     singlexibitionpayment { get; set; }
        public string   description { get; set; }
        public bool     deleted { get; set; }
        public int      status { get; set; }
        public bool     istax { get; set; }
        public string   statusname { get; set; }
        public bool     isverified { get; set; }
        public string   billidentifier { get; set; }
        public int      supplier { get; set; }
        public string   suppliername { get; set; }
        public int      budgettype { get; set; }
        public string   budgettypename { get; set; }
        public string   supplierother { get; set; }
        public DateTime creationdate { get; set; }
        public string   creationdatestring { get; set; }
        public DateTime updatedate { get; set; }
        public string   updatedatestring { get; set; }
        public DateTime deletedate { get; set; }
        public string   deletedatestring { get; set; }
        public DateTime aplicationdate { get; set; }
        public string   aplicationdatestring { get; set; }
        public decimal  invoicebalance { get; set; }
        public string   invoicebalancestring { get; set; }
        public ICollection<tblinvoiceditem> tblinvoiceditem { get; set; }
        public ICollection<tblpayment> tblpayment { get; set; }

        public void generateStringAmmounts()
        {
            this.generateTotalAmmount();

            this.ammountstring = MoneyUtils.ParseDecimalToString((decimal)this.ammount);
            this.taxesammountstring = MoneyUtils.ParseDecimalToString(this.taxesammount);
            this.ammounttotalstring = MoneyUtils.ParseDecimalToString(this.ammounttotal);
            this.othertaxesammountstring = MoneyUtils.ParseDecimalToString(this.othertaxesammount);
        }
        public void generateStringDates()
        {
            this.aplicationdatestring = DateTimeUtils.ParseDatetoString(this.aplicationdate);
            this.creationdatestring = DateTimeUtils.ParseDatetoString(this.creationdate);
        }

        private void generateTotalAmmount()
        {
            this.ammounttotal = (decimal)this.ammount + this.taxesammount + this.othertaxesammount;
        }

        public string InvoiceIdentifierComplete(string company, string profile, int invoiceNumber)
        {
            return string.Concat(company.ToUpper(), '-', profile.ToUpper(), '-', string.Format("{0:00000}", invoiceNumber));
        }

        public void calculateBalnace()
        {
            decimal _doc = this.tblinvoiceditem.Count() != 0 ? ((decimal)this.tblinvoiceditem.Sum(t => t.itemothertax) + (decimal)this.tblinvoiceditem.Sum(t => t.itemtax) + (decimal)this.tblinvoiceditem.Sum(t => t.itemsubtotal)) : 0m;
            decimal _pay = this.tblpayment.Count() != 0 ? ((decimal)this.tblpayment.Sum(t => t.paymentamount)) : 0m;

            this.invoicebalance = _doc - _pay;
            this.invoicebalancestring = MoneyUtils.ParseDecimalToString(this.invoicebalance);
        }

        public void calculateBalance(bool eraseData)
        {

            decimal _doc = this.tblinvoiceditem.Count() != 0 ? ((decimal)this.tblinvoiceditem.Sum(t => t.itemothertax) + (decimal)this.tblinvoiceditem.Sum(t => t.itemtax) + (decimal)this.tblinvoiceditem.Sum(t => t.itemsubtotal)) : 0m;
            decimal _pay = this.tblpayment.Count() != 0 ? ((decimal)this.tblpayment.Sum(t => t.paymentamount)) : 0m;

            if (eraseData) {
                this.tblinvoiceditem = new List<tblinvoiceditem>();
                this.tblpayment = new List<tblpayment>();
            }

            this.invoicebalance = _doc - _pay;
            this.invoicebalancestring = MoneyUtils.ParseDecimalToString(this.invoicebalance);
        }

    }
}