using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreManagement.API.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Route("book/{bookId}")]
        public async Task<IActionResult> GetCommentsByBookId(int bookId)
        {
            return HandleResult(await _commentService.GetCommentsByBookId(bookId));
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(PostCommentDto commentDto)
        {
            return HandleResult(await _commentService.PostComment(commentDto));
        }
    }
}
