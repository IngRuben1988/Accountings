using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.Models.commons
{
    public class docpaymentpurchase
    {
        public int Payment { get; set; }
        public int Purchase { get; set; }
        public int Memberships { get; set; }
        public int MembershipNumber { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public int BankAccntType { get; set; }
        public string BankAccntTypeName { get; set; }
        public int CurrencyPay { get; set; }
        public string CurrencyPayName { get; set; }
        public int Hotel { get; set; }
        public int HotelOrder { get; set; }
        public string HotelName { get; set; }
        public decimal paymentAmount { get; set; }
        public string paymentAmountString { get; set; }
        public decimal taxesAmmount { get; set; }
        public string taxesAmmountString { get; set; }
        public string authRef { get; set; }
        public long bankStatement { get; set; }
        public bool bankStatementLinked { get; set; }
        public DateTime creationDate { get; set; }
        public string creationDateString { get; set; }
        public DateTime paymentDate { get; set; }
        public string paymentDateString { get; set; }
        public tblcurrencies tblCurrency { get; set; }
        public string description { get; set; }
        public string authcode { get; set; }
        public decimal exchangerate { get; set; }
        public int terminal { get; set; }
        public long carddigit { get; set; }
        public int saleType { get; set; }
        public string saleTypeName { get; set; } 
    }
}