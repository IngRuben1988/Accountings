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
    
    public partial class tblextgroupcompanies
    {
        public int IdExtGroupCompanies { get; set; }
        public int idCompany { get; set; }
        public int IdExternalGroup { get; set; }
        public bool externalgroupcompanyActive { get; set; }
    
        public virtual tblcompanies tblcompanies { get; set; }
        public virtual tblexternalgroup tblexternalgroup { get; set; }
    }
}
