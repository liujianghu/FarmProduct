using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FarmProduct.Model;
using FarmProduct.Web.Common;

namespace FarmProduct.Web.Models
{
    public class CompanyEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入公司名字")]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "企业类别")]
        [Range(1, 100)]
        public short CompanyType { get; set; }

        [Range(1, 100)]
        public int ProvinceId { get; set; }

        [Range(1, 1000)]
        public int CityId { get; set; }

        [Range(1, 10000)]
        public int DistrictId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "请输入联系地址")]
        public string Address { get; set; }

        [Required(ErrorMessage = "请输入联系电话")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        public List<Province> ProvinceList { get; set; }

        public List<City> CityList { get; set; }

        public List<District> DistrictList { get; set; }

        public Dictionary<int, string> CompanyTypeList { get; set; }

        public CompanyEditModel()
        {
            this.ProvinceList = Utilts.Provinces;
            this.CityList = new List<City>();
            this.DistrictList = new List<District>();
            this.CompanyTypeList = Utilts.CompanyTypeDic;
        }

        public CompanyEditModel(Company company)
            : this()
        {
            this.Id = company.Id;
            this.CompanyName = company.CompanyName;
            this.CompanyType = company.CompanyType;
            this.ProvinceId = company.Province.Id;
            this.CityId = company.City.Id;
            this.DistrictId = company.Dictrict.Id;
            this.Email = company.Email;
            this.Address = company.Address;
            this.Telephone = company.Telephone;

            this.CityList = Utilts.LoadCityByProvinceId(this.ProvinceId);
            this.DistrictList = Utilts.LoadDistrictByCityId(this.CityId);
        }

    }
}