using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.bank.model
{
    public class financialstateitemstatements
    {
        public long idbankstatement { get; set; }
        public List<financialstateitem> fin_states { get; set; }

        public financialstateitemstatements() { }
    }
}