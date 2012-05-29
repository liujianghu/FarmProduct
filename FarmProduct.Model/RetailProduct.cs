using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FarmProduct.Model
{
    public class RetailProduct
    {
        public int Id { get; set; }

        public int AgriculturalProductId { get; set; }

        public int WholeSaleProductId { get; set; }

        public Company FromCompany { get; set; }

        public Company ToCompany { get; set; }

        public User InsertBy { get; set; }

        public DateTime InsertDate { get; set; }

        public int Batch { get; set; }

        public short ProductStatus { get; set; }

        public short SecurityStatus { get; set; }

    }
}
