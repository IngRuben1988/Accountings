using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTAServices.Services.accounts.model;
using VTAworldpass.VTAServices.Services.accounts.implements;

namespace VTAworldpass.VTAServices.Services.accounts.security
{
    public class PermissionsAttribute : ActionFilterAttribute
    {
        private string ActionKey;
        private string ControllerKey;
        private SessionModel model = null;
        private AccountServices services = new AccountServices();
        private RedirectToRouteResult routeData;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionKey = (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "-" + filterContext.ActionDescriptor.ActionName);
            ControllerKey = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            model = (SessionModel)HttpContext.Current.Session[Globals.ApplicationSession];
            var permissiones = services.AccountPermissions();
            bool comapre = permissiones.Contains(ActionKey);
            if (
                (model.userActive == Globals.activeRecord) && (comapre == true)
                )
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                //filterContext.ActionParameters.Values.Clear();
                routeData = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    (new { controller = "Errors", action = "NoPermistAction" })
                );
                filterContext.Result = routeData;
                return;
            }
        }
    }
}