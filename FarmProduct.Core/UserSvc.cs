using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;
using System.Configuration;
using Simple.Data;

using FarmProduct.Core.Common;

namespace FarmProduct.Core
{
    public class UserSvc
    {
        public static bool ValidateUser(string userName, string password)
        {
            var db = DataBaseHelper.Open();
            List<User> users = db.Users.FindAll(db.Users.UserName == userName
                                                    && db.Users.Password == password)
                                                    .ToList<User>();
            if (users != null && users.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static User LoadByUserName(string userName)
        {
            var db = DataBaseHelper.Open();
            User user = db.Users.FindByUserName(userName);
            return user;
        }

        public static User LoadById(int id)
        {
            var db = DataBaseHelper.Open();
            User user = db.Users.FindById(id);
            return user;
        }

        public static int Insert(User user)
        {
            var db = DataBaseHelper.Open();
            var existsUser = db.Users.FindByUserName(user.UserName);
            if (existsUser != null)
            {
                return ErrorCode.ExistsSameUser;
            }

            user.Id = LastIdSvc.GetNextTableId("Users");
            db.Users.Insert(user);

            return user.Id;
        }

        public static void Update(User user)
        {
            var db = DataBaseHelper.Open();

            db.Users.Update(user);
        }

        public static Tuple<List<User>, int> LoadUserListByCompanyId(int companyId, int pageIndex, int pageSize)
        {
            int skipCount = (pageIndex - 1) * pageSize;
            Future<int> totalCount;

            var db = DataBaseHelper.Open();
            List<User> list = db.Users.FindAll(db.Users.Company.Id == companyId && !db.Users.IsDeleted)
                                                              .OrderByDescending(db.Users.RealName)
                                                              .WithTotalCount(out totalCount)
                                                              .Skip(skipCount)
                                                              .Take(pageSize)
                                                              .ToList<User>();

            return new Tuple<List<User>, int>(list, totalCount.Value);
        }

        public static Tuple<List<User>, int> LoadAllUserList(int pageIndex, int pageSize)
        {
            int skipCount = (pageIndex - 1) * pageSize;
            Future<int> totalCount;

            var db = DataBaseHelper.Open();
            List<User> list = db.Users.Query()
                                                    .OrderByDescending(db.Users.RealName)
                                                    .WithTotalCount(out totalCount)
                                                    .Skip(skipCount)
                                                    .Take(pageSize)
                                                    .ToList<User>();

            return new Tuple<List<User>, int>(list, totalCount.Value);
        }

        public static void Delete(int id)
        {
            var db = DataBaseHelper.Open();
            User user = db.Users.FindById(id);
            user.IsDeleted = true;
            db.Users.Update(user);
        }

        public static bool InitUser(string adminName, string adminPwd)
        {
            if (adminName.Equals(Constants.DefaultAdminName, StringComparison.OrdinalIgnoreCase)
                && adminPwd.Equals(ConfigurationManager.AppSettings[Constants.DefaultAdminPwdKey], StringComparison.OrdinalIgnoreCase))
            {
                var db = DataBaseHelper.Open();

                int companyId = CompanySvc.Insert(new Company
                {
                    CompanyType = 999,
                    CompanyName = "系统管理部",
                    City = new City { Id = 124, CityName = "南昌市" },
                    Province = new Province { Id = 14, ProvinceName = "江西省" },
                    Dictrict = new District { Id = 1126, DistrictName = "青云谱区" },
                    Address = "",
                    Telephone = "",
                    Email = ""
                });

                var user = new User
                {
                    Id = LastIdSvc.GetNextTableId("Users"),
                    UserName = "admin",
                    Password = ConfigurationManager.AppSettings[Constants.DefaultAdminPwdKey].ToString(),
                    Company = CompanySvc.LoadById(companyId),
                    Email = "",
                    RealName = "Administrator",
                    UserRole = Role.Admin
                };

                db.Users.Insert(user);
                return true;
            }
            return false;
        }

    }
}
