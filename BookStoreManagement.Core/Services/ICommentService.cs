using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface ICommentService
    {
        Task<Response<int>> PostComment(PostCommentDto postComment);
        Task<Response<IEnumerable<CommentDto>>> GetCommentsByBookId(int bookId);
    }
}
