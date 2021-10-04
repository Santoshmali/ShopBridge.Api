using AutoMapper;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Data.DbModels.Catalog;

namespace ShopBridge.Api.Configurations
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ProductCreateRequest, Product>().ReverseMap();
            CreateMap<ProductModel, Product>().ReverseMap();
        }
    }
}
