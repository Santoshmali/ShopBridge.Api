using AutoMapper;
using ShopBridge.Core.Entities.Catalog;
using ShopBridge.Data.DbModels.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Api.Configurations
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<Product, Products>().ReverseMap();
        }
    }
}
