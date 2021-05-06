using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.Business.Services.Implementations;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.VTAServices.Services.attachments;
using VTAworldpass.VTAServices.Services.attachments.implements;
using VTAworldpass.VTAServices.Services.attachments.model;
using VTAworldpass.VTAServices.Services.budgets;
using static VTAworldpass.VTACore.Collections.CollectionsUtils;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{
    [SessionTimeOut]
    public class ReportsController : Controller
    {
        private readonly ReportServices reportServices;
        public readonly IAttachmentServices attachmentServices;
        private readonly IBudgetServices budgetServices;
        public ReportsController(IAttachmentServices _attachmentServices, IBudgetServices _budgetServices)
        {
            this.reportServices = new ReportServices();
            this.budgetServices = _budgetServices;
            this.attachmentServices = _attachmentServices;
        }
        [Permissions]
        public ActionResult Index()
        {
            return View();
        }
        [Permissions]
        public ActionResult monthlyexpenses()
        {
            return View("monthlyexpenses");
        }
        [Permissions]
        public ActionResult monthlyexpensesdetails()
        {
            return View("monthlyexpensesdetails");
        }
        [Permissions]
        public ActionResult reportexpenseconcentrated()
        {
            return View("expensereportconcentrated");
        }
        [Permissions]
        public ActionResult cashclosings()
        {
            return View("cashclosings");
        }
        [Permissions]
        public ActionResult bankaccountcash()
        {
            return View("cashbankaccount");
        }
        //[Permissions]
        //public ActionResult accouncashclosinghotelsreconciliation()
        //{
        //    return View("accouncashclosinghotelsreconciliation");
        //}
        //[Permissions]
        //public ActionResult accouncashclosinghotels()
        //{
        //    return View("accouncashclosinghotels");
        //}
        //[Permissions]
        //public ActionResult expensedetailsgroup()
        //{
        //    return View("expensedetailsgroup");
        //}
        //[Permissions]
        //public ActionResult expensesreportdetails()
        //{
        //    return View("expensesreportdetails");
        //}
        //[Permissions]
        //public ActionResult accountreportcanvas()
        //{
        //    return View("accountreportcanvas");
        //}
        [Permissions]
        public ActionResult accountsclosingreconciliations()
        {
            return View("accouncashclosingreconciliation");
        }

        [AllowAnonymous]
        public JsonResult expense(int year, int month, int Company, int typeReport)
        {
            try
            {
                Log.Info("Se generó reporte de Estado de Resultados, dateReport year: " + year + " , month: " + month + " company: " + Company);
                var data = reportServices.generateExpenses(year, month, Company, typeReport);
                // return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                return new JsonResult()
                {
                    ContentEncoding = Encoding.Default,
                    ContentType = "application/json",
                    Data = JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (Exception e)
            { Log.Error("ReportsController-expense", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public ActionResult expenseDetails(int year, int month, int company, int typereport)
        {
            try
            {
                Log.Info("Se generó reporte de Estado de Resultados en Detalle, dateReport year: " + year + " , month: " + month + " company: " + company);
                var data = reportServices.generateExpenses(year, month, company, typereport, Currencies.Mexican_Peso);
                return new JsonResult()
                {
                    ContentEncoding = Encoding.Default,
                    ContentType = "application/json",
                    Data = JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (Exception e)
            {
                Log.Error("ReportsController-expenseDetails", e);
                return new HttpStatusCodeResult(500, "Error ->" + e.Message + "[Stack-trace]" + e.StackTrace);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> expenseReportConcentrated(int? category, int? Type, int? Company, string applicationDateIni, string applicationDateFin, int results, int isTax, int singleExibitionPayment, int budgetType, string creationDateIni, string creationDateFin)
        {
            try
            {
                var _Type = Type != null ? Type : 0;
                Log.Info(string.Concat("Se generó reporte de Concentrado de Reportes, dateReport:  int category {0}, int Type {1}, int Hotel {2}, string applicationDateIni {3}, string applicationDateFin {4}, int results {5}", category, Type, Company, applicationDateIni, applicationDateFin, results));
                var data = await reportServices.expenseConcentratedReport((int)category, (int)_Type, (int)Company, DateTimeUtils.ParseStringToDate(applicationDateIni), DateTimeUtils.ParseStringToDate(applicationDateFin), results, isTax, singleExibitionPayment, budgetType, DateTimeUtils.ParseStringToDate(creationDateIni), DateTimeUtils.ParseStringToDate(creationDateFin));
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("ReportsController-expenseReportConcentrated", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateInput(false)]
        // public ActionResult ExportToExcelcompress(string fileName, string data, int[] items)
        public ActionResult ExportToExcelcompress(string nameFile, string gridHtml, string items, int typeReport)
        {
            Log.Info(string.Concat("Se hs producido la  generación de reporte concentrado, parámetros --> namefile: {0}, gridHtml: {1}, items: {2}, typeReport: {3} ", nameFile, gridHtml, items, typeReport));
            //string to utf
            byte[] utf = Encoding.Default.GetBytes(gridHtml);

            switch (typeReport)
            {
                case 1:
                    {
                        //string to utf
                        return File(utf, "application/ms-excel;charset=UTF-8;base64", string.Concat(nameFile, ".xls"));
                    }

                case 2:
                    {
                        try
                        {
                            // Preparing Directories
                            var archive = Server.MapPath("~/workZip/archive.zip");
                            var temp = Server.MapPath("~/workZip/temp/");


                            // Creating Direcotry if not exist
                            if (!Directory.Exists(temp))
                            {
                                Directory.CreateDirectory(temp);
                            }


                            // Getting Files from Invoices
                            string[] substrings = items.Split(',');
                            List<attachment> filesTemp = new List<attachment>();
                            for (int i = 0; i <= substrings.Count() - 1; i++)
                            {
                                var results = this.attachmentServices.GetAttachmentInv(Convert.ToInt32(substrings[i]));
                                filesTemp = (List<attachment>)IEnumerableUtils.AddList(filesTemp, results);
                            }

                            // Deleting duplicates
                            List<attachment> finalFiles = new List<attachment>();


                            foreach (attachment attach in filesTemp)
                            {
                                var result = finalFiles.Where(x => x.item == attach.item).FirstOrDefault();

                                if (result == null)
                                {
                                    finalFiles.Add(attach);
                                }

                            }

                            // clear any existing archive
                            if (System.IO.File.Exists(archive))
                            {
                                System.IO.File.Delete(archive);
                            }

                            // empty the temp folder
                            Directory.EnumerateFiles(temp).ToList().ForEach(f => System.IO.File.Delete(f));

                            // adding report and copy the selected files to the temp folder

                            int count = 1;
                            foreach (attachment file in finalFiles)
                            {


                                bool _copied = false;
                                string _filename = file.filename;
                                while (_copied == false)
                                {

                                    if (System.IO.File.Exists(Server.MapPath(file.directoryfull)))
                                    {
                                        try
                                        {
                                            System.IO.File.Copy(Server.MapPath(file.directoryfull), Path.Combine(temp, Path.GetFileName(_filename)));
                                            _copied = true;
                                        }
                                        catch (IOException e)
                                        {
                                            Log.Error("No se puede copiar el archivo a la direción solicitada" + "-----------> Exception: " + e.Message + "[Stack Trace] ------------> " + e.StackTrace);
                                            Log.Error("Se cambia nombre de archivo." + _filename);
                                            _filename = string.Concat(_filename.Substring(0, _filename.Length - 4), "(", count.ToString(), ")", _filename.Substring(_filename.Length - 4));
                                            count = count + 1;
                                            //_copied = false;
                                            Log.Error("Nuevo nombre de archivo." + _filename);
                                        }

                                    }
                                    else
                                    {

                                        Log.Error(" -----------------> File not Found  <-----------------");
                                        _copied = true;
                                    }

                                }

                            }

                            using (var fs = new FileStream(string.Concat(temp, nameFile, ".xls"), FileMode.Create, FileAccess.ReadWrite))
                            {
                                fs.Write(utf, 0, utf.Length);
                                // return true;
                            }

                            // create a new archive
                            ZipFile.CreateFromDirectory(temp, archive);

                            return File(archive, "application/zip", string.IsNullOrEmpty(nameFile) ? "_" : nameFile + "_archive.zip");
                        }
                        catch (Exception e)
                        {
                            Log.Error("Errorr al generar ZIP" + "Error : ----------------------> " + e.Message + " **************** [Stack-Trace]: ----------------------------->  " + e.StackTrace);
                            return File(utf, "application/ms-excel;charset=UTF-8;base64", string.Concat(nameFile, ".xls"));
                        }
                    }
                default:
                    {
                        return File(utf, "application/ms-excel;charset=UTF-8;base64", string.Concat(nameFile, ".xls"));
                    }

            }

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportToExcel(string gridHtml, string nameFile)
        {
            byte[] utf = Encoding.Default.GetBytes(gridHtml);

            try
            {

                string currentYear = (DateTime.Now.Year).ToString();
                var patchPhisical = Server.MapPath("~/Attachments/" + currentYear);
                var NAmedirection = patchPhisical + nameFile + ".xls";

                using (var fs = new FileStream(NAmedirection, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(utf, 0, utf.Length);
                    // return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                // return false;
            }

            return File(utf, "application/ms-excel;charset=UTF-8;base64", string.Concat(nameFile, ".xls"));
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult accountsClosing(string dateReportStart, string dateReportEnd, int PaymentMethod)
        {
            try
            {
                Log.Info("Se generó reporte de Corte de Caja dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "");
                var data = reportServices.generateReportCashClosing((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), PaymentMethod, budgetServices, 1);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("ReportsController-accountsClosing", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public ActionResult accountsClosingGenerateExcel(string dateReportStart, string dateReportEnd, int PaymentMethod)
        {
            try
            {
                var temp = Server.MapPath("~/downloads/");
                // Creating Direcotry if not exist
                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }

                var data = reportServices.generateReportCashClosingExcel((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), PaymentMethod, budgetServices, 1);
                Log.Info("Se generó reporte de Corte de Caja en excel dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "");
                return File("~" + data["urlrelative"], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Concat(data["nameFile"], ".xlsx"));
            }
            catch (Exception e)
            { Log.Error("reportsController-accountsClosingGenerateExcel", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult baccountCashClosing(string dateReportStart, string dateReportEnd, int PaymentMethod)
        {
            try
            {
                Log.Info("Se generó reporte de Corte de Caja dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "");
                var data = reportServices.generateReportCashClosing((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), PaymentMethod, budgetServices, 2);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("reportsController-baccountCashClosing", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public ActionResult baccountCashClosingGenerateExcel(string dateReportStart, string dateReportEnd, int PaymentMethod)
        {
            try
            {
                var temp = Server.MapPath("~/downloads/");
                // Creating Direcotry if not exist
                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }

                var data = reportServices.generateReportCashClosingExcel((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), PaymentMethod, budgetServices, 2);
                Log.Info("Se generó reporte de Corte de Caja en excel dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "");
                return File("~" + data["urlrelative"], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Concat(data["nameFile"], ".xlsx"));
            }
            catch (Exception e)
            { Log.Error("reportsController-baccountCashClosingGenerateExcel", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public JsonResult accountsClosingReconciliation(string dateReportStart, string dateReportEnd, int PaymentMethod)
        {
            try
            {
                Log.Info("Se generó reporte de Corte de Caja  Conciliado dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "");
                var data = reportServices.generateReportCashClosingReconciliation((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), PaymentMethod, budgetServices);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { Log.Error("reportsController-accountsClosingReconciliation", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public ActionResult accountsClosingGenerateExcelConciliations(string dateReportStart, string dateReportEnd, int PaymentMethod)
        {
            try
            {
                var temp = Server.MapPath("~/downloads/");
                // Creating Direcotry if not exist
                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }

                var data = reportServices.generateReportCashClosingExcelConciliations((DateTime)DateTimeUtils.ParseStringToDate(dateReportStart), (DateTime)DateTimeUtils.ParseStringToDate(dateReportEnd), PaymentMethod, budgetServices);
                Log.Info("Se generó reporte de Corte de Caja en excel dateReportStart: " + dateReportStart + "  --- dateReportEnd: " + dateReportEnd + ", Cuenta: " + PaymentMethod + "");
                return File("~" + data["urlrelative"], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Concat(data["nameFile"], ".xlsx"));
            }
            catch (Exception e)
            { Log.Error("reportsController-accountsClosingGenerateExcelConciliations", e); return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }
    }
}