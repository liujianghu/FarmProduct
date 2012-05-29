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

        public int AgriculturalProductId { get; set; }

        /// <summary>
        /// 分割父批发产品ID
        /// </summary>
        public int ParentId { get; set; }

        public int Batch { get; set; }

        public Company FromCompany { get; set; }

        public Company ToCompany { get; set; }

        public User InsertBy { get; set; }

        public short ProductStatus { get; set; }

        /// <summary>
        /// 安全状态
        /// </summary>
        public short SecurityStatus { get; set; }

        public DateTime InsertDate { get; set; }

    }
}
