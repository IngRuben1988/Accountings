using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.Models.commons;

namespace VTAworldpass.VTAServices.Services.incomes.model
{
    public class incomeitem : contableitembase
    {
        public int row { get; set; }
        public int index { get; set; }

        public incomeitem()
        { }

        public incomeitem(long item, long parent, string identifier, int segment, string segmentname, int accountl1, string accountl1name, int accountl1order, int accountl2, string accountl2name, int accountl2order, int accountl3, string accountl3name, int accountl3order, int accountl4, string accountl4name, int accountl4order, DateTime creationdate, decimal ammounttotal, string description)
        {
            this.item = item;
            this.parent = parent;
            this.identifier = identifier;
            this.accountl1 = accountl1;
            this.segment = segment;
            this.segmentname = segmentname;
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
            this.description = description;
            this.GenerateString();
        }

        protected override void GenerateString()
        {
            this.creationdatestring = DateTimeUtils.ParseDatetoString(this.creationdate);
            this.ammounttotalstring = MoneyUtils.ParseDecimalToString(this.ammounttotal);
        }
    }
}