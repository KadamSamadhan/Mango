using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        // GET: CouponController

        public async Task<ActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            try
            {
                ResponseDto? response = await _couponService.GetAllCouponsAsync();

                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));

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
            return View(list?.OrderByDescending(x=>x.CouponID));
        }
        public async Task<ActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CouponCreate(CouponDto coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResponseDto? response = await _couponService.CreateCouponAsync(coupon);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Coupon Created successfully";

                        return RedirectToAction(nameof(CouponIndex));

                    }
                    else
                    {
                        TempData["error"] = response?.Message; ;
                    }
                }
                return View(coupon);

            }
            catch (Exception ex)
            {
                TempData["error"] =ex.Message;
            }
            return View();
        }
        public async Task<ActionResult> CouponDelete(int CouponID)
        {
            ResponseDto? response = await _couponService.GetCouponByIDAsync(CouponID);

            if (response != null && response.IsSuccess)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                

                return View(model);

            }
            else
            {
                TempData["error"] = response?.Message; ;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponID);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted successfully";
              

                return RedirectToAction(nameof(CouponIndex));

            }
            else
            {
                TempData["error"] = response?.Message; ;
            }
            return View(couponDto);
        }
    }
}
