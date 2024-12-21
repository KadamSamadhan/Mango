using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: ProductController

        public async Task<ActionResult> ProductIndex()
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
                    TempData["error"] =response?.Message;
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message; 
            }
            return View(list?.OrderByDescending(x=>x.ProductId));
        }
        public async Task<ActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ProductCreate(ProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseDto? response = await _productService.CreateProductAsync(product);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Product Created successfully";

                        return RedirectToAction(nameof(ProductIndex));

                    }
                    else
                    {
                        TempData["error"] = response?.Message; ;
                    }
                }
                return View(product);

            }
            catch (Exception ex)
            {
                TempData["error"] =ex.Message;
            }
            return View();
        }
        public async Task<ActionResult> ProductDelete(int productID)
        {
            ResponseDto? response = await _productService.GetProductByIDAsync(productID);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                

                return View(model);

            }
            else
            {
                TempData["error"] = response?.Message; ;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> ProductDelete(ProductDto product)
        {
            ResponseDto? response = await _productService.DeleteProductAsync(product.ProductId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
              

                return RedirectToAction(nameof(ProductIndex));

            }
            else
            {
                TempData["error"] = response?.Message; ;
            }
            return View(product);
        }
    }
}
