using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FarmProduct.Model
{
    /// <summary>
    /// 产品批发表
    /// </summary>
    public class WholeSaleProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入产品名字")]
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public int AgriculturalProductId { get; set; }

        public string AgriculturalProductName { get; set; }

        /// <summary>
        /// 分割父批发产品ID
        /// </summary>
        public int ParentId { get; set; }

        public int Batch { get; set; }

        public Company FromCompany { get; set; }

        public Company ToCompany { get; set; }

        public User InsertBy { get; set; }

        public ProductStatus ProductStatus { get; set; }

        /// <summary>
        /// 安全状态
        /// </summary>
        public SecurityStatus SecurityStatus { get; set; }

        public DateTime InsertDate { get; set; }

    }
}
