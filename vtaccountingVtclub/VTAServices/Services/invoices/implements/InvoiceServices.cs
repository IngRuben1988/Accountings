using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.Business.Services.Implementations;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTAServices.Services.invoices.helpers;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.utilsapp;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTACore;
using VTAworldpass.VTAServices.Services.Logger;
using VTAworldpass.VTAServices.Services.attachments;

using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTAServices.Services.accounts.implements;
using VTAworldpass.Business.Services.Logger.Implementations;

namespace VTAworldpass.VTAServices.Services.invoices.implements
{
    public class InvoiceServices: InvoiceHelper, IInvoiceServices
    {
        private readonly IAccountServices accountServices;
        private readonly ILogsServices logsServices;
        private readonly UnitOfWork unit;
        private readonly GeneralRepository invoiceRepository;
        private readonly IUtilsappServices utilsappServices;

        public InvoiceServices(IAccountServices _accountServices, ILogsServices _logsServices, IUtilsappServices _utilsappServices, IAttachmentServices _attachmentServices)
        {
            this.accountServices   = _accountServices;
            this.logsServices      = _logsServices;
            this.utilsappServices  = _utilsappServices;
            //this.attachmentServices = _attachmentServices;
            this.invoiceRepository = new GeneralRepository();
            this.unit = new UnitOfWork();
        }

        public InvoiceServices()
        {
            this.accountServices = new AccountServices();
            this.utilsappServices = new UtilsappServices(accountServices);
            this.invoiceRepository = new GeneralRepository();
            this.logsServices = new LogsServices(accountServices);
            this.unit = new UnitOfWork();
        }

        public void ApplyStateInvoiceItems(int state, int[] invoicetems)
        {
            throw new NotImplementedException();
        }

        /********************invoice***********************/
        public async Task<IEnumerable<invoice>> GetInvoiceSearch
        (
          int? id, int? company, decimal? ammountIni, decimal? ammountEnd, 
          DateTime? applicationDateIni, DateTime? applicationDateFin, DateTime? creationDateIni, DateTime? creationDateFin
        )
        {
            List<invoice> lstInvoices = new List<invoice>();
            int[] listHotles = new int[accountServices.AccountCompanies().Count];
            listHotles = accountServices.AccountCompanies().ToArray();
            int[] listAccl3 = new int[9]; // por implementar
            lstInvoices = ConvertTbltoHelper(await this.invoiceRepository.gettblInvoiceSearchAsync(id, ammountIni, ammountEnd, company, applicationDateIni, applicationDateFin, creationDateIni, creationDateFin, listHotles, listAccl3));
            return lstInvoices.OrderBy(x => x.applicationdate);
        }

        public Task<invoice> GetInvoicebyId(int Invoice)
        {
            throw new NotImplementedException();
        }

        public async Task<invoice> SaveInvoice(invoice invoice)
        {
            // Preparing invoice
            tblinvoice model = await prepareToSave(invoice);
            try
            {
                model.invoicecreatedby = this.accountServices.AccountIdentity();
                model.invoiceupdatedby = this.accountServices.AccountIdentity();
                model = await this.unit.InvoiceRepository.InsertAsync(model);
                unit.CommitAsync();
                await this.logsServices.addTblLog(model, "Actualización de registro de Gastos");
                invoice.id = Convert.ToInt32(model.idinvoice);
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-saveDocument", e);
                throw new Exception(string.Concat("No se puede guardar la los datos.", "error en: ", e.Message, ",  [Stack Trace ----->>>]", e.StackTrace));
            }
            return ConvertTbltoHelper(this.unit.InvoiceRepository.Get(x => x.idinvoice == invoice.id, null, "tblcompanies,tblcurrencies,tblusers.tblprofilesaccounts").FirstOrDefault());
        }

        public async Task<invoice> UpdateInvoice(invoice invoice)
        {
            try
            {
                // Preparing document to update
                tblinvoice model = this.unit.InvoiceRepository.Get(x => x.idinvoice == invoice.id, null, "").FirstOrDefault();
                prepareToUpdate(invoice, model);
                model.invoiceupdatedby = this.accountServices.AccountIdentity();
                var resultModel = await this.unit.InvoiceRepository.UpdateAsync(model);
                invoice.id = model.idinvoice;
                // Saving log
                var resultLog = await this.logsServices.addTblLog(model, "Actualización de registro de Gastos");
                return ConvertTbltoHelper(resultModel);
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-updateDocument", e);
                this.unit.Rollback();
                throw new Exception(string.Concat("No se puede actualizar la información general de gasto.", "error en: ", e.Message, ",  [Stack Trace ----->>>]", e.StackTrace));
            }
        }

