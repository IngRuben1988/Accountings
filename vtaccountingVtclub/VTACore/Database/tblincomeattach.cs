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
    
    public partial class tblincomeattach
    {
        public int idincomeattach { get; set; }
        public long idincome { get; set; }
        public int idattach { get; set; }
        public string incomeattachname { get; set; }
        public string incomeattachshortname { get; set; }
        public string incomeattachdirectory { get; set; }
        public string incomeattachcontenttype { get; set; }
        public int incomeattachuserlastchange { get; set; }
        public Nullable<System.DateTime> incomeattachdatelastchange { get; set; }
        public bool incomeattachactive { get; set; }
    
        public virtual tblattachments tblattachments { get; set; }
        public virtual tblincome tblincome { get; set; }
        public virtual tblusers tblusers { get; set; }
    }
}
