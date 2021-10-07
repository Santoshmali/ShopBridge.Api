using AutoMapper;
using ShopBridge.Core.DataModels.Catalog;
using ShopBridge.Core.DataModels.Users;
using ShopBridge.Data.DbModels.Catalog;
using ShopBridge.Data.DbModels.Users;
using System;

namespace ShopBridge.Api.Configurations
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<ProductCreateRequest, Product>()
                .ForMember(dest => dest.AddedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<ProductModel, Product>().ReverseMap();
            CreateMap<ProductUpdateRequest, Product>()
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<RefreshToken, UserRefreshToken>().ReverseMap();
        }
    }
}
