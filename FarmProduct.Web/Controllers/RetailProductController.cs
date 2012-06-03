using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Model;
using FarmProduct.Core;
using FarmProduct.Web.Models;
using FarmProduct.Web.Extensions;
using FarmProduct.Web.Common;
using System.Security.Principal;

namespace FarmProduct.Web.Controllers
{
    public class RetailProductController : BaseController
    {
        //
        // GET: /RetailProduct/

        public ActionResult Index()
        {
            return View();
        }

        [UserAuthorize(Role.WholeSaleUser)]
        [HttpGet]
        public ActionResult Create(int wholeSaleId)
        {
            var wholeSaleProduct = WholeSaleProductSvc.LoadById(wholeSaleId);
            var model = new RetailProductEditModel
            {
                AgriculturalProductId = wholeSaleProduct.AgriculturalProductId,
                AgriculturalProductName = wholeSaleProduct.AgriculturalProductName,
                WholeSaleProductId = wholeSaleId,
                WholeSaleProductName = wholeSaleProduct.ProductName,
                ToCompanyList = CompanySvc.LoadCompanyByType((short)CompanyType.RetailCompany)
            };

            return View(model);
        }

        [UserAuthorize(Role.WholeSaleUser)]
        [HttpPost]
        public ActionResult Create(RetailProductEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ToCompanyList = CompanySvc.LoadCompanyByType((short)CompanyType.RetailCompany);
                ModelState.AddModelError(string.Empty, "请输入正确信息!");
                return View(model);
            }

            IIdentity id = HttpContext.User.Identity;
            var product = WholeSaleProductSvc.LoadById(model.WholeSaleProductId);

            model.InsertUserName = id.Name;
            model.ProductStatus = ProductStatus.Retail;
            model.InsertDate = DateTime.Now;

            int productId = RetailProductSvc.Insert(model.ToRetailProduct());

            if (productId > 0)
            {
                product.ProductStatus = ProductStatus.WholeSold;

                WholeSaleProductSvc.Update(product);
            }

            return RedirectToAction("WholeSold");
        }

        [UserAuthorize(Role.RetailUser)]
        [HttpGet]
        public ActionResult Cut(int parentId)
        {
            var product = RetailProductSvc.LoadById(parentId);
            var model = new RetailProductEditModel
            {
                ParentId = parentId,
                AgriculturalProductId = product.AgriculturalProductId,
                AgriculturalProductName = product.AgriculturalProductName,
                WholeSaleProductId = product.WholeSaleProductId,
                WholeSaleProductName = product.WholeSaleProductName,
                ToCompanyId = product.ToCompany.Id
            };
            return View(model);
        }

        [UserAuthorize(Role.RetailUser)]
        [HttpPost]
        public ActionResult Cut(RetailProductEditModel model)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "请输入正确信息");
                return View(model);
            }

            IIdentity id = HttpContext.User.Identity;
            var product = RetailProductSvc.LoadById(model.ParentId);

            model.AgriculturalProductId = product.AgriculturalProductId;
            model.AgriculturalProductName = product.AgriculturalProductName;
            model.WholeSaleProductId = product.WholeSaleProductId;
            model.WholeSaleProductName = product.WholeSaleProductName;
            model.InsertUserName = id.Name;
            model.ProductStatus = ProductStatus.CanRetail;
            model.InsertDate = DateTime.Now;

            int productId = RetailProductSvc.Insert(model.ToRetailProduct());
            if (productId > 0)
            {
                product.ProductStatus = ProductStatus.CutRetailProduct;
                RetailProductSvc.Update(product);
            }

            return RedirectToAction("CutProduct");
        }

        [UserAuthorize(Role.WholeSaleUser)]
        public ActionResult WholeSold(int pageIndex = 1)
        {
            IIdentity id = HttpContext.User.Identity;

            var tuple = RetailProductSvc.LoadFromProductByUserName(id.Name, pageIndex, PAGESIZE);

            var model = new ListViewModel<RetailProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.RetailUser)]
        public ActionResult CanCutProduct(int pageIndex = 1)
        {
            IIdentity id = HttpContext.User.Identity;

            var tuple = RetailProductSvc.LoadToProductByProductStatus(id.Name, pageIndex, PAGESIZE, ProductStatus.Retail);

            var model = new ListViewModel<RetailProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.RetailUser)]
        public ActionResult CanRetail(int pageIndex = 1)
        {
            IIdentity id = HttpContext.User.Identity;
            var statusList = new List<ProductStatus>() { ProductStatus.Retail, ProductStatus.CanRetail };

            var tuple = RetailProductSvc.LoadToProductByProductStatusList(id.Name, pageIndex, PAGESIZE, statusList);

            var model = new ListViewModel<RetailProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.RetailUser)]
        public ActionResult CutProduct(int pageIndex = 1)
        {
            IIdentity id = HttpContext.User.Identity;

            var tuple = RetailProductSvc.LoadToProductByProductStatus(id.Name, pageIndex, PAGESIZE, ProductStatus.CutRetailProduct);

            var model = new ListViewModel<RetailProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.RetailUser)]
        public ActionResult RetailedProduct(int pageIndex = 1)
        {
            IIdentity id = HttpContext.User.Identity;

            var tuple = RetailProductSvc.LoadToProductByProductStatus(id.Name, pageIndex, PAGESIZE, ProductStatus.Retailed);

            var model = new ListViewModel<RetailProduct>();
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        public JsonResult DeleteWholeSale(int id)
        {
            var result = new JsonResultModel
            {
                IsSuccess = true
            };

            var product = RetailProductSvc.LoadById(id);
            if (product.ProductStatus != ProductStatus.Retail || product.SecurityStatus != SecurityStatus.Safe)
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

            product.ProductStatus = ProductStatus.IsDeleted;
            RetailProductSvc.Update(product);

            return this.Json(result);
        }

        public JsonResult DeleteCutRetail(int id)
        {
            var result = new JsonResultModel
            {
                IsSuccess = true
            };

            var product = RetailProductSvc.LoadById(id);
            if (product.ProductStatus != ProductStatus.CanRetail || product.SecurityStatus != SecurityStatus.Safe)
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

            product.ProductStatus = ProductStatus.IsDeleted;
            RetailProductSvc.Update(product);

            return this.Json(result);
        }

        public JsonResult RetailProduct(int id)
        {
            var result = new JsonResultModel
            {
                IsSuccess = true
            };

            var product = RetailProductSvc.LoadById(id);
            if ((product.ProductStatus != ProductStatus.CanRetail && product.ProductStatus != ProductStatus.Retail) || product.SecurityStatus != SecurityStatus.Safe)
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

            product.ProductStatus = ProductStatus.Retailed;
            product.RetailedDate = DateTime.Now;
            RetailProductSvc.Update(product);

            return this.Json(result);
        }

        [UserAuthorize(Role.WholeSaleUser | Role.RetailUser)]
        public ActionResult Detail(int id)
        {
            var product = RetailProductSvc.LoadById(id);
            var model = new RetailProductEditModel(product);

            return View(model);
        }


        [UserAuthorize(Role.RetailUser)]
        public ActionResult Retail(int id)
        {
            var product = RetailProductSvc.LoadById(id);
            var model = new RetailProductEditModel(product);

            return View(model);
        }


    }
}
