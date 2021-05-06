using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTAworldpass.VTAServices.Services.formcontrols.model
{
    public class formselectmodel
    {
        public string value { get; set; }
        public int    valueint { get; set; }
        public string text { get; set; }
        public string shorttext { get; set; }
        public string shorttext2 { get; set; }
        public string selected { get; set; }

        public formselectmodel()
        { }

        public formselectmodel(string value, string text, string selected)
        {
            this.value = value;
            this.text = text;
            this.selected = selected;
        }

        public formselectmodel initialize()
        {
            formselectmodel frm = new formselectmodel();
            frm.value    = "0";
            frm.valueint = 0;
            frm.text     = "Seleccione ...";
            return frm;
        }

        public formselectmodel initialize1Min()
        {
            formselectmodel frm = new formselectmodel();
            frm.value    = "-1";
            frm.valueint = -1;
            frm.text     = "Seleccione ...";
            return frm;
        }
    }
}
