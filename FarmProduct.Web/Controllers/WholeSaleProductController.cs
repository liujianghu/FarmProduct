using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Core;
using FarmProduct.Model;
using FarmProduct.Web.Models;
using FarmProduct.Web.Extensions;
using FarmProduct.Web.Common;
using System.Security.Principal;

namespace FarmProduct.Web.Controllers
{
    public class WholeSaleProductController : BaseController
    {
        //
        // GET: /WholeProduct/

        public ActionResult Index()
        {
            return View();
        }

        [UserAuthorize(Role.FarmProductUser)]
        public ActionResult WholeSold(int pageIndex = 1)
        {
            IIdentity id = HttpContext.User.Identity;

            var tuple = WholeSaleProductSvc.LoadFromProductByUserName(id.Name, pageIndex, PAGESIZE);

            var model = new ListViewModel<WholeSaleProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.WholeSaleUser)]
        public ActionResult MyWholeSaleProduct(int pageIndex = 1)
        {
            IIdentity id = HttpContext.User.Identity;

            var tuple = WholeSaleProductSvc.LoadToProductByProductStatus(id.Name, pageIndex, PAGESIZE, ProductStatus.WholeSale);

            var model = new ListViewModel<WholeSaleProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.FarmProductUser)]
        [HttpGet]
        public ActionResult Create(int agriculturalProductId)
        {
            var model = new WholeSaleProductEditModel
            {
                AgriculturalProductId = agriculturalProductId,
                ToCompanyList = CompanySvc.LoadCompanyByType((short)CompanyType.WholeSaleCompany)
            };
            return View(model);
        }

        [UserAuthorize(Role.FarmProductUser | Role.WholeSaleUser)]
        public ActionResult Detail(int id)
        {
            var product = WholeSaleProductSvc.LoadById(id);
            var model = new WholeSaleProductEditModel(product);

            return View(model);
        }

        [UserAuthorize(Role.FarmProductUser)]
        [HttpPost]
        public ActionResult Create(WholeSaleProductEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ToCompanyList = CompanySvc.LoadCompanyByType((short)CompanyType.WholeSaleCompany);
                ModelState.AddModelError(string.Empty, "请输入正确信息!");
                return View(model);
            }

            IIdentity id = HttpContext.User.Identity;
            var product = AgriculturalProductSvc.LoadById(model.AgriculturalProductId);

            model.InsertUserName = id.Name;
            model.AgriculturalProductName = product.ProductName;
            model.ProductStatus = ProductStatus.WholeSale;
            
            int productId = WholeSaleProductSvc.Insert(model.ToWholeSaleProduct());

            if (productId > 0)
            {
                
                product.ProductStatus = ProductStatus.WholeSale;

                AgriculturalProductSvc.Update(product);
            }
            return RedirectToAction("WholeSold");
        }

        [UserAuthorize(Role.FarmProductUser | Role.WholeSaleUser)]
        public JsonResult DeleteWholeSale(int id)
        {
            var result = new JsonResultModel
            {
                IsSuccess = true
            };

            var product = WholeSaleProductSvc.LoadById(id);
            if (product.ProductStatus != ProductStatus.WholeSale || product.SecurityStatus != SecurityStatus.Safe)
            {
                result.SetFailure("此产品已被使用.");
                return this.Json(result);
            }

            IIdentity identity = HttpContext.User.Identity;
            var user = UserSvc.LoadByUserName(identity.Name);

            if (user.Company.Id != product.FromCompany.Id || user.Company.Id != product.ToCompany.Id)
            {
                result.SetFailure("你无权限操作此产品");
                return this.Json(result);
            }

            return this.Json(result);
        }

    }
}
