using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;

namespace FarmProduct.Core.Extensioins
{
    public static class ProductStatusExtension
    {
        public static short ToShort(this ProductStatus status)
        {
            return (short)status;
        }
    }
}
