using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Model;
using FarmProduct.Core;
using FarmProduct.Core.Common;
using FarmProduct.Web.Common;
using FarmProduct.Web.Models;
using System.Security.Principal;

namespace FarmProduct.Web.Controllers
{
    public class SecurityCheckController : BaseController
    {
        //
        // GET: /SecurityCheck/

         [UserAuthorize(Role.SecurityChecker)]
        public ActionResult Index()
        {
            var model = new ListViewModel<SecurityCheckHistory>();
            var list = SecurityCheckSvc.LoadAllHistory();

            model.Items = list;
            return View(model);
        }

         [UserAuthorize(Role.SecurityChecker)]
         public ActionResult ProductList()
         {
             var model = new ListViewModel<SecurityProdutModel>();
             model.Items = new List<SecurityProdutModel>();

             var agriculturls = AgriculturalProductSvc.LoadAllProductList();
             model.Items.AddRange((from p in agriculturls
                                   select new SecurityProdutModel
                                   {
                                       ProductId = p.Id,
                                       CompanyName = p.ProductOwner.CompanyName,
                                       ProductInsertDate = p.InsertDate,
                                       ProductName = p.ProductName,
                                       ProductType = 1,
                                       ProductTypeName = "农产品",
                                   }).ToList());

             var wholesales = WholeSaleProductSvc.LoadAllProductList();
             model.Items.AddRange((from w in wholesales
                                   select new SecurityProdutModel
                                   {
                                       ProductId = w.Id,
                                       CompanyName = w.FromCompany.CompanyName,
                                       ProductInsertDate = w.InsertDate,
                                       ProductName = w.ProductName,
                                       ProductTypeName = "批发产品",
                                       ProductType = 2
                                   }).ToList());

             var retails = RetailProductSvc.LoadAllProductList();
             model.Items.AddRange((from r in wholesales
                                   select new SecurityProdutModel
                                   {
                                       ProductId = r.Id,
                                       CompanyName = r.FromCompany.CompanyName,
                                       ProductInsertDate = r.InsertDate,
                                       ProductName = r.ProductName,
                                       ProductType = 2,
                                       ProductTypeName = "零售产品",
                                   }).ToList());

             model.Items = model.Items.OrderByDescending(p => p.ProductName).ToList();
             return View(model);
         }

         [UserAuthorize(Role.SecurityChecker)]
        [HttpGet]
         public ActionResult Create(int id, short productType)
         {
             var model = new SecurityCheckerEditModel();
             model.ProductId = id;
             model.ProductType = productType;
             if (productType == 1)
             {
                 var agricultural = AgriculturalProductSvc.LoadById(id);
                 
                 model.ProductName = agricultural.ProductName;
                 model.ProductTypeName = "农产品";
                 model.CompanyName = agricultural.ProductOwner.CompanyName;
                 model.CompanyId = agricultural.ProductOwner.Id;
             }
             else if (productType == 2)
             {
                 var wholeSale = WholeSaleProductSvc.LoadById(id);
                 model.ProductName = wholeSale.ProductName;
                 model.CompanyName = wholeSale.FromCompany.CompanyName;
                 model.CompanyId = wholeSale.FromCompany.Id;
                 model.ProductTypeName = "批发产品";
             }
             else
             {
                 var retail = RetailProductSvc.LoadById(id);
                 model.CompanyName = retail.FromCompany.CompanyName;
                 model.ProductName = retail.ProductName;
                 model.CompanyId = retail.FromCompany.Id;
                 model.ProductTypeName = "零售产品";
             }
             return View(model);
         }

         [UserAuthorize(Role.SecurityChecker)]
         public ActionResult Create(SecurityCheckerEditModel model)
         {
             Company company = CompanySvc.LoadById(model.CompanyId);
             switch (model.SecurityLevel)
             {
                 case 0:
                     this.StopByAgriculturalProductId(model.ProductId, model.ProductType);
                     break;
                 case 1:
                     break;
                 case 2:
                     this.StopProduct(model.ProductId, model.ProductType);
                     break;
                 case 3:
                     break;
                 default:
                     break;
             }
             IIdentity id = HttpContext.User.Identity;
             var user = UserSvc.LoadByUserName(id.Name);

             SecurityCheckHistory history = new SecurityCheckHistory
             {
                 InsertBy = user,
                 InsertDate= DateTime.Now,
                 InsertReason = model.InsertReason,
                 ProductName = model.ProductName,
                 ProductType = model.ProductType,
                 ProductOwer = company,
                 ProductId = model.ProductId,
                 ProductTypeName = model.ProductTypeName,
                 SecurityLevel = model.SecurityLevel
             };

             SecurityCheckSvc.Insert(history);

             return RedirectToAction("Index");
         }

         private void StopByAgriculturalProductId(int productId, short productType)
         {
             int agriculturalProductId = productId;
            if (productType == 2)
             {
                 var wholeSale = WholeSaleProductSvc.LoadById(productId);
                 agriculturalProductId = wholeSale.AgriculturalProductId;
             }
             else
             {
                 var retail = RetailProductSvc.LoadById(productId);
                 agriculturalProductId = retail.AgriculturalProductId;
             }

             var agriculut = AgriculturalProductSvc.LoadById(productId);
             agriculut.SecurityStatus = SecurityStatus.Dangerous;

             AgriculturalProductSvc.Update(agriculut);

             WholeSaleProductSvc.UpdateByAgriculturalProductId(productId);
             RetailProductSvc.UpdateByAgriculturalProductId(productId);
         }

         private void StopProduct(int productId, short productType)
         {
             if (productType == 1)
             {
                 var agricultural = AgriculturalProductSvc.LoadById(productId);
                 agricultural.SecurityStatus = SecurityStatus.Dangerous;
                 AgriculturalProductSvc.Update(agricultural);
             }
             else if (productType == 2)
             {
                 var wholeSale = WholeSaleProductSvc.LoadById(productId);
                 wholeSale.SecurityStatus = SecurityStatus.Dangerous;
                 WholeSaleProductSvc.Update(wholeSale);
             }
             else
             {
                 var retail = RetailProductSvc.LoadById(productId);
                 retail.SecurityStatus = SecurityStatus.Dangerous;
                 RetailProductSvc.Update(retail);
             }
         }

    }
}
