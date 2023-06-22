using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreManagement.API.Controllers
{
    public class PublishersController : BaseApiController
    {
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            return HandleResult(await _publisherService.GetPublishers());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            return HandleResult(await _publisherService.GetPublisher(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPublisher([FromBody] CreatePublisherDto publisherDto)
        {
            return HandleResult(await _publisherService.CreatePublisher(publisherDto));
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] UpdatePublisherDto publisherDto)
        {
            return HandleResult(await _publisherService.UpdatePublisher(id, publisherDto));
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            return HandleResult(await _publisherService.DeletePublisher(id));
        }
    }
}
