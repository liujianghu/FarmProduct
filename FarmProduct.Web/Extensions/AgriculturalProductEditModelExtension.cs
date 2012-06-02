using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmProduct.Model;
using FarmProduct.Web.Models;
using FarmProduct.Core;

namespace FarmProduct.Web.Extensions
{
    public static class AgriculturalProductEditModelExtension
    {
        public static AgriculturalProduct ToAgriculturalProduct(this AgriculturalProductEditModel model)
        {
            var user = UserSvc.LoadByUserName(model.InsertByUserName);
            return new AgriculturalProduct
            {
                Id = model.Id,
                ProductName = model.ProductName,
                InserBy = user,
                ProductCode = model.ProductCode,
                ProductOwner = user.Company,
                ProductStatus = model.ProductStatus,
                BirthDate = model.BirthDate,
                Batch = model.Batch,
                BreedType = model.BreedType,
                GrowthCycle = model.GrowthCycle,
                MedicalHistory = model.MedicalHistory,
                Weight = model.Weight,
                VaccineSituation = model.VaccineSituation,
                DeliverDate = model.DeliverDate,
                InsertDate = model.InsertDate,
                SecurityStatus = SecurityStatus.Safe,
                Remarks = model.Remarks
            };
        }
    }
}