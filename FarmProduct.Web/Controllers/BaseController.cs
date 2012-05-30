using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmProduct.Web.Models;

namespace FarmProduct.Web.Controllers
{
    public class BaseController : Controller
    {
        public static readonly int PAGESIZE = 15;

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                return;
            }

            var ex = filterContext.Exception ?? new Exception("No further information exists.");
            filterContext.ExceptionHandled = true;

            ErrorModel data = new ErrorModel
            {
                ErrorMessage = ex.Message,
                ErrorTime = DateTime.Now,
                StackTrace = ex.StackTrace
            };

            filterContext.Result = View("Error", data);
            base.OnException(filterContext);
        }
    }
}
