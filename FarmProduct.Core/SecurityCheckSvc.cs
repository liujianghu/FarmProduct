using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;
using FarmProduct.Core.Common;
using Simple.Data;

namespace FarmProduct.Core
{
    public class SecurityCheckSvc : BaseSvc
    {
        public static void Insert(SecurityCheckHistory history)
        {
            var db = DataBaseHelper.Open();
            history.Id = LastIdSvc.GetNextTableId("SecurityCheckHistories");

            db.SecurityCheckHistories.Insert(history);
        }

        public static List<SecurityCheckHistory> LoadAllHistory()
        {
            var db = DataBaseHelper.Open();

            List<SecurityCheckHistory> list = db.SecurityCheckHistories.Query()
                                                              .OrderByDescending(db.SecurityCheckHistories.Id)
                                                              .ToList<SecurityCheckHistory>();

            return list;

        }

    }
}
