using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmProduct.Model
{
    public class District
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public string DistrictName { get; set; }

        public bool IsDeleted { get; set; }

    }
}
