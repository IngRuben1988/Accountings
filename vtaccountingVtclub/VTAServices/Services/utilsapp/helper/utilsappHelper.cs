using System;
using VTAworldpass.VTACore.Utils;

namespace VTAworldpass.VTAServices.Services.utilsapp.helper
{
    public abstract class utilsappHelper
    {
        // Seraching Monday
        private int[] WeekDays = { 6, 0, 1, 2, 3, 4, 5 };

        protected DateTime getMondaybyDate(DateTime date)
        {
            DateTime currentdate = date.Subtract(new TimeSpan(WeekDays[DateTimeUtils.dayOfWeek(date)], 0, 0, 0));
            return currentdate;
        }
        protected DateTime getSundaybyDate(DateTime date)
        {
            DateTime currentdate = date.Add(new TimeSpan(6 - WeekDays[DateTimeUtils.dayOfWeek(date)], 0, 0, 0));
            return currentdate;
        }

        protected int daysDifference(DateTime promise, DateTime start)
        {
            TimeSpan tsDate = start.Subtract(promise);
            return tsDate.Days;
        }
    }
}