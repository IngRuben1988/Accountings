using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class accountModel
    {
        public int idBAccount { get; set; }
        public int idCompany { get; set; }
        public string companyName { get; set; }
        public string baccountName { get; set; }
        public List<formSelectModel> formSelectModel { get; set; }
        public accountModel()
        {
            formSelectModel = new List<formSelectModel>();
        }
    }
}