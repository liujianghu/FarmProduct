using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmProduct.Web.Models
{
    public class SecurityProdutModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CompanyName { get; set; }

        public string ProductTypeName { get; set; }

        public short ProductType { get; set; }

        public DateTime ProductInsertDate { get; set; }

    }
}