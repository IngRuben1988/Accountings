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
    
    public partial class tblmovementtype
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblmovementtype()
        {
            this.tblbankstatements2 = new HashSet<tblbankstatements2>();
        }
    
        public int idMovementType { get; set; }
        public string movementTypeName { get; set; }
        public bool movementTypeActive { get; set; }
        public int idOperationType { get; set; }
    
        public virtual tbloperationtype tbloperationtype { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankstatements2> tblbankstatements2 { get; set; }
    }
}
