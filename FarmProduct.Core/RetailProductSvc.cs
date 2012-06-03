using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;
using FarmProduct.Core.Common;
using Simple.Data;

namespace FarmProduct.Core
{
    public class RetailProductSvc : BaseSvc
    {
        public static int Insert(RetailProduct product)
        {
            var db = DataBaseHelper.Open();

            product.Id = LastIdSvc.GetNextTableId("WholeSaleProducts");
            product.ProductCode = CalculateProductCode(product.InsertBy.Company.Id);

            db.RetailProducts.Insert(product);

            return product.Id;
        }

        public static void Update(RetailProduct product)
        {
            var db = DataBaseHelper.Open();

            db.RetailProducts.Update(product);
        }

        public static void Delete(int id)
        {
            var db = DataBaseHelper.Open();
            RetailProduct product = db.RetailProducts.FindById(id);
            product.ProductStatus = ProductStatus.IsDeleted;

            db.RetailProducts.Update(product);
        }

        public static void UpdateByAgriculturalProductId(int productId)
        {
            var db = DataBaseHelper.Open();
            db.RetailProducts.UpdateByAgriculturalProductId(AgriculturalProductId: productId, SecurityStatus:  SecurityStatus.Dangerous);
        }

        public static RetailProduct LoadById(int id)
        {
            var db = DataBaseHelper.Open();

            return db.RetailProducts.FindById(id);
        }

        public static Tuple<List<RetailProduct>, int> LoadFromProductByUserName(
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

            List<RetailProduct> list = db.RetailProducts.FindAll(db.RetailProducts.FromCompany.Id == user.Company.Id)
                                                              .OrderByDescending(db.RetailProduct.Id)
                                                              .WithTotalCount(out totalCount)
                                                              .Skip(skipCount)
                                                              .Take(pageSize)
                                                              .ToList<RetailProduct>();

            return new Tuple<List<RetailProduct>, int>(list, totalCount.Value);
        }

        public static List<RetailProduct> LoadAllProductList()
        {
            var db = DataBaseHelper.Open();

            List<RetailProduct> list = db.RetailProducts.FindAll(db.RetailProducts.ProductStatus != ProductStatus.IsDeleted
                                                                &&db.RetailProducts.SecurityStatus  == SecurityStatus.Safe )
                                                              .ToList<RetailProduct>();

            return list;
        }

        public static Tuple<List<RetailProduct>, int> LoadToProductByProductStatus(
               string userName
            , int pageIndex
            , int pageSize
            , ProductStatus productStatus)
        {
            return LoadToProductByProductStatusList(userName, pageIndex, pageSize, new List<ProductStatus> { productStatus });
        }

        public static Tuple<List<RetailProduct>, int> LoadToProductByProductStatusList(
              string userName
           , int pageIndex
           , int pageSize
           , List<ProductStatus> productStatusList)
        {
            int skipCount = (pageIndex - 1) * pageSize;

            int totalCount = 0;
            Future<int> itemCount;

            var db = DataBaseHelper.Open();

            var user = UserSvc.LoadByUserName(userName);
            if (user == null)
            {
                throw new Exception("当前用户数据不存在.");
            }

            List<RetailProduct> list = new List<RetailProduct>();

            foreach (var item in productStatusList)
            {

                list.AddRange(db.RetailProducts.FindAll(db.RetailProducts.ProductStatus == item
                                                                        && db.RetailProducts.ToCompany.Id == user.Company.Id)
                                                              .OrderByDescending(db.RetailProducts.Id)
                                                              .WithTotalCount(out itemCount)
                                                              .Skip(skipCount)
                                                              .Take(pageSize)
                                                              .ToList<RetailProduct>());
                totalCount += itemCount.Value;
            }

            return new Tuple<List<RetailProduct>, int>(list, totalCount);
        }

    }
}
