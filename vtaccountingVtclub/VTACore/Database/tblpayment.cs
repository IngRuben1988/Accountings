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
    
    public partial class tblpayment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblpayment()
        {
            this.tblbankstat2invoice = new HashSet<tblbankstat2invoice>();
        }
    
        public long idpayment { get; set; }
        public long idinvoice { get; set; }
        public int idbaccount { get; set; }
        public int idbankprodttype { get; set; }
        public System.DateTime paymentdate { get; set; }
        public decimal paymentamount { get; set; }
        public string paymentauthref { get; set; }
        public int paymentcreatedby { get; set; }
        public System.DateTime paymentcreateon { get; set; }
        public int paymentupdatedby { get; set; }
        public System.DateTime paymentupdatedon { get; set; }
    
        public virtual tblbankaccount tblbankaccount { get; set; }
        public virtual tblbankprodttype tblbankprodttype { get; set; }
        public virtual tblinvoice tblinvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankstat2invoice> tblbankstat2invoice { get; set; }
    }
}