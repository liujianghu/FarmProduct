using System;
using System.Collections.Generic;
using System.Linq;

using FarmProduct.Model;
using FarmProduct.Core;
using FarmProduct.Core.Extensioins;
using Simple.Data;

namespace FarmProduct.Core
{
    public class AgriculturalProductSvc
    {
        /// <summary>
        /// 获取当前用户公司农产品
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Tuple<List<AgriculturalProduct>, int> LoadProductListByProductStatus(
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

            bool isAdmin = AuthorizationSvc.IsAdministrator(user.UserRole);

            List<AgriculturalProduct> list = db.AgriculturalProducts.FindAll(db.AgriculturalProducts.ProductStatus == productStatus
                                                                    && db.AgriculturalProducts.ProductOwner.Id == user.CompanyId) 
                                                              .OrderByDescending(db.AgriculturalProducts.InsertDate)
                                                              .WithTotalCount(out totalCount)
                                                              .Skip(skipCount)
                                                              .Take(pageSize)
                                                              .ToList<AgriculturalProduct>();

            return new Tuple<List<AgriculturalProduct>, int>(list, totalCount.Value);
        }

        public static int Insert(AgriculturalProduct product)
        {
            var db = DataBaseHelper.Open();

            product.Id = LastIdSvc.GetNextTableId("AgriculturalProducts");

            db.AgriculturalProducts.Insert(product);

            return product.Id;
        }

        public static void Update(AgriculturalProduct product)
        {
            var db = DataBaseHelper.Open();

            db.AgriculturalProducts.Update(product);
        }

        public static void Delete(int id)
        {
            var db = DataBaseHelper.Open();
            db.AgriculturalProducts.DeleteById(id);
        }

        public static AgriculturalProduct Detail(int id)
        {
            var db = DataBaseHelper.Open();
            AgriculturalProduct product = db.AgriculturalProducts.FindById(id);
            return product;
        }

    }
}
