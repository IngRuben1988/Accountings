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
    
    public partial class tblbankstat2fondo
    {
        public long idBankStat2Fondo { get; set; }
        public int idFondos { get; set; }
        public Nullable<long> idBankStatements2In { get; set; }
        public Nullable<long> idBankStatements2Out { get; set; }
    
        public virtual tblfondos tblfondos { get; set; }
        public virtual tblbankstatements2 tblbankstatements2 { get; set; }
        public virtual tblbankstatements2 tblbankstatements21 { get; set; }
    }
}