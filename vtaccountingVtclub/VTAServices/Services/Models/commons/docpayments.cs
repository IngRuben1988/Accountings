using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.Models.commons
{
    public class docpayments
    {
        public long Payment { get; set; }
        public long Invoice { get; set; }
        public int idBAccount { get; set; }
        public string baccountName { get; set; }
        public int currency { get; set; }
        public string currencyName { get; set; }
        public int Company { get; set; }
        public string CompanyName { get; set; }
        public int bankAccountType { get; set; }
        public string bankAccountTypeName { get; set; }
        public DateTime invoiceApplicationDate { get; set; }
        public string invoiceApplicationDateString { get; set; }
        public DateTime applicationDate { get; set; }
        public string applicationDateString { get; set; }
        public decimal chargeAmount { get; set; }
        public string chargeAmountString { get; set; }
        public string authReference { get; set; }
        public DateTime creationDate { get; set; }
        public string InvoiceIdentifier { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
    }
}