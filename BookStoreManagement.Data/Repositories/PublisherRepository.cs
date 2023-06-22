using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Data.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private readonly DataContext _context;
        public PublisherRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetPublisherWithBook()
        {
            return await _context.Publishers.Include(x => x.Books).ToListAsync();
        }
    }
}
