using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreManagement.Data.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsWithUser()
        {
            return await _context.Comments.Include(x => x.User).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsWithUserByBookId(int bookId)
        {
            return await _context.Comments.Include(x => x.User)
                .Where(x => x.BookId == bookId).ToListAsync();
        }

        public async Task<Comment> GetCommentWithUser(int id)
        {
            return await _context.Comments.Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
