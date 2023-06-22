using AutoMapper;
using BookStoreManagement.Core;
using BookStoreManagement.Core.Constants;
using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.DTO.Params;
using BookStoreManagement.Core.Entities;
using BookStoreManagement.Core.Services;
using BookStoreManagement.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreManagement.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoAccessor _photoAccessor;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoAccessor photoAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoAccessor = photoAccessor;
        }

        public async Task<Response<BookDto>> CreateBook(CreateBookDto bookDto)
        {
            List<string> errors = new();
            var check = await _unitOfWork.Books.SingleOrDefaultAsync(x => x.Title.Equals(bookDto.Title));
            if (check != null)
            {
                errors.Add("Book has existed");
                return Response<BookDto>.Failure(Messages.BAD_REQUEST, errors);
            }
            var book = _mapper.Map<Book>(bookDto);

            foreach(var categoryId in bookDto.CategoryIds)
            {
                book.BookCategories.Add(new BookCategories
                {
                    CategoryId = categoryId,
                });
            }

            var uploadResult = await _photoAccessor.AddPhoto(bookDto.File, Applications.BOOKCOVER);
            book.PhotoUrl = uploadResult.PhotoUrl;
            book.PhotoPublicId = uploadResult.PublicId;

            await _unitOfWork.Books.AddAsync(book);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.ADD_FAILURE);
                return Response<BookDto>.Failure(Messages.SAVE_FAILURE, errors);
            }

            var response = _mapper.Map<BookDto>(book);

            return Response<BookDto>.Success(200, response);
        }

        public async Task<Response<int>> DeleteBook(int Id)
        {
            List<string> errors = new();

            var book = await _unitOfWork.Books.GetByIdAsync(Id);

            if (book == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            _unitOfWork.Books.Remove(book);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.DELETE_FAILURE);
                return Response<int>.Failure(Messages.SAVE_FAILURE, errors);
            }

            return Response<int>.Success(200, 1);
        }

        public async Task<Response<BookDto>> GetBook(int Id)
        {
            List<string> errors = new();

            var book = await _unitOfWork.Books.GetBookWithRelation(Id);

            if (book == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<BookDto>.Failure(Messages.NOT_FOUND, errors);
            }

            var bookDto = _mapper.Map<BookDto>(book);

            foreach (var bookCategory in book.BookCategories)
            {
                bookDto.Categories.Add(_mapper.Map<CategoryDto>(bookCategory.Category));
            }

            return Response<BookDto>.Success(200, bookDto);
        }

        public async Task<Response<PagedList<BookDto>>> GetPagedBooks(BookParams param)
        {
            var queryBooks = _unitOfWork.Books.GetPagedBooksWithRelation(param);

            if (!string.IsNullOrEmpty(param.Category))
            {
                queryBooks = queryBooks.Where(x => x.BookCategories.Any(s => s.Category.Name.Replace("&", "") == param.Category));
            }

            if (!string.IsNullOrEmpty(param.Author))
            {
                queryBooks = queryBooks.Where(x => x.Author.Name == param.Author);
            }

            if (!string.IsNullOrEmpty(param.Publisher))
            {
                queryBooks = queryBooks.Where(x => x.Publisher.Name == param.Publisher);
            }

            var books = await PagedList<Book>.CreateAsync(queryBooks, param.PageNumber, param.PageSize);

            var booksDto = _mapper.Map<PagedList<Book>, PagedList<BookDto>>(books);

            for (int i = 0 ; i < books.Count ; i++)
            {
                foreach (var bookCategory in books[i].BookCategories)
                {
                    booksDto[i].Categories.Add(_mapper.Map<CategoryDto>(bookCategory.Category));
                }
            }

            return Response<PagedList<BookDto>>.Success(200, booksDto);
        }

        public async Task<Response<int>> UpdateBook(int Id, UpdateBookDto bookDto)
        {
            List<string> errors = new();
            var check = await _unitOfWork.Books.SingleOrDefaultAsync(x => x.Title.Equals(bookDto.Title));
            if (check != null)
            {
                errors.Add("Book has existed");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }
            var book = await _unitOfWork.Books.GetBookWithRelation(Id);

            if (book == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            _mapper.Map(bookDto, book);

            book.BookCategories.Clear();

            foreach (var categoryId in bookDto.CategoryIds)
            {
                book.BookCategories.Add(new BookCategories
                {
                    CategoryId = categoryId
                });
            }

            if (bookDto.File != null)
            {
                if(book.PhotoPublicId != Applications.DEFAUlT_BOOK_COVER_ID)
                {
                    var deletePhotoResult = await _photoAccessor.DeletePhoto(book.PhotoPublicId);
                    if (deletePhotoResult != "ok")
                    {
                        errors.Add(Errors.DELETE_PHOTO_FAILURE);
                        return Response<int>.Failure(Messages.BAD_REQUEST, errors);
                    }
                }

                var uploadResult = await _photoAccessor.AddPhoto(bookDto.File, Applications.BOOKCOVER);
                book.PhotoUrl = uploadResult.PhotoUrl;
                book.PhotoPublicId = uploadResult.PublicId;
            }

            book.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Books.Update(book);

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
