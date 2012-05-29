using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Core;
using FarmProduct.Model;
using FarmProduct.Web.Common;
using FarmProduct.Web.Models;

namespace FarmProduct.Web.Controllers
{
    public class CityController : BaseController
    {
        [UserAuthorize(Role.Admin)]
        public ActionResult Index(int provinceId = 0)
        {
            var model = new ListViewModel<City>();
            model.Items = CitySvc.LoadCityByProvinceIdList(provinceId);

            return View(model);
        }

        [UserAuthorize(Role.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost]
        public ActionResult Create(City model)
        {
            CitySvc.Insert(model);

            return RedirectToAction("Index");
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            CitySvc.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
