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
    
    public partial class tblprefixes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblprefixes()
        {
            this.tblbatchdetailpre = new HashSet<tblbatchdetailpre>();
        }
    
        public int idPrefix { get; set; }
        public int idHotelChain { get; set; }
        public string prefixAbbrev { get; set; }
        public bool prefixUsesHiphen { get; set; }
        public int prefixDigitPositions { get; set; }
        public bool prefixActive { get; set; }
        public string prefixAlt { get; set; }
        public Nullable<bool> websiteAccessAllowed { get; set; }
        public Nullable<decimal> PartnerPrice { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbatchdetailpre> tblbatchdetailpre { get; set; }
    }
}
