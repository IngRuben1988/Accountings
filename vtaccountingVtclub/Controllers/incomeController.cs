using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.VTAServices.Services.attachments;
using VTAworldpass.VTAServices.Services.comments;
using VTAworldpass.VTAServices.Services.incomes;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.comments.model;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace vtaccounting.vtclub.Controllers
{   
    [SessionTimeOut]
    public class IncomeController : Controller
    {
        private readonly IIncomeServices incomeServices;
        private readonly IAttachmentServices attachmentServices;
        private readonly ICommentServices commentServices;

        public IncomeController(IIncomeServices _incomeServices, IAttachmentServices _attachmentServices, ICommentServices _commentServices)
        {
            this.incomeServices = _incomeServices;
            this.attachmentServices = _attachmentServices;
            this.commentServices = _commentServices;
        }
        [Permissions]
        public ActionResult Index()
        {
            return View();
        }

        [Permissions]
        public ActionResult incomeaddvw()
        {
            return View();
        }

        [Permissions]
        public ActionResult incomeeditvw()
        {
            return View("incomeeditvw");
        }

        [Permissions]
        public ActionResult incomedeletevw()
        {
            return View("incomedeletevw");
        }


        /**************************incomes *****************************/
        [HttpPost]
        [Permissions]
        public async Task<ActionResult> SaveIncome(income item)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.SaveIncome(item), SystemControl.VTAMessages[3]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6]+":::"+SystemControl.VTAActions[0], e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [Permissions]
        public async Task<JsonResult> GetIncomes(int? id, int? number, int? company, decimal? ammountIni, decimal? ammountEnd, string applicationDateIni, string applicationDateFin, string creationDateIni, string creationDateFin)
        {
            try
            {
                var data = await this.incomeServices.getIncomesSearchAsync(number, company, ammountIni, ammountEnd, DateTimeUtils.ParseStringToDate(applicationDateIni), DateTimeUtils.ParseStringToDate(applicationDateFin), DateTimeUtils.ParseStringToDate(creationDateIni), DateTimeUtils.ParseStringToDate(creationDateFin));
                return Json(JsonSerialResponse.ResultSuccess(data, SystemControl.VTAMessages[3]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[3], e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Permissions]
        public async Task<ActionResult> UpdateIncome(income item)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.UpdateIncome(item), SystemControl.VTAMessages[1]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[1], e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [Permissions]
        public async Task<ActionResult> DeleteIncome(long id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.DeleteIncome(id), SystemControl.VTAMessages[2]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[2], e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }


        /**************************incomes items*****************************/
        [HttpGet]
        public async Task<ActionResult> GetIncomeItemsDetails(long id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.GetIncomeItemList(id), SystemControl.VTAMessages[3]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[3]+":::getincomeitemsdetails", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveIncomeItem(incomeitem item)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.SaveIncomeItem(item), SystemControl.VTAMessages[0]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[0]+":::saveincomeitem", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateIncomeItem(incomeitem item)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.UpdateIncomeItem(item), SystemControl.VTAMessages[1]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[1]+":::updateincomeitem", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteIncomeItem(long id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.DeleteIncomeItem(id), SystemControl.VTAMessages[2]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[2] + ":::deleteincomeitem", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }


        /**************************incomes payments*****************************/
        [HttpGet]
        public async Task<ActionResult> GetIncomeMovements(long id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.GetIncomeMovementsList(id), SystemControl.VTAMessages[3]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[3]+":::getincomemovements", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveIncomeMovement(incomepayment item)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.SaveIncomeMovement(item),SystemControl.VTAMessages[0]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[0] + ":::saveincomeitem", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }

        }

        [HttpPost]
        public async Task<ActionResult> DeleteIncomeMovement(long id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(await incomeServices.DeleteIncomeMovement(id), SystemControl.VTAMessages[2]), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error(SystemControl.VTAControllers[6] + ":::" + SystemControl.VTAActions[2]+":::deleteincomemovement", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        public ActionResult AttachFileIncomeAjax(int idAttach, int id)
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
                    { return Json(JsonSerialResponse.ResultError("No se ha seleccionado ningun archivo o uno de ellos excede el tamaño máximo de 2mb c/u"), JsonRequestBehavior.AllowGet); }
                }

                for (int i = 0; i <= Request.Files.Count - 1; i++)
                {
                    try
                    {
                        //Getting pach
                        string currentYear = (DateTime.Now.Year).ToString();
                        var patchPhisical = Server.MapPath("~/Attachments/Income/" + currentYear);
                        var patchVirtual = "~/Attachments/Income/" + currentYear;

                        // Creating Direcotry if not exist
                        if (!Directory.Exists(patchPhisical))
                        {
                            Directory.CreateDirectory(patchPhisical);
                        }
                        // Gettig metadata File to sabe attahFile object
                        tblincomeattach att = this.attachmentServices.ObtainAttachmentInc(Request.Files, i, idAttach, id);

                        string pathToSave = Path.Combine(Server.MapPath(patchVirtual),
                                                   Path.GetFileName(att.incomeattachshortname));
                        att.incomeattachdirectory = patchVirtual;
                        Request.Files[i].SaveAs(pathToSave);
                        data_ = this.attachmentServices.SaveAttachmentInc(att);
                        idAttachSaved = att.idincomeattach;

                        filessaved.Add(att.incomeattachname);

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
        public JsonResult getAttachmentsIncome(int id)
        {
            try
            {
                var data = this.attachmentServices.GetAttachemntInc(id);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("incomeController-getAttachmentsdocument", e); return Json(JsonSerialResponse.ResultRegisterNotFound("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        [Permissions]
        public ActionResult DeleteAttachmentIncome(int id)
        {

            try
            {

                var recordfile = this.attachmentServices.GetAttachemntIncSimple(id);
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
                            var data = this.attachmentServices.DeleteAttachmentInc(id);
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
                            var data = this.attachmentServices.DeleteAttachmentInc(id);
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
            { Log.Error("incomeController-deleteattachment", e); return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult getCommentIncome(int id)
        {
            try
            {
                var result = this.commentServices.GetCommentsByInc(id);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("invoiceController-getCommentInvoice", e); return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult addCommentIncome(string comments, int id)
        {
            try
            {
                Comments helper = new Comments { Description = comments, Income = id };

                helper.Income = id;
                this.commentServices.SaveInc(helper);
                return Json(JsonSerialResponse.ResultSuccess(comments, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("invoiceController-addCommentInvoice", e); return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }
    }
}