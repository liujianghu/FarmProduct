using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FarmProduct.Model
{
    public class AgriculturalProduct
    {
        public int Id { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public User InserBy { get; set; }

        /// <summary>
        /// 农产品厂家
        /// </summary>
        public Company ProductOwner { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public int Batch { get; set; }

        /// <summary>
        /// 品种
        /// </summary>
        public string BreedType { get; set; }

        /// <summary>
        /// 生长周期
        /// </summary>
        public int GrowthCycle { get; set; }

        /// <summary>
        /// 病史
        /// </summary>
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 使用疫苗情况
        /// </summary>
        public string VaccineSituation { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        /// <summary>
        /// 出栏日期
        /// </summary>
        public DateTime DeliverDate { get; set; }

        /// <summary>
        /// 预警信息
        /// </summary>
        public string EarlyWarningInfo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        public ProductStatus ProductStatus { get; set; }

        public DateTime InsertDate { get; set; }

        /// <summary>
        /// 安全状态
        /// </summary>
        public SecurityStatus SecurityStatus { get; set; }

        public AgriculturalProduct()
        {
            this.InsertDate = DateTime.Now;
            this.ProductStatus = Model.ProductStatus.Procreative;
            this.SecurityStatus = Model.SecurityStatus.Safe;
        }

    }
}
