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
    
    public partial class tblsourcedata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblsourcedata()
        {
            this.tblbankaccountsourcedata = new HashSet<tblbankaccountsourcedata>();
            this.tblexpensereportsourcedata = new HashSet<tblexpensereportsourcedata>();
        }
    
        public int idsourcedata { get; set; }
        public string sourcedataname { get; set; }
        public bool sourcedataactive { get; set; }
        public string sourcedatadescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankaccountsourcedata> tblbankaccountsourcedata { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblexpensereportsourcedata> tblexpensereportsourcedata { get; set; }
    }
}
