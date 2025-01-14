﻿using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICouponService
    {
          Task<ResponseDto?> GetCouponAsync(string couponCode);
          Task<ResponseDto?> GetAllCouponsAsync();
          Task<ResponseDto?> GetCouponByIDAsync(int coupinId);
          Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
          Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
          Task<ResponseDto?> DeleteCouponAsync(int coupinId);
    }
}
