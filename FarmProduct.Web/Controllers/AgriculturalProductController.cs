using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using FarmProduct.Model;
using FarmProduct.Web.Common;
using FarmProduct.Web.Extensions;
using FarmProduct.Core;
using FarmProduct.Web.Models;

namespace FarmProduct.Web.Controllers
{
    public class AgriculturalProductController : BaseController
    {
        //
        // GET: /AgriculturalProduct/
        [UserAuthorize(Role.FarmProductUser)]
        public ActionResult Index(int pageIndex = 1, ProductStatus productStatus = ProductStatus.Procreative)
        {
            IIdentity id = HttpContext.User.Identity;

            var tuple = AgriculturalProductSvc.LoadProductListByProductStatus(id.Name, pageIndex, PAGESIZE, productStatus);

            var model = new ListViewModel<AgriculturalProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.FarmProductUser)]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new AgriculturalProductEditModel();
            return View(model);
        }

        [UserAuthorize(Role.FarmProductUser)]
        [HttpPost]
        public ActionResult Create(AgriculturalProductEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "请输入正确的信息");
                return View(model);
            }
            IIdentity identity = HttpContext.User.Identity;
            model.InsertByUserName = identity.Name;
            model.ProductStatus = ProductStatus.Procreative;


            AgriculturalProductSvc.Insert(model.ToAgriculturalProduct());

            return RedirectToAction("Index");
        }

        [UserAuthorize(Role.FarmProductUser)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = AgriculturalProductSvc.LoadById(id);

            var model = new AgriculturalProductEditModel(product);

            return View(model);
        }

        [UserAuthorizeAttribute(Role.FarmProductUser)]
        [HttpPost]
        public ActionResult Edit(AgriculturalProductEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "请输入正确的信息!");
                return View(model);
            }

            var product = AgriculturalProductSvc.LoadById(model.Id);
            if (product.ProductStatus != ProductStatus.Procreative || product.SecurityStatus != SecurityStatus.Safe)
            {
                ModelState.AddModelError(string.Empty, "此产品无法被修改!");
                return View(model);
            }

            IIdentity identity = HttpContext.User.Identity;
            var user = UserSvc.LoadByUserName(identity.Name);
            if (user.Company.Id != product.ProductOwner.Id)
            {
                ModelState.AddModelError(string.Empty, "你无权限修改此产品!");
                return View(model);
            }

            model.ProductStatus = ProductStatus.Procreative;

            AgriculturalProductSvc.Update(model.ToAgriculturalProduct());

            return RedirectToAction("Index");
        }

        [UserAuthorize(Role.Admin)]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel();
            var product = AgriculturalProductSvc.LoadById(id);
            if (product.ProductStatus != ProductStatus.Procreative || product.SecurityStatus != SecurityStatus.Safe)
            {
                result.SetFailure("此产品已被销售!");
                return this.Json(result);
            }

            IIdentity identity = HttpContext.User.Identity;
            var user = UserSvc.LoadByUserName(identity.Name);
            if (user.Company.Id != product.ProductOwner.Id)
            {
                result.SetFailure("你无权限修改此产品!");
                return this.Json(result);
            }

            AgriculturalProductSvc.Delete(id);
            return this.Json(result);
        }

        public ActionResult Detail(int id)
        {
            var product = AgriculturalProductSvc.LoadById(id);

            var model = new AgriculturalProductEditModel(product);

            return View(model);
        }

    }
}
