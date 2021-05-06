using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.incomes.model
{
    public class docpaymentincome
    {
        public long IncomeMovement { get; set; }
        public long Income { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal incomemovchargedAmount { get; set; }
        public string incomemovchargedAmountString { get; set; }
        public int Company { get; set; }
        public int CompanyOrder { get; set; }
        public string CompanyName { get; set; }
        public DateTime incomemovapplicationDate { get; set; }
        public string incomemovapplicationDateString { get; set; }
        public int Terminal { get; set; }
        public string incomemovcard { get; set; }
        public int CurrencyPay { get; set; }
        public string CurrencyPayName { get; set; }
        public bool bankStatementLinked { get; set; }
        public long bankStatement { get; set; }
        public string incomemovauthCode { get; set; }
        public int BankAccount { get; set; }
        public string BankAccountName { get; set; }
        public int BankAccntType { get; set; }
        public string BankAccntTypeName { get; set; }
        public string incomemovauthRef { get; set; }
        public string identifier { get; set; }
    }
}