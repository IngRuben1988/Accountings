using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Utils;

namespace VTAworldpass.VTAServices.Services.bank.model
{
    public class financialstateitem
    {
        public int      rowindex            { get; set; }
        public int      rownumber           { get; set; }
        public long     reference           { get; set; }
        public long     referenceitem       { get; set; }
        public int      accounttype         { get; set; }
        public bool     bankstatementlinked { get; set; } // Pasar como propiedad en una lista
        public int      bankstatementstatus { get; set; } // Pasar como propiedad en una lista
        public DateTime aplicationdate      { get; set; }
        public string   aplicationdatestring { get; set; }
        public decimal  appliedammount       { get; set; }
        public string   appliedammountstring { get; set; }
        public decimal  balance             { get; set; }
        public string   hotelname           { get; set; }
        public string   balancestring       { get; set; }
        public string   paymentmethodname   { get; set; }
        public int      sourcedata          { get; set; }
        public string   sourcedataname      { get; set; }
        public string   description         { get; set; }


        public financialstateitem()
        {
        }

        public financialstateitem(string description, DateTime aplicationDate, decimal appliedAmmount)
        {
            this.description            = description;
            this.aplicationdate         = aplicationDate;
            this.aplicationdatestring   = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.appliedammount         = appliedAmmount;
            this.appliedammountstring   = MoneyUtils.ParseDecimalToString(appliedAmmount);
        }

        public financialstateitem(string description, DateTime aplicationDate, decimal appliedAmmount, int SourceData, string SourceDataName)
        {
            this.description          = description;
            this.aplicationdate       = aplicationDate;
            this.aplicationdatestring = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.appliedammount       = appliedAmmount;
            this.appliedammountstring = MoneyUtils.ParseDecimalToString(appliedAmmount);
            this.sourcedata           = SourceData;
            this.sourcedataname       = SourceDataName;
        }

        public financialstateitem(DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, string SourceDataName, long Reference, string description)
        {
            this.aplicationdate         = aplicationDate;
            this.appliedammount         = appliedAmmount;
            this.hotelname              = HotelName;
            this.aplicationdatestring   = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.balance                = balance;
            this.paymentmethodname      = PaymentMethodName;
            this.sourcedataname         = SourceDataName;
            this.reference              = Reference;
            this.description            = description;
        }

        public financialstateitem(DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long Reference, string description)
        {
            this.aplicationdate = aplicationDate;
            this.appliedammount = appliedAmmount;
            this.hotelname      = HotelName;
            this.balance        = balance;
            this.paymentmethodname = PaymentMethodName;
            this.sourcedata     = SourceData;
            this.sourcedataname = SourceDataName;
            this.reference      = Reference;
            this.description    = description;
        }

        public financialstateitem(DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, string SourceDataName, long Reference, long ReferenceItem, string description)
        {
            this.aplicationdate     = aplicationDate;
            this.appliedammount     = appliedAmmount;
            this.hotelname          = HotelName;
            this.balance            = balance;
            this.paymentmethodname  = PaymentMethodName;
            this.sourcedataname     = SourceDataName;
            this.reference          = Reference;
            this.referenceitem      = ReferenceItem;
            this.description        = description;
        }

        public financialstateitem(DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long Reference, long ReferenceItem, string description)
        {
            this.aplicationdate = aplicationDate;
            this.appliedammount = appliedAmmount;
            this.hotelname      = HotelName;
            this.balance        = balance;
            this.paymentmethodname = PaymentMethodName;
            this.sourcedata     = SourceData;
            this.sourcedataname = SourceDataName;
            this.reference      = Reference;
            this.referenceitem  = ReferenceItem;
            this.description    = description;
        }

        public financialstateitem(DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long Reference, long ReferenceItem, string description, bool bankStatementLinked)
        {
            this.aplicationdate     = aplicationDate;
            this.appliedammount     = appliedAmmount;
            this.hotelname          = HotelName;
            this.balance            = balance;
            this.paymentmethodname  = PaymentMethodName;
            this.sourcedata         = SourceData;
            this.sourcedataname     = SourceDataName;
            this.reference          = Reference;
            this.referenceitem      = ReferenceItem;
            this.description        = description;
            this.bankstatementlinked = bankStatementLinked;
        }

        public financialstateitem(int accounttype, DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long Reference, long ReferenceItem, string description)
        {
            this.accounttype       = accounttype;
            this.aplicationdate    = aplicationDate;
            this.appliedammount    = appliedAmmount;
            this.hotelname         = HotelName;
            this.balance           = balance;
            this.paymentmethodname = PaymentMethodName;
            this.sourcedata        = SourceData;
            this.sourcedataname    = SourceDataName;
            this.reference         = Reference;
            this.referenceitem     = ReferenceItem;
            this.description       = description;
            this.bankstatementlinked = false;
        }

        public financialstateitem(int accounttype, DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, int SourceData, string SourceDataName, long Reference, long ReferenceItem, string description, bool bankStatementLinked)
        {
            this.accounttype         = accounttype;
            this.aplicationdate      = aplicationDate;
            this.appliedammount      = appliedAmmount;
            this.hotelname           = HotelName;
            this.balance             = balance;
            this.paymentmethodname   = PaymentMethodName;
            this.sourcedata          = SourceData;
            this.sourcedataname      = SourceDataName;
            this.reference           = Reference;
            this.referenceitem       = ReferenceItem;
            this.description         = description;
            this.bankstatementlinked = bankStatementLinked;
        }

        public financialstateitem(int accounttype, DateTime aplicationDate, decimal appliedAmmount, decimal balance, string PaymentMethodName, string HotelName, string SourceDataName, long Reference, string description)
        {
            this.accounttype        = accounttype;
            this.aplicationdate     = aplicationDate;
            this.appliedammount     = appliedAmmount;
            this.hotelname          = HotelName;
            this.aplicationdatestring = DateTimeUtils.ParseDatetoString(aplicationDate);
            this.balance            = balance;
            this.paymentmethodname  = PaymentMethodName;
            this.sourcedataname     = SourceDataName;
            this.reference          = Reference;
            this.description        = description;
        }



        public void generateString()
        {
            this.aplicationdatestring = DateTimeUtils.ParseDatetoString(this.aplicationdate);
            this.appliedammountstring = MoneyUtils.ParseDecimalToString(this.appliedammount);
            this.balancestring = MoneyUtils.ParseDecimalToString(this.balance);
        }

    }


}