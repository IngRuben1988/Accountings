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
    
    public partial class tblbankstatementsLog
    {
        public int Log_Id { get; set; }
        public Nullable<System.DateTime> Log_Date { get; set; }
        public Nullable<int> Log_User { get; set; }
        public string Log_Obs { get; set; }
        public long idBankStatements { get; set; }
        public int idBAccount { get; set; }
        public int idTPV { get; set; }
        public int idCompany { get; set; }
        public Nullable<int> idBankStatementMethod { get; set; }
        public System.DateTime bankstatementAplicationDate { get; set; }
        public decimal bankstatementAppliedAmmount { get; set; }
        public Nullable<decimal> bankstatementBankFee { get; set; }
        public Nullable<decimal> bankstatementAppliedAmmountFinal { get; set; }
        public string bankStatementsTC { get; set; }
        public string bankStatementsAuthCode { get; set; }
    }
}
