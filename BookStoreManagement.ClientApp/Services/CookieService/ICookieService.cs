using BookStoreManagement.ClientApp.Models.DTO;
using System;

namespace BookStoreManagement.ClientApp.Services.CookieService
{
    public interface ICookieService
    {
        void SetCookie(CookieDto cookieDto);
        CookieDto GetCookieData();
        void ClearCookie();
    }
}
