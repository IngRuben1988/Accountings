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
    
    public partial class tblreservationcancellation
    {
        public int idReservationCancel { get; set; }
        public Nullable<int> idReservation { get; set; }
        public Nullable<System.DateTime> cancellationDate { get; set; }
        public string cancellationReason { get; set; }
        public Nullable<int> idCancelType { get; set; }
        public Nullable<int> idCurrency { get; set; }
        public Nullable<int> terminal { get; set; }
        public Nullable<decimal> amountReturned { get; set; }
        public Nullable<decimal> exchangeRate { get; set; }
    
        public virtual tblcurrencies tblcurrencies { get; set; }
        public virtual tblreservations tblreservations { get; set; }
    }
}
