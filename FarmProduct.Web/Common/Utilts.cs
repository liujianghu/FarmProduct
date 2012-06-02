using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using FarmProduct.Model;
using FarmProduct.Core;
using System.Xml.Linq;

namespace FarmProduct.Web.Common
{
    public class Utilts
    {
        private static Dictionary<int, string> _companyTypeDic;
        public static Dictionary<int, string> CompanyTypeDic
        {
            get
            {
                if (_companyTypeDic == null)
                {
                    _companyTypeDic = new Dictionary<int, string>();
                    _companyTypeDic.Add((short)CompanyType.AgriculturalCompany, "生产商");
                    _companyTypeDic.Add((short)CompanyType.WholeSaleCompany, "批发商");
                    _companyTypeDic.Add((short)CompanyType.RetailCompany, "零售商");
                    _companyTypeDic.Add((short)CompanyType.SecurityCompany, "检测机关");
                    _companyTypeDic.Add((short)CompanyType.SystemCompany, "系统管理");
                }
                return _companyTypeDic;
            }
        }

        public static int CalculatePageCount(int recordCount, int pageSize)
        {
            if (pageSize == 0)
            {
                return 0;
            }
            double pageCount = Convert.ToDouble(recordCount) / pageSize;
            return (int)Math.Ceiling(pageCount);
        }

        private static List<SelectListItem> _companyTypeList;
        public static List<SelectListItem> CompanyTypeList
        {
            get
            {
                if (_companyTypeList == null)
                {
                    _companyTypeList = (from d in CompanyTypeDic
                                        select new SelectListItem
                                        {
                                            Text = d.Value,
                                            Value = d.Key.ToString()
                                        }).ToList();
                }
                return _companyTypeList;
            }
        }

        private static List<Province> _provinces;
        public static List<Province> Provinces
        {
            get
            {
                if (_provinces == null)
                {
                    _provinces = LoadProvinceList();
                }
                return _provinces;
            }
        }

        private static List<Province> LoadProvinceList()
        {
            XDocument xdoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/Content/Provinces.xml"));
            var data = (from item in xdoc.Descendants("Province")
                        select new Province
                        {
                            Id = Convert.ToInt32(item.Attribute("ID").Value),
                            ProvinceName = item.Attribute("ProvinceName").Value
                        })
                        .OrderBy(t => t.ProvinceName)
                        .ToList();
            return data;
        }

        public static Province LoadProvinceById(int id)
        {
            XDocument xdoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/Content/Provinces.xml"));
            var data = xdoc.Descendants("Province").FirstOrDefault(t => t.Attribute("ID").Value == id.ToString());

            if (data != null)
            {
                return new Province
                {
                    Id = Convert.ToInt32(data.Attribute("ID").Value),
                    ProvinceName = data.Attribute("ProvinceName").Value
                };
            }
            return null;
        }

        public static City LoadCityById(int id)
        {
            XDocument xdoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/Content/Cities.xml"));

            var data = xdoc.Descendants("City").FirstOrDefault(t => t.Attribute("ID").Value == id.ToString());
            if (data != null)
            {
                return new City
                {
                    Id = Convert.ToInt32(data.Attribute("ID").Value),
                    CityName = data.Attribute("CityName").Value,
                    ProvinceId = Convert.ToInt32(data.Attribute("PID").Value)
                };
            }

            return null;
        }

        public static District LoadDistrictById(int id)
        {
            XDocument xdoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/Content/Districts.xml"));
            var data = xdoc.Descendants("District").FirstOrDefault(t => t.Attribute("ID").Value == id.ToString());

            if (data != null)
            {
                return new District
                {
                    Id = Convert.ToInt32(data.Attribute("ID").Value),
                    DistrictName = data.Attribute("DistrictName").Value,
                    CityId = Convert.ToInt32(data.Attribute("CID").Value)
                };
            }
            return null;
        }

        public static List<City> LoadCityByProvinceId(int provinceId)
        {
            XDocument xdoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/Content/Cities.xml"));

            var data = (from c in xdoc.Descendants("City")
                        where c.Attribute("PID").Value == provinceId.ToString()
                        select new City
                        {
                            Id = Convert.ToInt32(c.Attribute("ID").Value),
                            CityName = c.Attribute("CityName").Value
                        })
                            .OrderBy(c => c.CityName)
                            .ToList();

            return data;
        }

        public static List<District> LoadDistrictByCityId(int cityId)
        {
            XDocument xdoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/Content/Districts.xml"));
            var data = (from d in xdoc.Descendants("District")
                        where d.Attribute("CID").Value == cityId.ToString()
                        select new District
                        {
                            Id = Convert.ToInt32(d.Attribute("ID").Value),
                            DistrictName = d.Attribute("DistrictName").Value
                        })
                            .OrderBy(d => d.DistrictName)
                            .ToList();

            return data;
        }

        public static Role LoadRoleByCompanyType(int companyType)
        {
            switch (companyType)
            {
                case 1:
                    return Role.FarmProductUser;
                case 2:
                    return Role.WholeSaleUser;
                case 3:
                    return Role.RetailUser;
                case 4:
                    return Role.SecurityChecker;
                case 999:
                    return Role.Admin;
                default:
                    throw new Exception("无法找到公司对应的角色类型.");
            }
        }



    }
}