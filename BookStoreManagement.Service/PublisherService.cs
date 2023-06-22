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
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<PublisherDto>> CreatePublisher(CreatePublisherDto publisherDto)
        {
            List<string> errors = new();
            var check = await _unitOfWork.Publishers.SingleOrDefaultAsync(x => x.Name.Equals(publisherDto.Name));
            if (check != null)
            {
                errors.Add("Publisher has existed");
                return Response<PublisherDto>.Failure(Messages.BAD_REQUEST, errors);
            }
            if (string.IsNullOrWhiteSpace(publisherDto.Name))
            {
                errors.Add("Publisher name cannot be null");
                return Response<PublisherDto>.Failure(Messages.BAD_REQUEST, errors);
            }

            var publisher = _mapper.Map<Publisher>(publisherDto);

            await _unitOfWork.Publishers.AddAsync(publisher);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.ADD_FAILURE);
                return Response<PublisherDto>.Failure(Messages.SAVE_FAILURE, errors);
            }

            var response = _mapper.Map<PublisherDto>(publisher);
            return Response<PublisherDto>.Success(200, response);
        }

        public async Task<Response<int>> DeletePublisher(int Id)
        {
            List<string> errors = new();

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(Id);

            if (publisher == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            _unitOfWork.Publishers.Remove(publisher);

            var result = await _unitOfWork.SaveAsync() > 0;

            if (!result)
            {
                errors.Add(Errors.DELETE_FAILURE);
                return Response<int>.Failure(Messages.SAVE_FAILURE, errors);
            }

            return Response<int>.Success(200, 1);
        }

        public async Task<Response<PublisherDto>> GetPublisher(int Id)
        {
            List<string> errors = new();

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(Id);

            if (publisher == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<PublisherDto>.Failure(Messages.NOT_FOUND, errors);
            }

            var result = _mapper.Map<PublisherDto>(publisher);

            return Response<PublisherDto>.Success(200, result);
        }

        public async Task<Response<IEnumerable<PublisherDto>>> GetPublishers()
        {
            var publishers = await _unitOfWork.Publishers.GetPublisherWithBook();

            var result = _mapper.Map<List<PublisherDto>>(publishers);

            return Response<IEnumerable<PublisherDto>>.Success(200, result);
        }

        public async Task<Response<int>> UpdatePublisher(int Id, UpdatePublisherDto publisherDto)
        {
            List<string> errors = new();
            var check = await _unitOfWork.Publishers.SingleOrDefaultAsync(x => x.Name.Equals(publisherDto.Name));
            if (check != null)
            {
                errors.Add("Publisher has existed");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }
            if (string.IsNullOrWhiteSpace(publisherDto.Name))
            {
                errors.Add("Publisher name cannot be null");
                return Response<int>.Failure(Messages.BAD_REQUEST, errors);
            }

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(Id);

            if (publisher == null)
            {
                errors.Add(Errors.RESOURCE_NOTFOUND);
                return Response<int>.Failure(Messages.NOT_FOUND, errors);
            }

            publisher.Name = publisherDto.Name;
            publisher.Description = publisherDto.Description;

            _unitOfWork.Publishers.Update(publisher);

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
