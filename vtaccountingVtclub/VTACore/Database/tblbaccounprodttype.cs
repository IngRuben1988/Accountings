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
    
    public partial class tblbaccounprodttype
    {
        public int idbaccountprodtype { get; set; }
        public int idbaccount { get; set; }
        public int idbankprodttype { get; set; }
        public int baccountprodtypecreatedby { get; set; }
        public System.DateTime baccountprodtypecreationdate { get; set; }
        public bool baccountprodtypeactive { get; set; }
        public bool baccountprodtypeallowneg { get; set; }
    
        public virtual tblbankaccount tblbankaccount { get; set; }
        public virtual tblbankprodttype tblbankprodttype { get; set; }
    }
}
