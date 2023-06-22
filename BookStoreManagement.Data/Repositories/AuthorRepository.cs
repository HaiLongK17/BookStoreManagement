using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly DataContext _context;
        public AuthorRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthorWithBook()
        {
            return await _context.Authors.Include(x => x.Books).ToListAsync();
        }
    }
}
