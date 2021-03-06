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
    
    public partial class tblbankstatements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblbankstatements()
        {
            this.tblbankstateincome = new HashSet<tblbankstateincome>();
            this.tblbankstatementsdet = new HashSet<tblbankstatementsdet>();
            this.tblbankstateparentreserv = new HashSet<tblbankstateparentreserv>();
            this.tblbankstatepurchase = new HashSet<tblbankstatepurchase>();
            this.tblbankstatereserv = new HashSet<tblbankstatereserv>();
        }
    
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
    
        public virtual tblbankaccount tblbankaccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankstateincome> tblbankstateincome { get; set; }
        public virtual tblbankstatementmethod tblbankstatementmethod { get; set; }
        public virtual tblcompanies tblcompanies { get; set; }
        public virtual tbltpv tbltpv { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankstatementsdet> tblbankstatementsdet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankstateparentreserv> tblbankstateparentreserv { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankstatepurchase> tblbankstatepurchase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankstatereserv> tblbankstatereserv { get; set; }
    }
}
