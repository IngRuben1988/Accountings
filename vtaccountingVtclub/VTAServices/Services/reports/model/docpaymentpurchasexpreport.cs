using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTAServices.Services.Models.commons;

namespace VTAworldpass.VTAServices.Services.reports.model
{
    public class docpaymentpurchasexpreport :docpaymentpurchase
    {
        public int accountl1 { get; set; }
        public string accountl1Name { get; set; }
        public int accountl1Order { get; set; }
        public int accountl2 { get; set; }
        public string accountl2Name { get; set; }
        public int accountl2Order { get; set; }
        public int accountl3 { get; set; }
        public string accountl3Name { get; set; }
        public int accountl3Order { get; set; }
        public int accountl4 { get; set; }
        public string accountl4Name { get; set; }
        public int accountl4Order { get; set; }

        public docpaymentpurchasexpreport()
        {

        }
    }
}