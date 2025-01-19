using IdentityModel;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IShopingCartService _shopingCartService;
        public CartController(IShopingCartService shopingCartService)
        {
            _shopingCartService = shopingCartService;
        }

        [Authorize]
        public async Task< IActionResult> CartIndex()
        {
            return  View(await LoadCartDtoBasedOnLoggedInUser());
        }

        private async Task<CartDto?> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId =User.Claims.Where(u=>u.Type== JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response= await _shopingCartService.GetCartByUserIdAsync(userId);
            if (response != null & response.IsSuccess)
            {

                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            
            return new CartDto();
        }

        public async Task<IActionResult?> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _shopingCartService.RemoveFromCartAsync(cartDetailsId);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }
        public async Task<IActionResult?> ApplyCoupon(CartDto cartDto)
        {
            ResponseDto? response = await _shopingCartService.ApplyCouponAsync(cartDto);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }
        public async Task<IActionResult?> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            ResponseDto? response = await _shopingCartService.ApplyCouponAsync(cartDto);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }
    }
}
