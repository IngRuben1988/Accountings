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
    
    public partial class tblbatchdetail
    {
        public int id { get; set; }
        public Nullable<int> idBatch { get; set; }
        public Nullable<int> idPurchase { get; set; }
        public Nullable<int> batchUser { get; set; }
        public System.DateTime batchCreate { get; set; }
        public Nullable<decimal> batchmonto { get; set; }
    
        public virtual tblpurchases tblpurchases { get; set; }
        public virtual tblbatch tblbatch { get; set; }
    }
}