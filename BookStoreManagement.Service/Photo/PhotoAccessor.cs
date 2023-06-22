using BookStoreManagement.Core.Constants;
using BookStoreManagement.Core.Services;
using BookStoreManagement.Core.Wrappers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BookStoreManagement.Service.Photo
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;

        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }
        
        public async Task<PhotoUploadResult> AddPhoto(IFormFile file, string entity)
        {
            var uploadParams = new ImageUploadParams();

            if (file == null)
            {
                if (entity == Applications.USER)
                {
                    var result = GetDefaultPhoto(entity);
                    return new PhotoUploadResult
                    {
                        PublicId = result.DefaultPublicId,
                        PhotoUrl = result.DefaultUrl
                    };
                }
                if (entity == Applications.BOOKCOVER)
                {
                    var result = GetDefaultPhoto(entity);
                    return new PhotoUploadResult
                    {
                        PublicId = result.DefaultPublicId,
                        PhotoUrl = result.DefaultUrl
                    };
                }
            }

            using var stream = file.OpenReadStream();

            uploadParams.File = new FileDescription(file.FileName, stream);
            uploadParams.Folder = $"BookStore/{entity}/";

            if(entity == Applications.BOOKCOVER)
            {
                uploadParams.Transformation = new Transformation().Height(565).Width(450).Crop("fill");
            }
            if (entity == Applications.USER)
            {
                uploadParams.Transformation = new Transformation().Height(500).Width(500).Crop("fill");
            }

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return new PhotoUploadResult
            {
                PhotoUrl = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            };
        }

        public async Task<string> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok" ? result.Result : null;
        }

        private static DefaultPhoto GetDefaultPhoto(string entity)
        {
            if (entity == Applications.USER)
            {
                return new DefaultPhoto
                {
                    DefaultUrl = Applications.DEFAUlT_USER_AVATAR,
                    DefaultPublicId = Applications.DEFAUlT_USER_AVATAR_ID
                };
            }
            if (entity == Applications.BOOKCOVER)
            {
                return new DefaultPhoto
                {
                    DefaultUrl = Applications.DEFAUlT_BOOK_COVER,
                    DefaultPublicId = Applications.DEFAUlT_BOOK_COVER_ID
                };
            }
            return null;
        }


    }
}
