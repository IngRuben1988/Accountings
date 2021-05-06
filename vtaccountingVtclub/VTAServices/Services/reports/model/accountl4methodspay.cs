using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.reports.model
{
    public class accountl4methodspay
    {

        public int accountl1 { get; set; }
        public int accountl2 { get; set; }
        public int accountl3 { get; set; }
        public int accountl4 { get; set; }
        public string accl1name { get; set; }
        public string accl2name { get; set; }
        public string accl3name { get; set; }
        public string accl4name { get; set; }
        public int accountl1order { get; set; }
        public int accountl2order { get; set; }
        public int accountl3order { get; set; }
        public int accountl4order { get; set; }
        public int paymentmethod { get; set; }
        public string paymentmethodname { get; set; }

        public accountl4methodspay()
        { }

        public accountl4methodspay(int accountl1, int accountl2, int accountl3, int accountl4, string accl1name, string accl2name, string accl3name, string accl4name, int paymentmethod, string paymentMethodName)
        {
            this.accountl1 = accountl1;
            this.accountl2 = accountl2;
            this.accountl3 = accountl3;
            this.accountl4 = accountl4;
            this.accl1name = accl1name;
            this.accl2name = accl2name;
            this.accl3name = accl3name;
            this.accl4name = accl4name;
            this.paymentmethod = paymentmethod;
            this.paymentmethodname = paymentMethodName;
        }

        public accountl4methodspay(int accountl1, int accountl2, int accountl3, int accountl4, string accl1name, string accl2name, string accl3Name, string accl4name, int accountl1order, int accountl2order, int accountl3order, int accountl4order, int paymentmethod, string paymentmethodname)
        {
            this.accountl1 = accountl1;
            this.accountl2 = accountl2;
            this.accountl3 = accountl3;
            this.accountl4 = accountl4;
            this.accl1name = accl1name;
            this.accl2name = accl2name;
            this.accl3name = accl3Name;
            this.accl4name = accl4name;
            this.accountl1order = accountl1order;
            this.accountl2order = accountl2order;
            this.accountl3order = accountl3order;
            this.accountl4order = accountl4order;
            this.paymentmethod = paymentmethod;
            this.paymentmethodname = paymentmethodname;
        }

    }
}