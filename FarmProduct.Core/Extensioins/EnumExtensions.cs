using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmProduct.Core.Extensioins
{
    public static class EnumExtensions
    {
        private static void IsEnumWithFlags<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(string.Format("Type '{0}' is not an enum", typeof(T).FullName));
            if (!Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
                throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute", typeof(T).FullName));
        }

        public static IEnumerable<T> GetFlags<T>(this T value) where T : struct
        {
            IsEnumWithFlags<T>();
            return from flag 
                   in Enum.GetValues(typeof(T)).Cast<T>() 
                   let lValue = Convert.ToInt64(value) 
                   let lFlag = Convert.ToInt64(flag) 
                   where (lValue & lFlag) != 0 select flag;
        }
    }
}
