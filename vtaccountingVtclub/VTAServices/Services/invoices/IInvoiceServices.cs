using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.invoices
{
    public interface IInvoiceServices
    {
        void ApplyStateInvoiceItems(int state, int[] invoicetems);
        // Invoice
        Task<IEnumerable<invoice>> GetInvoiceSearch(int? id, int? company, decimal? ammountIni, decimal? ammountEnd, DateTime? applicationDateIni, DateTime? applicationDateFin, DateTime? creationDateIni, DateTime? creationDateFin);
        Task<invoice> GetInvoicebyId(int Invoice);
        Task<invoice> SaveInvoice(invoice invoice);
        Task<invoice> UpdateInvoice(invoice invoice);
        Task<int> DeleteInvoice(long id);
        // InvoiceItems
        Task<List<invoiceitems>> GetInvoiceItembyInvoice(long Invoice);
        Task<invoiceitems> SaveInvoiceItem(invoiceitems invoiceitems);
        Task<invoiceitems> UpdateInvoiceItem(invoiceitems invoiceitems);
        Task<int> DeleteInvoiceItem(int id);
        invoiceitems ConvertInvoiceItemtoHelper(tblinvoiceditem model);
        // InvoicePayments
        Task<List<invoicepayment>> GetInvoicePaymentbyId(int Invoice);
        Task<invoicepayment> SaveInvoicePayments(invoicepayment docitems);
        Task<int> DeleteInvoicePayments(int id);

        string NotPermitAction();
        Task<invoice> SaveInvoiceByBudget(invoice document, invoiceitems docitems, invoicepayment docpaymt);
    }
}
