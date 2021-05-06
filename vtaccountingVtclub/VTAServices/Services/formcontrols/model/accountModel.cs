using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VTAworldpass.VTAServices.Services.formcontrols.model
{
    public class accountmodel
    {
        public int idBAccount { get; set; }
        public int idCompany { get; set; }
        public string companyName { get; set; }
        public string baccountName { get; set; }
        public List<formselectmodel> formSelectModel { get; set; }
        public accountmodel()
        {
            formSelectModel = new List<formselectmodel>();
        }
    }
}