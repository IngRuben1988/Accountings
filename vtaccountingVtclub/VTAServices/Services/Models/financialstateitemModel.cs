using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Utils;

namespace VTAworldpass.VTAServices.Services.Models
{
    public class financialstateitemModel
    {
        public int rowIndex { get; set; }
        public int rowNumber { get; set; }
        public long? Reference { get; set; }
        public long ReferenceItem { get; set; }
        public int accounttype { get; set; }
        public bool bankStatementLinked { get; set; } // Pasar como propiedad en una lista
        public int bankStatementStatus { get; set; } // Pasar como propiedad en una lista
        public DateTime? aplicationDate { get; set; }
        public string aplicationDateString { get; set; }
        public decimal appliedAmmount { get; set; }
        public string appliedAmmountString { get; set; }
        public decimal? balance { get; set; }
        public string HotelName { get; set; }
        public string balanceString { get; set; }
        public string PaymentMethodName { get; set; }
        public int SourceData { get; set; }
        public string SourceDataName { get; set; }
        public string description { get; set; }
        public int typeRsv { get; set; }
        // public List<actions> actions { get; set; }


        public financialstateitemModel()
        {
        }

        public financialstateitemModel(string description, DateTime aplicationDate, decimal appliedAmmount)
        {
            this.description = description;
            this.aplicationDate = aplicationDate;
            this.aplicationDateString = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.appliedAmmount = appliedAmmount;
            this.appliedAmmountString = MoneyUtils.ParseDecimalToString(appliedAmmount);
        }

        public financialstateitemModel(string description, DateTime? aplicationDate, decimal appliedAmmount, int SourceData, string SourceDataName)
        {
            this.description = description;
            this.aplicationDate = aplicationDate;
            this.aplicationDateString = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.appliedAmmount = appliedAmmount;
            this.appliedAmmountString = MoneyUtils.ParseDecimalToString(appliedAmmount);
            this.SourceData = SourceData;
            this.SourceDataName = SourceDataName;
        }

        public financialstateitemModel(DateTime? aplicationDate, decimal? appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, string SourceDataName, long? Reference, string description)
        {
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = (decimal)appliedAmmount;
            this.HotelName = HotelName;
            this.aplicationDateString = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.description = description;
        }

        public financialstateitemModel(DateTime? aplicationDate, decimal? appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long? Reference, string description)
        {
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = (decimal)appliedAmmount;
            this.HotelName = HotelName;
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.SourceData = SourceData;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.description = description;
        }

        public financialstateitemModel(DateTime? aplicationDate, decimal? appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, string SourceDataName, long? Reference, long ReferenceItem, string description)
        {
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = (decimal)appliedAmmount;
            this.HotelName = HotelName;
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.ReferenceItem = ReferenceItem;
            this.description = description;
        }

        public financialstateitemModel(DateTime? aplicationdate, decimal? ammounttotal, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long? Reference, long ReferenceItem, string description)
        {
            this.aplicationDate = aplicationdate;
            this.appliedAmmount = (decimal)ammounttotal;
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.HotelName = HotelName;
            this.SourceData = SourceData;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.ReferenceItem = ReferenceItem;
            this.description = description;
        }

        public financialstateitemModel(DateTime? aplicationDate, decimal? appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long? Reference, long ReferenceItem, string description, bool bankStatementLinked, int typeRsv)
        {
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = (decimal)appliedAmmount;
            this.HotelName = HotelName;
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.SourceData = SourceData;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.ReferenceItem = ReferenceItem;
            this.description = description;
            this.bankStatementLinked = bankStatementLinked;
            this.typeRsv = typeRsv;
        }


        public financialstateitemModel(int accounttype, DateTime? aplicationDate, decimal? appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long? Reference, long ReferenceItem, string description)
        {
            this.accounttype = accounttype;
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = (decimal)appliedAmmount;
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.HotelName = HotelName;
            this.SourceData = SourceData;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.ReferenceItem = ReferenceItem;
            this.description = description;
            this.bankStatementLinked = false;
        }


        public financialstateitemModel(int accounttype, DateTime? aplicationDate, decimal? appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long? Reference, long ReferenceItem, string description, bool bankStatementLinked, int rsvtype)
        {
            this.accounttype = accounttype;
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = (decimal)appliedAmmount;
            this.HotelName = HotelName;
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.SourceData = SourceData;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.ReferenceItem = ReferenceItem;
            this.description = description;
            this.bankStatementLinked = bankStatementLinked;
            this.typeRsv = rsvtype;
        }

        public financialstateitemModel(int accounttype, DateTime? aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, string SourceDataName, long? Reference, string description)
        {
            this.accounttype = accounttype;
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = appliedAmmount;
            this.HotelName = HotelName;
            this.aplicationDateString = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.description = description;
        }


        public financialstateitemModel(int accounttype, DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string SourceDataName, long Reference, string description)
        {
            this.accounttype = accounttype;
            this.aplicationDate = aplicationDate;
            this.appliedAmmount = appliedAmmount;
            this.aplicationDateString = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.balance = balance;
            this.PaymentMethodName = PaymentMethodName;
            this.SourceDataName = SourceDataName;
            this.Reference = Reference;
            this.description = description;
        }


        public void generateString()
        {
            this.aplicationDateString = DateTimeUtils.ParseDatetoString(this.aplicationDate);
            this.appliedAmmountString = MoneyUtils.ParseDecimalToString((decimal)this.appliedAmmount);
            this.balanceString = MoneyUtils.ParseDecimalToString((decimal)this.balance);
        }
    }
}