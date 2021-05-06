using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.bank.model
{
    public class fondosmaxammount
    {
        public int      idfondosmax                { get; set; }
        public int      idcompany                  { get; set; }
        public string   companyname                { get; set; }
        public int      idpaymentmethod            { get; set; }
        public string   paymentmethodname          { get; set; }
        public int      idcurrency                 { get; set; }
        public string   currencyname               { get; set; }
        public decimal  fondomaxammount            { get; set; }
        public string   fondomaxdescription        { get; set; }
        public int      fondomaxcreatedby          { get; set; }
        public DateTime fondomaxcreationdate       { get; set; }
        public string   fondomaxcreationdatestring { get; set; }
    }
}