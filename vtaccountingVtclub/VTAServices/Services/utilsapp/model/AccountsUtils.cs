using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.utilsapp.model
{
    public class AccountsUtils
    {
        public static string IdentifierComplete(string company, string profile, int invoiceNumber)
        {
            return string.Concat(company.ToUpper(), '-', profile.ToUpper(), '-', string.Format("{0:00000}", invoiceNumber));
        }
    }
}