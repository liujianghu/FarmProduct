using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;
using Simple.Data;

namespace FarmProduct.Core
{
    public class CompanySvc
    {
        public static int Insert(Company company)
        {
            var db = DataBaseHelper.Open();
            company.Id = LastIdSvc.GetNextTableId("Companies");
            db.Companies.Insert(company);
            return company.Id;
        }

        public static Company LoadById(int id)
        {
            var db = DataBaseHelper.Open();

            Company company = db.Companies.FindById(id);
            return company;
        }

        public static List<Company> LoadAllCompany()
        {
            var db = DataBaseHelper.Open();
            var list = db.Companies.FindAll(db.Companies.IsDeleted == false)
                                                .ToList<Company>();
            return list;
        }

        public static void Update(Company company)
        {
            var db = DataBaseHelper.Open();
            db.Companies.Update(company);
        }

        public static void Delete(int id)
        {
            var db = DataBaseHelper.Open();
            Company company = db.Companies.FindById(id);
            company.IsDeleted = true;
            db.Companies.UpdateById(company);
        }

        public static Tuple<List<Company>, int> LoadCompanyList(int pageIndex, int pageSize)
        {
            var db = DataBaseHelper.Open();

            int skipCount = (pageIndex - 1) * pageSize;
            Future<int> totalCount;

            var list = db.Companies.FindAll(db.Companies.IsDeleted == false)
                                                .OrderByDescending(db.Companies.CompanyName)
                                                .WithTotalCount(out totalCount)
                                                .Skip(skipCount)
                                                .Take(pageSize)
                                                .ToList<Company>();

            return new Tuple<List<Company>, int>(list, totalCount.Value);

        }

    }
}
