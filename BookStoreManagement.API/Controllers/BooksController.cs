using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.DTO.Params;
using BookStoreManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreManagement.API.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] BookParams param)
        {
            return HandlePagedResult(await _bookService.GetPagedBooks(param));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return HandleResult(await _bookService.GetBook(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromForm] CreateBookDto bookDto)
        {
            return HandleResult(await _bookService.CreateBook(bookDto));
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] UpdateBookDto bookDto)
        {
            return HandleResult(await _bookService.UpdateBook(id, bookDto));
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return HandleResult(await _bookService.DeleteBook(id));
        }
    }
}
