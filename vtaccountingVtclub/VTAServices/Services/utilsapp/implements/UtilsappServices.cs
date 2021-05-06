using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.utilsapp;
using VTAworldpass.VTAServices.Services.utilsapp.helper;

namespace VTAworldpass.VTAServices.Services.utilsapp.implements
{
    public class UtilsappServices : utilsappHelper, IUtilsappServices
    {
        private IAccountServices accountServices;
        public UtilsappServices(IAccountServices _accountServices)
        {
            this.accountServices = _accountServices;
        }

        public DateTime getStartDateCalendardocuments(DateTime? date, double? weeksBefore)
        {
            DateTime final = DateTime.Now;
            if (date != null) final = (DateTime)date;


            var permission = this.accountServices.isInPermission("outtimestart");

            if (permission == false)
            {
                if (weeksBefore == null) weeksBefore = Globals.WeeksToExtemporal;
                final = final.AddDays(-1 * ((double)weeksBefore * Globals.WeekDays));
                return final;
            }
            else
            {
                var year = DateTime.Now.Year - 1;
                return final = new DateTime(year, 1, 1);
            }
        }

        public DateTime getFinalDateCalendardocuments()
        {
            DateTime final = DateTime.Now;

            var permission = this.accountServices.isInPermission("outtimeend");

            if (permission == false)
            { return final; }
            else
            { return final = new DateTime(DateTime.Now.Year, 12, 31); }
        }

        public DateTime getMondayCurrentWeek(DateTime? date)
        {
            DateTime calculate = DateTime.Now;
            if (date != null) calculate = (DateTime)date;
            return this.getMondaybyDate(calculate);
        }

        public DateTime getSundayCurrentWeek(DateTime? date)
        {
            DateTime calculate = DateTime.Now;
            if (date != null) calculate = (DateTime)date;
            return this.getSundaybyDate(calculate);
        }

        public int diferenceDays(DateTime promise, DateTime start)
        {
            return this.daysDifference(promise, start);
        }
    }
}