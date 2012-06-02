using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmProduct.Core
{
    public class BaseSvc
    {
        public static string CalculateProductCode(int companyId)
        {
            Random rand = new Random();

            return string.Format("{0}-{1}-{2}", companyId, DateTime.Now.ToString("yyyyMMddHHmmss"), rand.Next(10000, 100000));
        }
    }
}
