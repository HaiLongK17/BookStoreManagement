using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStoreManagement.Data.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserWithRefreshTokens(string email)
        {
            return await _context.Users
                .Include(x => x.RefreshTokens).FirstOrDefaultAsync(x=>x.Email == email);
        }
    }
}
