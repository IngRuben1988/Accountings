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
    
    public partial class tblaccountl4accounttype
    {
        public int idaccL4acctypereport { get; set; }
        public int idaccountl4 { get; set; }
        public int idaccounttypereport { get; set; }
    
        public virtual tblaccountsl4 tblaccountsl4 { get; set; }
        public virtual tblaccounttypereport tblaccounttypereport { get; set; }
    }
}
