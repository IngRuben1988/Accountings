using System;
using System.Collections.Generic;
using System.Linq;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.attachments.model;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.Models.commons;

namespace VTAworldpass.VTAServices.Services.incomes.model
{
    public class income : contablebase
    {
        public int row { get; set; }
        public int index { get; set; }

        public DateTime creationdate { get; set; }
        public string creationdatestring { get; set; }
        public int createdby { get; set; }
        public DateTime updateon { get; set; }
        public string updateonstring { get; set; }
        public int updatedby { get; set; }

        public virtual IEnumerable<incomeitem> incomeitems { get; set; }
        public virtual IList<attachment> attachments { get; set; }


        public income()
        { }

        public income(long item, int number, string identifier, int currency, string currencyname, decimal cost, int company, string companyname, int segment, string segmentname, int createdby, int updatedby, DateTime applicationdate, DateTime creationdate, DateTime updateon)
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
            this.createdby = createdby;
            this.updatedby = updatedby;
            this.applicationdate = applicationdate;
            this.creationdate = creationdate;
            this.updateon = updateon;
            this.cost = cost;

            this.GenerateString();
        }

        public income(long item, int number, string identifier, int currency, string currencyname, int company, string companyname, int segment, string segmentname, int createdby, int updatedby, List<tblincomeitem> incomeitems, DateTime applicationdate, DateTime creationdate, DateTime updateon)
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
            this.createdby = createdby;
            this.updatedby = updatedby;
            this.applicationdate = applicationdate;
            this.creationdate = creationdate;
            this.updateon = updateon;
            this.cost = incomeitems != null && incomeitems.Count() != 0 ? incomeitems.Sum(c => c.incomeitemsubtotal) : 0m;
            this.GenerateString();
        }

        public income(long item, int number, string identifier, int currency, string currencyname, int company, string companyname, int segment, string segmentname, int createdby, int updatedby, ICollection<tblincomeitem> incomeitems, DateTime applicationdate, DateTime creationdate, DateTime updateon)
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
            this.createdby = createdby;
            this.updatedby = updatedby;
            this.applicationdate = applicationdate;
            this.creationdate = creationdate;
            this.updateon = updateon;
            this.cost = incomeitems != null && incomeitems.Count() != 0 ? incomeitems.Sum(c => c.incomeitemsubtotal) : 0m;
            this.GenerateString();
        }

        protected override void GenerateString()
        {
            this.applicationdatestring = DateTimeUtils.ParseDatetoString(this.applicationdate);
            this.applicationdatestring0 = DateTimeUtils.ParseDatetoString2(this.applicationdate);
            this.creationdatestring = DateTimeUtils.ParseDatetoString(this.creationdate);
            this.updateonstring = DateTimeUtils.ParseDatetoString(this.updateon);
            this.coststring = MoneyUtils.ParseDecimalToString(this.cost);
        }

    }
}