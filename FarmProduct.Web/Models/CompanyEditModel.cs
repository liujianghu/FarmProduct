using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FarmProduct.Model;

namespace FarmProduct.Web.Models
{
    public class CompanyEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入公司名字")]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        public short CompanyType { get; set; }

        [Range(1,100)]
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

         public CompanyEditModel()
         {
         }

         public CompanyEditModel(Company company)
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
         }

    }
}