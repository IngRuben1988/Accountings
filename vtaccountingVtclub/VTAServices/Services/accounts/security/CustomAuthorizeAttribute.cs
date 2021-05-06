using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTACore.Logger;
using static VTAworldpass.VTAServices.Resolves.JsonResolve;

namespace VTAworldpass.VTAServices.Services.accounts.security
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        private string ActionKey;
        private string Controller;

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            HttpCookie cookie = null;
            RedirectToRouteResult routeData = null;
            try
            {
                // Getting Cookie VTAworldpassCookie
                cookie = filterContext.HttpContext.Request.Cookies.Get("ApplicationCookie");
                Log.Info("Se puede obtener el HttpCookie - VTAworldpassCookie" + cookie.Value);
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
                { 
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        if (permissionsClaims.Where(t => t.Value == ActionKey).FirstOrDefault() == null)
                        {
                            var x_ = new System.Web.Mvc.JsonResult();
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
                                routeData = new RedirectToRouteResult(
                                    new System.Web.Routing.RouteValueDictionary(
                                        new { controller = "Account", action = "NoAccess" }
                                ));
                                filterContext.Result = routeData;
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {   
                Log.Error("No se puede obtener el VTAworldpassCookie - " + e.Message + " |-------> " + e.StackTrace);
                routeData = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(
                        new { controller = "Account", action = "Login" }
                    ));
                filterContext.Result = routeData;
            }
        }
    }
}