using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmProduct.Model;
using FarmProduct.Web.Models;
using FarmProduct.Core;

namespace FarmProduct.Web.Extensions
{
    public static class WholeProductEditModelExtension
    {
        public static WholeSaleProduct ToWholeSaleProduct(this WholeSaleProductEditModel model)
        {
            var user = UserSvc.LoadByUserName(model.InsertUserName);
            var company = CompanySvc.LoadById(model.ToCompanyId);
            return new WholeSaleProduct
            {
                Id = model.Id,
                ProductName = model.ProductName,
                ProductCode = model.ProductCode,
                ProductStatus = model.ProductStatus,
                AgriculturalProductId = model.AgriculturalProductId,
                AgriculturalProductName = model.AgriculturalProductName,
                ParentId = model.ParentId,
                Batch = model.Batch,
                InsertDate = model.InsertDate,
                FromCompany = user.Company,
                ToCompany = company,
                SecurityStatus = model.SecurityStatus,
                InsertBy = user
            };
        }
    }
}