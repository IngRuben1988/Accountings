using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.reports.model
{
    public class expensereportsourcedata
    {

        public int idexpensereportsourcedata { get; set; }

        public int company { get; set; }

        public int sourcedata { get; set; }

        public IList<int> types { get; set; }

        public expensereportsourcedata()
        { }

        public expensereportsourcedata(int idexpensereportsourcedata, int company, int sourcedata, IList<int> Types)
        {
            this.idexpensereportsourcedata = idexpensereportsourcedata;
            this.company = company;
            this.sourcedata = sourcedata;
            this.types = Types;
        }

    }
}