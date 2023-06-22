using BookStoreManagement.ClientApp.Models.DTO;
using System.Threading.Tasks;

namespace BookStoreManagement.ClientApp.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUser();
        Task<CookieDto> RefreshToken();
    }
}
