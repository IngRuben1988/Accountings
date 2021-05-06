using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.attachments;
using VTAworldpass.VTAServices.Services.attachments.model;
using VTAworldpass.VTAServices.Services.utilsapp;
using VTAworldpass.VTAServices.Services.utilsapp.model;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{
    public class UtilsappController : Controller
    {
        private IUtilsappServices utilsappServices;
        private IAccountServices accoutnServices;
        private IAttachmentServices attachmentServices;
        UnitOfWork unity;

        public UtilsappController(IUtilsappServices _utilsappServices, IAccountServices _accoutnServices, IAttachmentServices _attachmentServices)
        {
            this.utilsappServices = _utilsappServices;
            this.accoutnServices = _accoutnServices;
            this.attachmentServices = _attachmentServices;
            this.unity = new UnitOfWork();
        }

        [HttpGet]
        public JsonResult getAviableDateIncInvAdd(DateTime? date)
        {
            try
            {
                datetimeinterval interval = new datetimeinterval(DateTimeUtils.ParseDatetoString(this.utilsappServices.getStartDateCalendardocuments(DateTime.Now, Globals.WeeksToExtemporal)), DateTimeUtils.ParseDatetoString(this.utilsappServices.getFinalDateCalendardocuments()));
                return Json(JsonSerialResponse.ResultSuccess(interval, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }

        }

        public JsonResult calculateTax(decimal value)
        {
            var result = unity.ParametersRepository.Get(x => x.parameterDescription.Contains("VTA-Tax"), null, "").FirstOrDefault();
            decimal taxes;
            if (result != null)
            {
                taxes = decimal.Multiply(Convert.ToDecimal(result.parameterValue), value) / 100;
            }
            else
            {
                taxes = decimal.Multiply(Globals.vta_TAX, value) / 100;
            }
            return Json(JsonSerialResponse.ResultSuccess(taxes, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult downloadbinarydocumentBills(int id, int? type)
        {
            attachment result = new attachment();
            if(type != null)
            {
                result = this.attachmentServices.GetAttachemntIncSimple(id);
            }
            else
            {
                result = this.attachmentServices.GetAttachmentInvSimple(id);
            }

            var exist = System.IO.File.Exists(Server.MapPath(result.directoryfull));

            if (!exist)
            {
                Log.Info(string.Concat("El archivo no existe.", result.filename));
                return RedirectToAction("customexplain", "Errors", new { id = 1 });
            }
            else
            {
                Log.Info(string.Concat("Archivo generado. {0}", result.filename));
                return File(result.directoryfull, string.Concat("{0};charset=UTF-8;base64", result.contentType), result.filename);
            }


        }
    }
}