using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.Models.commons;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTAServices.Services.reports.model
{
    public class docpaymentreservexpreport : docpaymentreserv
    {
        public int accountl1 { get; set; }
        public string accountl1name { get; set; }
        public int accountl1order { get; set; }
        public int accountl2 { get; set; }
        public string accountl2name { get; set; }
        public int accountl2order { get; set; }
        public int accountl3 { get; set; }
        public string accountl3name { get; set; }
        public int accountl3order { get; set; }
        public int accountl4 { get; set; }
        public string accountl4name { get; set; }
        public int accountl4order { get; set; }

        public docpaymentreservexpreport()
        { }

        public docpaymentreservexpreport docpaymentreservexpreport_db_TO_docpaymentreservexpreport(docpaymentreservexpreport_db itemhelper)
        {
            docpaymentreservexpreport item = new docpaymentreservexpreport();

            item.ReservationPayment = itemhelper.ReservationPayment;
            item.Reservation = itemhelper.Reservation;
            item.PaymentMethod = itemhelper.PaymentMethod;
            item.PaymentMethodName = itemhelper.PaymentMethodName;
            item.reservationPaymentQuantity = itemhelper.reservationPaymentQuantity == null ? 0m : (decimal)itemhelper.reservationPaymentQuantity;
            item.reservationPaymentDate = itemhelper.reservationPaymentDate == null ? new DateTime(1970, 1, 1, 0, 0, 0) : (DateTime)itemhelper.reservationPaymentDate;
            item.tarjeta = itemhelper.tarjeta == null ? 0 : (int)itemhelper.tarjeta;
            item.CurrencyPay = itemhelper.CurrencyPay == null ? 0 : (int)itemhelper.CurrencyPay;
            item.CurrencyPayName = itemhelper.CurrencyPayName;
            item.authRef = itemhelper.authRef;
            item.CurrencytoExchange = (int)Currencies.US_Dollar;
            item.CurrencytoExchangeName = Currencies.US_Dollar.ToString().Replace('_', ' ');
            item.CurrencytoExchangeRate = itemhelper.CurrencytoExchangeRate == null ? 0m : (decimal)itemhelper.CurrencytoExchangeRate;

            item.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(item.reservationPaymentQuantity);
            item.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(item.reservationPaymentDate);

            return item;
        }
    }

    public class docpaymentreservexpreport_db : docpaymentreserv_db
    {
        public int accountl1 { get; set; }
        public string accountl1name { get; set; }
        public int accountl1order { get; set; }
        public int accountl2 { get; set; }
        public string accountl2name { get; set; }
        public int accountl2order { get; set; }
        public int accountl3 { get; set; }
        public string accountl3name { get; set; }
        public int accountl3order { get; set; }
        public int accountl4 { get; set; }
        public string accountl4name { get; set; }
        public int accountl4order { get; set; }

        public docpaymentreservexpreport_db()
        { }

        public docpaymentreservexpreport_db(int ReservationPayment, int Reservation, int PaymentMethod, string PaymentMethodName, decimal? reservationPaymentQuantity, DateTime? reservationPaymentDate, int? tarjeta, int? CurrencyPay, string CurrencyPayName, string authRef, int CurrencytoExchange, string CurrencytoExchangeName, decimal? CurrencytoExchangeRate)
        {
            this.ReservationPayment = ReservationPayment;
            this.Reservation = Reservation;
            this.PaymentMethod = this.PaymentMethod;
            this.PaymentMethodName = PaymentMethodName;
            this.reservationPaymentQuantity = reservationPaymentQuantity;
            this.reservationPaymentDate = reservationPaymentDate;
            this.tarjeta = tarjeta;
            this.CurrencyPay = CurrencyPay;
            this.CurrencyPayName = CurrencyPayName;
            this.authRef = authRef;
            this.CurrencytoExchange = (int)Currencies.US_Dollar;
            this.CurrencytoExchangeName = Currencies.US_Dollar.ToString().Replace('_', ' ');
            this.CurrencytoExchangeRate = CurrencytoExchangeRate;
        }
    }
}
