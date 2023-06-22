using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.DTO.Params;
using BookStoreManagement.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface IBookService
    {
        Task<Response<PagedList<BookDto>>> GetPagedBooks(BookParams param);
        Task<Response<BookDto>> GetBook(int Id);
        Task<Response<BookDto>> CreateBook(CreateBookDto bookDto);
        Task<Response<int>> UpdateBook(int Id, UpdateBookDto bookDto);
        Task<Response<int>> DeleteBook(int Id);
    }
}
