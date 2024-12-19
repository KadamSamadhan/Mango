using AutoMapper;

namespace Mango.Services.CouponAPI.Models.Dto
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<CouponDto, Coupon>();
                    config.CreateMap<Coupon, CouponDto>();
                });
            return mappingConfig;
        }



    }
}
