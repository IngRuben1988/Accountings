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
    
    public partial class tblbatch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblbatch()
        {
            this.tblbatchdetail = new HashSet<tblbatchdetail>();
            this.tblbatchdetailpre = new HashSet<tblbatchdetailpre>();
        }
    
        public int idBatch { get; set; }
        public Nullable<System.DateTime> batchDateCreate { get; set; }
        public string idFactura { get; set; }
        public Nullable<int> batchUser { get; set; }
        public Nullable<decimal> batchMonto { get; set; }
        public Nullable<System.DateTime> batchDate { get; set; }
        public Nullable<int> idPaymentForm { get; set; }
        public Nullable<int> idbaccount { get; set; }
        public Nullable<System.DateTime> datePay { get; set; }
        public Nullable<int> idcompany { get; set; }
        public Nullable<decimal> creditMonto { get; set; }
    
        public virtual tblbankaccount tblbankaccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbatchdetail> tblbatchdetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbatchdetailpre> tblbatchdetailpre { get; set; }
    }
}
