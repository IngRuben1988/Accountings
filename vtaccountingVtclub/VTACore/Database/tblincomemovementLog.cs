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
    
    public partial class tblincomemovementLog
    {
        public long LogId { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public Nullable<int> LogUser { get; set; }
        public string LogObs { get; set; }
        public long idincomeMovement { get; set; }
        public long idincome { get; set; }
        public int idbaccount { get; set; }
        public int idBankAccntType { get; set; }
        public Nullable<int> idtpv { get; set; }
        public string incomemovcard { get; set; }
        public System.DateTime incomemovapplicationdate { get; set; }
        public decimal incomemovchargedamount { get; set; }
        public string incomemovauthref { get; set; }
        public System.DateTime incomemovcreationdate { get; set; }
        public int incomemovcreatedby { get; set; }
        public System.DateTime incomemovupdatedon { get; set; }
        public int incomemovupdatedby { get; set; }
        public bool incomemovcanceled { get; set; }
        public bool incomemovdeleted { get; set; }
    }
}
