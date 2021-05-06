using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class userModel
    {
        public int idUser { get; set; }
        public string userName { get; set; }
        public string userCompledName { get; set; }
        public string userEmail { get; set; }
        public string userProfile { get; set; }
        public int idProfile { get; set; }
        public string password { get; set; }
        public string passwordconfirm { get; set; }
        public string data { get; set; }
    }
}