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
    
    public partial class tblaccountsl3
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblaccountsl3()
        {
            this.tblprofaccclass3 = new HashSet<tblprofaccclass3>();
            this.tblaccountsl4 = new HashSet<tblaccountsl4>();
        }
    
        public int idaccountl3 { get; set; }
        public int idaccountl2 { get; set; }
        public string accountl3name { get; set; }
        public string accountl3description { get; set; }
        public Nullable<bool> accountl3active { get; set; }
        public int accountl3order { get; set; }
    
        public virtual tblaccountsl2 tblaccountsl2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblprofaccclass3> tblprofaccclass3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblaccountsl4> tblaccountsl4 { get; set; }
    }
}
