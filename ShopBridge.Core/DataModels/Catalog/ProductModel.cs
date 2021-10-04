using System;

namespace ShopBridge.Core.DataModels.Catalog
{
    public class ProductModel
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
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
