using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmProduct.Model;
using System.ComponentModel.DataAnnotations;

namespace FarmProduct.Web.Models
{
    public class RetailProductEditModel
    {
        public int Id { get; set; }

        [Range(1, 10000000)]
        public int AgriculturalProductId { get; set; }

        public string AgriculturalProductName { get; set; }

        [Range(1, 100000000)]
        public int WholeSaleProductId { get; set; }

        public string WholeSaleProductName { get; set; }

        [Required(ErrorMessage = "请输入产品名称")]
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public int ParentId { get; set; }

        public int FromCompanyId { get; set; }

        public string FromCompanyName { get; set; }

        [Range(1, 10000)]
        public int ToCompanyId { get; set; }

        public string ToCompanyName { get; set; }

        public string InsertUserName { get; set; }

        public string InsertUserRealName { get; set; }

        public DateTime InsertDate { get; set; }

        [Range(1, 100000)]
        public int Batch { get; set; }

        public ProductStatus ProductStatus { get; set; }

        public SecurityStatus SecurityStatus { get; set; }

        public List<Company> ToCompanyList { get; set; }

        public RetailProductEditModel()
        {
            this.ToCompanyList = new List<Company>();
            this.InsertDate = new DateTime();
            this.SecurityStatus = Model.SecurityStatus.Safe;
        }

        public RetailProductEditModel(RetailProduct product)
        {
            this.Id = product.Id;
            this.AgriculturalProductId = product.AgriculturalProductId;
            this.AgriculturalProductName = product.AgriculturalProductName;
            this.WholeSaleProductId = product.WholeSaleProductId;
            this.WholeSaleProductName = product.WholeSaleProductName;
            this.ProductName = product.ProductName;
            this.ProductCode = product.ProductCode;
            this.ProductStatus = product.ProductStatus;
            this.ToCompanyId = product.ToCompany.Id;
            this.FromCompanyId = product.FromCompany.Id;
            this.FromCompanyName = product.FromCompany.CompanyName;
            this.ToCompanyName = product.ToCompany.CompanyName;
            this.InsertDate = product.InsertDate;
            this.InsertUserName = product.InsertBy.UserName;
            this.InsertUserRealName = product.InsertBy.RealName;
            this.ParentId = product.ParentId;
            this.Batch = product.Batch;
        }

    }
}