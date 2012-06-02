using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Core;
using FarmProduct.Core.Common;
using FarmProduct.Model;
using FarmProduct.Web.Common;
using FarmProduct.Web.Extensions;
using FarmProduct.Web.Models;

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
                tuple = UserSvc.LoadUserListByCompanyId(user.Company.Id, pageIndex, PAGESIZE);
            }

            model.Items = tuple.Item1;
            model.PageCount = Utilts.CalculatePageCount(tuple.Item2, PAGESIZE);
            model.CurrentPageIndex = pageIndex;
            return View(model);
        }

        [UserAuthorize(Role.Admin)]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new UserEditModel();
            return View(model);
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost]
        public ActionResult Create(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "请输入正确信息!");
                return View(model);
            }

            int result = UserSvc.Insert(model.ToUser());

            if (result == ErrorCode.ExistsSameUser)
            {
                ModelState.AddModelError(string.Empty, "此用户名已存在!");
                return View(model);
            }

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
            var model = new UserEditModel(user);
            return View(model);
        }

        [UserAuthorize(Role.Admin)]
        [HttpPost]
        public ActionResult Edit(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "请输入正确信息!");
                return View(model);
            }

            UserSvc.Update(model.ToUser());
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
