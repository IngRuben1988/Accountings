using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTACore.Utils
{
    public class MoneyUtils
    {
        public static string ParseDecimalToString(decimal money)
        {
            return string.Format("{0:#,###,##0.00}", money);
        }
    }
}