using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetPagedBooksWithRelation(PagingParams param);
        Task<Book> GetBookWithRelation(int bookId);
    }
}
