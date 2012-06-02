using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FarmProduct.Model;

namespace FarmProduct.Web.Models
{
    public class AgriculturalProductEditModel
    {
        public int Id { get; set; }

        public string ProductCode { get; set; }

        [Required(ErrorMessage = "请输入产品名字")]
        public string ProductName { get; set; }

        public string InsertUserRealName { get; set; }

        public string InsertByUserName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [Range(1, 100000)]
        public int Batch { get; set; }

        /// <summary>
        /// 品种
        /// </summary>
        [Required(ErrorMessage = "请输入品种")]
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
        [Required(ErrorMessage = "请输入出栏日期")]
        [DataType(DataType.DateTime)]
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

        public AgriculturalProductEditModel()
        {
            this.BirthDate = DateTime.Now;
            this.DeliverDate = DateTime.Now;
            this.InsertDate = DateTime.Now;
        }

        public AgriculturalProductEditModel(AgriculturalProduct product)
            : this()
        {
            this.Id = product.Id;
            this.ProductName = product.ProductName;
            this.InsertUserRealName = product.InserBy.RealName;
            this.InsertByUserName = product.InserBy.UserName;
            this.ProductCode = product.ProductCode;
            this.ProductStatus = product.ProductStatus;
            this.BirthDate = product.BirthDate;
            this.Batch = product.Batch;
            this.BreedType = product.BreedType;
            this.GrowthCycle = product.GrowthCycle;
            this.MedicalHistory = product.MedicalHistory;
            this.Weight = product.Weight;
            this.VaccineSituation = product.VaccineSituation;
            this.DeliverDate = product.DeliverDate;
            this.InsertDate = product.InsertDate;
            this.Remarks = product.Remarks;
            this.SecurityStatus = product.SecurityStatus;
        }
    }
}