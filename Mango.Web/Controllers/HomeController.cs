using IdentityModel;
using Mango.Web.Models;
using Mango.Web.Service;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IShopingCartService _shopingCartService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IShopingCartService shopingCartService)
        {
            _logger = logger;
            _productService = productService;
            _shopingCartService = shopingCartService;
        }
        

        public async Task< IActionResult> Index()
        {
            List<ProductDto>? list = new();
            try
            {
                ResponseDto? response = await _productService.GetAllProductsAsync();

                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

                }
                else
                {
                    TempData["error"] = response?.Message;
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View(list?.OrderByDescending(x => x.ProductId));
        }

        [Authorize]
        public async Task<IActionResult> ProductDetail(int productId)
        {
             ProductDto? productDto = new();
            try
            {
                ResponseDto? response = await _productService.GetProductByIDAsync(productId);

                if (response != null && response.IsSuccess)
                {
                    productDto = JsonConvert.DeserializeObject< ProductDto>(Convert.ToString(response.Result));

                }
                else
                {
                    TempData["error"] = response?.Message;
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View(productDto);
        }
        [Authorize]
        [HttpPost]
        [ActionName("ProductDetail")]
        public async Task<IActionResult> ProductDetail(ProductDto productDto)
        {
            CartDto cartDto = new CartDto
            {

                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?. FirstOrDefault()?.Value

                }
            };
            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count=productDto.Count,
                ProductId= productDto.ProductId,
            };
            List<CartDetailsDto> cartDetailsDtos = new()
            {
                cartDetails
            };
            cartDto.CartDetails = cartDetailsDtos;


            try
            {
                ResponseDto? response = await _shopingCartService.UpsertCartAsync(cartDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Item has been added to the shoping";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
