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
    
    public partial class tblpermissions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblpermissions()
        {
            this.tbluserpermissions = new HashSet<tbluserpermissions>();
        }
    
        public int idPermission { get; set; }
        public string permissionController { get; set; }
        public string permissionAction { get; set; }
        public string permissionArea { get; set; }
        public string permissionImageClass { get; set; }
        public string permissionTitle { get; set; }
        public string permissionDescription { get; set; }
        public string permissisonActiveli { get; set; }
        public bool permissionEstatus { get; set; }
        public int permissionParentId { get; set; }
        public bool permissionIsParent { get; set; }
        public Nullable<bool> permissionHasChild { get; set; }
        public bool PermissionMenu { get; set; }
        public bool permissionIsHtml { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbluserpermissions> tbluserpermissions { get; set; }
    }
}
