using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IShopingCartService
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetsilsId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
    }
}
