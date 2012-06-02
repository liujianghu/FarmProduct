using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FarmProduct.Model;

namespace FarmProduct.Web.Models
{
    public class WholeSaleProductEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入产品名字")]
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        [Range(1,100000000)]
        public int AgriculturalProductId { get; set; }

        public string AgriculturalProductName { get; set; }

        /// <summary>
        /// 分割父批发产品ID
        /// </summary>
        [Range(0, 100000000)]
        public int ParentId { get; set; }

        [Range(1, 10000)]
        public int Batch { get; set; }

        public string FromCompanyName { get; set; }

        [Range(1, 100000)]
        public int ToCompanyId { get; set; }

        public string ToCompanyName { get; set; }

        public string InsertUserName { get; set; }

        public string InsertUserRealName { get; set; }

        public ProductStatus ProductStatus { get; set; }

        /// <summary>
        /// 安全状态
        /// </summary>
        public SecurityStatus SecurityStatus { get; set; }

        public DateTime InsertDate { get; set; }

        public List<Company> ToCompanyList { get; set; }

        public WholeSaleProductEditModel()
        {
            this.InsertDate = DateTime.Now;
            this.SecurityStatus = Model.SecurityStatus.Safe;
            this.ToCompanyList = new List<Company>();
        }

        public WholeSaleProductEditModel(WholeSaleProduct product)
        {
            this.Id = product.Id;
            this.ProductName = product.ProductName;
            this.ProductCode = product.ProductCode;
            this.ProductStatus = product.ProductStatus;
            this.AgriculturalProductId = product.AgriculturalProductId;
            this.ParentId = product.ParentId;
            this.Batch = product.Batch;
            this.SecurityStatus = product.SecurityStatus;
            this.InsertDate = product.InsertDate;
            this.ToCompanyId = product.ToCompany.Id;
            this.FromCompanyName = product.FromCompany.CompanyName;
            this.ToCompanyName = product.ToCompany.CompanyName;
            this.InsertUserName = product.InsertBy.UserName;
            this.InsertUserRealName = product.InsertBy.RealName;

        }
    }
}