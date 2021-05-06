using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTACore.Logger;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.Web.VTAworldpass.Business.Services.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        // private currentUserPrincipal = HttpContext.Current.User.Identity;

        private string ActionKey;
        private string Controller;

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            // I need to read cookie values here
            // HttpCookie cookie = filterContext.HttpContext.Request.Cookies.Get("VTAccounting");

            HttpCookie cookie = null;
            RedirectToRouteResult routeData = null;

            try
            {
                // Getting Cookie
                cookie = filterContext.HttpContext.Request.Cookies.Get("VTAccountingCookie");
                Log.Info("Se puede obtener el HttpCookie - VTAccountingCookie" + cookie.Value);

                // Getting Action 
                ActionKey = (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "-" + filterContext.ActionDescriptor.ActionName).ToLower();
                Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;


                // Getting  USer Principal
                var identity = (ClaimsIdentity)filterContext.HttpContext.User.Identity;
                IEnumerable<Claim> claims = identity.Claims;

                // Getting Permission
                var permissionsClaims = claims.Where(t => t.Type == "userpermission").ToList(); // get All permissions

                // Evaluating CLaims
                if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                { // Check for authorization

                    // Evaluando si es petición Ajax
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        if (permissionsClaims.Where(t => t.Value == ActionKey).FirstOrDefault() == null)
                        {
                            // filterContext.Result = Json(JsonSerialResponse.ResultError(message), JsonRequestBehavior.AllowGet); return;
                            // base.HandleUnauthorizedRequest(filterContext);

                            var x_ = new System.Web.Mvc.JsonResult(); //Json(JsonSerialResponse.ResultError(message), JsonRequestBehavior.AllowGet);
                            x_.ContentType = "";
                            x_.ContentEncoding = System.Text.Encoding.UTF8;
                            x_.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                            x_.Data = JsonSerialResponse.ResultError("No tiene permisos para realizar esta acción");

                            filterContext.Result = x_;


                        }
                    }
                    else // De lo contrario es petición de Vista
                    {

                        if (ActionKey != "home-index")
                        {
                            var result = permissionsClaims.Where(t => t.Value == ActionKey).FirstOrDefault();
                            if (result == null)
                            {
                                //filterContext.Result = RedirectToAction("NoAccess", "Account"); return;
                                routeData = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                            (new
                            {
                                controller = "Account",
                                action = "NoAccess"
                            }
                            ));
                                filterContext.Result = routeData;
                            }
                        }
                    }
                }



            }
            catch (Exception e)
            {   // On exception in Getting Coockie Redictint to Login 
                Log.Error("No se puede obtener el VTAccountingCookie - " + e.Message + "  |------->" + e.StackTrace);
                routeData = new RedirectToRouteResult
                            (new System.Web.Routing.RouteValueDictionary(new
                            { controller = "Account", action = "Login"}
                            ));

                filterContext.Result = routeData;
            }
        }
    }
}