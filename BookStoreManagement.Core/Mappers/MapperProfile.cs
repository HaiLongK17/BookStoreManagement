using AutoMapper;
using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreManagement.Core.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap()
                .ForMember(d => d.BookCount, o => o.MapFrom(s => s.BookCategories.Count));
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

            CreateMap<AuthorDto, Author>().ReverseMap()
                .ForMember(d => d.BookCount, o => o.MapFrom(s => s.Books.Count));
            CreateMap<CreateAuthorDto, Author>().ReverseMap();
            CreateMap<UpdateAuthorDto, Author>().ReverseMap();

            CreateMap<PublisherDto, Publisher>().ReverseMap()
                .ForMember(d => d.BookCount, o => o.MapFrom(s => s.Books.Count));
            CreateMap<CreatePublisherDto, Publisher>().ReverseMap();
            CreateMap<UpdatePublisherDto, Publisher>().ReverseMap();

            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<UserCommentDto, User>().ReverseMap();

            CreateMap<CommentDto, Comment>().ReverseMap()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User));
            CreateMap<PostCommentDto, Comment>().ReverseMap();

            CreateMap<BookDto, Book>().ReverseMap()
                .ForMember(d => d.PublisherDto, o => o.MapFrom(s => s.Publisher))
                .ForMember(d => d.AuthorDto, o => o.MapFrom(s => s.Author))
                .ForMember(d => d.Comments, o => o.MapFrom(s => s.Comments));
            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();

            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));
        }
    }
}
