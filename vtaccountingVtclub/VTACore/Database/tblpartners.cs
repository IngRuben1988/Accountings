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
    
    public partial class tblpartners
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblpartners()
        {
            this.tblpurchases = new HashSet<tblpurchases>();
            this.tblreservations = new HashSet<tblreservations>();
            this.tblbatchdetailpre = new HashSet<tblbatchdetailpre>();
        }
    
        public int idPartner { get; set; }
        public int idHotelChain { get; set; }
        public string partnerName { get; set; }
        public bool partnerActive { get; set; }
        public Nullable<bool> batchShow { get; set; }
    
        public virtual tblhotelchains tblhotelchains { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpurchases> tblpurchases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblreservations> tblreservations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbatchdetailpre> tblbatchdetailpre { get; set; }
    }
}
