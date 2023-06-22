using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Wrappers;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<CookieDto>> Login(LoginDto loginDto);
        Task<Response<CookieDto>> Register(RegisterDto registerDto);
        Task<Response<CookieDto>> RefreshToken(CookieDto cookie);
        Task<Response<UserDto>> GetCurrentUser(CookieDto cookie);
    }
}
