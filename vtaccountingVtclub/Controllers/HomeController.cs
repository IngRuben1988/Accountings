using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.accounts.security;

namespace VTAworldpass.Controllers
{
    [SessionTimeOut]
    public class HomeController : Controller
    {
        public readonly IAccountServices _accountServices;

        public HomeController(IAccountServices accountServices)
        {
            this._accountServices = accountServices;
        }
        [Permissions]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult table()
        {
            return View();
        }
    }
}