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
    
    public partial class tblbankstatementsdet
    {
        public long idBankStatementsDet { get; set; }
        public Nullable<long> idBankStatements { get; set; }
        public int idTPV { get; set; }
        public Nullable<System.DateTime> bankStatementsDetSaleDate { get; set; }
        public Nullable<decimal> bankStatementsDetSaleAmnt { get; set; }
        public string bankStatementsDetTC { get; set; }
        public string bankStatementsDetAuthCode { get; set; }
    
        public virtual tblbankstatements tblbankstatements { get; set; }
        public virtual tbltpv tbltpv { get; set; }
    }
}