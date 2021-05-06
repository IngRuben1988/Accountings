using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTAServices.Services.accounts.model;

namespace VTAworldpass.VTAServices.Services.accounts.security
{
    public class SessionTimeOutAttribute: ActionFilterAttribute
    {
        private SessionModel model;
        private RedirectToRouteResult routeData;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            model = (SessionModel)HttpContext.Current.Session[Globals.ApplicationSession];
            if ((HttpContext.Current.Session[Globals.ApplicationSession] == null) ||
                (model.userActive != Globals.unactiveRecord))
            {
                routeData = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    (new { controller = "Account", action = "Login" })
                );
                filterContext.Result = routeData;
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}