        public async Task<int> DeleteInvoice(long id)
        {
            var invoice = unit.InvoiceRepository.Get(x => x.idinvoice == id, null, "").FirstOrDefault();
            if (invoice.tblpayment.Count != 0)
            {
                List<long> idList = invoice.tblpayment.Select(y => y.idpayment).ToList();
                foreach (long model in idList)
                {
                    var resultModelP = await this.DeleteInvoicePayments((int)model);
                }
            }

            if (invoice.tblinvoiceditem.Count != 0)
            {
                List<long> idList = invoice.tblinvoiceditem.Select(y => y.idinvoiceitem).ToList();
                foreach (int model in idList)
                {
                    var resultModelI = await this.DeleteInvoiceItem((int)model);
                }
            }

            if (invoice.tblinvoiceattach.Count != 0)
            {
                List<tblinvoiceattach> lstModels = invoice.tblinvoiceattach.ToList();
                foreach (tblinvoiceattach model in lstModels)
                {
                    var resultModelAt = await this.unit.InvoiceAttachmentRepository.DeleteAsync(model);
                }
            }
            // Saving log
            var resultmodelLog = await this.logsServices.addTblLog(invoice, "Eliminando de registro tblinvoice");
            var resultModel = await this.unit.InvoiceRepository.DeleteAsync(invoice);
            return resultModel;
        }

        /********************invoice item***********************/
        public async Task<List<invoiceitems>> GetInvoiceItembyInvoice(long Invoice)
        {
            List<invoiceitems> list = new List<invoiceitems>();
            try
            {
                list = ConvertTbltoHelper((await unit.InvoiceItemRepository.GetAsync(x => x.idinvoice == Invoice, null, "tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1,tblinvoiceitemstatus,tblSuppliers,tblbugettype,tblinvoice.tblcompanies")).ToList());
                return list;
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-getdocumentsItembyInvoice ", e);
                throw new Exception(string.Concat("No es posible recuperar los conceptos de gastos.", e));
            }
        }

        public async Task<invoiceitems> SaveInvoiceItem(invoiceitems invoiceitems)
        {
            var _invoice = await unit.InvoiceRepository.FindAsync(x => x.idinvoice == invoiceitems.id);
            tblinvoiceditem model;
            try
            {
                if (_invoice == null)
                {
                    Log.Error("InvoiceServices-saveDocumentItem ");
                    throw new Exception(string.Concat("No es posible asociar a una factura el conceptos de gasto."));
                }

                model = prepareToSave(invoiceitems);
                model.iduser = this.accountServices.AccountIdentity();
                model.idinvoiceitemstatus = this.CalculateDociItemStatusToSave(_invoice, model, utilsappServices);
                model.itemcreatedby = 1;
                model = await unit.InvoiceItemRepository.InsertAsync(model);
                this.unit.CommitAsync();
                var resultLog = await this.logsServices.addTblLog(model, "Guardo de registro de Gastos");
                invoiceitems.item = model.idinvoiceitem;

            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-saveDocumentItem", e);
                throw new Exception(string.Concat("No es posible guardar los conceptos de gastos.", e));
            }
            return invoiceitems;
        }

        public async Task<invoiceitems> UpdateInvoiceItem(invoiceitems invoiceitems)
        {
            tblinvoiceditem model = new tblinvoiceditem();
            try
            {
                model = this.unit.InvoiceItemRepository.Get(t => t.idinvoiceitem == invoiceitems.item, null, "").FirstOrDefault();
                this.prepareToUpdate(invoiceitems, model);
                model.iduser = this.accountServices.AccountIdentity();
                var resultModel = await this.unit.InvoiceItemRepository.UpdateAsync(model);
                this.unit.Commit();
                // Saving Log
                var resultLog = await this.logsServices.addTblLog(model, "Actualizando de registro tblinvoiceitem");

                return invoiceitems;
                //return convertTbltoHelper(this.unity.InvoiceItemRepository.Get(t => t.idInvoiceItem == docitems.Item, null, "").FirstOrDefault());
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-updateDocumentItem", e);
                throw new Exception(string.Concat("No se puede actualizar los datos del  gasto.", "error en: ", e.Message, ",  [Stack Trace ----->>>]", e.StackTrace));
            }
        }

        public async Task<int> DeleteInvoiceItem(int id)
        {
            try
            {
                tblinvoiceditem model = this.unit.InvoiceItemRepository.Get(x => x.idinvoiceitem == id, null, "").FirstOrDefault();
                // Saving log
                var resultLog = await this.logsServices.addTblLog(model, "Eliminando de registro tblinvoiceitem");
                // Deleting
                var resultModel = await this.unit.InvoiceItemRepository.DeleteAsync(model);
                return resultModel;
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-deletedodcItem", e);
                this.unit.Rollback();
                throw new Exception(string.Concat("No se puede eliminar los datos del gasto.", "error en: ", e.Message, ",  [Stack Trace ----->>>]", e.StackTrace));
            }
        }

