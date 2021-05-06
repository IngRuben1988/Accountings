using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Utils;

namespace VTAworldpass.VTAServices.Services.Models.commons
{
    public class contableitembase
    {
        public long item { get; set; }
        public long parent { get; set; }
        public string identifier { get; set; }
        public int segment { get; set; }
        public string segmentname { get; set; }
        public int company { get; set; }
        public string companyName { get; set; }
        public int accountl1 { get; set; }
        public string accountl1name { get; set; }
        public int accountl1order { get; set; }
        public int accountl2 { get; set; }
        public string accountl2name { get; set; }
        public int accountl2order { get; set; }
        public int accountl3 { get; set; }
        public string accountl3name { get; set; }
        public int accountl3order { get; set; }
        public int accountl4 { get; set; }
        public string accountl4name { get; set; }
        public int accountl4order { get; set; }
        public DateTime creationdate { get; set; }
        public string creationdatestring { get; set; }
        public decimal ammounttotal { get; set; }
        public string ammounttotalstring { get; set; }
        public string description { get; set; }
        //public string companyname { get; set; }
        //public int companyorder { get; set; }

        //public int bankaccount { get; set; }
        //public string bankaccountname { get; set; }
        //public int bankaccnttype { get; set; }
        //public string bankaccnttypename { get; set; }
        //public int baclass { get; set; }
        //public string baclassname { get; set; }

        //public int currency { get; set; }
        //public string currencyname { get; set; }
        //public int tpv { get; set; }
        //public string tpvname { get; set; }
        //public string card { get; set; }
        //public string authcode { get; set; }

        //public DateTime aplicationdate { get; set; }
        //public string aplicationdatestring { get; set; }

        public contableitembase()
        { }

        public contableitembase(long item, long parent, string identifier, int segment, string segmentname, int accountl1, string accountl1name, int accountl1order, int accountl2, string accountl2name, int accountl2order, int accountl3, string accountl3name, int accountl3order, int accountl4, string accountl4name, int accountl4order, DateTime creationdate, decimal ammounttotal)
        {
            this.item = item;
            this.parent = parent;
            this.identifier = identifier;
            this.segment = segment;
            this.segmentname = segmentname;
            this.accountl1 = accountl1;
            this.accountl1name = accountl1name;
            this.accountl1order = accountl1order;
            this.accountl2 = accountl2;
            this.accountl2name = accountl2name;
            this.accountl2order = accountl2order;
            this.accountl3 = accountl3;
            this.accountl3name = accountl3name;
            this.accountl3order = accountl3order;
            this.accountl4 = accountl4;
            this.accountl4name = accountl4name;
            this.accountl4order = accountl4order;
            this.creationdate = creationdate;
            this.ammounttotal = ammounttotal;

            this.GenerateString();
        }

        protected virtual void GenerateString()
        {
            this.creationdatestring = DateTimeUtils.ParseDatetoString(this.creationdate);
            this.ammounttotalstring = MoneyUtils.ParseDecimalToString(this.ammounttotal);
        }

    }
}