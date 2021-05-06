using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.Models
{
    public class bankaccountsourcedataModel
    {
        public int BankaccountSourcedata { get; set; }

        public int BAccount { get; set; }

        public int SourceData { get; set; }

        public DateTime? sourcedataDateStart { get; set; }

        public List<int> Types { get; set; }
    }
}