using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmProduct.Model
{
    public class City
    {
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        public string CityName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
