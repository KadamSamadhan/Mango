using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IProductService
    {
          Task<ResponseDto?> GetProductAsync(string ProductCode);
          Task<ResponseDto?> GetAllProductsAsync();
          Task<ResponseDto?> GetProductByIDAsync(int productId);
          Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
          Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
          Task<ResponseDto?> DeleteProductAsync(int productId);
    }
}
