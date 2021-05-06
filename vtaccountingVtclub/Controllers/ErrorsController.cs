using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTAworldpass.VTACore.Helpers;

namespace VTAworldpass.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        [Authorize]
        public ActionResult Index(int id)
        {

            string message = "";
            switch (id)
            {
                case 1:
                    message = "No existe el archivo";
                    break;

                default:
                    break;
            }

            ViewBag.message = message;

            return View();
        }

        public ActionResult customexplain(int id, string messageerror)
        {
            string message = "";
            switch (id) // No Existe el archivo
            {
                case 1:
                    message = messageerror;
                    break;
                case 2:
                    message = messageerror;
                    break;


                default:
                    break;
            }

            ViewBag.message = message;

            return View();
        }

        public ActionResult NoPermistAction()
        {
            var data = Globals.NotPermitAction;
            ViewBag.data = data;

            return View();
        }
    }
}