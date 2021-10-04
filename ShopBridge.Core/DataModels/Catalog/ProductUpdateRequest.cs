using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Core.DataModels.Catalog
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsNotReturnable { get; set; }
        public bool IsDownloadable { get; set; }
        public bool IsShipEnabled { get; set; }
        public bool IsActive { get; set; }
    }
}
