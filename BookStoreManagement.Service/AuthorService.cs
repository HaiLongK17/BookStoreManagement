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
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<AuthorDto>> CreateAuthor(CreateAuthorDto authorDto)
        {
            List<string> errors = new();
            var check = await _unitOfWork.Authors.SingleOrDefaultAsync(x => x.Name.Equals(authorDto.Name));
            if (check != null)
            {
                errors.Add("Author has existed");
                return Response<AuthorDto>.Failure(Messages.BAD_REQUEST, errors);
            }
            if (string.IsNullOrWhiteSpace(authorDto.Name))
            {
                errors.Add("Author name cannot be null");
                return Response<AuthorDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var author = _mapper.Map<Author>(authorDto);

            await _unitOfWork.Authors.AddAsync(author);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.ADD_FAILURE);
                return Response<AuthorDto>.Failure(Messages.SAVE_FAILURE, errors);
            }

            var response = _mapper.Map<AuthorDto>(author);
            return Response<AuthorDto>.Success(200, response);
        }

        public async Task<Response<int>> DeleteAuthor(int Id)
        {
            List<string> errors = new();

            var author = await _unitOfWork.Authors.GetByIdAsync(Id);

            if (author == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            _unitOfWork.Authors.Remove(author);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.DELETE_FAILURE);
                return Response<int>.Failure(Messages.SAVE_FAILURE, errors);
            }

            return Response<int>.Success(200, 1);
        }

        public async Task<Response<AuthorDto>> GetAuthor(int Id)
        {
            List<string> errors = new();

            var author = await _unitOfWork.Authors.GetByIdAsync(Id);

            if (author == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<AuthorDto>.Failure(Messages.NOT_FOUND, errors);
            }

            var result = _mapper.Map<AuthorDto>(author);

            return Response<AuthorDto>.Success(200, result);
        }

        public async Task<Response<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _unitOfWork.Authors.GetAuthorWithBook();

            var result = _mapper.Map<List<AuthorDto>>(authors);

            return Response<IEnumerable<AuthorDto>>.Success(200, result);
        }

        public async Task<Response<int>> UpdateAuthor(int Id, UpdateAuthorDto authorDto)
        {
            List<string> errors = new();
            var check = await _unitOfWork.Authors.SingleOrDefaultAsync(x => x.Name.Equals(authorDto.Name));
            if (check != null)
            {
                errors.Add("Author has existed");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }
            if (string.IsNullOrWhiteSpace(authorDto.Name))
            {
                errors.Add("Author name cannot be null");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }

            var author = await _unitOfWork.Authors.GetByIdAsync(Id);

            if (author == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            author.Name = authorDto.Name;
            author.Description = authorDto.Description;

            _unitOfWork.Authors.Update(author);

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
