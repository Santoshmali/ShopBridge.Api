using System;
namespace ShopBridge.Core.DataModels
{
    public class Error
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
