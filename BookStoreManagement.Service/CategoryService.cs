using AutoMapper;
using BookStoreManagement.Core;
using BookStoreManagement.Core.Constants;
using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Services;
using BookStoreManagement.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CategoryDto>> CreateCategory(CreateCategoryDto categoryDto)
        {
            List<string> errors = new();
            var check= await _unitOfWork.Categories.SingleOrDefaultAsync(x=>x.Name.Equals(categoryDto.Name));
             if (check != null)
            {
                errors.Add("Category name has existed");
                return Response<CategoryDto>.Failure(Messages.BAD_REQUEST, errors);
            }
            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                errors.Add("Category name cannot be null");
                return Response<CategoryDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var category = _mapper.Map<Category>(categoryDto);

            await _unitOfWork.Categories.AddAsync(category);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.ADD_FAILURE);
                return Response<CategoryDto>.Failure(Messages.SAVE_FAILURE, errors);
            }

            var response = _mapper.Map<CategoryDto>(category);
            return Response<CategoryDto>.Success(200, response);
        }

        public async Task<Response<int>> DeleteCategory(int Id)
        {
            List<string> errors = new();

            var category = await _unitOfWork.Categories.GetByIdAsync(Id);

            if (category == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            _unitOfWork.Categories.Remove(category);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.DELETE_FAILURE);
                return Response<int>.Failure(Messages.SAVE_FAILURE, errors);
            }

            return Response<int>.Success(200, 1);
        }

        public async Task<Response<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetCategoriesWithBook();

            var result = _mapper.Map<List<CategoryDto>>(categories);

            return Response<IEnumerable<CategoryDto>>.Success(200, result);
        }

        public async Task<Response<CategoryDto>> GetCategory(int Id)
        {
            List<string> errors = new();

            var category = await _unitOfWork.Categories.GetByIdAsync(Id);

            if (category == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<CategoryDto>.Failure(Messages.NOT_FOUND, errors);
            }

            var result = _mapper.Map<CategoryDto>(category);

            return Response<CategoryDto>.Success(200, result);
        }

        public async Task<Response<int>> UpdateCategory(int Id, UpdateCategoryDto categoryDto)
        {
            List<string> errors = new();
            var check= await _unitOfWork.Categories.SingleOrDefaultAsync(x=>x.Name.Equals(categoryDto.Name));
             if (check != null)
            {
                errors.Add("Category name has existed");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }

            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                errors.Add("Category name cannot be null");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }
           
            var category = await _unitOfWork.Categories.GetByIdAsync(Id);

            if (category == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            _unitOfWork.Categories.Update(category);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.UPDATE_FAILURE);
                return Response<int>.Failure(Messages.SAVE_FAILURE, errors);
            }

            return Response<int>.Success(200, 1);
        }
    }
}
