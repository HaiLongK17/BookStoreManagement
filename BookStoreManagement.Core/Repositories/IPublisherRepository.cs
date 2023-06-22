using BookStoreManagement.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Repositories
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Task<IEnumerable<Publisher>> GetPublisherWithBook();
    }
}
