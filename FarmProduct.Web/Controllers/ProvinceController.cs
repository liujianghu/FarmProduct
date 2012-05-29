using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Model;
using FarmProduct.Core;
using FarmProduct.Web.Common;
using FarmProduct.Web.Models;

namespace FarmProduct.Web.Controllers
{
    public class ProvinceController : Controller
    {

         [UserAuthorize(Role.Admin)]
        public ActionResult Index()
        {
            var model = new ListViewModel<Province>();
            model.Items = ProvinceSvc.LoadProvinceList();
            model.CurrentPageIndex = 1;
            model.PageCount = 0;

            return View(model);
        }

         [UserAuthorize(Role.Admin)]
         [HttpGet]
         public ActionResult Create()
         {
             return View();
         }

         [UserAuthorize(Role.Admin)]
         [HttpPost]
         public ActionResult Create(Province model)
         {
             ProvinceSvc.Insert(model);
             return RedirectToAction("Index");
         }

         [UserAuthorize(Role.Admin)]
         [HttpPost, ActionName("Delete")]
         public ActionResult Delete(int id)
         {
             CompanySvc.Delete(id);
             return RedirectToAction("Index");
         }

    }
}
