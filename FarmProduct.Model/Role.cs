using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmProduct.Model
{
    [Flags]
    public enum Role 
    {
        Guest = 0,
        Admin = 1,
        SecurityChecker = 2,
        FarmProductUser = 4,
        WholeSaleUser = 8,
        RetailUser = 16
    }
}
