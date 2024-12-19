using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        private readonly AppDbContext _db;
        public AuthController(IAuthService authService, AppDbContext db)
        {
            _db = db;
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);

            }
            else

            return Ok(_responseDto);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.Token != null)
            {

                if (loginResponse.User == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Username or password is incorrect";
                    return BadRequest(_responseDto);
                }
                _responseDto.Result = loginResponse;
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Invalid Token";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegistrationRequestDto registrationRequestDto)
        {
            var assignRoleSuccessful = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());
            

                if (!assignRoleSuccessful)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Error encountered";
                    return BadRequest(_responseDto);
                }
            return Ok(_responseDto);
        }
    }
}
