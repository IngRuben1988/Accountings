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
    
    public partial class tblbugettype
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblbugettype()
        {
            this.tblinvoiceditem = new HashSet<tblinvoiceditem>();
        }
    
        public int idbudgettype { get; set; }
        public string budgettypename { get; set; }
        public string budgettypedescription { get; set; }
        public int budgettypeorder { get; set; }
        public bool budgettypeactive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblinvoiceditem> tblinvoiceditem { get; set; }
    }
}
