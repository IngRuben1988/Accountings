using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.Models.commons;

namespace VTAworldpass.VTAServices.Services.incomes.model
{
    public class incomepayment : financialitembase
    {
        public int     row               { get; set; }
        public int     index             { get; set; }

        public incomepayment()
        { }

        public incomepayment(long item, long parent, string identifier, int segment, string segmentname, int company, string companyname, int companyorder, int bankaccount, string bankaccountname, int bankaccnttype, string bankaccnttypename, int currency, string currencyname, int tpv, string tpvname, string card, decimal ammounttotal, string description, DateTime creationdate, DateTime aplicationdate, string authcode)
        {
            this.item = item;
            this.parent = parent;
            this.identifier = identifier;
            this.segment = segment;
            this.segmentname = segmentname;
            this.company = company;
            this.companyname = companyname;
            this.companyorder = companyorder;
            this.bankaccount = bankaccount;
            this.bankaccountname = bankaccountname;
            this.bankaccnttype = bankaccnttype;
            this.bankaccnttypename = bankaccnttypename;
            this.currency = currency;
            this.currencyname = currencyname;
            this.tpv = tpv;
            this.tpvname = tpvname;
            this.card = card;
            this.authcode = authcode;
            this.ammounttotal = ammounttotal;
            this.description = description;
            this.creationdate = creationdate;
            this.aplicationdate = aplicationdate;
            this.GenerateString();
        }

        protected override void GenerateString()
        {
            this.aplicationdatestring = DateTimeUtils.ParseDatetoString(this.aplicationdate);
            this.creationdatestring   = DateTimeUtils.ParseDatetoString(this.creationdate);
            this.ammounttotalstring   = MoneyUtils.ParseDecimalToString(this.ammounttotal);
        }
    }
}