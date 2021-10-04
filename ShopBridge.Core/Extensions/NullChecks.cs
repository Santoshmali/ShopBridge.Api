using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Core.Extensions
{
    public static class NullChecks
    {
        public static bool IsNull<T>(this T o) where T : class
        {
            return o == null;
        }
    }
}
