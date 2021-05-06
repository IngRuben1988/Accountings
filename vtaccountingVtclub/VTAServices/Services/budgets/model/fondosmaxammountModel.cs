using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.budgets.model
{
    public class fondosmaxammountModel
    {
        public int FondosMax { get; set; }
        public int Company { get; set; }
        public string CompanyName { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public int Currency { get; set; }
        public string CurrencyName { get; set; }
        public decimal fondosmaxAmmount { get; set; }
        public string fondosmaxDescription { get; set; }
        public int fondosmaxCreatedby { get; set; }
        public DateTime fondosmaxCreationDate { get; set; }
        public string fondosmaxCreationDateString { get; set; }

        public bool editable { get; set; }
    }
}