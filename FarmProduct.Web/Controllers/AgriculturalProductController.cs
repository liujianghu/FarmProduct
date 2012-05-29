using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

using FarmProduct.Model;
using FarmProduct.Web.Common;
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
        public ActionResult Create()
        {
            return View();
        }

    }
}
