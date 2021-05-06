using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.accounts.implements;
using VTAworldpass.VTAServices.Services.accounts.model;
using VTAworldpass.VTAServices.Services.accounts.security;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager _auth;
        private readonly IAccountServices _accountServices;

        public AccountController()
        {
            this._accountServices = new AccountServices();
        }

        public AccountController(IAuthenticationManager auth, IAccountServices accountServices)
        {
            this._auth = auth;
            this._accountServices = accountServices;
        }
        // GET: Account
        [Permissions]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult AccountIdentify()
        {
            var data = System.Web.HttpContext.Current.Session[Globals.ApplicationSession];
            return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                bool passed = _accountServices.AccountVerifier(model);//_accountServices.verifyUser(model);
                if (model.UserName != null && model.Password != null && passed == true)
                {
                    var users = _accountServices.AccountData(model);//_accountServices.AddIdentityBasic(model);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario o password incorrecto.");
                }

                return View();
            }
            catch (Exception e)
            {
                Log.Error("No se puede inicar sesión.", e);
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult isBasic()
        {
            try
            {
                return Json(JsonSerialResponse.ResultSuccess(_accountServices.isOpertive(), "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            { return Json(JsonSerialResponse.ResultError("Error ->" + e.Message + "[Stack-trace]" + e.StackTrace), JsonRequestBehavior.AllowGet); }
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            System.Web.HttpContext.Current.Session[Globals.ApplicationSession] = null;
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
            //var data = Globals.NotWorkIn;
            //return Json(JsonSerialResponse.ResultSuccess(data, "Consulta realizada correctamente"), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult NoAccess()
        {
            return View();
        }
    }
}