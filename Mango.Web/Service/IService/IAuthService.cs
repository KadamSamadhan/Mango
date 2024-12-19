using Mango.Web.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace Mango.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RedisterAsync(RegistrationRequestDto registerRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto assignRoleRequestDto);

    }
}
