using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Core.Extensions
{
    public static class ThrowExceptions
    {
        public static T ThrowIfNull<T>(this T o, string parameter) where T : class
        {
            if (o == null)
            {
                throw new ArgumentNullException(parameter);
            }

            return o;
        }
    }
}
