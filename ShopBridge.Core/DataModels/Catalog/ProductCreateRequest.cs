using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Core.DataModels.Catalog
{
    public class ProductCreateRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
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
