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
    
    public partial class tblinvoicecomments
    {
        public int idinvoicecomment { get; set; }
        public long idinvoice { get; set; }
        public int iduser { get; set; }
        public string invoicecommentdescription { get; set; }
        public System.DateTime invoicecommentcreactiondate { get; set; }
    
        public virtual tblinvoice tblinvoice { get; set; }
        public virtual tblusers tblusers { get; set; }
    }
}
