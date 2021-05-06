using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.accounts.model;
using VTAworldpass.VTAServices.Services.accounts.security;
using VTAworldpass.VTAServices.Services.formcontrols;
using VTAworldpass.VTAServices.Services.users;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{
    [SessionTimeOut]
    public class ConfigController : Controller
    {
        private UserServices userService;
        private FormControlServices formservice;

        public ConfigController()
        {
            userService = new UserServices();
            formservice = new FormControlServices();
        }

        // GET: Config
        [Permissions]
        public ActionResult Users()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public JsonResult getAllUsers()
        {
            try
            {
                var data = userService.getAllUsers();
                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the users list, usersController.getAllUsers", e);
                return Json(JsonSerialResponse.ResultError("Unable to get the users list, usersController.getAllUsers" + e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult searchUsers(string parameters)//string id, string userName
        {
            try
            {
                var search = JsonConvert.DeserializeObject<userModel>(parameters);
                List<userModel> data;
                
                 int userId = search.idUser.ToString() == "" ? 0 : search.idUser;
                 data = userService.getUserSearch(userId, search.userName);

                return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the users list, usersController.searchUsers", e);
                return Json(JsonSerialResponse.ResultError("Unable to get the users list, usersController.searchUsers" + e.Message, "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public JsonResult getUserToFillSelect()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getUserFill(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Log.Error("Unable to get the users list, usersController.getUserToFillSelect", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }
        [AllowAnonymous]
        public JsonResult getProfileToFill()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getProfileFill(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the profile list, usersController.getProfileToFill", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }


        [AllowAnonymous]
        public JsonResult getCompaniesToFill()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getCompaniesFill(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the companies list, usersController.getCompaniesToFill", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        public JsonResult getAccountToFill()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getAccountFill(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the accounts list, usersController.getAccountToFill", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        public JsonResult getPermissionsToFill()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(formservice.getPermissionsFill(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the permissions list, usersController.getPermissionsToFill", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult getCompaniesByIdUser(int idUser)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(userService.getCompaniesByIdUser(idUser), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the companies list to user , usersController.getCompaniesByIdUser", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult getAccountByIdUser(int idUser)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(userService.getAccountByIdUser(idUser), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the user accounts list, usersController.getAccountByIdUser", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult getPermissionsByIdUser(int idUser)
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(userService.getPermissionsByIdUser(idUser), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the user permissions list, usersController.getPermissionsByIdUser", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult saveInfoUserByUser(userModel user)
        {
            try
            {
                var data = userService.saveInfoUserByidUser(user);
                if (data != null)
                {
                    if(data.data != null)
                    {
                        return Json(JsonSerialResponse.ResultSuccess(data, data.data), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(JsonSerialResponse.ResultError("No se pudo guardar la información"), JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                Log.Error("Unable to save info to user, usersController.saveInfoUserByUser", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult saveCompaniesByUser(List<userCompaniesModel> companies)
        {
            try
            {
                var data = userService.saveCompaniesByidUser(companies);
                if (data != null)
                {
                    return Json(JsonSerialResponse.ResultSuccess(data.Count, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(JsonSerialResponse.ResultError("No se pudo guardar la información"), JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                Log.Error("Unable to save Companies to user, usersController.saveCompaniesByUser", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult saveAccountsByUser(List<userAccountModel> accounts)
        {
            try
            {
                var data = userService.saveAccountsByidUser(accounts);
                if (data != null)
                {
                    return Json(JsonSerialResponse.ResultSuccess(data.Count, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(JsonSerialResponse.ResultError("No se pudo guardar la información"), JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                Log.Error("Unable to save user accounts, usersController.saveAccountsByUser", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult savePermissionsByUser(List<userPermissionsModel> permissions)
        {
            try
            {
                var data = userService.savePermissionsByidUser(permissions);
                if (data != null)
                {
                    return Json(JsonSerialResponse.ResultSuccess(data.Count, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(JsonSerialResponse.ResultError("No se pudo guardar la información"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to save user permissions, usersController.savePermissionsByUser", e);
                return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet);
            }

        }

    }
}