using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface IPublisherService
    {
        Task<Response<IEnumerable<PublisherDto>>> GetPublishers();
        Task<Response<PublisherDto>> GetPublisher(int Id);
        Task<Response<PublisherDto>> CreatePublisher(CreatePublisherDto publisherDto);
        Task<Response<int>> UpdatePublisher(int Id, UpdatePublisherDto publisherDto);
        Task<Response<int>> DeletePublisher(int Id);
    }
}
