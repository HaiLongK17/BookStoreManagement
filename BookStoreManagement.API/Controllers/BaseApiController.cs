using BookStoreManagement.API.Extensions;
using BookStoreManagement.Core.Constants;
using BookStoreManagement.Core.DTO;
using BookStoreManagement.Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Response<T> result)
        {
            if (result.IsSucceeded() && !result.Data.Equals(default)) return Ok(new Response<T>
            {
                StatusCode = result.StatusCode,
                Message = "OK",
                Data = result.Data
            });

            if (!result.IsSucceeded() && result.Message == Messages.NOT_FOUND) return NotFound(new Response<T>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = result.Message,
                Errors = result.Errors,
                Data = result.Data
            });

            if (!result.IsSucceeded() && result.Message == Messages.BAD_REQUEST) return BadRequest(new Response<T>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = result.Message,
                Errors = result.Errors,
                Data = result.Data
            });

            if (!result.IsSucceeded() && result.Message == Messages.UNAUTHORIZED) return Unauthorized(new Response<T>
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Message = result.Message,
                Errors = result.Errors,
                Data = result.Data
            });

            return BadRequest(string.Join(", ", result.Errors));
        }

        protected ActionResult HandlePagedResult<T>(Response<PagedList<T>> result)
        {
            if (result.IsSucceeded() && !result.Data.Equals(default))
            {
                Response.AddPaginationHeader(result.Data.CurrentPage, result.Data.PageSize,
                    result.Data.TotalCount, result.Data.TotalPages);
                return Ok(new Response<PagedList<T>>
                {
                    StatusCode = result.StatusCode,
                    Message = "OK",
                    Data = result.Data
                });
            }

            if (!result.IsSucceeded() && result.Message == Messages.NOT_FOUND) return NotFound(new Response<PagedList<T>>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = result.Message,
                Errors = result.Errors,
                Data = result.Data
            });

            if (!result.IsSucceeded() && result.Message == Messages.BAD_REQUEST) return BadRequest(new Response<PagedList<T>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = result.Message,
                Errors = result.Errors,
                Data = result.Data
            });

            if (!result.IsSucceeded() && result.Message == Messages.UNAUTHORIZED) return Unauthorized(new Response<PagedList<T>>
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Message = result.Message,
                Errors = result.Errors,
                Data = result.Data
            });

            return BadRequest(string.Join(", ", result.Errors));
        }
    }
}
