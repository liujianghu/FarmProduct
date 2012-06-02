using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;
using FarmProduct.Core.Common;
using Simple.Data;

namespace FarmProduct.Core
{
    public class WholeSaleProductSvc : BaseSvc
    {
        public static int Insert(WholeSaleProduct product)
        {
            var db = DataBaseHelper.Open();

            product.Id = LastIdSvc.GetNextTableId("WholeSaleProducts");
            product.ProductCode = CalculateProductCode(product.InsertBy.Company.Id);

            db.WholeSaleProducts.Insert(product);

            return product.Id;
        }

        public static void Update(WholeSaleProduct product)
        {
            var db = DataBaseHelper.Open();

            db.WholeSaleProducts.Update(product);
        }

        public static void Delete(int id)
        {
            var db = DataBaseHelper.Open();
            db.WholeSaleProducts.Delete(Id: id);
        }

        public static WholeSaleProduct LoadById(int id)
        {
            var db = DataBaseHelper.Open();

            return db.WholeSaleProducts.FindById(id);
        }

        public static Tuple<List<WholeSaleProduct>, int> LoadFromProductByUserName(
               string userName
            , int pageIndex
            , int pageSize)
        {
            int skipCount = (pageIndex - 1) * pageSize;
            Future<int> totalCount;

            var db = DataBaseHelper.Open();

            var user = UserSvc.LoadByUserName(userName);
            if (user == null)
            {
                throw new Exception("当前用户数据不存在.");
            }

            List<WholeSaleProduct> list = db.WholeSaleProducts.FindAll(db.WholeSaleProducts.FromCompany.Id == user.Company.Id)
                                                              .OrderByDescending(db.WholeSaleProducts.Id)
                                                              .WithTotalCount(out totalCount)
                                                              .Skip(skipCount)
                                                              .Take(pageSize)
                                                              .ToList<WholeSaleProduct>();

            return new Tuple<List<WholeSaleProduct>, int>(list, totalCount.Value);
        }

        public static Tuple<List<WholeSaleProduct>, int> LoadToProductByProductStatus(
               string userName
            , int pageIndex
            , int pageSize
            , ProductStatus productStatus)
        {
            int skipCount = (pageIndex - 1) * pageSize;
            Future<int> totalCount;

            var db = DataBaseHelper.Open();

            var user = UserSvc.LoadByUserName(userName);
            if (user == null)
            {
                throw new Exception("当前用户数据不存在.");
            }

            List<WholeSaleProduct> list = db.WholeSaleProducts.FindAll(db.WholeSaleProducts.ProductStatus == productStatus
                                                                        && db.WholeSaleProducts.ToCompany.Id == user.Company.Id)
                                                              .OrderByDescending(db.WholeSaleProducts.Id)
                                                              .WithTotalCount(out totalCount)
                                                              .Skip(skipCount)
                                                              .Take(pageSize)
                                                              .ToList<WholeSaleProduct>();

            return new Tuple<List<WholeSaleProduct>, int>(list, totalCount.Value);
        }

    }
}
