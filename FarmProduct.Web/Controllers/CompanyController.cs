using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

using FarmProduct.Core;
using FarmProduct.Model;
using FarmProduct.Web.Common;
using FarmProduct.Web.Extensions;
using FarmProduct.Web.Models;

namespace FarmProduct.Web.Controllers
{
    public class CompanyController : BaseController
    {
        //
        // GET: /Company/
        [UserAuthorize(Role.Admin)]
        public ActionResult Index(int pageIndex = 1)
        {
            var model = new ListViewModel<Company>();
            var tuple = CompanySvc.LoadCompanyList(pageIndex, PAGESIZE);
            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;

            return View(model);
        }

        [UserAuthorize(Role.Admin)]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CompanyEditModel();
            return View(model);
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost]
        public ActionResult Create(CompanyEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "请正确输入信息！");
                return View(model);
            }
            CompanySvc.Insert(model.ToCompany());
            return RedirectToAction("Index");
        }

        [UserAuthorize(Role.FarmProductUser | Role.WholeSaleUser | Role.RetailUser)]
        [HttpGet]
        public ActionResult Detail(int id)
        {
            IIdentity identity = HttpContext.User.Identity;
            var user = UserSvc.LoadByUserName(identity.Name);
            if (!AuthorizationSvc.IsAdministrator(user.UserRole) && user.CompanyId != id)
            {
                return RedirectToRoute("UnAuthorize");
            }

            var model = CompanySvc.LoadById(id);

            return View(model);
        }

        [UserAuthorize(Role.Admin)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var company = CompanySvc.LoadById(id);

            if (company == null)
            {
                return RedirectToAction("Index");
            }
            CompanyEditModel model = new CompanyEditModel(company);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CompanyEditModel model)
        {
            return RedirectToAction("Index");
        }

        [UserAuthorize(Role.Admin)]
        public JsonResult Delete(int id)
        {
            CompanySvc.Delete(id);
            return this.Json(true);
        }

        [UserAuthorize(Role.Admin)]
        public JsonResult LoadCityByProvinceId(int provinceId)
        {
            XDocument xdoc = XDocument.Load(Server.MapPath("~/Content/Cities.xml"));

            var data = (from c in xdoc.Descendants("City")
                        where c.Attribute("PID").Value == provinceId.ToString()
                        select new City
                        {
                            Id = Convert.ToInt32(c.Attribute("ID").Value),
                            CityName = c.Attribute("CityName").Value
                        })
                                .OrderBy(c => c.CityName)
                                .ToList();
            return this.Json(data);
        }

        [UserAuthorize(Role.Admin)]
        public JsonResult LoadDistrictByCityId(int cityId)
        {
            XDocument xdoc = XDocument.Load(Server.MapPath("~/Content/Districts.xml"));
            var data = (from d in xdoc.Descendants("District")
                                where d.Attribute("CID").Value == cityId.ToString()
                                select new District
                                {
                                    Id = Convert.ToInt32(d.Attribute("ID").Value),
                                    DistrictName = d.Attribute("DistrictName").Value
                                })
                                .OrderBy(d => d.DistrictName)
                                .ToList();
            return this.Json(data);
        }

    }
}
