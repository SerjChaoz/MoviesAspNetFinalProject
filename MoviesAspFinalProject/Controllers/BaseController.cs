using MoviesAspFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesAspFinalProject.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected DataContext db = new DataContext();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.ApplicationName = Constants.ApplicationName;
            base.OnActionExecuted(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}