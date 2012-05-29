using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;

namespace FarmProduct.Core
{
    public class CitySvc
    {
        public static int Insert(City city)
        {
            var db = DataBaseHelper.Open();
            city.Id = LastIdSvc.GetNextTableId("Cities");
            db.Cities.Insert(city); 

            return city.Id;
        }

        public static List<City> LoadCityByProvinceIdList(int provinceId)
        {
            var db = DataBaseHelper.Open();

            var list = db.Cities.FindAll(db.Cities.IsDeleted == false && db.Cities.ProvinceId == provinceId)
                                        .ToList<City>();

            return list;
        }

        public static void Delete(int id)
        {
            var db = DataBaseHelper.Open();

            db.Cities.DeleteById(id);
        }

    }
}