        public invoiceitems ConvertInvoiceItemtoHelper(tblinvoiceditem model)
        {
            return this.ConvertTbltoHelper(model);
        }

        public async Task<List<invoicepayment>> GetInvoicePaymentbyId(int Invoice)
        {
            List<invoicepayment> list = new List<invoicepayment>();
            try
            {
                list = ConvertTbltoHelper((await unit.PaymentsVtaRepository.GetAsync(x => x.tblinvoice.idinvoice == Invoice, null, "tblbankaccount.tblcurrencies,tblbankaccount.tblbank,tblinvoice,tblbankprodttype")).ToList());
                return list;
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-GetInvoicePaymentbyId ", e);
                throw new Exception(string.Concat("No es posible recuperar los conceptos de gastos.", "error en: InvoiceServices-getdocumentsPaymentbyInvoice", e.Message, ",  [Stack Trace ----->>>]", e.StackTrace));
            }
        }

        public async Task<invoicepayment> SaveInvoicePayments(invoicepayment payment)
        {
            var _invoice = await unit.InvoiceRepository.FindAsync(x => x.idinvoice == payment.Invoice);
            tblpayment model;
            try
            {
                if (_invoice == null)
                {
                    Log.Error("InvoiceServices-saveDocumentPayments");
                    throw new Exception(string.Concat("No es posible asociar a una factura el pago."));
                }
                payment.aplicationDate = _invoice.invoicedate;
                model = this.prepareToSave(payment);
                model.paymentcreatedby = this.accountServices.AccountIdentity();
                model.paymentupdatedby = this.accountServices.AccountIdentity();
                model = await this.unit.PaymentsVtaRepository.InsertAsync(model);
                //this.unity.CommitAsync();
                var resultLog = await this.logsServices.addTblLog(model, "Guardo de registro de Gastos");
                payment.Payment = model.idpayment;
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-saveDocumentPayments", e);
                this.unit.Rollback();
                throw new Exception(string.Concat("No es posible guardar el pago.", "error en: ", e.Message, ",  [Stack Trace ----->>>]", e.StackTrace));
            }
            return payment;
        }

        public async Task<int> DeleteInvoicePayments(int id)
        {
            try
            {
                tblpayment model = this.unit.PaymentsVtaRepository.Get(x => x.idpayment == id, null, "").FirstOrDefault();
                // Saving log
                var resultLog = await this.logsServices.addTblLog(model, "Eliminando de registro tblpayment");
                // Deleting
                var resultModel = await this.unit.PaymentsVtaRepository.DeleteAsync(model);
                return resultModel;
            }
            catch (Exception e)
            {
                Log.Error("InvoiceServices-deleteDocumentPayments", e);
                this.unit.Rollback();
                throw new Exception(string.Concat("No se puede eliminar el pago.", "error en: ", e.Message, ",  [Stack Trace ----->>>]", e.StackTrace));
            }
        }

        public string NotPermitAction()
        {
            return Globals.NotPermitAction;
        }

        public async Task<invoice> SaveInvoiceByBudget(invoice document, invoiceitems docitems, invoicepayment docpaymt)
        {

            invoice documentSaved = new invoice();
            invoiceitems docitemsSaved = new invoiceitems();
            invoicepayment docpaymtSaved = new invoicepayment();

            // Saving Document
            try
            {
                documentSaved = await this.SaveInvoice(document);
            }

            catch (Exception e)
            {
                Log.Error("InvoiceServices-SaveInvoiceByBudget[invoice]", e);

                return null;
            }

            // Saving Docitem
            try
            {
                docitems.id = documentSaved.id;
                docitemsSaved = await this.SaveInvoiceItem(docitems);
            }

            catch (Exception e)
            {
                Log.Error("InvoiceServices-SaveInvoiceByBudget[invoiceitems]", e);
                var result = this.DeleteInvoice(docitemsSaved.id);

                return null;
            }

            // Saving Payment
            try
            {
                docpaymt.Invoice = documentSaved.id;
                docpaymtSaved = await this.SaveInvoicePayments(docpaymt);
            }

            catch (Exception e)
            {
                Log.Error("InvoiceServices-SaveInvoiceByBudget[invoicepayment]", e);
                var result = await DeleteInvoice(docitemsSaved.id);

                return null;
            }

            return documentSaved;
        }

        public invoiceitems convertInvoiceItemtoHelper(tblinvoiceditem model)
        {
            return this.ConvertTbltoHelper(model);
        }

        public invoicepayment convertPaymentToHelper(tblpayment model)
        {
            return this.ConvertTbltoHelper(model);
        }
    }
}