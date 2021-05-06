using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.VTAServices.Services.invoices;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTAServices.Services.attachments;
using VTAworldpass.VTAServices.Services.comments;
using VTAworldpass.VTAServices.Services.comments.model;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{   
    [SessionTimeOut]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceServices invoiceservice;
        private readonly IAttachmentServices attachmentServices;
        private readonly ICommentServices commentServices;

        public InvoiceController(IInvoiceServices _invoiceservice, IAttachmentServices _attachmentServices, ICommentServices _commentServices)
        {
            this.invoiceservice = _invoiceservice;
            this.attachmentServices = _attachmentServices;
            this.commentServices = _commentServices;
        }

        // GET: invoice
        [Permissions]
        public ActionResult invoiceapp()
        {
            return View();
        }
        // GET: invoice search
        [Permissions]
        public ActionResult invoicesearch()
        {
            return View();
        }


        /**-------------------- invoices -----------------------------**/
        [HttpPost] // OK
        [Permissions]
        public async Task<ActionResult> SendInvoice(invoice parameter)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await this.invoiceservice.SaveInvoice(parameter), SystemControl.VTAMessages[0]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[0] + ":::SendInvoice", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [Permissions]
        public async Task<JsonResult> GetInvoice(int? id, int? InvoiceNumber, int? company, decimal? ammountIni, decimal? ammountEnd, string applicationDateIni, string applicationDateFin, string creationDateIni, string creationDateFin)
        {
            try
            {
                var data = await this.invoiceservice.GetInvoiceSearch(InvoiceNumber, company, ammountIni, ammountEnd, DateTimeUtils.ParseStringToDate(applicationDateIni), DateTimeUtils.ParseStringToDate(applicationDateFin), DateTimeUtils.ParseStringToDate(creationDateIni), DateTimeUtils.ParseStringToDate(creationDateFin));
                return Json(JsonSerialResponse.ResultSuccess(data, SystemControl.VTAMessages[3]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[3] + ":::GetInvoice", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] // OK
        [Permissions]
        public async Task<ActionResult> UpdateInvoice(invoice parameter)
        {
            try
            {
                var data = await this.invoiceservice.UpdateInvoice(parameter);
                return Json(JsonSerialResponse.ResultSuccess(data, SystemControl.VTAMessages[1]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[1] + ":::UpdateInvoice", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [Permissions]
        public async Task<ActionResult> DeleteInvoice(int id)
        {
            try
            {
                var result = await this.invoiceservice.DeleteInvoice(id);
                return Json(JsonSerialResponse.ResultSuccess(result, SystemControl.VTAMessages[2]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[2] + ":::DeleteInvoice", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        /**-------------------- invoices item ------------------------**/
        [HttpPost]
        public async Task<ActionResult> SendInvoiceItem(invoiceitems parameter)
        {
            try
            {
                var data = await this.invoiceservice.SaveInvoiceItem(parameter);
                return Json(JsonSerialResponse.ResultSuccess(data, SystemControl.VTAMessages[0]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[0] + ":::SendInvoiceItem", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + " [Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendInvoiceItemUpdate(invoiceitems parameter)
        {
            try
            {
                var result = await this.invoiceservice.UpdateInvoiceItem(parameter);
                return Json(JsonSerialResponse.ResultSuccess(result, SystemControl.VTAMessages[1]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[1] + ":::SendInvoiceItemUpdate", e); 
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + " [Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendInvoiceItemDelete(int parameter)
        {
            try
            {
                var resultModel = await this.invoiceservice.DeleteInvoiceItem(parameter);
                return Json(JsonSerialResponse.ResultSuccess(parameter, SystemControl.VTAMessages[2]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[2] + ":::SendInvoiceItemDelete", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + " [Stack-trace]" + e.StackTrace);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> GetInvoiceitemsbyId(long? parameter)
        {
            try
            {
                var data = parameter == null ? null : await this.invoiceservice.GetInvoiceItembyInvoice((long)parameter);
                return Json(JsonSerialResponse.ResultSuccess(data, SystemControl.VTAMessages[3]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[3] + ":::GetInvoiceitemsbyId", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }
        /**--------------------invoices payments-----------------------------**/
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SendInvoicePayment(invoicepayment parameter)
        {
            try
            {
                var data = await this.invoiceservice.SaveInvoicePayments(parameter);
                return Json(JsonSerialResponse.ResultSuccess(data, SystemControl.VTAMessages[1]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[1] + ":::SendInvoicePayment", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + " [Stack-trace]" + e.StackTrace);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> InvoicePaymentsGetbyId(int? id)
        {
            try
            {
                var data = id == null ? null : await this.invoiceservice.GetInvoicePaymentbyId((int)id);
                return Json(JsonSerialResponse.ResultSuccess(data, SystemControl.VTAMessages[3]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[3] + ":::InvoicePaymentsGetbyId", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> InvoicePaymentDelete(int id)
        {
            try
            {
                var resultModel = await this.invoiceservice.DeleteInvoicePayments(id);
                return Json(JsonSerialResponse.ResultSuccess(id, SystemControl.VTAMessages[2]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[7] + ":::" + SystemControl.VTAActions[2] + ":::InvoicePaymentDelete", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + " [Stack-trace]" + e.StackTrace);
            }
        }


        ///**************************** FILE UPLOAD POST AJAX ********************/
        [HttpPost]
        [Permissions]
        public ActionResult AttachFileInvoiceAjax(int idAttach, int id)
        {
            List<string> filessaved = new List<string>();
            List<string> filesrejected = new List<string>();

            if (!(Request.Files.Count > 10 || Request.Files.Count == 0))
            {
                var data_ = new tblinvoiceattach();
                int idAttachSaved = 0;

                for (int i = 0; i <= Request.Files.Count - 1; i++)
                {
                    if (Request.Files[i].FileName == "" || Request.Files[i].ContentLength > 2100000) // Aprox 2mb
                    { return Json(JsonSerialResponse.ResultError("No se ha seleccionado ningun archivo o uno de ellos excede el tamaño máximo de 2mb c/u"), JsonRequestBehavior.AllowGet); }
                }

                for (int i = 0; i <= Request.Files.Count - 1; i++)
                {
                    try
                    {
                        //Getting pach
                        string currentYear = (DateTime.Now.Year).ToString();
                        var patchPhisical = Server.MapPath("~/Attachments/Invoice/" + currentYear);
                        var patchVirtual = "~/Attachments/Invoice/" + currentYear;

                        // Creating Direcotry if not exist
                        if (!Directory.Exists(patchPhisical))
                        {
                            Directory.CreateDirectory(patchPhisical);
                        }
                        // Gettig metadata File to sabe attahFile object
                        tblinvoiceattach att = this.attachmentServices.ObtainAttachmentInv(Request.Files, i, idAttach, id);

                        string pathToSave = Path.Combine(Server.MapPath(patchVirtual),
                                                   Path.GetFileName(att.invoiceattachshortname));
                        att.invoiceattachdirectory = patchVirtual;
                        Request.Files[i].SaveAs(pathToSave);
                        data_ = this.attachmentServices.SaveAttachmentInv(att);
                        idAttachSaved = att.idinvoiceattach;

                        filessaved.Add(att.invoiceattachname);

                    }
                    catch (Exception e)
                    {
                        return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
                    }
                }

                    return Json(JsonSerialResponse.ResultSuccess(filessaved, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonSerialResponse.ResultError("Solo se puede cargar hasta  10 archivos de 2Mb c/u. y máximo total de 10 mb, o no hay seleccionado ningun archivo"), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult getAttachmentsInvoice(int id)
        {
            try
            {
                var data = this.attachmentServices.GetAttachmentInv(id);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("invoiceController-getAttachmentsdocument", e); return Json(JsonSerialResponse.ResultRegisterNotFound("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        [Permissions]
        public ActionResult DeleteAttachment(int id)
        {

            try
            {

                var recordfile = this.attachmentServices.GetAttachmentInvSimple(id);
                if (recordfile == null)
                {
                    return Json(JsonSerialResponse.ResultError(" No se encuentra el registro Error ->"), JsonRequestBehavior.AllowGet);
                }
                else
                {

                    if (System.IO.File.Exists(Server.MapPath(recordfile.directoryfull)))
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(recordfile.directoryfull));
                            var data = this.attachmentServices.DeleteAttachmentInv(id);
                            return Json(JsonSerialResponse.ResultSuccess(true, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                        }
                        catch (IOException e)
                        {
                            Log.Error("No se puede eliminar el archivo de la direción solicitada" + "-----------> Exception: " + e.Message + "[Stack Trace] ------------> " + e.StackTrace);
                            Log.Error("Nuevo nombre de archivo.");
                            throw new Exception("No se puede borrar el registro de attachment.");
                        }
                    }
                    else
                    {
                        Log.Info("No existe archivo, se procede a borrar solo el registro.");
                        try
                        {
                            var data = this.attachmentServices.DeleteAttachmentInv(id);
                            return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception e)
                        {
                            Log.Error("No se puede borrar el registro de attachment.", e);
                            throw new Exception("No se puede borrar el registro de attachment.");
                        }
                    }
                }
            }
            catch (Exception e)
            { Log.Error("invoiceController-deleteattachment", e); return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        /**************** COMMENTS ACTIONS ***********************/

        [AllowAnonymous]
        [HttpGet]
        public JsonResult getCommentInvoice(int id)
        {
            try
            {
                var result = this.commentServices.GetCommentsByInv(id);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("invoiceController-getCommentInvoice", e); return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult addCommentInvoice(string comments, int id)
        {
            try
            {
                Comments helper = new Comments { Description = comments, Invoice = id };

                helper.Invoice = id;
                this.commentServices.SaveInv(helper);
                return Json(JsonSerialResponse.ResultSuccess(comments, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("invoiceController-addCommentInvoice", e); return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }
    }
}