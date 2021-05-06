using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTAworldpass.VTAServices.Services.utilsapp
{
    public interface IUtilsappServices
    {
        DateTime getStartDateCalendardocuments(DateTime? date, double? weeksBefore);
        DateTime getFinalDateCalendardocuments();
        DateTime getMondayCurrentWeek(DateTime? date);
        DateTime getSundayCurrentWeek(DateTime? date);
        int diferenceDays(DateTime promise, DateTime start);
    }
}
