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
    
    public partial class tblmemberships
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblmemberships()
        {
            this.tblmembers = new HashSet<tblmembers>();
            this.tblpurchases = new HashSet<tblpurchases>();
            this.tblreservations = new HashSet<tblreservations>();
        }
    
        public int idMembership { get; set; }
        public int idMembershipStatus { get; set; }
        public string membershipNumberPref { get; set; }
        public Nullable<long> membershipNumberFolio { get; set; }
        public string membershipNumberPartner { get; set; }
        public Nullable<System.DateTime> membershipCancelationDate { get; set; }
        public string membershipObservations { get; set; }
        public string membershipPassword { get; set; }
        public System.Guid membershipGUID { get; set; }
        public Nullable<System.DateTime> membershipRecordDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblmembers> tblmembers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblpurchases> tblpurchases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblreservations> tblreservations { get; set; }
    }
}
