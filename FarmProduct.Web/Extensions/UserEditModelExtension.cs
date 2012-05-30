using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FarmProduct.Model;
using FarmProduct.Web.Models;
using FarmProduct.Core;
using FarmProduct.Web.Common;

namespace FarmProduct.Web.Extensions
{
    public static class UserEditModelExtension
    {
        public static User ToUser(this UserEditModel model)
        {
            var company = CompanySvc.LoadById(model.CompanyId);
            return new User
            {
                Id = model.Id,
                UserName = model.UserName,
                RealName = model.RealName,
                Password = model.Password,
                Email = model.Email,
                Telephone = model.Telephone,
                Company = company,
                UserRole = Utilts.LoadRoleByCompanyType(company.CompanyType)
            };
        }
    }
}