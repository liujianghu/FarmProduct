using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;

namespace FarmProduct.Core
{
    public class CountySvc
    {
        public static int Insert(District county)
        {
            var db = DataBaseHelper.Open();
            county.Id = LastIdSvc.GetNextTableId("Counties");
            db.Conties.Insert(county);
            return county.Id;
        }

        public static List<District> LoadCountyList()
        {
            var db = DataBaseHelper.Open();
            var list = db.Counties.FindAll(db.Counties.IsDeleted == false)
                                                .ToList<District>();
            return list;
        }

    }
}
