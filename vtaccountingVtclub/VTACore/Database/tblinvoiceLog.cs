//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VTAworldpass.VTACore.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblinvoiceLog
    {
        public int LogId { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public Nullable<int> LogUser { get; set; }
        public string LogObs { get; set; }
        public long idinvoice { get; set; }
        public int idcompany { get; set; }
        public Nullable<int> idcurrency { get; set; }
        public System.DateTime invoicedate { get; set; }
        public int invoicenumber { get; set; }
        public int invoicecreatedby { get; set; }
        public System.DateTime invoicecreateon { get; set; }
        public int invoiceupdatedby { get; set; }
        public System.DateTime invoiceupdateon { get; set; }
        public int invoicedeletedby { get; set; }
        public System.DateTime invoicedeleteon { get; set; }
    }
}
