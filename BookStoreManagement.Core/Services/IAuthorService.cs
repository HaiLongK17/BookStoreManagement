using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface IAuthorService
    {
        Task<Response<IEnumerable<AuthorDto>>> GetAuthors();
        Task<Response<AuthorDto>> GetAuthor(int Id);
        Task<Response<AuthorDto>> CreateAuthor(CreateAuthorDto authorDto);
        Task<Response<int>> UpdateAuthor(int Id, UpdateAuthorDto authorDto);
        Task<Response<int>> DeleteAuthor(int Id);
    }
}
