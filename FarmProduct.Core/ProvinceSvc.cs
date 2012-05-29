using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;


namespace FarmProduct.Core
{
    public class ProvinceSvc
    {
        public static int Insert(Province province)
        {
            var db = DataBaseHelper.Open();

            province.Id = LastIdSvc.GetNextTableId("Provinces");
            db.Provinces.Insert(province);
            return province.Id;
        }

        public static List<Province> LoadProvinceList()
        {
            var db = DataBaseHelper.Open();

            var list = db.Provinces.FindAll(db.Provinces.IsDeleted == false)
                            .ToList<Province>();
            return list;
        }

        public static void Delete(int id)
        {
            var db = DataBaseHelper.Open();

            db.Provinces.DeleteById();
        }

    }
}
