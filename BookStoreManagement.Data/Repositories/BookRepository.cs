using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreManagement.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Book> GetPagedBooksWithRelation(PagingParams param)
        {
            return _context.Books.Include(x => x.Author).Include(x => x.Publisher)
                .Include(x => x.BookCategories).ThenInclude(s => s.Category).AsQueryable();
        }

        public async Task<Book> GetBookWithRelation(int bookId)
        {
            return await _context.Books.Include(x => x.Author).Include(x => x.Publisher)
                .Include(x => x.BookCategories).ThenInclude(s => s.Category).FirstOrDefaultAsync(x => x.Id == bookId);
        }
    }
}
