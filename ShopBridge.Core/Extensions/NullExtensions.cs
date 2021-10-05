using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Core.Extensions
{
    public static class NullExtensions
    {
        public sealed class NotNullAttribute : Attribute { }
        public static bool IsNull<T>([NotNull] this T o) 
        {
            return o == null;
        }

        public static T ThrowIfNull<T>([NotNull] this T o, string parameter) where T : class
        {
            if (o == null)
            {
                throw new ArgumentNullException(parameter);
            }

            return o;
        }

        public static T ThrowIfNullOrZero<T>([NotNull] this T o, string parameter)
        {
            if((o is int || o is long) && Convert.ToUInt64(o) <= 0)
            {
                throw new ArgumentNullException(parameter);
            }
            return o;
        }
    }
}
