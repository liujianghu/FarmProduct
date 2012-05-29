using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Core;
using FarmProduct.Model;
using FarmProduct.Web.Models;
using FarmProduct.Web.Common;
using System.Security.Principal;

namespace FarmProduct.Web.Controllers
{
    public class UserController : BaseController
    {
        [UserAuthorize(Role.Admin | Role.FarmProductUser | Role.WholeSaleUser | Role.SecurityChecker)]
        public ActionResult Index(int pageIndex = 1)
        {
            var model = new ListViewModel<User>();

            IIdentity identity = HttpContext.User.Identity;
            var user = UserSvc.LoadByUserName(identity.Name);
            bool isAdmin = AuthorizationSvc.IsAdministrator(user.UserRole);

            Tuple<List<User>, int> tuple;
            if (isAdmin)
            {
                tuple = UserSvc.LoadAllUserList(pageIndex, PAGESIZE);
            }
            else
            {
                tuple = UserSvc.LoadUserListByCompanyId(user.CompanyId, pageIndex, PAGESIZE);
            }

            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;
            return View();
        }

        [UserAuthorize(Role.Admin)]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost]
        public ActionResult Create(User user)
        {
            UserSvc.Insert(user);
            return RedirectToAction("Index");
        }

        [UserAuthorize(Role.FarmProductUser | Role.WholeSaleUser | Role.RetailUser | Role.SecurityChecker)]
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var user = UserSvc.LoadById(id);
            return View(user);
        }

        [UserAuthorize(Role.Admin)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = UserSvc.LoadById(id);
            return View(user);
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            UserSvc.Update(user);
            return RedirectToAction("Index");
        }

        [UserAuthorize(Role.Admin)]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = UserSvc.LoadById(id);
            return View(user);
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSvc.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
