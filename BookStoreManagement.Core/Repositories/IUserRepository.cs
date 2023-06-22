using BookStoreManagement.Core.Entities;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserWithRefreshTokens(string email);
    }
}
