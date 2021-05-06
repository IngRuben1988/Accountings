using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.Models.commons
{
    public class docpaymentparentreserv
    {
        public long ReservationPayment { get; set; }
        public int Reservation { get; set; }
        public int ReservationType { get; set; }
        public string ReservationTypeName { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal reservationPaymentQuantity { get; set; }
        public string reservationPaymentQuantityString { get; set; }
        public int Hotel { get; set; }
        public int HotelOrder { get; set; }
        public string HotelName { get; set; }
        public DateTime reservationPaymentDate { get; set; }
        public string reservationPaymentDateString { get; set; }
        public int idTerminal { get; set; }
        public int idCreditCardType { get; set; }
        public int tarjeta { get; set; }
        public int CurrencyPay { get; set; }
        public string CurrencyPayName { get; set; }
        public string authRef { get; set; }
        public bool bankStatementLinked { get; set; }
        public long bankStatement { get; set; }
        public int CurrencytoExchange { get; set; }
        public string CurrencytoExchangeName { get; set; }
        public decimal CurrencytoExchangeRate { get; set; }
        public string authcode { get; set; }
    }
}