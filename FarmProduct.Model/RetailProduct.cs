using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using FarmProduct.Model;

namespace FarmProduct.Model
{
    public class RetailProduct
    {
        public int Id { get; set; }

        public int AgriculturalProductId { get; set; }

        public string AgriculturalProductName { get; set; }

        public int WholeSaleProductId { get; set; }

        public string WholeSaleProductName { get; set; }

        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public int ParentId { get; set; }

        public Company FromCompany { get; set; }

        public Company ToCompany { get; set; }

        public User InsertBy { get; set; }

        public DateTime InsertDate { get; set; }

        public int Batch { get; set; }

        public ProductStatus ProductStatus { get; set; }

        public SecurityStatus SecurityStatus { get; set; }

        public DateTime RetailedDate { get; set; }

        public RetailProduct()
        {
            this.SecurityStatus = Model.SecurityStatus.Safe;
            this.InsertDate = DateTime.Now;
            this.ProductStatus = Model.ProductStatus.Retail;
        }

    }
}
