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
    
    public partial class tblinvoiceditem
    {
        public long idinvoiceitem { get; set; }
        public long idinvoice { get; set; }
        public int iduser { get; set; }
        public int idaccountl4 { get; set; }
        public int idinvoiceitemstatus { get; set; }
        public int idbudgettype { get; set; }
        public int idsupplier { get; set; }
        public Nullable<decimal> itemsubtotal { get; set; }
        public string itemdescription { get; set; }
        public bool ditemistax { get; set; }
        public decimal itemtax { get; set; }
        public string itemidentifier { get; set; }
        public string itemsupplierother { get; set; }
        public decimal itemothertax { get; set; }
        public bool itemsinglepayment { get; set; }
        public int itemcreatedby { get; set; }
        public System.DateTime itemcreateon { get; set; }
        public int itemupdatedby { get; set; }
        public System.DateTime itemupdateon { get; set; }
    
        public virtual tblaccountsl4 tblaccountsl4 { get; set; }
        public virtual tblbugettype tblbugettype { get; set; }
        public virtual tblinvoice tblinvoice { get; set; }
        public virtual tblinvoiceitemstatus tblinvoiceitemstatus { get; set; }
        public virtual tblSuppliers tblSuppliers { get; set; }
        public virtual tblusers tblusers { get; set; }
    }
}