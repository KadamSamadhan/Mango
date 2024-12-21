using AutoMapper;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Models
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<ProductDto, Product>();
                    config.CreateMap<Product, ProductDto>();
                });
            return mappingConfig;
        }



    }
}
