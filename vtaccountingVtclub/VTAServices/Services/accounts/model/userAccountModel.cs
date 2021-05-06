using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class userAccountModel
    {
        public int idUserBAccount { get; set; }
        public int idUser { get; set; }
        public int idBAccount { get; set; }
        public int userbaccountCreateBy { get; set; }
        public string userbaccountDate { get; set; }
        public bool userbaccountActive { get; set; }
    }
}