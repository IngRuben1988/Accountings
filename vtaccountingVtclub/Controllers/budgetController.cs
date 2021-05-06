using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.VTACore.Cores.Globales;
using VTAworldpass.VTACore.Utils;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;
using System.Threading.Tasks;
using VTAworldpass.VTAServices.Services.budgets;
using VTAworldpass.VTAServices.Services.budgets.model;

namespace VTAworldpass.Controllers
{
    [SessionTimeOut]
    public class BudgetController : Controller
    {
        private readonly IBudgetServices budgetServices;

        public BudgetController(IBudgetServices _budgetServices)
        {
            this.budgetServices = _budgetServices;
        }
        // GET: budget
        [Permissions]
        public ActionResult Index(int? type)
        {
            ViewBag.Title = type != null ? "Financiamientos" : "Movimientos";
            string tipo = Request.QueryString["type"];
            ViewBag.Type = tipo;
            return View();
        }
        [Permissions]
        public ActionResult Budgetautorization()
        {
            return View("budgetautorization", null);
        }
        [Permissions]
        public ActionResult Finance()
        {
            return RedirectToAction("Index", new { type = (int)Enumerables.Finance.Intercompany_Finance });
        }

        public async Task<JsonResult> getBudgets(int? id, int? Type, int? PaymentMethod, int? idCurrency, string fondofechaEntrega, string fondoFechaInicio, string fondoFechaFin, decimal? fondoMontoInicio, decimal? fondoMontoFin, int? Company)
        {
            try
            {
                var data = await this.budgetServices.getWeeklyBudgets(Type, id, PaymentMethod, idCurrency, DateTimeUtils.ParseStringToDate(fondofechaEntrega), DateTimeUtils.ParseStringToDate(fondoFechaInicio), DateTimeUtils.ParseStringToDate(fondoFechaFin), fondoMontoInicio, fondoMontoFin, Company);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public JsonResult getBudget(int Company, string fecha, int idpaymentMethod)
        {
            try
            {
                var data = this.budgetServices.getBudgetAvailable(Company, (DateTime)DateTimeUtils.ParseStringToDate(fecha), idpaymentMethod);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public JsonResult getBudgetTypeMethodPay(int Company, string fecha, int idpaymentMethod, int BankAccntType)
        {
            try
            {
                var data = this.budgetServices.getBudgetAvailable(Company, (DateTime)DateTimeUtils.ParseStringToDate(fecha), idpaymentMethod, BankAccntType);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        public JsonResult getBudgetFinalDate(int? PaymentMethod, int? Hotel, int? Currency, string FechaInicioString)
        {
            try
            {
                var data = this.budgetServices.CalculateBudgetFinalDate(PaymentMethod, Hotel, Currency, FechaInicioString);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        public async Task<JsonResult> budgetUp(fondoModel helper)
        {
            try
            {
                var data = await this.budgetServices.AddFondo(helper);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        public async Task<JsonResult> budgetUpdate(fondoModel helper)
        {
            try
            {
                var data = await this.budgetServices.UpdateFondoAsync(helper);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        public async Task<JsonResult> budgetDelete(fondoModel helper)
        {
            try
            {
                var data = await this.budgetServices.DeleteFondo(helper);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }


        /************************   calculando es saldo de los fondos por fecha y no por semanas ********************/
        [AllowAnonymous]
        [HttpPost]
        public JsonResult getTotalFinanceState(int idPaymentMethod, string startDate, string endDate)
        {
            try
            {
                //var date = DateTime.Now; // DateTimeUtils.ParseStringToDate(startDate); // se reemplaza por la fecha actual en que se realza la petición
                var data = budgetServices.getTotalFinanceState(idPaymentMethod, null, DateTime.Now);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        /*********************** BUDGET MAXLIMIT **************************************/

        public JsonResult LimitbudgetGet(fondosmaxammountModel maxlimit)
        {
            try
            {
                var data = this.budgetServices.searchBudgetLimiter(maxlimit);
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [HttpPost]
        public JsonResult LimitbudgetSet(fondosmaxammountModel maxlimit)
        {
            try
            {
                this.budgetServices.saveBudgetLimiter(maxlimit);
                return Json(JsonSerialResponse.ResultSuccess(maxlimit, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }

        }

        [HttpPost]
        public JsonResult LimitbudgetUpdate(fondosmaxammountModel maxlimit)
        {
            try
            {
                this.budgetServices.updateBudgetLimiter(maxlimit);
                return Json(JsonSerialResponse.ResultSuccess(maxlimit, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }

        }

        public JsonResult LimitbudgetDelete(int id)
        {
            try
            {
                this.budgetServices.deleteBudgetLimiter(id);
                return Json(JsonSerialResponse.ResultSuccess(id, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError(e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }

        }
    }
}