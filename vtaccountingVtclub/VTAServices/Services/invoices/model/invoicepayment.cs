using System;
using System.Collections.Generic;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.invoices.model
{
    public class invoicepayment
    {
        public long          Payment             { get; set; }
        public long          Invoice             { get; set; }
        public string        InvoiceIdentifier   { get; set; }
        public int           PaymentMethod       { get; set; }
        public string        PaymentMethodName   { get; set; }
        public int           BankAccntType       { get; set; }
        public string        BankAccntTypeName   { get; set; }
        public int           CurrencyPay         { get; set; }
        public string        CurrencyPayName     { get; set; }
        public int           Segment             { get; set; }
        public string        SegmentName         { get; set; }
        public int           Company             { get; set; }
        public int           CompanyOrder        { get; set; }
        public string        CompanyName         { get; set; }
        public decimal       chargedAmount       { get; set; }
        public string        chargedAmountString { get; set; }
        public decimal       taxesAmmount        { get; set; }
        public string        taxesAmmountString  { get; set; }
        public string        authRef             { get; set; }
        public DateTime      creationDate        { get; set; }
        public string        creationDateString  { get; set; }
        public DateTime      aplicationDate      { get; set; }
        public string        aplicationDateString { get; set; }
        public string        description         { get; set; }
        public tblcurrencies tblcurrency         { get; set; }
        public tblinvoice    tblinvoice          { get; set; }
        public IEnumerable<invoicepayment> invoiceitems { get; set; }

    }
}