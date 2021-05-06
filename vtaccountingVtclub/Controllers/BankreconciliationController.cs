using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.VTAServices.Services.bankreconciliation;
using VTAworldpass.VTAServices.Services.bankreconciliation.helpers;
using VTAworldpass.VTAServices.Services.Models;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{
    public class BankreconciliationController : Controller
    {
        private readonly IBankReconciliationServices bankReconciliationService;

        public BankreconciliationController(IBankReconciliationServices _bankReconciliationServices)
        {
            this.bankReconciliationService = _bankReconciliationServices;
        }

        // GET: bankreconciliation

        [Permissions]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Permissions]
        public ActionResult bankstatement()
        {
            return View("bankstatementsupfile");
        }

        [Permissions]
        public ActionResult baccountstatements()
        {
            return View("baccountconciliation");
        }

        // Actions
        [AllowAnonymous]
        public ActionResult getSCBKPos(string dateReportStart, string dateReportEnd, decimal? ammountStart, decimal? ammountEnd, int? Tpv, int? externalgroup, int? Company, int? PaymentMethod, int? hotel, int? status)
        {
            try
            {
                int _Tpv = (Tpv == null ? 0 : (int)Tpv); int _externalgroup = (externalgroup == null ? 0 : (int)externalgroup); int _PaymentMethod = (PaymentMethod == null ? 0 : (int)PaymentMethod); int _hotel = (hotel == null ? 0 : (int)hotel); int _statusReconciliation = (status == null ? -1 : (int)status); decimal ams = ammountStart == null ? 0m : (decimal)ammountStart; decimal ame = ammountEnd == null ? 0m : (decimal)ammountEnd;
                var data = bankReconciliationService.getBakReconcilitions(DateTimeUtils.ParseStringToDate(dateReportStart), DateTimeUtils.ParseStringToDate(dateReportEnd), _Tpv, _externalgroup, (int)Company, _PaymentMethod, _hotel, _statusReconciliation, ams, ame, false, false);


                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("bankreconciliationController-getSCBKPos", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public ActionResult GetStatement2CBANKACCNTPos(string dateReportStart, string dateReportEnd, decimal? ammountStart, decimal? ammountEnd, int? Tpv, int? externalgroup, int? Company, int? PaymentMethod,  int? status, int? typeSourceData, string description)
        {
            try
            {
                int _Tpv = (Tpv == null ? 0 : (int)Tpv); int _externalgroup = (externalgroup == null ? 0 : (int)externalgroup); int _PaymentMethod = (PaymentMethod == null ? 0 : (int)PaymentMethod); int _typeSourceData = (typeSourceData == null ? 0 : (int)typeSourceData); int _statusReconciliation = (status == null ? -1 : (int)status); decimal ams = ammountStart == null ? 0m : (decimal)ammountStart; decimal ame = ammountEnd == null ? 0m : (decimal)ammountEnd;
                var data = bankReconciliationService.getBankStat2Reconcilitions(DateTimeUtils.ParseStringToDate(dateReportStart), DateTimeUtils.ParseStringToDate(dateReportEnd), _Tpv, _externalgroup, (int)Company, _PaymentMethod, _typeSourceData, description, _statusReconciliation, ams, ame, false, false);


                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("bankreconciliationController-getbaccountStatement2CBKPos", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public ActionResult getSCBKPosbyId(long id)
        {
            try
            {
                var data = bankReconciliationService.getBakReconciliationsDetailsbyId(id);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("bankreconciliationController-getSCBKPosbyId", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }


        [AllowAnonymous]
        public ActionResult getSCBKPosbyItemsId(long id)
        {
            try
            {
                var result = bankReconciliationService.getBakReconciliationsDetailsbyId(id);
                List<financialstateitemModel> list = new List<financialstateitemModel>();

                if (result != null)
                {
                    list = result.financialstateitemlist.ToList();
                }

                return Json(JsonSerialResponse.ResultSuccess(list, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                Log.Error("bankreconciliationController-getSCBKPosbyItemsId", e);
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult GetStatement2CBANKACCNTPosByItemsId(long id)
        {
            try
            {
                var result = bankReconciliationService.getBankStat2ReconciliationsDetailsById(id);
                List<financialstateitemModel> list = new List<financialstateitemModel>();

                if (result != null)
                {
                    list = result.financialstateitemlist.ToList();
                }

                return Json(JsonSerialResponse.ResultSuccess(list, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                Log.Error("bankreconciliationController-getbaccountStatement2CBKPosbyItemsId", e);
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult getSCBKPosbyReferenceReferenceItem(int sourcedata, long reference, long referenceitem,int rsvType)
        {
            try
            {
                var data = bankReconciliationService.getBakReconciliationsDetailsbyRefenceReferenceItem(sourcedata, reference, referenceitem, rsvType);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("bankreconciliationController-getSCBKPosbyReferenceReferenceItem", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public JsonResult getSearchFinancialStateItemList(string dateReportStart, string dateReportEnd, int? PaymentMethod, int? Tpv, decimal? ammountStart, decimal? ammountEnd, int? hotel)
        {
            try
            {
                int _tpv = Tpv == null ? 0 : (int)Tpv;
                int _PaymentMethod = PaymentMethod == null ? 0 : (int)PaymentMethod;
                decimal _ammountStart = ammountStart == null ? 0 : (decimal)ammountStart;
                decimal _ammountEnd = ammountEnd == null ? 0 : (decimal)ammountEnd;
                int _hotel = hotel == null ? 0 : (int)hotel;

                var data = bankReconciliationService.getSearchFinancialStateItemList((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), _PaymentMethod, _tpv, _ammountStart, _ammountEnd, _hotel);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            { Log.Error("bankreconciliationController-getSearchFinancialStateItemList", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public JsonResult GetSearchFinancialState2ItemList(string dateReportStart, string dateReportEnd, int? PaymentMethod, int? Tpv, decimal? ammountStart, decimal? ammountEnd, int? typeSourceData, string description)
        {
            try
            {
                int _tpv = Tpv == null ? 0 : (int)Tpv;
                int _PaymentMethod = PaymentMethod == null ? 0 : (int)PaymentMethod;
                decimal _ammountStart = ammountStart == null ? 0 : (decimal)ammountStart;
                decimal _ammountEnd = ammountEnd == null ? 0 : (decimal)ammountEnd;
                int _typeSourceData = typeSourceData == null ? 0 : (int)typeSourceData;

                var data = bankReconciliationService.getSearchFinancialState2ItemList((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), _PaymentMethod, _tpv, _ammountStart, _ammountEnd, _typeSourceData, description);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            { Log.Error("bankreconciliationController-GetSearchFinancialState2ItemList", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult saveSCBKPosItem(int idBankStatement, financialstateitemModel financialstateitem)
        {
            try
            {
                var result = bankReconciliationService.saveBankStatementItem(idBankStatement, financialstateitem);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-saveSCBKPosItem", e);
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveStatement2CBANKACCNTPosItem(int idBankStatement, financialstateitemModel financialstateitem)
        {
            try
            {
                var result = bankReconciliationService.saveBankStatementItem2(idBankStatement, financialstateitem);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-saveStatement2CBANKACCNTPosItem", e);
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult deleteSCBKPosItem(int idBankStatement, financialstateitemModel financialstateitem)
        {
            try
            {
                var result = bankReconciliationService.deleteBankStatementItem(idBankStatement, financialstateitem);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-deleteSCBKPosItem", e);
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteStatement2CBANKACCNTPosItem(int idBankStatement, financialstateitemModel financialstateitem)
        {
            try
            {
                var result = bankReconciliationService.deleteBankStatementItem2(idBankStatement, financialstateitem);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-DeleteStatement2CBANKACCNTPosItem", e);
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult deleteSCBKPosItemReferenceReferenceItem(int SourceData, long Reference, long ReferenceItem, int rsvType)
        {
            try
            {
                var result = bankReconciliationService.deleteBankStatementItem(SourceData, Reference, ReferenceItem, rsvType);
                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                // return Json(JsonSerialResponse.ResultSuccess(Convert.ToInt32("258"), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-deleteSCBKPosItemReferenceReferenceItem", e);
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult getSCBKPosExcel(string dateReportStart, string dateReportEnd, int? Tpv, int? externalgroup, int company, int? PaymentMethod, int? hotel, int? status, decimal? ammountStart, decimal? ammountEnd)
        {
            try
            {
                int _Tpv = (Tpv == null ? 0 : (int)Tpv); int _externalgroup = (externalgroup == null ? 0 : (int)externalgroup); int _PaymentMethod = (PaymentMethod == null ? 0 : (int)PaymentMethod); int _hotel = (hotel == null ? 0 : (int)hotel); int _statusReconciliation = (status == null ? -1 : (int)status); decimal ams = ammountStart == null ? 0m : (decimal)ammountStart; decimal ame = ammountEnd == null ? 0m : (decimal)ammountEnd;
                var data = bankReconciliationService.generateReportReconciliationsExcel(DateTimeUtils.ParseStringToDate(dateReportStart), DateTimeUtils.ParseStringToDate(dateReportEnd), _Tpv, _externalgroup, company, _PaymentMethod, _hotel, _statusReconciliation, ams, ame);
                Log.Info("Se generó reporte de Búsqueda de conciliaciones en excel dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "" + ", Hotel:" + hotel + "");

                byte[] sFileByte = System.IO.File.ReadAllBytes(data["url"]);
                System.IO.File.Delete(data["url"]);

                return File(sFileByte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Concat(data["nameFile"], ".xlsx"));
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-getSCBKPosExcel", e);

                return RedirectToAction("customexplain", "Error");
            }
        }

        [AllowAnonymous]
        public ActionResult GetStatement2CBANKACCNTPosExcel(string dateReportStart, string dateReportEnd, int? Tpv, int? externalgroup, int company, int? PaymentMethod, int? hotel, int? status, decimal? ammountStart, decimal? ammountEnd)
        {
            try
            {
                int _Tpv = (Tpv == null ? 0 : (int)Tpv); int _externalgroup = (externalgroup == null ? 0 : (int)externalgroup); int _PaymentMethod = (PaymentMethod == null ? 0 : (int)PaymentMethod); int _hotel = (hotel == null ? 0 : (int)hotel); int _statusReconciliation = (status == null ? -1 : (int)status); decimal ams = ammountStart == null ? 0m : (decimal)ammountStart; decimal ame = ammountEnd == null ? 0m : (decimal)ammountEnd;
                var data = bankReconciliationService.generateReportBaReconciliationsExcel(DateTimeUtils.ParseStringToDate(dateReportStart), DateTimeUtils.ParseStringToDate(dateReportEnd), _Tpv, _externalgroup, company, _PaymentMethod, _hotel, _statusReconciliation, ams, ame);
                Log.Info("Se generó reporte de Búsqueda de conciliaciones en excel dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "" + ", Hotel:" + hotel + "");

                byte[] sFileByte = System.IO.File.ReadAllBytes(data["url"]);
                System.IO.File.Delete(data["url"]);

                return File(sFileByte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Concat(data["nameFile"], ".xlsx"));
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-GetStatement2CBANKACCNTPosExcel", e);

                return RedirectToAction("customexplain", "Error");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult deleteSCBKPosbyId(long id)
        {
            try
            {
                bankReconciliationService.deleteBankStatement(id);
                return Json(JsonSerialResponse.ResultSuccess(0L, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-deleteSCBKPosbyId", e);
                // return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteStatement2CBANKACCNTPosById(long id)
        {
            try
            {
                bankReconciliationService.deleteBankStatement2(id);
                return Json(JsonSerialResponse.ResultSuccess(0L, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-DeleteStatement2CBANKACCNTPosById", e);
                // return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult deleteSCBKPosbyIds(long[] data)
        {
            try
            {
                bankReconciliationService.deleteBankStatement(data);
                return Json(JsonSerialResponse.ResultSuccess(0L, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-deleteSCBKPosbyIds", e);
                // return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteStatement2CBANKACCNTPosByIds(long[] data)
        {
            try
            {
                bankReconciliationService.deleteBankStatement2(data);
                return Json(JsonSerialResponse.ResultSuccess(0L, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-DeleteStatement2CBANKACCNTPosByIds", e);
                // return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult bankStatementsUpFile()
        {
            XLSFileImportErrors error_report = new XLSFileImportErrors();
            try
            {
                var files = bankReconciliationService.getFiles(Request.Files, ref error_report);
                var result = bankReconciliationService.addXLScotiaPos(files);

                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("bankreconciliationController-bankStatementsUpFile", e);
                // return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }

        }


        /*
        [HttpPost]
        [AllowAnonymous]
        public ActionResult BankStatementsUploadFileConciliations()
        {
            try
            {
                var files = bankReconciliationService.getFiles(Request.Files);
                List<string> result = bankReconciliationService.ReconcileFromFiles(files);

                return Json(JsonSerialResponse.ResultSuccess(result, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                Log.Error("bankreconciliationController-BankStatementsUploadFileConciliations", e);
                return new HttpStatusCodeResult(500, e.Message);
            }
        }*/

        [HttpPost]
        [AllowAnonymous]
        public ActionResult bankStatementsUpFilevsctps()
        {
            XLSFileImportErrors errorReport = new XLSFileImportErrors();
            XLSImportingOutput result = null;

            try
            {
                var files = bankReconciliationService.getFiles(Request.Files, ref errorReport);
                if (files.Count > 0) bankReconciliationService.ReconcileFromFiles(files, ref errorReport);
            }

            catch (Exception ex)
            {
                errorReport.AddCriticalStop("Falló la importación del(los) archivo(s).", ex);
            }

            try
            {
                result = new XLSImportingOutput()
                {
                    oper_uuid = errorReport.oper_uuid.ToString(),
                    error_count = errorReport.error_count,
                    has_errors = errorReport.has_errors,
                    was_halted = errorReport.was_halted,
                    error_report = errorReport.GrindErrorReport()
                };
            }

            catch { }

            errorReport = null;

            return Json(JsonSerialResponse.ResultSuccess(result, "Operación realizada correctamente."), JsonRequestBehavior.AllowGet);
        }
    }
}