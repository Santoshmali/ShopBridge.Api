using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Core
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity Identifier
        /// </summary>
        public int Id { get; set; }
    }
}
