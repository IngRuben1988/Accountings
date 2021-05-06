using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.utilsapp.model
{
    public class datetimeinterval
    {
        public string datetimeini { get; set; }

        public string datetimeend { get; set; }

        public DateTime _datetimeini { get; set; }

        public DateTime _datetimeend { get; set; }


        public datetimeinterval()
        {
        }

        public datetimeinterval(DateTime start)
        {
            _datetimeini = start;
        }
        public datetimeinterval(DateTime start, DateTime end)
        {
            _datetimeini = start;
            _datetimeend = end;
        }

        public datetimeinterval(string start, string end)
        {
            this.datetimeini = start;
            this.datetimeend = end;

        }

        public datetimeinterval(string start)
        {
            this.datetimeini = start;
        }

        public datetimeinterval(DateTime _start, DateTime _end, string start, string end)
        {
            this.datetimeini = start;
            this.datetimeend = end;
            this._datetimeini = _start;
            this._datetimeend = _end;

        }


    }
}