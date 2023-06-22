using BookStoreManagement.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorWithBook();
    }
}
