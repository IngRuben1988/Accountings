using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Utils;

namespace VTAworldpass.VTAServices.Services.Models.commons
{
    public class contablebase
    {
        public long item { get; set; }
        public int number { get; set; }
        public string identifier { get; set; }
        public int currency { get; set; }
        public string currencyname { get; set; }
        public int company { get; set; }
        public string companyname { get; set; }
        public int segment { get; set; }
        public string segmentname { get; set; }
        public DateTime applicationdate { get; set; }
        public string applicationdatestring { get; set; }
        public string applicationdatestring0 { get; set; }
        public decimal cost { get; set; }
        public string coststring { get; set; }

        public contablebase()
        { }

        public contablebase(long item, int number, string identifier, int currency, string currencyname, decimal cost, int company, string companyname, int segment, string segmentname, DateTime applicationdate)
        {
            this.item = item;
            this.number = number;
            this.identifier = identifier;
            this.currency = currency;
            this.currencyname = currencyname;
            this.company = company;
            this.companyname = companyname;
            this.segment = segment;
            this.segmentname = segmentname;
            this.cost = cost;

            GenerateString();
        }

        protected virtual void GenerateString()
        {
            this.applicationdatestring = DateTimeUtils.ParseDatetoString(this.applicationdate);
            this.applicationdatestring0 = DateTimeUtils.ParseDatetoString(this.applicationdate);
            this.coststring = MoneyUtils.ParseDecimalToString(this.cost);

        }
    }
}
