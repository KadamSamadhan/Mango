using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService  _baseService;
        public ProductService(IBaseService baseService) {
            _baseService=baseService;
        }
        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = productDto,
                Url = StaticDetails.ProductAPIBase + "/api/product/"  
            });
        }

        public async Task<ResponseDto?>  DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.ProductAPIBase + "/api/product/"+ productId
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBase + "/api/product"
            });
        }

        public Task<ResponseDto?> GetProductAsync(string ProductCode)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?>  GetProductByIDAsync(int coupinId)
        {
           return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBase + "/api/product/" + coupinId
            });
        }

        public async Task<ResponseDto?>  UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = productDto,
                Url = StaticDetails.ProductAPIBase + "/api/product/"
            });
        }
    }
}
