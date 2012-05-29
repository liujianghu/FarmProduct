using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FarmProduct.Model;
using FarmProduct.Core;
using System.Security.Principal;

namespace FarmProduct.Web.Common
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly Role _role;

        public UserAuthorizeAttribute(Role role)
        {
            this._role = role;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("Error");
            }
            else
            {
                var user = UserSvc.LoadByUserName(filterContext.HttpContext.User.Identity.Name);
                if (user == null || !AuthorizationSvc.IsAuthorized(user, this._role))
                {
                    filterContext.Result = new RedirectResult("UnAuthorize");
                }
            }
            
            base.OnAuthorization(filterContext);
        }
    }
}