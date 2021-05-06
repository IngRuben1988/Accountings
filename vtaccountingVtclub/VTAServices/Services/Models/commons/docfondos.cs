using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.Models.commons
{
    public class docfondos
    {
        public int Fondo { get; set; }
        public int baccntpaymentMethod { get; set; }
        public string baccntpaymentMethodName { get; set; }
        public int currency { get; set; }
        public string currencyName { get; set; }
        public int baccntfinacialMethod { get; set; }
        public string baccntfinancialMethodName { get; set; }
        public int CurrencyPay { get; set; }
        public string CurrencyPayName { get; set; }
        public DateTime fechaEntrega { get; set; }
        public string fechaEntregaString { get; set; }
        public DateTime fechaInicio { get; set; }
        public string fechaInicioString { get; set; }
        public DateTime fechaFin { get; set; }
        public string fechaFinString { get; set; }
        public DateTime fondoCreationDate { get; set; }
        public string fondoCreationDateString { get; set; }
        public decimal fondoMonto { get; set; }
        public string fondoMontoString { get; set; }
        public string fondoComments { get; set; }
        public decimal fondoFee { get; set; }
        public long? fondoInvoice { get; set; }
        public int? financeType { get; set; }
        public string financeTypeName { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
    }
}