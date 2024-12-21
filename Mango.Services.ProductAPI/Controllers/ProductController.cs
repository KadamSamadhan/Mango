using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public ProductController(AppDbContext db, IMapper mapper)
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
                IEnumerable<Product> objProductList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objProductList);
                _response.Result = objProductList;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product objProduct  = _db.Products.First(x => x.ProductId == id);

                _response.Result = _mapper.Map<ProductDto>(objProduct);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product objProduct = _mapper.Map<Product>(productDto);
                _db.Products.Add(objProduct);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(objProduct);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return _response;
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto    productDto)
        {
            try
            {
                Product objProduct = _mapper.Map<Product>(productDto);
                _db.Products.Update(objProduct);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(objProduct);
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
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int productId)
        {
            try
            {
                Product  objProduct  = _db.Products.First(x => x.ProductId == productId);
                _db.Products.Remove(objProduct);
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
