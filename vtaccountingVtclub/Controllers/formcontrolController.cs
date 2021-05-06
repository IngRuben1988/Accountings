using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.formcontrols;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{
    public class FormcontrolController : Controller
    {
        private FormControlServices formservice;
        private readonly IAccountServices accountServices;

        public FormcontrolController(IAccountServices _accountServices)
        {
            accountServices = _accountServices;
            formservice = new FormControlServices();
        }


        #region Actions by User-Companies

        [HttpGet]
        public JsonResult getSegmentsbyCompanyUser()
        {
            try
            {
                int idUser = this.accountServices.AccountIdentity();
                return Json(JsonSerialResponse.ResultSuccess(formservice.getSegmentbyCompanies(idUser), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCompaniesBySegment(int id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getCompaniesbySegment(id, accountServices.AccountIdentity()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCompaniesCurrencies(int id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getCurrenciesbyCompanies(id), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult getCompaniesByUser()
        {
            try
            {
                int idUser = this.accountServices.AccountIdentity();
                return Json(JsonSerialResponse.ResultSuccess(formservice.SelectServicesCompaniesbyUser(idUser), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Contable Accounts by Segment and ACCL1 
        /**************** Accounts By Segments *************/

        public ActionResult getAccountL1ByProfileSegment(int id, int accl1)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountL1byProfileSegment(this.accountServices.AccountProfile(), this.accountServices.AccountIdentity(), id, accl1), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getAccountLayer2byIdndSegment(int id, int idSegment, int accl1)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer2byIdndSegment(id, this.accountServices.AccountProfile(), this.accountServices.AccountIdentity(), idSegment, accl1), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getAccountLayer3byIdndSegment(int id, int idSegment, int accl1)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer3byIdndSegment(id, this.accountServices.AccountProfile(), this.accountServices.AccountIdentity(), idSegment, accl1), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getAccountLayer4byIdndSegment(int id, int idSegment, int accl1)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer4byIdndSegment(id, this.accountServices.AccountProfile(), this.accountServices.AccountIdentity(), idSegment, accl1), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Commons

        public JsonResult getapppyearsavaliables()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAppYearsAvailables(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        // Obtiene los tipos de Reporte (ER-BG)
        public JsonResult gettypefinancialreport()
        {
            try
            {
                //var hotels = accountServices.getUserCompanies();
                return Json(JsonSerialResponse.ResultSuccess(formservice.getReportFinancialType(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Commons 2
        public JsonResult getCurrencyUser()
        {
            try
            {
                List<int> listHotles = new List<int>();
                listHotles = accountServices.AccountCompanies();

                return Json(JsonSerialResponse.ResultSuccess(formservice.getCurrenciesbyHotels(listHotles), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult getAttachmentsTypes()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.SelectAttachmentTypes(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        // GET Suppliers by User-HOtel
        public JsonResult getSuppliersbyUserHotels()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getSupppliersbyByUserHotels(this.accountServices.AccountCompanies()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getAccountLayerbyProfile()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer1byProfileandUser(this.accountServices.AccountProfile()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getAccountLayer2byId(int id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer2byId(id), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult getAccountLayer2byIdndSegement(int id, int idSegment, int accl1)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer2byIdndSegment(id, this.accountServices.AccountProfile(), this.accountServices.AccountIdentity(), idSegment, accl1), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getAccountLayer3byId(int id)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer3byId(id), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult getAccountLayer4byId(int id, int Company, int accl1)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer4byId(id, this.accountServices.AccountProfile(), this.accountServices.AccountIdentity(), Company, accl1), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        public JsonResult getBudgetTypes()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.SelectBudgetTypes(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getAccountByCurrencyProfile(int idCurrency, int idBankAccntType, int idCompany)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountbyUserProfileAndIdCurrency(accountServices.AccountIdentity(), idBankAccntType, idCurrency, idCompany), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult getBAccountProductsbyUser(int idCurrency)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getBAccountProductsbyUser(this.accountServices.AccountIdentity(), idCurrency), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getBAccountProductsbyUserClass(int idCurrency, int idClassification)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getBAccountProductsbyUserClass(this.accountServices.AccountIdentity(), idCurrency, idClassification), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult getAccountByCurrencyClasficationProfile(int idCurrency, int idBankAccntType, int idClasification, int company)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountbyUserProfileAndIdCurrencyndClasification(accountServices.AccountIdentity(), idBankAccntType, idCurrency, idClasification, company), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getBankAccountClasification(int parameter)//usamos el id del Currency
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getBankClasificationbyCurrencyUser(this.accountServices.AccountIdentity(), parameter), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getTpvUserBankAccount(int idBankAccount)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getTpvsUserUserBankAccount(idBankAccount), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getBankAccountbyCompaniesUserBankAccount(int Company)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getBAccountByCompanyUserBankAccount(Company, accountServices.AccountIdentity()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCompaniesByUserBankAccountGive(int? Type)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getCompaniesbyUserBankAccount(accountServices.AccountIdentity(), Type), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCompaniesByUserBankAccountReceive(int? Type)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getCompaniesbyUserBankAccountDest(accountServices.AccountIdentity(), Type), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCompaniesbyGroupUserBankAccount(int externalgroup)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getExternalGroupsbySegmentUserBankAccount(externalgroup, accountServices.AccountIdentity()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getReconciliationStatus()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getReconciliationStatus(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

         public JsonResult getExternalGroupUserBank()
         {
             try
             {
                 return Json(JsonSerialResponse.ResultSuccess(formservice.getEXternalGroupbyUserBankAccount(accountServices.AccountIdentity()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
             }
             catch (Exception e)
             {
                 return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
             }
         }
        /// <summary>
        /// Obtener Monedas
        /// </summary>
        /// <returns></returns>
        // GET Currencies
        public JsonResult getCurrencies()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.SelectCurrencies(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult getAccountLayer3bySegment(int idSegment, int accl1)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountLayer3bySegment(this.accountServices.AccountProfile(), this.accountServices.AccountIdentity(), idSegment, accl1), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getSegmentByUserBankAccount()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getSegmentByUserBankAccount(accountServices.AccountProfile()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCompaniesbySegmentUserBankAccount(int Segment)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getCompnaiesbySegmentUserBankAccount(Segment, accountServices.AccountProfile()), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError(e.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getSourceData()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getSourceData(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getOperationType()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getOperationType(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }
    }
}