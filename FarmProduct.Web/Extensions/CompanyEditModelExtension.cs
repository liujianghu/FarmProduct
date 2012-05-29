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
    public static class CompanyEditModelExtension
    {
        public static Company ToCompany(this CompanyEditModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Company
            {
                Id = model.Id,
                CompanyName = model.CompanyName,
                CompanyType = model.CompanyType,
                Province = Utilts.LoadProvinceById(model.ProvinceId),
                City = Utilts.LoadCityById(model.CityId),
                Dictrict = Utilts.LoadDistrictById(model.DistrictId),
                Email = model.Email,
                Telephone = model.Telephone,
                Address = model.Address,
                IsDeleted = false
            };
        }
    }
}