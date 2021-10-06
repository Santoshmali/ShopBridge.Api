using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Core
{
    // custom exception class for throwing application specific exceptions 
    // that can be caught and handled within the application
    public class ShopBridgeAppException : Exception
    {
        public ShopBridgeAppException() : base() { }
    }
}
