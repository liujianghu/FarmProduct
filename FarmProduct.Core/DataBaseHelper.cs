using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Simple.Data.MongoDB;
using Simple.Data;
using System.Configuration;

using FarmProduct.Model;

namespace FarmProduct.Core
{
    internal static class DataBaseHelper
    {
        public static dynamic Open()
        {
            return Database.Opener.OpenMongo(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ToString());
        }

        public static MongoServer CreateServer()
        {
            MongoServer server = MongoDB.Driver.MongoServer.Create(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ToString());
            return server;
        }

        public static void Reset()
        {
            var server = MongoServer.Create(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ToString());
            server.Connect();
            server.DropDatabase(Constants.DataBaseName);
            //InsertData(server.GetDatabase("test"));
        }

        public static void Empty()
        {
            var server = MongoServer.Create(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ToString());
            server.Connect();
            server.DropDatabase(Constants.DataBaseName);
        }

        public static void Init()
        {
            var server = MongoServer.Create(ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ToString());
            server.Connect();
            InsertData(server.GetDatabase(Constants.DataBaseName));
        }

        private static void InsertData(MongoDatabase db)
        {
            var user = new User
            {
                Id = 1,
                UserName = "admin",
                Password = ConfigurationManager.AppSettings[Constants.DefaultAdminPwdKey].ToString(),
                CompanyId = 0,
                Email = "",
                RealName= "Administrator",
                UserRole = Role.Admin
            };

            db.GetCollection("Users").Insert(user);
        }

    }
}
