using BookStoreManagement.Core.Wrappers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BookStoreManagement.Core.Services
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file, string entity);
        Task<string> DeletePhoto(string publicId);
    }
}
