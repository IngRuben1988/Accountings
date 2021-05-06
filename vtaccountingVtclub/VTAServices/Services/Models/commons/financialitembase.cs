using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Utils;

namespace VTAworldpass.VTAServices.Services.Models.commons
{
    public class financialitembase
    {
        public long item { get; set; }
        public long parent { get; set; }
        public string identifier { get; set; }
        public int segment { get; set; }
        public string segmentname { get; set; }
        public int company { get; set; }
        public string companyname { get; set; }
        public int companyorder { get; set; }

        public int bankaccount { get; set; }
        public string bankaccountname { get; set; }
        public int bankaccnttype { get; set; }
        public string bankaccnttypename { get; set; }
        public int currency { get; set; }
        public string currencyname { get; set; }
        public int tpv { get; set; }
        public string tpvname { get; set; }
        public string card { get; set; }
        public string authcode { get; set; }
        public decimal ammounttotal { get; set; }
        public string ammounttotalstring { get; set; }
        public int baclass { get; set; }
        public string description { get; set; }
        public DateTime creationdate { get; set; }
        public string creationdatestring { get; set; }
        public DateTime aplicationdate { get; set; }
        public string aplicationdatestring { get; set; }



        public financialitembase()
        { }


        protected virtual void GenerateString()
        {
            this.creationdatestring = DateTimeUtils.ParseDatetoString(this.creationdate);
            this.ammounttotalstring = MoneyUtils.ParseDecimalToString(this.ammounttotal);
        }
    }
}