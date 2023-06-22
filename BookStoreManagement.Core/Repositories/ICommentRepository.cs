using BookStoreManagement.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllCommentsWithUser();
        Task<Comment> GetCommentWithUser(int id);
        Task<IEnumerable<Comment>> GetCommentsWithUserByBookId(int bookId);
    }
}
