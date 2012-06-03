using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FarmProduct.Model;
using FarmProduct.Web.Models;
using FarmProduct.Core;

namespace FarmProduct.Web.Extensions
{
    public static class RetailProductEditModelExtension
    {
        public static RetailProduct ToRetailProduct(this RetailProductEditModel model)
        {
            var user = UserSvc.LoadByUserName(model.InsertUserName);
            var company = CompanySvc.LoadById(model.ToCompanyId);

            return new RetailProduct
            {
                Id = model.Id,
                AgriculturalProductId = model.AgriculturalProductId,
                AgriculturalProductName = model.AgriculturalProductName,
                WholeSaleProductId = model.AgriculturalProductId,
                WholeSaleProductName = model.WholeSaleProductName,
                ProductName = model.ProductName,
                ProductCode = model.ProductCode,
                ParentId = model.ParentId,
                Batch = model.Batch,
                ProductStatus = model.ProductStatus,
                FromCompany = user.Company,
                ToCompany = company,
                InsertBy = user,
                InsertDate = model.InsertDate,
                SecurityStatus = model.SecurityStatus
            };
        }
    }
}