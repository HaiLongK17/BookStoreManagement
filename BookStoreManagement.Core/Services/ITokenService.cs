using BookStoreManagement.Core.Entities;

namespace BookStoreManagement.Core.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken(bool isStayed);
    }
}
