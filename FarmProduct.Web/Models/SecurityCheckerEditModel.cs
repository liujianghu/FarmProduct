using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmProduct.Web.Models
{
    public class SecurityCheckerEditModel
    {
        public int Id { get; set; }

        public string InsertReason { get; set; }

        /// <summary>
        /// 0-瘟疫，1-同批次产品，2-某个农产品， 3-局部污染
        /// </summary>
        public short SecurityLevel { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CompanyName { get; set; }

        public int CompanyId { get; set; }

        /// <summary>
        /// 1=农产品，2=批发产品，3=零售产品
        /// </summary>
        public short ProductType { get; set; }

        public string ProductTypeName { get; set; }
    }
}