using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.VTAServices.Services.attachments;
using VTAworldpass.VTAServices.Services.attachments.implements;
using VTAworldpass.VTAServices.Services.comments;
using VTAworldpass.VTAServices.Services.comments.implements;
using VTAworldpass.VTAServices.Services.comments.model;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{
    [SessionTimeOut]
    public class ComplementController : Controller
    {
        private readonly IAttachmentServices attachmentServices;
        private readonly AttachmentServices  servicesA;
        private readonly ICommentServices    commentServices;
        private readonly CommentServices     servicesB;

        public ComplementController()
        {
            this.attachmentServices = servicesA;
            this.commentServices = servicesB;
        }

        //************Attachments************
        [HttpPost]
        public ActionResult AttachFileUploadInvoice(int idAttach, int parameter)
        {
            List<string> filessaved     = new List<string>();
            List<string> filesrejected  = new List<string>();
            if (!(Request.Files.Count > 10 || Request.Files.Count == 0))
            {
                var data_ = new tblinvoiceattach();
                int idAttachSaved = 0;
                for (int i = 0; i <= Request.Files.Count - 1; i++)
                {
                    if (Request.Files[i].FileName == "" || Request.Files[i].ContentLength > 2100000)
                        return Json(JsonSerialResponse.ResultError("No se ha seleccionado ningun archivo o uno de ellos excede el tamaño máximo de 2mb c/u"), JsonRequestBehavior.AllowGet);
                }
                for (int i = 0; i <= Request.Files.Count - 1; i++)
                {
                    try
                    {
                        //Getting pach
                        string currentYear = (DateTime.Now.Year).ToString();
                        var patchPhisical = Server.MapPath("~/VTDrive/Attachments/Invoice/" + currentYear);
                        var patchVirtual = "~/VTDrive/Attachments/Invoice/" + currentYear;
                        // Creating Direcotry if not exist
                        if (!Directory.Exists(patchPhisical))
                        {
                            Directory.CreateDirectory(patchPhisical);
                        }
                        // Gettig metadata File to sabe attahFile object
                        tblinvoiceattach att = this.attachmentServices.ObtainAttachmentInv(Request.Files, i, idAttach, parameter);
                        string pathToSave = Path.Combine(Server.MapPath(patchVirtual), Path.GetFileName(att.invoiceattachshortname));
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

        [HttpPost]
        public ActionResult AttachFileUploadIncome(int idAttach, int parameter)
        {
            List<string> filessaved = new List<string>();
            List<string> filesrejected = new List<string>();
            if (!(Request.Files.Count > 10 || Request.Files.Count == 0))
            {
                var data_ = new tblincomeattach();
                int idAttachSaved = 0;
                for (int i = 0; i <= Request.Files.Count - 1; i++)
                {
                    if (Request.Files[i].FileName == "" || Request.Files[i].ContentLength > 2100000) // Aprox 2mb
                    return Json(JsonSerialResponse.ResultError("No se ha seleccionado ningun archivo o uno de ellos excede el tamaño máximo de 2mb c/u"), JsonRequestBehavior.AllowGet); 
                }
                for (int i = 0; i <= Request.Files.Count - 1; i++)
                {
                    try
                    {
                        //Getting pach
                        string currentYear = (DateTime.Now.Year).ToString();
                        var patchPhisical = Server.MapPath("~/VTDrive/Attachments/Income/" + currentYear);
                        var patchVirtual = "~/VTDrive/Attachments/Income/" + currentYear;
                        // Creating Direcotry if not exist
                        if (!Directory.Exists(patchPhisical))
                        {
                            Directory.CreateDirectory(patchPhisical);
                        }
                        // Gettig metadata File to sabe attahFile object
                        tblincomeattach att = this.attachmentServices.ObtainAttachmentInc(Request.Files, i, idAttach, parameter);
                        string pathToSave   = Path.Combine(Server.MapPath(patchVirtual), Path.GetFileName(att.incomeattachshortname));
                        att.incomeattachdirectory = patchVirtual;
                        Request.Files[i].SaveAs(pathToSave);
                        data_ = this.attachmentServices.SaveAttachmentInc(att);
                        idAttachSaved = att.idattach;

                        filessaved.Add(att.incomeattachname);
                    }
                    catch (Exception e)
                    {
                        return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
                    }

                }
                return Json(JsonSerialResponse.ResultSuccess(filessaved, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonSerialResponse.ResultError("Solo se puede cargar hasta  10 archivos de 2Mb c/u. y máximo total de 10 mb, o no hay seleccionado ningun archivo"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult   GetAttachmentInv(int parameter)
        {
            try
            {
                var data = this.attachmentServices.GetAttachmentInv(parameter);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                Log.Error("ComplementsController-getAttachmentsdocument", e);
                return Json(JsonSerialResponse.ResultRegisterNotFound("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult   GetAttachmentInc(int parameter)
        {
            try
            {
                var data = this.attachmentServices.GetAttachemntInc(parameter);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                Log.Error("ComplementsController-getAttachmentsdocument", e);
                return Json(JsonSerialResponse.ResultRegisterNotFound("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteAttachmentInv(int parameter)
        {
            try
            {
                var recordfile = this.attachmentServices.GetAttachmentInvSimple(parameter);
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
                            var data = this.attachmentServices.DeleteAttachmentInv(parameter);
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
                            var data = this.attachmentServices.DeleteAttachmentInv(parameter);
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
            {
                Log.Error("ComplementsController-deleteattachment", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteAttachmentInc(int parameter)
        {
            try
            {
                var recordfile = this.attachmentServices.GetAttachemntIncSimple(parameter);
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
                            var data = this.attachmentServices.DeleteAttachmentInc(parameter);
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
                            var data = this.attachmentServices.DeleteAttachmentInc(parameter);
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
            {
                Log.Error("ComplementsController-deleteattachment", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }


        //************Comments************
        [HttpPost]
        public JsonResult   AddCommentIncome(int parameter, string comments)
        {
            try
            {
                Comments helper = new Comments { Description = comments, Income = parameter };
                helper.Invoice = parameter;
                this.commentServices.SaveInc(helper);
                return Json(JsonSerialResponse.ResultSuccess(comments, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("documentsController-AddCommentInvoice", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult   GetCommentIncome(int parameter)
        {
            try
            {
                var result = this.commentServices.GetCommentsByInc(parameter);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("documentsController-getCommentInvoice", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult   AddCommentInvoice(string comments, int parameter)
        {
            try
            {
                Comments helper = new Comments { Description = comments, Invoice = parameter };
                helper.Invoice = parameter;
                this.commentServices.SaveInv(helper);
                return Json(JsonSerialResponse.ResultSuccess(comments, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("documentsController-addCommentInvoice", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult   GetCommentInvoice(int parameter)
        {
            try
            {
                var result = this.commentServices.GetCommentsByInv(parameter);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("documentsController-getCommentInvoice", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

    }
}