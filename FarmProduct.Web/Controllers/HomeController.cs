using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Model;
using FarmProduct.Web.Common;

namespace FarmProduct.Web.Controllers
{
    public class HomeController : Controller
    {
        [UserAuthorize(Role.FarmProductUser | Role.RetailUser | Role.WholeSaleUser | Role.SecurityChecker)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
