using AutoMapper;
using Azure;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();

        }
        [HttpGet]
       public ResponseDto Get()
        {
            try
            { 
            IEnumerable<Coupon> objCouponList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objCouponList);
                _response.Result = objCouponList;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message=ex.Message;
            }

           
            return _response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon  objCoupon= _db.Coupons.First(x=>x.CouponID==id);
              
                _response.Result = _mapper.Map<CouponDto>(objCoupon); 
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
        [HttpGet]
        [Route("GetByCouponCode/{couponcode}:string")]
        public ResponseDto GetByCouponCode(string couponcode)
        {
            try
            {
                Coupon objCoupon = _db.Coupons.First(x => x.CouponCode.ToLower() == couponcode.ToLower());

                _response.Result = _mapper.Map<CouponDto>(objCoupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon objCoupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(objCoupon);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(objCoupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon objCoupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(objCoupon);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(objCoupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
        [HttpDelete]
        [Route("{coupinId:int}")]
        public ResponseDto Delete(int coupinId)
        {
            try
            {
                Coupon objCoupon = _db.Coupons.First(x=>x.CouponID== coupinId);
                _db.Coupons.Remove(objCoupon);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
    }
}
