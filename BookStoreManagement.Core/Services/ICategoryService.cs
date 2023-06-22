using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface ICategoryService
    {
        Task<Response<IEnumerable<CategoryDto>>> GetCategories();
        Task<Response<CategoryDto>> GetCategory(int Id);
        Task<Response<CategoryDto>> CreateCategory(CreateCategoryDto categoryDto);
        Task<Response<int>> UpdateCategory(int Id, UpdateCategoryDto categoryDto);
        Task<Response<int>> DeleteCategory(int Id);
    }
}
