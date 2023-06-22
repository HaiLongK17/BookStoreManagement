using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreManagement.API.Controllers
{
    public class AuthorsController : BaseApiController
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return HandleResult(await _authorService.GetAuthors());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            return HandleResult(await _authorService.GetAuthor(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorDto authorDto)
        {
            return HandleResult(await _authorService.CreateAuthor(authorDto));
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto authorDto)
        {
            return HandleResult(await _authorService.UpdateAuthor(id, authorDto));
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return HandleResult(await _authorService.DeleteAuthor(id));
        }
    }
}
