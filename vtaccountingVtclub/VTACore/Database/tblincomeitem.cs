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
    
    public partial class tblincomeitem
    {
        public long idincomeitem { get; set; }
        public long idincome { get; set; }
        public int idAccountl4 { get; set; }
        public int idincomeitemstatus { get; set; }
        public int iduser { get; set; }
        public System.DateTime incomeitemdate { get; set; }
        public decimal incomeitemsubtotal { get; set; }
        public string incomedescription { get; set; }
    
        public virtual tblaccountsl4 tblaccountsl4 { get; set; }
        public virtual tblincome tblincome { get; set; }
        public virtual tblinvoiceitemstatus tblinvoiceitemstatus { get; set; }
        public virtual tblusers tblusers { get; set; }
    }
}
