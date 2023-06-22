using AutoMapper;
using BookStoreManagement.Core.DTO;
using System.Collections.Generic;

namespace BookStoreManagement.Core.Mappers
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        private readonly IMapper _mapper;
        public PagedListConverter(IMapper mapper)
        {
            _mapper = mapper;
        }
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = _mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);

            return new PagedList<TDestination>(collection, source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}
