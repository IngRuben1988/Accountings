using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class formSelectModel
    {
        public string value { get; set; }
        public int valueInt { get; set; }
        public string text { get; set; }
        public string shortText { get; set; }
        public string shortText2 { get; set; }
        public string selected { get; set; }

        public formSelectModel()
        { }

        public formSelectModel(string value, string text, string selected)
        {
            this.value = value;
            this.text = text;
            this.selected = selected;
        }

        public formSelectModel initialize()
        {
            formSelectModel frm = new formSelectModel();
            frm.value = "0"; frm.valueInt = 0; frm.text = "Todos ..."; return frm;
        }

        public formSelectModel initialize1Min()
        {
            formSelectModel frm = new formSelectModel();
            frm.value = "-1"; frm.valueInt = -1; frm.text = "Todos ..."; return frm;
        }
    }
}