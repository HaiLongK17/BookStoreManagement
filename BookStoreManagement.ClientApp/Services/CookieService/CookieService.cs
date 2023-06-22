using BookStoreManagement.ClientApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BookStoreManagement.ClientApp.Services.CookieService
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CookieService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void ClearCookie()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete("jwt");
            _contextAccessor.HttpContext?.Response.Cookies.Delete("email");
            _contextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");
        }

        public CookieDto GetCookieData()
        {
            return new CookieDto
            {
                Email = _contextAccessor.HttpContext?.Request.Cookies["email"],
                Token = _contextAccessor.HttpContext?.Request.Cookies["jwt"],
                RefreshToken = _contextAccessor.HttpContext?.Request.Cookies["refreshToken"]
            };
        }

        public void SetCookie(CookieDto cookieDto)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieDto.ExpireTime
            };

            _contextAccessor.HttpContext.Response.Cookies.Append("jwt", cookieDto.Token, cookieOptions);
            _contextAccessor.HttpContext.Response.Cookies.Append("email", cookieDto.Email, cookieOptions);
            _contextAccessor.HttpContext.Response.Cookies.Append("refreshToken", cookieDto.RefreshToken, cookieOptions);
        }
    }
}
