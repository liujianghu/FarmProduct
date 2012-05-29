using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmProduct.Model
{
    public class Company  
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public short CompanyType { get; set; }

        public Province Province { get; set; }

        public City City { get; set; }

        public District Dictrict { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public bool IsDeleted { get; set; }

        public Company()
        {
            this.IsDeleted = false;
        }

    }
}
