using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;
using System.Net.Http;

namespace Mango.Services.ShoppingCartAPI.Service
{
 
    public class CouponService:ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CouponDto> GetCoupon(string couponcode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetByCouponCode/{couponcode}:string");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp != null)
            {
                if (resp.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
                }
            }
            return new CouponDto();
        }
    }
}
